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
    /// <summary>
    /// Клас для роботи із Турами
    /// </summary>
    public class Tour: INotifyPropertyChanged, IDataErrorInfo
    {
        #region Data Validation
        /// <summary>
        /// Поле для отримання помилок
        /// </summary>
        public string Error { get { return null; } }

        private Dictionary<string, string> TourErrorCollection { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// Методд який при зміні полів в текст боксах перевіряє чи валідні данні в них
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Властивість для доступу до закритого поля _id
        /// </summary>
        public string Id 
        {
            get => _id;
            set => _id = value; 
        }
        /// <summary>
        /// Властивість для доступу до закритого поля _name
        /// </summary>
        public string Name 
        {
            get => _name;
            set => _name = value;
        }
        /// <summary>
        /// Властивість для доступу до закритого поля _price
        /// </summary>
        public string Price 
        {
            get => _price;
            set => _price = value;
        }
        /// <summary>
        /// Властивість для доступу до закритого поля _country
        /// </summary>
        public string Country 
        {
            get => _country;
            set => _country = value;
        }
        /// <summary>
        /// Властивість для доступу до закритого поля _hotel
        /// </summary>
        public string Hotel 
        {
            get => _hotel;
            set => _hotel = value;
        }
        /// <summary>
        /// Властивість для доступу до закритого поля _description
        /// </summary>
        public string Description 
        {
            get => _description;
            set => _description = value;
        }
        /// <summary>
        /// Властивість для доступу до закритого поля _image
        /// </summary>
        public string Image 
        {
            get => _image;
            set => _image = value;
        }
        /// <summary>
        /// Поле для отримання картинки
        /// </summary>
        public BitmapImage TourImage { get; set; }
        /// <summary>
        ///  івент який спрацьовіє при зміні даниз в текст боксі
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Метод який викликає івент при зміні даних в текст боксі
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName) =>  PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        /// <summary>
        /// Перевизначення методу Tostring() при якому використання цього методу буде повертати назву туру  
        /// </summary>
        /// <returns></returns>
        public override string ToString() => Name;

    }
}
