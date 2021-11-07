using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Tour_agency.Models
{

    public class Client : Person, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Data Validation
        public string Error { get { return null; } }

        public Dictionary<string, string> ClientsErrorCollection { get; private set; } = new Dictionary<string, string>();

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
                            result = "Ім'я клієнта не може бути порожнє "; ;
                        }
                        break;
                    case "surname":
                        if (string.IsNullOrEmpty(surname))
                        {
                            result = "Прізвище клієнта не може бути порожнє ";
                        }
                        break;
                    case "patronymic":
                        if (string.IsNullOrEmpty(patronymic))
                        {
                            result = "по батькові клієнта не може бути порожнє ";
                        }
                        break;
                    case "phone":
                        if (string.IsNullOrEmpty(phone))
                        {
                            result = "Номер телефону клієнта не може бути порожнє ";
                        }
                        break;
                    case "dateOFDeparture":
                        if (string.IsNullOrEmpty(dateOFDeparture))
                        {
                            result = "Дата відправлення не може бути порожнє ";
                        }
                        break;
                    case "returnDate":
                        if (string.IsNullOrEmpty(returnDate))
                        {
                            result = "Дата повернення не може бути порожнє ";
                        }
                        break;
                }

               /* if (ClientsErrorCollection.ContainsKey(columnName))
                {
                    ClientsErrorCollection[columnName] = result;
                }
                else if (result != null)
                    ClientsErrorCollection.Add(columnName, result);

                OnPropertyChanged("ClientsErrorCollection");

                if (result != null)
                    return "!";
                else return "";*/

                if (ClientsErrorCollection.ContainsKey(columnName))//Якщо колекція вже має ключ(тобто, наше поле), більше його не треба створювати, натомість, додати тільки текст помилки
                {
                    ClientsErrorCollection[columnName] = result;
                }
                else if (result != null)
                    ClientsErrorCollection.Add(columnName, result);//Якщо колекція ще не має такого ключа - додати і ключ, і текст помилки

                if (result != null)
                {
                    CanSave = false;
                }
                else
                {
                    CanSave = true;
                }

                OnPropertyChanged("WorkersErrorCollection");

                return result == null ? string.Empty : "!";
            }
        }

        private bool canSave;
        public bool CanSave
        {
            get
            {
                return canSave;
            }
            set
            {
                canSave = value;
                OnPropertyChanged("CanSave");
            }
        }
        #endregion
        public string phone { get; set; }
        public string idTour { get; set; }
        public string dateOFDeparture { get; set; }
        public string returnDate { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
