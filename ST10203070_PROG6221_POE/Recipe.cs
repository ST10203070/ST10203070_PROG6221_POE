using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10203070_PROG6221_POE
{
    class Recipe
    {
        //Declaring array of Ingredients class to store ingredients
        private Ingredient[] ingredients;
        //Declaring string array to store steps
        private string[] steps;

        //Recipe constructor used for creating ingredients and steps arrays
        public Recipe() 
        {
            //Creating ingredients array
            ingredients = new Ingredient[0];
            //Creating steps array
            steps = new string[0];
        }

        //Method to add ingredient object to Ingredient class array
        public void AddIngredient(Ingredient ingredient) 
        {
            //Resizing ingredients array to fit a new ingredient object
            Array.Resize(ref ingredients, ingredients.Length +1);
            //Storing ingredient object into last index of ingredients array
            ingredients[ingredients.Length - 1] = ingredient;
        }

    }
}
