using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    public class Person
    {
        public string FullName { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public bool IsMarried { get; set; }

        public string BirthCountry { get; set; }

        public string Profession { get; set; }

        public Person Mother { get; set; }

        public Person Father { get; set; }

        public List<Person> Children { get; set; }

        public string GetTitle()
        {
            if (Gender == Gender.Male) return "Mr.";
            if (Gender == Gender.Female)
            {
                if (IsMarried) return "Mrs.";
                return "Ms";
            }

            return "";
        }
    }

    public enum Gender
    {
        Female, 
        Male
    }
}
