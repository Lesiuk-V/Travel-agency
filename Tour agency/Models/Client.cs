using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tour_agency.Models
{

    public class Client : INotifyPropertyChanged, IDataErrorInfo
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
                    case "surname":
                        if (string.IsNullOrEmpty(surname))
                        {
                            result = "Worker last name cannot be empty";
                        }
                        break;
                    case "patronymic":
                        if (string.IsNullOrEmpty(patronymic))
                        {
                            result = "Worker middle name cannot be empty";
                        }
                        break;
                    case "phone":
                        if (string.IsNullOrEmpty(phone))
                        {
                            result = "Worker position cannot be empty";
                        }
                        break;
                    case "tour":
                        if (string.IsNullOrEmpty(idTour))
                        {
                            result = "Worker position cannot be empty";
                        }
                        break;
                    case "dateOFDeparture":
                        if (string.IsNullOrEmpty(dateOFDeparture))
                        {
                            result = "Worker position cannot be empty";
                        }
                        break;
                    case "returnDate":
                        if (string.IsNullOrEmpty(returnDate))
                        {
                            result = "Worker address cannot be empty";
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
        public string surname { get; set; }
        public string patronymic { get; set; }
        public string phone { get; set; }
        public string idTour { get; set; }
        public string dateOFDeparture { get; set; }
        public string returnDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
