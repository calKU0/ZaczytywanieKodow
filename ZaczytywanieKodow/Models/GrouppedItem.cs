using System.Collections.Generic;

namespace ZaczytywanieKodow.Models
{
    public class GrouppedItem
    {
        public int Id { get; set; }
        public List<int> TwrGidNumer { get; set; }
        public List<string> KodSystem { get; set; }
        public string KodDostawcy { get; set; }
        public List<string> Dostawca { get; set; }
        public int Wyszukiwania { get; set; }
        public string PolaczoneKody { get; set; }
        public bool WieleKodow { get; set; }

        public GrouppedItem()
        {
            KodSystem = new List<string>();
            Dostawca = new List<string>();
            TwrGidNumer = new List<int>();
        }
    }
}
