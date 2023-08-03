using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using DAFKiller_NightBuild.View;
using DAFKiller_NightBuild.Views;
using AutoUpdaterDotNET;

namespace DAFKiller_NightBuild
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Запуск приложения с проверкой наличия соединения с сетью Интернет и последующей проверкой наличия обновлений
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            Directory.SetCurrentDirectory(AppContext.BaseDirectory);
            string curVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString(2);
            string exeName = AppDomain.CurrentDomain.FriendlyName;
            string exePath = Assembly.GetExecutingAssembly().Location;
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads\\Update\\";
            var loginView = new LoginView();
            if(IsInternetAvialable())
            {
                AutoUpdater.Start("https://truckx.net/Updates/AutoUpdater.xml");
                AutoUpdater.HttpUserAgent = "AutoUpdater";
                AutoUpdater.DownloadPath = downloadsPath;
                var currentDirectory = new DirectoryInfo(exePath);
                if (currentDirectory.Parent != null)
                {
                    AutoUpdater.InstallationPath = currentDirectory.Parent.FullName;
                }
                loginView.Show();

            }
            else { Application.Current.Shutdown(); }

            loginView.IsVisibleChanged += (s, ev) =>
            {

                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainView();
                    mainView.Show();
                    loginView.Close();
                }
            };
        }
        /// <summary>
        /// Проверка подключения к сети Интернет
        /// </summary>
        /// <returns></returns>
        private bool IsInternetAvialable()
        {
            string remoteUri = "https://www.microsoft.com/favicon.ico";
            byte[] myDataBuffer;
            bool isInternetAvial = false;
            WebClient myWebClient = new WebClient();
            try
            {
                myDataBuffer = myWebClient.DownloadData(remoteUri);
                if (myDataBuffer.Length > 0)
                {
                    isInternetAvial = true;
                }
            }
            catch (Exception)
            {
                isInternetAvial = false;
            }
            return isInternetAvial;
        }

    }
}
