using FunWithRxNet.Models;
using FunWithRxNet.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FunWithRxNet.Services
{
    public class ColorsService
    {
        public static ColorsService Instance { get; } = new ColorsService();

        public static ImmutableList<ColorRecord> AllColors => _allColors; 

        private static ImmutableList<ColorRecord> _allColors;

        static ColorsService()
        {
            var json = Resources.ColorsJson;
            IEnumerable<KeyValuePair<string, JToken>> tokens = JObject.Parse(json);

            _allColors = tokens.Select(pair => new ColorRecord(
                displayName: pair.Value.ToString(),
                color: FromHex(pair.Key)
                )).ToImmutableList();
        }

        public static Color FromHex(string hex)
        {
            return (Color)ColorConverter.ConvertFromString(hex);
        }

        private ColorsService()
        {

        }

        public async Task<ImmutableList<ColorRecord>> Search(string keyword)
        {
            Debug.WriteLine("Started search for " + keyword);
            await Task.Delay(3000);

            if (string.IsNullOrWhiteSpace(keyword)) return ImmutableList<ColorRecord>.Empty;

            var res = AllColors
                .Where(clr => clr.DisplayName.ToLower().Contains(keyword.ToLower()))
                .ToImmutableList();

            Debug.WriteLine("Completed search for " + keyword);
            return res;
        }
        
    }
}
