using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FunWithReflection
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class CheckAttribute: Attribute
    {
        public CheckAttribute()
        {

        }

        public CheckAttribute(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }
    }
}
