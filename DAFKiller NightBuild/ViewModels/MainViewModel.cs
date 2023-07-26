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

        //Constructor
        public MainViewModel()
        {
            userRepository= new UserRepository();
            LoadCurrentUserData();
            RunCommand = new ViewModelCommand(ExecuteRunCommand, CanExecuteRunCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand);
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
