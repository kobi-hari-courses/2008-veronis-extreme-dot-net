using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FunWithRxNet.Models
{
    public class ColorRecord
    {
        public string DisplayName { get; }

        public Color Color { get;}

        public ColorRecord(string displayName = "", Color color = default)
        {
            DisplayName = displayName;
            Color = color;
        }
    }
}
