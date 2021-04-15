using ShoppingCartVatsalMeetJankiParth.DataAccess;
using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.Views;
using System.Windows;
using Utility.Authentication;

namespace ShoppingCartVatsalMeetJankiParth.ViewModels
{
    class ChangePasswordViewModel : ViewModelBase
    {

        private ChangePasswordWindow changePasswordWindow;
        private User user;

        LoginDataAccessor dataAccessor = new LoginDataAccessor();
        public DelegateCommand PasswordChangeCommand { get; }

        public ChangePasswordViewModel(User _user, ChangePasswordWindow changePassword)
        {
            user = _user;
            this.changePasswordWindow = changePassword;
            PasswordChangeCommand = new DelegateCommand(ChangePassword);
        }

        private void ChangePassword(object parameter)
        {
            string currentPassword = changePasswordWindow.CurrentPasswordTextBox.Password;
            string newPassword = changePasswordWindow.NewPasswordTextBox.Password;
            string confirmNewPassword = changePasswordWindow.ConfirmNewPasswordTextBox.Password;

            if (!string.IsNullOrEmpty(currentPassword) && !string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(confirmNewPassword))
            {
                if (user != null)
                {
                    if (!PasswordUtility.CheckPassword(currentPassword, user.Password))
                    {
                        MessageBox.Show("Incorrect old password.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (newPassword != confirmNewPassword)
                    {
                        MessageBox.Show("Password and Confirm Password doesn't match!.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    //var newPasswordHash = PasswordUtility.GeneratePasswordHash(newPassword);
                    //user.Password = newPasswordHash;

                    dataAccessor.ChangePassword(user, currentPassword, newPassword);
                    MessageBox.Show("Password changed successfully!!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearAllValues();
                    changePasswordWindow.Hide();
                }
            }
            else
            {
                MessageBox.Show("Fields cannot be empty!.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearAllValues()
        {
            changePasswordWindow.CurrentPasswordTextBox.Password = "";
            changePasswordWindow.NewPasswordTextBox.Password = "";
            changePasswordWindow.ConfirmNewPasswordTextBox.Password = "";

        }
    }
}
