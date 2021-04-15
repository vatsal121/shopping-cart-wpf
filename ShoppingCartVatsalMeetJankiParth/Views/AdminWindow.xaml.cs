using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.ViewModels;
using System.Windows;

namespace ShoppingCartVatsalMeetJankiParth.Views
{
    /// <summary>
    /// Interaction logic for AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow(User user = null)
        {
            InitializeComponent();
            DataContext = new AdminViewModel(this, user);
        }
    }
}
