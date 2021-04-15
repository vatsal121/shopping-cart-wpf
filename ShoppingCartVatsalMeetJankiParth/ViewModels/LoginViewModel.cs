using ShoppingCartVatsalMeetJankiParth.DataAccess;
using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.Views;
using System;
using Utility.Authentication;

namespace ShoppingCartVatsalMeetJankiParth.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public event Action<User> LoginSuccess;
        public event Action<string> LoginError;

        private readonly IPasswordAccessor passwordAccessor;
        private LoginWindow loginWindow;
        private LoginDataAccessor LoginDataAccessor { get; set; }

        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
                LoginCommand.OnCanExecuteChanged();
            }
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand SignUpCommand { get; }

        public LoginViewModel(IPasswordAccessor passwordAccessor)
        {
            loginWindow = passwordAccessor as LoginWindow;
            this.passwordAccessor = passwordAccessor;
            this.passwordAccessor.PasswordChanged += PasswordChanged;

            LoginDataAccessor = new LoginDataAccessor();
            LoginCommand = new DelegateCommand(ExecuteLogin, CanExecuteLogin);
            SignUpCommand = new DelegateCommand(ExecuteSignUp);
        }

        private void PasswordChanged()
        {
            LoginCommand.OnCanExecuteChanged();
        }

        private void ExecuteLogin(object _)
        {
            //// TODO
            if (LoginDataAccessor.UserExists(Username.Trim()))
            {
                User user = LoginDataAccessor.Get(Username.Trim());
                if (user != null)
                {
                    bool success = PasswordUtility.CheckPassword(passwordAccessor.Password, user.Password);

                    if (success)
                    {
                        LoginSuccess?.Invoke(user);
                    }
                    else
                    {
                        LoginError?.Invoke("Invalid username or password.");
                    }
                }
                else
                {
                    LoginError?.Invoke("User does not exists!.");
                }
            }
            else
            {
                LoginError?.Invoke("User does not exists!.");
            }
        }

        private bool CanExecuteLogin(object _)
        {
            return !string.IsNullOrWhiteSpace(Username)
                && !string.IsNullOrWhiteSpace(passwordAccessor.Password);
        }

        private void ExecuteSignUp(object _)
        {
            loginWindow.Hide();
            RegisterWindow registerView = new RegisterWindow();
            registerView.Show();
            loginWindow = null;
        }
    }
}
