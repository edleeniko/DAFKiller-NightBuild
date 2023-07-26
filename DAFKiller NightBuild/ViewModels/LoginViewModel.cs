using DAFKiller_NightBuild.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using DAFKiller_NightBuild.Repositories;
using System.Net;
using System.Threading;
using System.Security.Principal;
using System.Net.Sockets;
using CompID;
using DAFKiller_NightBuild.Helpers;
using System.IO;
using System.Reflection;

namespace DAFKiller_NightBuild.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        //Fields
        private string _username;
        private string _password;
        private string _errorMessage;
        private bool _isViewVisible = true;

        private IUserRepository userRepository;
        //Properties
        public string Username { 
            get => _username;
            set { _username = value;
                OnPropertyChanged(nameof(Username));
            }  }
        public string Password { 
            get => _password;
            set { _password = value;
                OnPropertyChanged(nameof(Password));
            }  }
        public string ErrorMessage { 
            get => _errorMessage;
            set { _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }  }
        public bool IsViewVisible { 
            get => _isViewVisible;
            set { _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }  }


        //Commands
        public ICommand LoginCommand { get; }
        public ICommand RecoverPasswordCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public ICommand ExitCommand { get; }

        //Constructor
        public LoginViewModel()
        {
            userRepository = new UserRepository();
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            ExitCommand = new ViewModelCommand(ExecuteExitCommand);
            RecoverPasswordCommand = new ViewModelCommand(p => ExecuteRecoverPasswordCommand("", ""));
        }

        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if ( string.IsNullOrEmpty(Username) || Username.Length < 4 || string.IsNullOrEmpty(Password) || Password.Length < 4 )
            validData = false;
            else validData = true;
            return validData;
        }


        private void ExecuteLoginCommand(object obj)
        {
            string hwid = CompID.CompID.UniqueId;
            string m_exePath = string.Empty;
            LogWriter lw = new LogWriter($"Hardware ID = {hwid}\nUserName = {Username}\nPassword = {Password}", "log.txt");
            var isValidUser = userRepository.AuthenticateUser(new NetworkCredential(Username, Password));
            var isPayedUser = userRepository.PayedUser(new NetworkCredential(Username, Password));

            if (isValidUser)
            {
                lw.LogWrite($"User {Username} authorized", "log.txt");
                if (isPayedUser)
                {
                    lw.LogWrite($"Hardware ID = {hwid}\nUserName = {Username}\nPassword = {Password}\nPayedUser = {isPayedUser}", "user.txt");
                }
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(Username), null);
                IsViewVisible = false;
            }
            else
            {
                lw.LogWrite($"User {Username} not authorized", "log.txt");
                ErrorMessage = "* Invalid username or password *";
            }
        }
        private void ExecuteExitCommand(object obj)
        {
            Application.Current.Shutdown();
        }

        private void ExecuteRecoverPasswordCommand(string username, string email)
        {
            throw new NotImplementedException();
        }


    }
}
