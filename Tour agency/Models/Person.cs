using System;
using System.Collections.Generic;
using System.Text;

namespace Tour_agency.Models
{
    /// <summary>
    /// Асбтрактний клас в якому визначені базові поля
    /// </summary>
    public abstract class Person
    {
        private string _id;
        private string _name;
        private string _surname;
        private string _patronymic;
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
        /// Властивість для доступу до закритого поля _surname
        /// </summary>
        public string Surname 
        {
            get => _surname;
            set => _surname = value; 
        }
        /// <summary>
        /// Властивість для доступу до закритого поля _patronymic
        /// </summary>
        public string Patronymic 
        {
            get => _patronymic;
            set => _patronymic = value;
        }
    }
}
