using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.ViewModels;
using System.Windows;

namespace ShoppingCartVatsalMeetJankiParth.Views
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        public ProductWindow(User user = null)
        {
            InitializeComponent();
            DataContext = new ProductViewModel(this, user);

        }
    }
}
