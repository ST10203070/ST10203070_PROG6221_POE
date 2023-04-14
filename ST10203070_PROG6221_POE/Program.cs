using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10203070_PROG6221_POE
{
    class Program
    {
        //Main method
        static void Main(String[] args) 
        {
            //Creating object of Recipe class
            Recipe recipe = new Recipe();

            //Welcome message
            Console.WriteLine("Welcome to recipe manager!");
            //Request for recipe details
            Console.WriteLine("Please enter details for new recipe:");
            //Request for number of ingredients
            Console.WriteLine("Enter the number of ingredients for this recipe: ");
            //Saving number of ingredients in variable numIngredients
            int numIngredients = Convert.ToInt32(Console.ReadLine());

            //Loop to get and store details for each ingredient: Name, Quantity, Unit of measurement
            for (int i = 0; i < numIngredients; i++) 
            {
                //Asking for details of specific ingredient number
                Console.WriteLine("Enter details for ingredient " + (i +1));
                //Asking for ingredient name
                Console.WriteLine("Name: ");
                //Saving name in varibale name
                string name = Console.ReadLine();
                //Asking for ingredient quantity
                Console.WriteLine("Quantity: ");
                //Saving quantity in varibale quantity
                double quantity = Convert.ToDouble(Console.ReadLine()); 
                //Asking for unit of measurement
                Console.WriteLine("Unit of measurement: ");
                //Saving unit of measurement in varibale unit
                string unit = Console.ReadLine();

                //Creating new object of Ingredient class with current name, quantity, and unit
                Ingredient ingredient = new Ingredient(name, quantity, unit);
                //Saving ingredient object to ingredients array
                recipe.AddIngredient(ingredient);
            }


        }
    }
}