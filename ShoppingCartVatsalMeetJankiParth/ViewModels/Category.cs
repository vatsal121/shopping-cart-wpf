using ShoppingCartVatsalMeetJankiParth.Entities;
using System.ComponentModel;

namespace ShoppingCartVatsalMeetJankiParth.ViewModels
{
    public class Category : Entity, INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion


        private string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                OnPropertyChanged(nameof(CategoryName));
            }
        }
    }
}
