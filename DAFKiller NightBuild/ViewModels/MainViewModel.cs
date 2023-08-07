using DAFKiller_NightBuild.Models;
using DAFKiller_NightBuild.Repositories;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using Wpf.Ui.Controls;
using DAFKiller.Core;

namespace DAFKiller_NightBuild.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private string _password;
        private string _message;
        private IUserRepository userRepository;
        private UserAccountModel _userAccount;
        static string filename = "user.txt";
        static string workingDirectory = Environment.CurrentDirectory;
        static string file = $"{workingDirectory}\\{filename}";
        static string filenameUserData = "userdata.dat";
        static string fileUserData = $"{workingDirectory}\\{filenameUserData}";


        public UserAccountModel UserAccount 
        {
            get => _userAccount;
            set 
            { 
                _userAccount = value; 
                OnPropertyChanged(nameof(UserAccount));
            } 
        }

        //Properties
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        //Commands
        public ICommand ExitCommand { get; }
        public ICommand RunCommand { get; }
        public ICommand RunDiagnosticCommand { get; }
        public ICommand RunProgrammingCommand { get; }
        public ICommand RunTruckSystemConfiguration { get; }
        public ICommand RunReplaceControlUnit { get; }
        public ICommand RunCustomParameters { get; }
        public ICommand RunMileageCommand { get; }
        public ICommand RunRemoteCommand { get; }
        public ICommand RunDocumentsCommand { get; }
        public ICommand RunSettingsCommmand { get; }
        public ICommand RunFullCommand { get; }

        //Constructor
        public MainViewModel()
        {
            userRepository= new UserRepository();
            LoadCurrentUserData();
            RunCommand = new ViewModelCommand(ExecuteRunCommand, CanExecuteRunCommand);
            RunDiagnosticCommand = new ViewModelCommand(ExecuteRunDiagnosticCommand, CanExecuteRunDiagnosticCommand);
            RunProgrammingCommand = new ViewModelCommand(ExecuteRunProgrammingCommand, CanExecuteRunProgrammingCommand);
            RunTruckSystemConfiguration = new ViewModelCommand(ExecuteRunTruckSystemConfiguration, CanExecuteRunTruckSystemConfiguration);
            RunReplaceControlUnit = new ViewModelCommand(ExecuteRunReplaceControlUnit, CanExecuteRunReplaceControlUnit);
            RunCustomParameters = new ViewModelCommand(ExecuteRunCustomParameters, CanExecuteRunCustomParameters);
            RunMileageCommand = new ViewModelCommand(ExecuteRunMileageCommand, CanExecuteRunMileageCommand);
            RunRemoteCommand = new ViewModelCommand(ExecuteRunRemoteCommand, CanExecuteRunRemoteCommand);
            RunDocumentsCommand = new ViewModelCommand(ExecuteRunDocumentsCommand, CanExecuteRunDocumentsCommand);
            RunSettingsCommmand = new ViewModelCommand(ExecuteRunSettingsCommmand, CanExecuteRunSettingsCommmand);
            RunFullCommand = new ViewModelCommand(ExecuteRunFullCommand, CanExecuteRunFullCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand);
        }

        private bool CanExecuteRunCustomParameters(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("CustomParameters"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunCustomParameters(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunReplaceControlUnit(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("ReplaceControUnit"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunReplaceControlUnit(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunTruckSystemConfiguration(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("TruckSystemConfiguration"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunTruckSystemConfiguration(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunSettingsCommmand(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("Settings"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunSettingsCommmand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunDocumentsCommand(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("Documents"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunDocumentsCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunFullCommand(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("Full"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunFullCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunRemoteCommand(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("Remote"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunRemoteCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunMileageCommand(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("Mileage"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunMileageCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunProgrammingCommand(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("Programming"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunProgrammingCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunDiagnosticCommand(object obj)
        {
            var loadUserRights = SaverLoader.Load<UserModel>(fileUserData);
            string rights = loadUserRights.UserRights.ToString();
            bool canExecute;
            if (rights.Contains("Diagnostic"))
            {
                canExecute = true;
            }
            else
                canExecute = false;
            return canExecute;
        }

        private void ExecuteRunDiagnosticCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void LoadCurrentUserData()
        {
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);

            if (user != null)
            {
                UserAccount = new UserAccountModel()
                {
                    UserName = user.UserName,
                    DisplayName = $"Welcome * {user.UserName} *\nYour hardware ID: {user.HwId}\nUser rights: {user.UserRights}",
                    ProfilePicture = null
                };
            }
        }

        private bool CanExecuteRunCommand(object obj)
        {
            return false;
        }

        private void ExecuteRunCommand(object obj)
        {
            
        }

        private void ExecuteExitCommand(object obj)
        {
            if (File.Exists(file) & File.Exists(fileUserData))
            {
                File.Delete(file);
                File.Delete(fileUserData);
            }

            Application.Current.Shutdown();
        }
    }
}
