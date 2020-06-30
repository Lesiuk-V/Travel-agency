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
                    case "name":
                        if (string.IsNullOrEmpty(name))
                        {
                            result = "Поле назви туру не може бути порожнє ";
                        }
                        break;
                    case "price":
                        Regex regex = new Regex(@"^[0-9.]+$");
                        if (string.IsNullOrEmpty(price))
                        {
                            result = "Поле ціни не може бути порожнє ";
                        }
                        else if (!regex.IsMatch(price))
                        {
                            result = "Поле ціни може містити тільки цифри та порожнє ";
                        }
                        break;
                    case "country":
                        if (string.IsNullOrEmpty(country))
                        {
                            result = "Поле країни не може бути порожнє ";
                        }
                        break;
                    case "hotel":
                        if (string.IsNullOrEmpty(hotel))
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
        public string id { get; set; }
        public string name { get; set; }
        public string price { get; set; }
        public string country { get; set; }
        public string hotel { get; set; }
        public string description { get; set; }
        public string image { get; set; }

        public BitmapImage tourImage { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public override string ToString()
        {
            return name;
        }
    }
}
