using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    internal class Note
    {
        public string Name { get; set; }
        public string TypeOf { get; set; }
        private int price { get; set; }
        public int Price
        {
            set
            {
                if (value < 0)
                {
                    price = value * -1;
                    Add = false;
                }
                else
                {
                    price = value;
                    Add = true;
                }
            }
            get
            {
                return price;
            }
        }
        public string Date;
        public bool Add { get; set; }
    }
}
