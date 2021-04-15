using ShoppingCartVatsalMeetJankiParth.ViewModels;
using System;
using System.ComponentModel;

namespace ShoppingCartVatsalMeetJankiParth.Entities
{
    public class Product : Entity, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion



        private string productName;
        public string ProductName
        {
            get => productName;
            set
            {
                productName = value;
            }
        }

        private int categoryId;
        public int CategoryId
        {
            get => categoryId;
            set
            {
                categoryId = value;
            }
        }

        private decimal price;
        public decimal Price
        {
            get => price;
            set
            {
                price = value;
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
            }
        }


        private int quantity;
        public int Quantity
        {
            get => quantity;
            set
            {
                quantity = value;
            }
        }

        public Category Category { get; set; }

        public Product()
        { }

        public Product(string productName, int categoryId, decimal price, string description, int quantity)
            : this(default, default, default, productName, categoryId, price, description, quantity, null)
        { }

        public Product(int id, DateTime dateCreated, DateTime? dateModified,
                        string productName, int categoryId, decimal price, string description, int quantity, Category category)
            : base(id, dateCreated, dateModified)
        {
            ProductName = productName;
            CategoryId = categoryId;
            Price = price;
            Description = description;
            Quantity = quantity;
            Category = category;
        }

    }
}
