using Org.BouncyCastle.Asn1.Ocsp;
using SignLib.Certificates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DAFCertificateGenerator
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        string certDir = Environment.CurrentDirectory;
        static string rootserialNumber = "1CE644712CD23094434DDDE71C8AFA98";
        static string intermadiateserialNumber = "7500000009F7C2F4CAE13BD7FF000000000009";
        static string clientserialNumber = "1CE644712CD23094434DDDE71C8AFA98";
        public string clientID => tbClientID.Text;
        public int expiryDays => Int32.Parse(tbExpirationDays.Text);
        public string emailClient => tbEmail.Text;
        public string certPassword => tbPassword.Text;


        private void btnCreateCert_Click(object sender, EventArgs e)
        {
            CreateRootCertificateAsPFX(certDir + "\\b2b" + clientID + "S03_ca_1.pfx", certPassword);
            CreateIntermadiateCertificateAsPFX(certDir + "\\b2b" + clientID + "S03_ca_1.pfx", certPassword, certDir + "\\b2b" + clientID + "S03_ca_0.pfx", certPassword);
            CreateLastCertificateAsPFX(certDir + "\\b2b" + clientID + "S03_ca_0.pfx", certPassword, certDir + "\\b2b" + clientID + "S03.pfx", certPassword);
            X509Chain ch = new X509Chain();
            //ch.Build(certDir + "\\b2b" + clientID + "S03_ca_0.pfx", certPassword, certDir + "\\b2b" + clientID + "S03.pfx");

            ExtractCertificateInformation(certDir + "\\b2b" + clientID + "S03.pfx", certPassword);

        }

        public void ExtractCertificateInformation(string CertificatePath, string CertificatePassword)
        {
            //create a .NET X509Certificate2 object from a PFX file
            X509Certificate2 cert = new X509Certificate2(CertificatePath, CertificatePassword);
            richTbInfo.AppendText("\n\nCeertificate subject: " + cert.Subject);
            richTbInfo.AppendText("\nCertificate issued by: " + cert.Issuer);
            richTbInfo.AppendText("\nCertificate will expire on: " + cert.NotAfter.ToString());
            richTbInfo.AppendText("\nCertificate is time valid: " + DigitalCertificate.VerifyDigitalCertificate(cert,VerificationType.LocalTime).ToString());
            richTbInfo.AppendText(cert.Version.ToString());
        }



        private X509Certificate2Collection CollectB2BCertificatesInStore()
        {
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.OpenExistingOnly);
            X509Certificate2Collection result = x509Store.Certificates.Find(X509FindType.FindByTimeValid, DateTime.Now, false).Find(X509FindType.FindByTemplateName, "1.3.6.1.4.1.311.21.8.15191615.3961195.8341936.9132609.6700102.66.1308780127.675695868", false);
            x509Store.Close();
            return result;
        }

        public void CreateRootCertificateAsPFX(string RootCertificatePath, string RootCertificatePassword)
        {

            //on the demo version the certificates will be valid 30 days only
            //this is the single restriction of the library in demo mode
            X509CertificateGenerator certGenerator = new X509CertificateGenerator(rootserialNumber);

            //set the validity of the certificate
            certGenerator.ValidFrom = DateTime.Now;
            
            //The certificate will be valid only 30 days on the demo version of the library
            certGenerator.ValidTo = DateTime.Now.AddDays(expiryDays);

            //set the signing algorithm and the key size
            certGenerator.KeySize = KeySize.KeySize2048Bit;
            certGenerator.SignatureAlgorithm = SignatureAlgorithm.SHA256WithRSA;


            certGenerator.AddToSubject(SubjectType.CN, "PACCARTrustRoot");
            certGenerator.AddToSubject(SubjectType.GIVENNAME, "b2b48053S02_ca_1");
            certGenerator.AddToSubject(SubjectType.OU, "PACCAR");
            certGenerator.AddToSubject(SubjectType.O, "PACCAR inc");
            certGenerator.AddToSubject(SubjectType.L, "Bellevue");
            certGenerator.AddToSubject(SubjectType.ST, "WA");
            certGenerator.AddToSubject(SubjectType.C, "US");
            certGenerator.AddToSubject(SubjectType.EMAILADDRESS, "DomainAdminsNA@paccar.com");

            //also you can use an alternative method
            certGenerator.Subject = "CN=PACCARTrustRoot, OU=PACCAR, O=PACCAR inc, L=Bellevue, ST=WA, C=US, EMAILADDRESS=DomainAdminsNA@paccar.com";

            //add some extensions to the Root certificate
            certGenerator.Extensions.AddKeyUsage(CertificateKeyUsage.DigitalSignature);
            certGenerator.Extensions.AddKeyUsage(CertificateKeyUsage.CertificateSigning);
            certGenerator.Extensions.AddKeyUsage(CertificateKeyUsage.CRLSigning);
            certGenerator.Extensions.KeyUsageIsCritical = true;
            certGenerator.Extensions.AddEnhancedKeyUsage(Oid.FromOidValue("1.2.840.113549.1.1.1", OidGroup.Template));

            certGenerator.FriendlyName = "b2b" + clientID + "S03_ca_1";
            X509Certificate2Collection certificates = new X509Certificate2Collection();

            //create the PFX certificate as Root certificate
            File.WriteAllBytes(RootCertificatePath, certGenerator.GenerateCertificate(RootCertificatePassword, true));

            //save the public part of the certificate
            File.WriteAllBytes(RootCertificatePath + ".cer", new X509Certificate2(RootCertificatePath, RootCertificatePassword).RawData);
            richTbInfo.AppendText("\nRoot certificate was created!");
        }

        public void CreateIntermadiateCertificateAsPFX(string RootCertificatePath, string RootCertificatePassword, string ClientCertificatePath, string ClientCertificatePassword)
        {
            //on the demo version the certificates will be valid 30 days only
            //this is the single restriction of the library in demo mode
            X509CertificateGenerator certGenerator = new X509CertificateGenerator(intermadiateserialNumber);

            //set the validity of the certificate
            certGenerator.ValidFrom = DateTime.Now;
            //The certificate will be valid only 30 days on the demo version of the library
            certGenerator.ValidTo = DateTime.Now.AddDays(expiryDays);

            //load the Root Certificate to sign the user certificate
            certGenerator.LoadRootCertificate(File.ReadAllBytes(RootCertificatePath), RootCertificatePassword);

            //set the signing algorithm and the key size
            certGenerator.KeySize = KeySize.KeySize2048Bit;
            certGenerator.SignatureAlgorithm = SignatureAlgorithm.SHA256WithRSA;

            certGenerator.AddToSubject(SubjectType.CN, "PACCARISSUECA2001");
            certGenerator.AddToSubject(SubjectType.DC, "paccar");
            certGenerator.AddToSubject(SubjectType.DC, "com");



            //also you can use an alternative method
            certGenerator.Subject = "CN=PACCARISSUECA2001, DC=paccar, DC=com";

            //add some simple extensions to the client certificate
            certGenerator.Extensions.AddKeyUsage(CertificateKeyUsage.DigitalSignature);
            certGenerator.Extensions.AddKeyUsage(CertificateKeyUsage.CertificateSigning);
            certGenerator.Extensions.AddKeyUsage(CertificateKeyUsage.CRLSigning);
            certGenerator.Extensions.AddEnhancedKeyUsage(Oid.FromOidValue("1.3.6.1.4.1.311.21.8", OidGroup.Template));


            //optionally, set a friendly name
            certGenerator.FriendlyName = "b2b" + clientID + "S03_ca_0";

            //create the PFX certificate as user certificate
            File.WriteAllBytes(ClientCertificatePath, certGenerator.GenerateCertificate(ClientCertificatePassword, true));

            //save the public part of the certificate
            File.WriteAllBytes(ClientCertificatePath + ".cer", new X509Certificate2(ClientCertificatePath, ClientCertificatePassword).RawData);
            richTbInfo.AppendText("\nUser certificate was created!");
        }

        public void CreateLastCertificateAsPFX(string RootCertificatePath, string RootCertificatePassword, string ClientCertificatePath, string ClientCertificatePassword)
        {
            //on the demo version the certificates will be valid 30 days only
            //this is the single restriction of the library in demo mode
            X509CertificateGenerator certGenerator = new X509CertificateGenerator(clientserialNumber);

            //set the validity of the certificate
            certGenerator.ValidFrom = DateTime.Now;
            //The certificate will be valid only 30 days on the demo version of the library
            certGenerator.ValidTo = DateTime.Now.AddDays(expiryDays);

            //load the Root Certificate to sign the user certificate
            certGenerator.LoadRootCertificate(File.ReadAllBytes(RootCertificatePath), RootCertificatePassword);

            //set the signing algorithm and the key size
            certGenerator.KeySize = KeySize.KeySize2048Bit;
            certGenerator.SignatureAlgorithm = SignatureAlgorithm.SHA256WithRSA;

            certGenerator.AddToSubject(SubjectType.CN, "b2b" + clientID + "S03");
            certGenerator.AddToSubject(SubjectType.EMAILADDRESS, emailClient);
            certGenerator.AddToSubject(SubjectType.O, "Dealernet_" + clientID);



            //also you can use an alternative method
            //certGenerator.Subject = "CN=Simple user certificate, OU=Organization Unit User Certificate, E=user@email.com";

            //add some simple extensions to the client certificate
            certGenerator.Extensions.AddKeyUsage(CertificateKeyUsage.DigitalSignature);
            certGenerator.Extensions.AddKeyUsage(CertificateKeyUsage.KeyEncipherment);

            //add some enhanced extensions to the client certificate marked as critical

            certGenerator.Extensions.AddEnhancedKeyUsage(CertificateEnhancedKeyUsage.ClientAuthentication);
            certGenerator.Extensions.AddEnhancedKeyUsage(Oid.FromOidValue("1.3.6.1.4.1.311.21.8", OidGroup.Template));

            //certGenerator.Extensions.AddEnhancedKeyUsage((new System.Security.Cryptography.Oid("1.3.6.1.4.1.311.21.8.15191615.3961195.8341936.9132609.6700102.66.1308780127.675695868")));
            certGenerator.Extensions.KeyUsageIsCritical = false;

            
            //optionally, set a friendly name
            certGenerator.FriendlyName = "b2b" + clientID + "S03";
            

            //create the PFX certificate as user certificate
            File.WriteAllBytes(ClientCertificatePath, certGenerator.GenerateCertificate(ClientCertificatePassword, false));

            //save the public part of the certificate
            File.WriteAllBytes(ClientCertificatePath + ".cer", new X509Certificate2(ClientCertificatePath, ClientCertificatePassword).RawData);
            richTbInfo.AppendText("\nClient certificate was created!");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            int count = CollectB2BCertificatesInStore().Count;
            richTbInfo.AppendText($"\nВ хранилище сертификатов найдено {count} сертификатов!");

        }

        private void richTbInfo_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
