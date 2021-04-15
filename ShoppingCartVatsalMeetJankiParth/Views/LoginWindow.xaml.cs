using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.ViewModels;
using System;
using System.Windows;
using Utility.Authentication;

namespace ShoppingCartVatsalMeetJankiParth.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window, IPasswordAccessor
    {
        public LoginViewModel ViewModel { get; }

        public event Action PasswordChanged;

        public string Password => PasswordPasswordBox.Password;

        public void ClearPassword()
        {
            PasswordPasswordBox.Clear();
        }

        public LoginWindow()
        {
            InitializeComponent();
            ViewModel = new LoginViewModel(this);
            ViewModel.LoginError += LoginError;
            ViewModel.LoginSuccess += LoginSuccess;
            DataContext = ViewModel;
        }

        private void LoginError(string errorMessage)
        {
            MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation, MessageBoxResult.OK);
        }

        private void LoginSuccess(User user)
        {
            if (user != null)
            {
                this.Hide();
                if (user.Role == UserRole.Admin)
                {
                    AdminWindow adminWindow = new AdminWindow(user);
                    adminWindow.Show();
                }
                else
                {
                    ProductWindow productWindow = new ProductWindow(user);
                    productWindow.Show();
                }
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MinWidth = MaxWidth = Width;
            MinHeight = MaxHeight = Height;
        }

        private void PasswordPasswordBoxChanged(object sender, RoutedEventArgs e)
        {
            PasswordChanged?.Invoke();
        }
    }
}
