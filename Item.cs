using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart
{
    internal class Item
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public bool IsChecked { get; set; }

        public Item(string name, int price, int quantity = 0, bool isChecked = false)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            IsChecked = isChecked;
        }
    }
}
