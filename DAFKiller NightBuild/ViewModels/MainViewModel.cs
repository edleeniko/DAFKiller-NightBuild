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
        public ICommand RunMileageCommand { get; }
        public ICommand RunRemoteCommand { get; }
        public ICommand RunFullCommand { get; }

        //Constructor
        public MainViewModel()
        {
            userRepository= new UserRepository();
            LoadCurrentUserData();
            RunCommand = new ViewModelCommand(ExecuteRunCommand, CanExecuteRunCommand);
            RunDiagnosticCommand = new ViewModelCommand(ExecuteRunDiagnosticCommand, CanExecuteRunDiagnosticCommand);
            RunProgrammingCommand = new ViewModelCommand(ExecuteRunProgrammingCommand, CanExecuteRunProgrammingCommand);
            RunMileageCommand = new ViewModelCommand(ExecuteRunMileageCommand, CanExecuteRunMileageCommand);
            RunRemoteCommand = new ViewModelCommand(ExecuteRunRemoteCommand, CanExecuteRunRemoteCommand);
            RunFullCommand = new ViewModelCommand(ExecuteRunFullCommand, CanExecuteRunFullCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand);
        }

        private bool CanExecuteRunFullCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void ExecuteRunFullCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteRunRemoteCommand(object obj)
        {
            bool canExecute = false;
            var loadUserRights = SaverLoader.Load<UserModel>("c:\\temp.q");
            string rights = loadUserRights.UserRights.ToString();
            if (rights.Contains("AccessRemote"))
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
            bool canExecute = false;
            var loadUserRights = SaverLoader.Load<UserModel>("c:\\temp.q");
            string rights = loadUserRights.UserRights.ToString();
            if (rights.Contains("AccessMileage"))
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
            bool canExecute = false;
            var loadUserRights = SaverLoader.Load<UserModel>("c:\\temp.q");
            string rights = loadUserRights.UserRights.ToString();
            if (rights.Contains("AccessProgramming"))
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
            bool canExecute = false;
            var loadUserRights = SaverLoader.Load<UserModel>("c:\\temp.q");
            string rights = loadUserRights.UserRights.ToString();
            if (rights.Contains("AccessDiagnostic"))
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
            
            bool payedUser;
            if (File.Exists(file))
            {
                payedUser = true;
            }
            else
            {
                payedUser = false;
            }
                return payedUser;

            /*
            bool result = false;
            var user = userRepository.GetByUsername(Thread.CurrentPrincipal.Identity.Name);
            if (user.UserRights == (UserModel.UserRightsFlags)8)
            {
               result= true;
            }
            return result;
            */
        }

        private void ExecuteRunCommand(object obj)
        {
            
        }

        private void ExecuteExitCommand(object obj)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }

            Application.Current.Shutdown();
        }
    }
}
