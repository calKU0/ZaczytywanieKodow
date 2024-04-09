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
        public List<int> TwrGidNumer { get; set; }
        public List<string> Nazwa { get; set; }
        public List<string> KodSystem { get; set; }
        public string KodDostawcy { get; set; }
        public string KodOem { get; set; }
        public string CenaZakupu { get; set; }
        public string Grupa6 { get; set; }
        public string Zastosowanie { get; set; }
        public List<string> Dostawca { get; set; }
        public List<decimal> OstatniaCenaZakupu { get; set; }
        public List<string> Waluta { get; set; }
        public int Wyszukiwania { get; set; }
        public string PolaczoneKody { get; set; }
        public string PolaczoneZastosowanie { get; set; }

        public Item()
        {
            KodSystem = new List<string>();
            Nazwa = new List<string>();
            Dostawca = new List<string>();
            TwrGidNumer = new List<int>();
            OstatniaCenaZakupu = new List<decimal>();
            Waluta = new List<string>();
        }
    }
}
