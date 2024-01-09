using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZaczytywanieKodow.Models
{
    public class Item
    {
        public int Id { get; set; }
        public List<string> KodSystem { get; set; }
        public string KodDostawcy { get; set; }
        public string KodOem { get; set; }
        public List<string> Dostawca { get; set; }
        public int Wyszukiwania { get; set; }
        public string PolaczoneKody { get; set; }
        public bool WieleKodow { get; set; }

        public Item()
        {
            KodSystem = new List<string>();
            Dostawca = new List<string>();
        }
    }
}
