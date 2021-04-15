using ShoppingCartVatsalMeetJankiParth.DataAccess;
using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ShoppingCartVatsalMeetJankiParth.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {

        private const decimal DISCOUNT_PERCENTAGE = 20;
        ProductDataAccessor productDataAccessor = new ProductDataAccessor();
        CartDataAccessor cartDataAccessor = new CartDataAccessor();
        ProductWindow productWindow;

        private ObservableCollection<Product> productList;
        public ObservableCollection<Product> ProductList
        {
            get => productList;
            set
            {
                productList = value;
                OnPropertyChanged(nameof(ProductList));
            }
        }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }


        private string productName;
        public string ProductName
        {
            get => productName;
            set
            {
                productName = value;
                OnPropertyChanged(nameof(ProductName));
            }
        }

        private decimal categoryId;
        public decimal CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
                OnPropertyChanged(nameof(CategoryId));
            }
        }

        private long price;
        public long Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private long quantity;
        public long Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        private User user;
        public DelegateCommand ChangePasswordCommand { get; }
        public DelegateCommand LogOutCommand { get; }
        public DelegateCommand AddCartCommand { get; }
        public DelegateCommand ViewCartCommand { get; }


        #region Constructor
        public ProductViewModel(ProductWindow productWindow, User _user = null)
        {
            /*            SelectedIndex = -1;
            */
            this.productWindow = productWindow;
            if (_user != null)
            {
                user = _user;
                this.productWindow.UserNameLabel.Content = "Welcome " + user.Username + "!.";
            }
            GetAllData();
            ChangePasswordCommand = new DelegateCommand(ChangePassword);
            LogOutCommand = new DelegateCommand(LogOut);
            AddCartCommand = new DelegateCommand(AddCart);
            ViewCartCommand = new DelegateCommand(ViewCart);
        }

        #endregion

        private void GetAllData()
        {
            var empList = productDataAccessor.GetAll();
            if (empList != null && empList.Count > 0)
            {
                ProductList = new ObservableCollection<Product>(empList);

            }

        }
        private void ChangePassword(object parameter)
        {
            ChangePasswordWindow changePassword = new ChangePasswordWindow(user);
            changePassword.Show();
        }

        private void AddCart(object parameter)
        {
            int QtyOrdered = 1;
            if (SelectedProduct != null)
            {
                if (selectedProduct.Quantity <= 0)
                {
                    MessageBox.Show("Out of stock! It will be available once it is back in stock!.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var getCartList = cartDataAccessor.GetAll($"Where ProductId= {selectedProduct.Id} and UserId= {user.Id} and IsCustomerCheckedOut={0}", null);
                Cart cart = null;
                if (getCartList != null && getCartList.Count > 0)
                {
                    cart = getCartList.FirstOrDefault();
                }
                decimal discountedPrice = 0;
                if (cart != null)
                {
                    QtyOrdered = cart.QtyOrdered;

                    discountedPrice = (selectedProduct.Price - (selectedProduct.Price * DISCOUNT_PERCENTAGE / 100)) * QtyOrdered;
                    cart.QtyOrdered = QtyOrdered + 1;
                    cart.FinalProductPrice = discountedPrice;
                    cartDataAccessor.UpdateCart(cart);
                }
                else
                {
                    discountedPrice = (selectedProduct.Price - (selectedProduct.Price * DISCOUNT_PERCENTAGE / 100)) * QtyOrdered;
                    cart = new Cart()
                    {
                        UserId = user.Id,
                        ProductId = SelectedProduct.Id,
                        QtyOrdered = QtyOrdered,
                        IsCustomerCheckedOut = false,
                        DiscountPercentage = 20,
                        FinalProductPrice = discountedPrice,
                    };
                    productDataAccessor.AddToCart(cart);

                }
                selectedProduct.Quantity -= QtyOrdered;
                productDataAccessor.Update(selectedProduct);
                GetAllData();

                MessageBox.Show("Successfully added product to cart!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ViewCart(object parameter)
        {
            productWindow.Hide();
            CartWindow cartWindow = new CartWindow(user);
            cartWindow.Show();
        }

        private void LogOut(object parameter)
        {
            user = null;
            productWindow.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            productWindow = null;
        }

    }
}
