using System;
using System.Collections.Generic;
using System.Text;

namespace Tour_agency.Models
{
    public abstract class Person
    {
        public string id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string patronymic { get; set; }
    }
}
