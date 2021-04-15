using ShoppingCartVatsalMeetJankiParth.DataAccess;
using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.Views;
using System.Windows;
using Utility.Authentication;

namespace ShoppingCartVatsalMeetJankiParth.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private RegisterWindow registerWindow;
        LoginDataAccessor loginDataAccessor = new LoginDataAccessor();

        private User user;

        private string userName;
        public string Username
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public DelegateCommand LoginCommand { get; }
        public DelegateCommand SignUpCommand { get; }
        public RegisterViewModel(RegisterWindow register = null)
        {
            if (register != null)
            {
                registerWindow = register;
            }

            SignUpCommand = new DelegateCommand(ExecuteSignUp);
            LoginCommand = new DelegateCommand(ExecuteLogin);
        }

        private void ExecuteSignUp(object _)
        {
            string userName = Username is null ? "" : Username.Trim();
            string password = registerWindow.PasswordTextBox.Password is null ? "" : registerWindow.PasswordTextBox.Password.Trim();
            string confirmPassword = registerWindow.ConfirmPasswordTextBox.Password is null ? "" : registerWindow.ConfirmPasswordTextBox.Password.Trim();
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmPassword))
            {
                if (password != confirmPassword)
                {
                    MessageBox.Show("Password and Confirm Password doesn't match!.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (loginDataAccessor.UserExists(userName))
                {
                    MessageBox.Show("Username already exists. Please select a new username!.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var passHashObj = PasswordUtility.GeneratePasswordHash(password);
                user = new User()
                {
                    Username = userName,
                    Password = passHashObj,
                    //DateCreated = DateTime.Now,
                    Role = UserRole.Customer
                };
                loginDataAccessor.Add(user);
                MessageBox.Show("Registered Successfully!!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearAllData();
            }
            else
            {
                MessageBox.Show("Fields cannot be empty!!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void ExecuteLogin(object _)
        {
            registerWindow.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            registerWindow = null;
        }

        public void ClearAllData()
        {
            Username = "";
            OnPropertyChanged(nameof(Username));
            registerWindow.PasswordTextBox.Password = registerWindow.ConfirmPasswordTextBox.Password = "";

        }

    }
}
