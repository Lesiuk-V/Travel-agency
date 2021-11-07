using System;
using System.Collections.Generic;
using System.Text;

namespace Tour_agency.Models
{
    public abstract class Person
    {
        private string _id;
        private string _name;
        private string _surname;
        private string _patronymic;
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
        public string Surname 
        {
            get => _surname;
            set => _surname = value; 
        }
        public string Patronymic 
        {
            get => _patronymic;
            set => _patronymic = value;
        }
    }
}
