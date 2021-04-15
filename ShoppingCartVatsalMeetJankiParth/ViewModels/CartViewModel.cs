using ShoppingCartVatsalMeetJankiParth.DataAccess;
using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ShoppingCartVatsalMeetJankiParth.ViewModels
{
    class CartViewModel : ViewModelBase
    {

        CartWindow cartWindow;
        User user;
        CartDataAccessor cartDataAccessor = new CartDataAccessor();
        ProductDataAccessor productDataAccessor = new ProductDataAccessor();

        private ObservableCollection<Cart> cartList;

        public ObservableCollection<Cart> CartList
        {
            get => cartList;
            set
            {
                cartList = value;
                OnPropertyChanged(nameof(CartList));
            }
        }

        private Cart selectedProduct;
        public Cart SelectedProduct
        {
            get => selectedProduct;
            set
            {
                selectedProduct = value;
                OnPropertyChanged(nameof(SelectedProduct));
                UpdateCartDetails();
            }
        }

        public int SelectedIndex { get; set; }

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

        private string cartTotal;

        public string CartTotal
        {
            get => cartTotal;
            set
            {
                cartTotal = value;
                OnPropertyChanged(nameof(CartTotal));
            }
        }

        public DelegateCommand RemoveCartCommand { get; }
        public DelegateCommand EmptyCartCommand { get; }
        public DelegateCommand CheckoutCommand { get; }
        public DelegateCommand GoBackCommand { get; }

        #region Constructor
        public CartViewModel(CartWindow window, User _user)
        {
            SelectedIndex = -1;
            cartWindow = window;
            user = _user;
            GetCartList();
            RemoveCartCommand = new DelegateCommand(RemoveFromCart);
            EmptyCartCommand = new DelegateCommand(EmptyCart);
            CheckoutCommand = new DelegateCommand(CheckOut);

            GoBackCommand = new DelegateCommand(GoBack);

        }

        #endregion

        private void GoBack(object parameter)
        {
            cartWindow.Hide();
            ProductWindow window = new ProductWindow(user);
            window.Show();
            cartWindow = null;
        }

        private void GetCartList()
        {
            var cartList = cartDataAccessor.GetAll($"WHERE UserId='{user.Id}' and IsCustomerCheckedOut={0} ", null);

            decimal finalTotalOfCart = 0;
            if (cartList != null && cartList.Count > 0)
            {
                CartList = new ObservableCollection<Cart>(cartList);
                finalTotalOfCart = cartList.Sum(x => x.FinalProductPrice);
            }
            else
            {
                CartList = new ObservableCollection<Cart>();
            }

            CartTotal = "Cart Total is: " + finalTotalOfCart.ToString() + " $.";
        }
        private void RemoveFromCart(object parameter)
        {
            if (selectedProduct != null)
            {
                bool success = cartDataAccessor.Remove(selectedProduct);
                if (success)
                {
                    MessageBox.Show("Successfully removed product(s) from the cart!.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetCartList();
                }
                else
                {
                    MessageBox.Show("Error while removing from the cart!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void EmptyCart(object parameter)
        {
            if (CartList != null && CartList.Count > 0)
            {
                int count = 0;
                foreach (var item in CartList)
                {
                    bool success = cartDataAccessor.Remove(item);
                    if (success)
                    {
                        count++;
                    }
                }
                if (count == CartList.Count)
                {
                    MessageBox.Show("Cart emptied successfully!!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetCartList();
                }
                else
                {
                    MessageBox.Show("Some products were not removed from the cart! Please try again after sometime.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void CheckOut(object parameter)
        {
            if (CartList != null && CartList.Count > 0)
            {
                foreach (var item in CartList)
                {
                    item.IsCustomerCheckedOut = true;
                    cartDataAccessor.UpdateCart(item);


                    Product product = productDataAccessor.Get(item.ProductId);
                    if (product != null)
                    {
                        if (product.Quantity == item.QtyOrdered)
                        {
                            product.Quantity = 0;
                        }
                        else
                        {
                            product.Quantity -= item.QtyOrdered;
                        }
                        productDataAccessor.Update(product);
                    }

                }
                MessageBox.Show("Checked out successfully!!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                GetCartList();
            }

        }

        private void UpdateCartDetails()
        {
            if (SelectedIndex >= 0)
            {
                Cart cartObj = CartList[SelectedIndex];

                decimal newPrice = (cartObj.ProductDetails.Price - (cartObj.ProductDetails.Price * cartObj.DiscountPercentage / 100)) * cartObj.QtyOrdered;
                if (cartObj.QtyOrdered > cartObj.ProductDetails.Quantity)
                {
                    MessageBox.Show("Cannot order products more than available stock.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (cartObj.QtyOrdered <= 0)
                {
                    bool success = cartDataAccessor.Remove(cartObj);
                    if (success)
                    {
                        MessageBox.Show("Successfully removed product(s) from the cart as quantity ordered is 0 or less!.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetCartList();
                    }
                    else
                    {
                        MessageBox.Show("Error while removing from the cart!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                cartObj.FinalProductPrice = newPrice;
                cartDataAccessor.UpdateCart(cartObj);
                SelectedIndex = -1;
            }

            SelectedIndex = CartList.IndexOf(selectedProduct);

        }
    }
}
