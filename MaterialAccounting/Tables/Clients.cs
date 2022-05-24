using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialAccounting.Tables
{
    class Clients
    {
        public int ID { get;}
        public string Name { get; set; }
        public string Count { get; set; }
        public string generalPrice { get; set; }
        public string Category { get; set; }
        public Clients(int id, string stName, string stCount, string stGeneralPrice, string stCategory)
        {
            ID = id;
            Name = stName;
            Count = stCount;
            generalPrice = stGeneralPrice;
            Category = stCategory;
        }
    }
}
