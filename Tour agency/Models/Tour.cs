using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Tour_agency.Models
{

    public class Tour: INotifyPropertyChanged, IDataErrorInfo
    {
        #region Data Validation
        public string Error { get { return null; } }

        public Dictionary<string, string> TourErrorCollection { get; private set; } = new Dictionary<string, string>();

        public string this[string columnName]
        {
            get
            {
                string result = null;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                        {
                            result = "Поле назви туру не може бути порожнє ";
                        }
                        break;
                    case "Price":
                        Regex regex = new Regex(@"^[0-9.]+$");
                        if (string.IsNullOrEmpty(Price))
                        {
                            result = "Поле ціни не може бути порожнє ";
                        }
                        else if (!regex.IsMatch(Price))
                        {
                            result = "Поле ціни може містити тільки цифри та порожнє ";
                        }
                        break;
                    case "Country":
                        if (string.IsNullOrEmpty(Country))
                        {
                            result = "Поле країни не може бути порожнє ";
                        }
                        break;
                    case "Hotel":
                        if (string.IsNullOrEmpty(Hotel))
                        {
                            result = "Поле готелю не може бути порожнє ";
                        }
                        break;
                }

                if (TourErrorCollection.ContainsKey(columnName))
                {
                    TourErrorCollection[columnName] = result;
                }
                else if (result != null)
                    TourErrorCollection.Add(columnName, result);

                OnPropertyChanged("TourErrorCollection");

                if (result != null)
                    return "!";
                else return "";
            }
        }
        #endregion
        private string _id;
        private string _name;
        private string _price;
        private string _country;
        private string _hotel;
        private string _description;
        private string _image;
        public string Id 
        {
            get => _id;
            set => _id = value; 
        }
        public string Name 
        {
            get => _name;
            set => _name = value;
        }
        public string Price 
        {
            get => _price;
            set => _price = value;
        }
        public string Country 
        {
            get => _country;
            set => _country = value;
        }
        public string Hotel 
        {
            get => _hotel;
            set => _hotel = value;
        }
        public string Description 
        {
            get => _description;
            set => _description = value;
        }
        public string Image 
        {
            get => _image;
            set => _image = value;
        }

        public BitmapImage tourImage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) =>  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public override string ToString() => Name;

    }
}
