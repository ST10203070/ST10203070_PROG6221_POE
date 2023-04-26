using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10203070_PROG6221_POE
{
    class Ingredient
    {
        //Variables to hold name, quantity, original quantity, and unit
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double OriginalQuantity { get; set; }
        public string Unit { get; set; }

        //Ingredient constructor taking name, quantity, and unit as parameters 
        public Ingredient(string name, double quantity, string unit)
        {
            Name = name;
            Quantity = quantity;
            OriginalQuantity = quantity;
            Unit = unit;
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//