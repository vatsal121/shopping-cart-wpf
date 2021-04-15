using System.ComponentModel;

namespace ShoppingCartVatsalMeetJankiParth.Entities
{
    public class Cart : Entity, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion


        private int userId;

        public int UserId
        {
            get => userId;

            set
            {
                userId = value;
                OnPropertyChanged(nameof(UserId));
            }
        }

        private int productId;

        public int ProductId
        {
            get => productId;

            set
            {
                productId = value;
                OnPropertyChanged(nameof(ProductId));
            }
        }

        private decimal discountPercentage;
        public decimal DiscountPercentage
        {
            get => discountPercentage; set
            {
                discountPercentage = value;
                OnPropertyChanged(nameof(DiscountPercentage));
            }
        }

        private bool isCustomerCheckedOut;
        public bool IsCustomerCheckedOut
        {
            get => isCustomerCheckedOut; set
            {
                isCustomerCheckedOut = value;
                OnPropertyChanged(nameof(IsCustomerCheckedOut));
            }
        }

        private int qtyOrdered;
        public int QtyOrdered
        {
            get => qtyOrdered;
            set
            {
                qtyOrdered = value;
                OnPropertyChanged(nameof(QtyOrdered));
                if (ProductDetails != null)
                {
                    FinalProductPrice = (ProductDetails.Price - (ProductDetails.Price * DiscountPercentage / 100)) * qtyOrdered;
                    OnPropertyChanged(nameof(FinalProductPrice));
                }
            }
        }

        private decimal finalProductPrice;
        public decimal FinalProductPrice
        {
            get => finalProductPrice;
            set
            {
                finalProductPrice = value;
                OnPropertyChanged(nameof(FinalProductPrice));
            }
        }

        public Product ProductDetails { get; set; }

    }
}
