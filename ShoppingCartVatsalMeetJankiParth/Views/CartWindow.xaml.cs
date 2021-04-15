using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.ViewModels;
using System.Windows;

namespace ShoppingCartVatsalMeetJankiParth.Views
{
    /// <summary>
    /// Interaction logic for CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        public CartWindow(User user = null)
        {
            InitializeComponent();
            DataContext = new CartViewModel(this, user);
        }
    }
}
