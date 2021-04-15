using ShoppingCartVatsalMeetJankiParth.ViewModels;
using System.Windows;

namespace ShoppingCartVatsalMeetJankiParth.Views
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {

        public RegisterWindow()
        {
            InitializeComponent();
            DataContext = new RegisterViewModel(this);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            MinWidth = MaxWidth = Width;
            MinHeight = MaxHeight = Height;
        }


    }
}
