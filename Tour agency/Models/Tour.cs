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
        //Поле, яке слугує для відправки винятків при некоректному вводі, в даному випадку, воно нічого не відправляє, так як не потрібно:
        public string Error { get { return null; } }

        //Словник помилок, який використовується для підказок(ToolTip), які з'являються біля контролу для визначення конкретної помилки
        public Dictionary<string, string> WorkersErrorCollection { get; private set; } = new Dictionary<string, string>();

        //Поле, яке безпосередньо робить перевірки:
        public string this[string columnName]
        {
            get
            {
                //на початку ніяких помилок немає, тому null
                string result = null;
                //"перебираємо" ймовірні помилки
                switch (columnName)
                {
                    case "name":
                        if (string.IsNullOrEmpty(name)) //якщо ввід нульовий, тобто, немає ніякого значення
                        {
                            result = "Worker name cannot be empty";//"Ім'я користувача не може бути порожнім"
                        }
                        break;
                    case "price":
                        Regex regex = new Regex(@"^[0-9.]+$");//дозволяє тільки цифри і крапку
                        //Match match;
                        if (string.IsNullOrEmpty(price))
                        {
                            result = "Price cannot be empty";
                        }
                        else if (!regex.IsMatch(price))
                        {
                            result = "Price can contain only digits and a single dot";
                        }
                        break;
                    case "country":
                        if (string.IsNullOrEmpty(country))
                        {
                            result = "Worker middle name cannot be empty";
                        }
                        break;
                    case "hotel":
                        if (string.IsNullOrEmpty(hotel))
                        {
                            result = "Worker position cannot be empty";
                        }
                        break;
                    case "description":
                        if (string.IsNullOrEmpty(description))
                        {
                            result = "Worker position cannot be empty";
                        }
                        break;
                }

                //Додавання помилок у словник
                if (WorkersErrorCollection.ContainsKey(columnName))//Якщо колекція вже має ключ(тобто, наше поле), більше його не треба створювати, натомість, додати тільки текст помилки
                {
                    WorkersErrorCollection[columnName] = result;
                }
                else if (result != null)
                    WorkersErrorCollection.Add(columnName, result);//Якщо колекція ще не має такого ключа - додати і ключ, і текст помилки

                OnPropertyChanged("WorkersErrorCollection");//Поле WorkersErrorCollection підписалось на подію OnPropertyChanged

                if (result != null)//якщо в полі result є помилка, показати в повідомленні "!"
                    return "!";
                else return "";//Якщо ні - нічого не показувати
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
