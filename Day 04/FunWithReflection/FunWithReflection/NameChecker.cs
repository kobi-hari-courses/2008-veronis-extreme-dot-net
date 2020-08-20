using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    public static class NameChecker
    {
        [Check("First and Last name exist")]
        public static string CheckFirstAndLastName(Person p)
        {
            var wordCount = p.FullName.Split(' ').Where(s => !string.IsNullOrWhiteSpace(s)).Count();

            if (wordCount >= 2) return null;

            return "Last name is missing";
        }

        [Check("Has birth country")]
        public static string CheckBirthCountry(Person p)
        {
            if (string.IsNullOrWhiteSpace(p.BirthCountry))
                return "Has no Birth Country";

            return null;
        }

        [Check("Has profession")]
        public static string CheckProfession(Person p)
        {
            if (string.IsNullOrWhiteSpace(p.Profession))
                return "Has no Profession";

            return null;
        }

        [Check("Has University")] 
        public static string CheckUniversity(Student s)
        {
            if (string.IsNullOrWhiteSpace(s.University))
                return "Has no University";

            return null;
        }

    }
}
