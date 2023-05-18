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
        //Declaring Recipes List of Recipe class to store recipes
        private List<Recipe> recipes;

        //Recipe constructor used for creating ingredients and steps lists
        public Recipe()
        {
            //Initializing ingredients list
            ingredients = new List<Ingredient>();
            //Initializing steps list
            steps = new List<string>();
            //Initializing list recipes of Recipe class
            recipes = new List<Recipe>();
        }

        //Method to add ingredient object to Ingredient class list
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

        //Method to add step to steps list
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

        //Method to add recipe to recipes list
        public void AddRecipe(Recipe recipe) 
        {
            recipes.Add(recipe);
        }

        //Method to display recipe
        public void DisplayRecipe(Program program)
        {
            //Changing foreground colour to blue for recipe display
            Console.ForegroundColor = ConsoleColor.Blue;
            //Display recipe opening message
            Console.WriteLine($"\nRecipe {program.recipeName} details");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Ingredients:");
            //Using loop to print ingredient name, quantity, and unit for each ingredient in ingredients list
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} \n\nCalories: {ingredient.Calories} \n\nFood group: {ingredient.FoodGroup}");
            }

            //Using loop to print each step from steps list
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < steps.Count; i++)
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

        //Method to get ingredients
        public List<Ingredient> GetIngredients()
        {
            return ingredients;
        }

        //Method to display list of recipes in alphabetical order by name
        public void DisplayRecipeList(Program program)
        {
            //Sorting recipes by list by recipeNmae in alphabetical order
            var sortedRecipes = recipes.OrderBy(r => r.program.recipeName);
            //Concatenating each recipeName and displaying it to the user
            Console.WriteLine(string.Join(", ", sortedRecipes.Select(r => r.program.recipeName)));


            /*
            //Foreach loop to iterate over sorted recipes
            foreach(var recipe in recipes)
                {
                //Changing foreground colour to blue for recipe display
                Console.ForegroundColor = ConsoleColor.Blue;
                //Display recipe opening message
                Console.WriteLine($"\nRecipe {program.recipeName}:");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("Ingredients:");
                //Using loop to print ingredient name, quantity, and unit for each ingredient in ingredients list
                foreach (var ingredient in ingredients)
                {
                    Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} \n\nCalories: {ingredient.Calories} \n\nFood group: {ingredient.FoodGroup}");
                }

                //Using loop to print each step from steps list
                Console.WriteLine("\nSteps:");
                for (int i = 0; i < steps.Count; i++)
                {
                    Console.WriteLine($"{(i + 1)}: {steps[i]}");
                }
                Console.WriteLine("----------------------------------------");
                //Changing foreground colour back to gray
                Console.ForegroundColor = ConsoleColor.Gray;
            }

            */
        }

        //Method to display specific recipe based on recipeName
        public void DisplaySpecificRecipe(Program program) 
        {
        
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//