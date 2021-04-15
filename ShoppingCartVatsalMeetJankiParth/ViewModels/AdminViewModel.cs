using ShoppingCartVatsalMeetJankiParth.DataAccess;
using ShoppingCartVatsalMeetJankiParth.Entities;
using ShoppingCartVatsalMeetJankiParth.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ShoppingCartVatsalMeetJankiParth.ViewModels
{
    class AdminViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        ProductDataAccessor productDataAccessor = new ProductDataAccessor();
        CartDataAccessor cartDataAccessor = new CartDataAccessor();

        CategoryDataAccessor categoryDataAccessor = new CategoryDataAccessor();
        User user;
        AdminWindow adminWindow;

        private ObservableCollection<Category> categoryList;
        public ObservableCollection<Category> CategoryList
        {
            get => categoryList;
            set
            {
                categoryList = value;
                OnPropertyChanged(nameof(CategoryList));
            }
        }

        private Product selectedCategory;
        public Product SelectedCategory
        {
            get => selectedCategory;
            set
            {
                selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));

            }
        }



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
                UpdateProductDetails();

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

        private int quantity;

        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public int SelectedIndex { get; set; }

        private string totalSales;
        public string TotalSales
        {
            get => totalSales; set
            {
                totalSales = value;
                OnPropertyChanged(nameof(TotalSales));
            }
        }
        public DelegateCommand ChangePasswordCommand { get; }
        public DelegateCommand LogOutCommand { get; }

        public DelegateCommand AddNewProductCommand { get; }

        public DelegateCommand RemoveProductCommand { get; }

        #region Constructor
        public AdminViewModel(AdminWindow admin, User _user)
        {
            SelectedIndex = -1;

            adminWindow = admin;
            user = _user;
            adminWindow.UserNameLabel.Content = "Welcome " + user.Username + "!.";
            GetAllData();
            ChangePasswordCommand = new DelegateCommand(ChangePassword);
            LogOutCommand = new DelegateCommand(LogOut);
            AddNewProductCommand = new DelegateCommand(AddNewProduct);
            RemoveProductCommand = new DelegateCommand(RemoveProduct);
        }
        #endregion

        private void GetAllData()
        {
            var productList = productDataAccessor.GetAll();
            decimal totalSalesAmt = 0;
            if (productList != null && productList.Count > 0)
            {
                ProductList = new ObservableCollection<Product>(productList);
                foreach (var item in ProductList)
                {
                    var soldItemCartList = cartDataAccessor.GetAll($" where ProductId={item.Id} and IsCustomerCheckedOut={1}", null);
                    if (soldItemCartList != null && soldItemCartList.Count > 0)
                    {
                        totalSalesAmt += soldItemCartList.Sum(x => x.FinalProductPrice);
                    }
                }
            }

            TotalSales = "Total Sales: " + totalSalesAmt.ToString() + " $.";


            var catList = categoryDataAccessor.GetAll();
            if (catList != null && catList.Count > 0)
            {
                CategoryList = new ObservableCollection<Category>(catList);
            }

        }
        private void ChangePassword(object parameter)
        {
            ChangePasswordWindow changePassword = new ChangePasswordWindow(user);
            changePassword.Show();
        }

        private void LogOut(object parameter)
        {
            user = null;
            adminWindow.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            adminWindow = null;
        }

        private void AddNewProduct(object parameter)
        {
            var selectedCategory = parameter as ComboBox;
            if (CheckStringIsEmptyNullOrWhiteSpace(ProductName) || CheckStringIsEmptyNullOrWhiteSpace(Price.ToString()) || CheckStringIsEmptyNullOrWhiteSpace(Description) || CheckStringIsEmptyNullOrWhiteSpace(Quantity.ToString()))
            {
                MessageBox.Show("Fields cannot be empty!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Product product = new Product(ProductName, (int)selectedCategory.SelectedValue, Price, Description, Quantity);

            productDataAccessor.Add(product);
            MessageBox.Show("Product added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            ProductName = "";
            Price = 0;
            Description = "";
            Quantity = 0;
            SelectedCategory = null;

            OnPropertyChanged(nameof(ProductName));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(Quantity));
            OnPropertyChanged(nameof(SelectedCategory));

            GetAllData();
        }

        private void RemoveProduct(object parameter)
        {
            SelectedIndex = -1;
            if (selectedProduct != null)
            {
                var cartList = cartDataAccessor.GetAll($" Where ProductId={selectedProduct.Id}", null);
                if (cartList != null && cartList.Count > 0)
                {
                    foreach (var item in cartList)
                    {
                        cartDataAccessor.Remove(item);
                    }
                }
                productDataAccessor.Remove(selectedProduct);
                MessageBox.Show("Successfully Removed the product!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                GetAllData();
            }
        }
        private void UpdateProductDetails()
        {
            if (SelectedIndex >= 0)
            {
                Product prodObj = ProductList[SelectedIndex];

                decimal newPrice = prodObj.Price * prodObj.Quantity;
                productDataAccessor.Update(prodObj);
                SelectedIndex = -1;
                GetAllData();
            }

            SelectedIndex = ProductList.IndexOf(selectedProduct);

        }

        private bool CheckStringIsEmptyNullOrWhiteSpace(string s)
        {
            return string.IsNullOrEmpty(s) || string.IsNullOrWhiteSpace(s);
        }
    }
}