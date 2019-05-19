using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp
{
    class ShoppingList: MenuOfProduct
    {

        public int Quantity { get; set; }
        public double Profit { get; set; }

        public ShoppingList(int Id, string Type, string Name, double PricePLN, int Quantity, double Profit) : base(Id, Type, Name, PricePLN)
        {
            this.Id = Id;
            this.Type = Type;
            this.Name = Name;
            this.PricePLN = PricePLN;
            this.Quantity = Quantity;
            this.Profit = Profit;
        }
    }
}
