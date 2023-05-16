using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10203070_PROG6221_POE
{
    class Recipe
    {
        //Object of Program Class
        Program program = new Program();
        //Declaring list of Ingredients class to store ingredients
        private List<Ingredient> ingredients;
        //Declaring string list to store steps
        private List<string> steps;

        //Recipe constructor used for creating ingredients and steps lists
        public Recipe() 
        {
            //Creating ingredients list
            ingredients = new List<Ingredient>();
            //Creating steps list
            steps = new List<string>();
        }

        //Method to add ingredient object to Ingredient class array
        public void AddIngredient(Ingredient ingredient) 
        {
            /*
            //Resizing ingredients array to fit a new ingredient object
            Array.Resize(ref ingredients, ingredients.Length +1);
            //Storing ingredient object into last index of ingredients array
            ingredients[ingredients.Length - 1] = ingredient;
            */

            //Adding ingredient object to ingredients list
            ingredients.Add(ingredient);
        }

        //Method to add step to steps array
        public void AddStep(string step)
        {
            /*
            //Resizing steps array to fit a new step
            Array.Resize(ref steps, steps.Length +1);
            //Storing step into last index of steps array
            steps[steps.Length - 1] = step;
            */

            //Adding step to steps list
            steps.Add(step);
        }

        //Method to display recipe
        public void DisplayRecipe() 
        {
            //Changing foreground colour to blue for recipe display
            Console.ForegroundColor = ConsoleColor.Blue;
            //Display recipe opening message
            Console.WriteLine("\nRecipe details");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Ingredients:");
            //Using loop to print ingredient name, quantity, and unit for each ingredient in ingredients list
            foreach(var ingredient in ingredients) 
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}");
            }

            //Using loop to print each step from steps list
            Console.WriteLine("\nSteps:");
            for(int i = 0; i < steps.Count; i++) 
            {
                Console.WriteLine($"{(i + 1)}: {steps[i]}");
            }
            Console.WriteLine("----------------------------------------");
            //Changing foreground colour back to gray
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Method to scale recipe
        public void ScaleRecipe(double factor) 
        {
            //For loop to scale each ingredient quantity in ingredients list by scallingFactor
            for (int i = 0; i < ingredients.Count; i++) 
            {
                ingredients[i].Quantity *= factor;
            }
        }

        //Method to reset quantiies to default
        public void ResetQuantities()
        {
            //For loop to reset each ingredient quantity in ingredients list to original quantity
            for (int i = 0; i < ingredients.Count; i++)
            {
                ingredients[i].Quantity = ingredients[i].OriginalQuantity;
            }
        }

        //Method to clear recipe
        public void ClearRecipe()
        {
            /*
            //Setting numIngredients in Program class to zero
            program.numIngredients = 0;
            //Setting numSteps in Program class to zero
            program.numSteps = 0;
            //Setting ingredients array to null
            ingredients = null;
            //Setting steps array to null
            steps = null;
            */

            //Clearing ingredients list
            ingredients.Clear();
            //Clearing steps list
            steps.Clear();
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//