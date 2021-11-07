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
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                        {
                            result = "Ім'я клієнта не може бути порожнє "; ;
                        }
                        break;
                    case "Surname":
                        if (string.IsNullOrEmpty(Surname))
                        {
                            result = "Прізвище клієнта не може бути порожнє ";
                        }
                        break;
                    case "Patronymic":
                        if (string.IsNullOrEmpty(Patronymic))
                        {
                            result = "по батькові клієнта не може бути порожнє ";
                        }
                        break;
                    case "Phone":
                        if (string.IsNullOrEmpty(Phone))
                        {
                            result = "Номер телефону клієнта не може бути порожнє ";
                        }
                        break;
                    case "DateOFDeparture":
                        if (string.IsNullOrEmpty(DateOFDeparture))
                        {
                            result = "Дата відправлення не може бути порожнє ";
                        }
                        break;
                    case "ReturnDate":
                        if (string.IsNullOrEmpty(ReturnDate))
                        {
                            result = "Дата повернення не може бути порожнє ";
                        }
                        break;
                    case "IdTour":
                        if(string.IsNullOrEmpty(IdTour))
                        {
                            result = "Виберіть тур";
                        }
                        break;
                }

                if (ClientsErrorCollection.ContainsKey(columnName))
                {
                    ClientsErrorCollection[columnName] = result;
                }
                else if (result != null)
                    ClientsErrorCollection.Add(columnName, result);

                OnPropertyChanged("ClientsErrorCollection");

                if (result != null)
                    return "!";
                else return "";
            }
        }


        #endregion
        private string _phone;
        private string _idTour;
        private string _dateOfDeparture;
        private string _returnDate;
        public string Phone 
        { 
            get => _phone;
            set => _phone = value;
        }
        public string IdTour 
        {
            get => _idTour;
            set => _idTour = value;
        }
        public string DateOFDeparture 
        {
            get => _dateOfDeparture;
            set => _dateOfDeparture = value;
        }
        public string ReturnDate 
        {
            get => _returnDate;
            set => _returnDate = value;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
