using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10203070_PROG6221_POE
{
    class Recipe
    {
        //Declaring list of Ingredients class to store ingredients
        private List<Ingredient> ingredients;
        //Declaring string list to store steps
        private List<string> steps;
        //Declaring Recipes List of Recipe class to store recipes
        private List<Recipe> recipes;
        public int RecipeID { get; private set; }
        public string RecipeName { get; set; }

        //Recipe constructor
        public Recipe(int recipeId, string recipeName)
        {
            //Initializing ingredients list
            ingredients = new List<Ingredient>();
            //Initializing steps list
            steps = new List<string>();
            //Initializing list recipes of Recipe class
            recipes = new List<Recipe>();
            RecipeID = recipeId;
            RecipeName = recipeName;
        }

        //Method to add ingredient object to Ingredient class list
        public void AddIngredient(Recipe recipe, Ingredient ingredient)
        {
            //Adding ingredient object to ingredients list
            recipe.ingredients.Add(ingredient);
        }

        //Method to add step to steps list
        public void AddStep(Recipe recipe, string step)
        {
            //Adding step to steps list
            recipe.steps.Add(step);
        }

        //Method to add recipe to recipes list
        public void AddRecipe(Recipe recipe) 
        {
            recipes.Add(recipe);
        }

        //Method to display recipe
        public void DisplayRecipe(Recipe recipe)
        {
            //Changing foreground colour to blue for recipe display
            Console.ForegroundColor = ConsoleColor.Blue;
            //Display recipe opening message
            Console.WriteLine($"\nRecipe {RecipeName} details");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Ingredients:");
            //Using loop to print ingredient name, quantity, unit, calories, and food group for each ingredient in ingredients list
            foreach (var ingredient in ingredients)
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} \n\nCalories: {ingredient.Calories} \n\nFood group: {ingredient.FoodGroup}");
            }
            //Displaying total calories
            Console.WriteLine($"\nTotal Calories: {CalculateTotalCalories(recipe)}");

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
            //For loop to scale each ingredient quantity in ingredients list by factor
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
            //Clearing ingredients list
            ingredients.Clear();
            //Clearing steps list
            steps.Clear();
        }

        //Method to display list of recipes in alphabetical order by name
        public void DisplayRecipeList()
        {
            //Sorting recipes by list by recipeNmae in alphabetical order
            List<Recipe> sortedRecipes = recipes.OrderBy(recipe => recipe.RecipeName).ToList();
            //Concatenating each recipeName and displaying it to the user
            Console.WriteLine("\nRecipe list: ");
            for(int i = 0; i < sortedRecipes.Count; i++) 
            {
                Console.WriteLine($"{i+1}. {sortedRecipes[i].RecipeName}");
            }
        }

        //Method to display specific recipe based on users choice
        public void DisplaySpecificRecipe() 
        {
            //Getting number corresponding to recipe name 
            Console.Write("Enter the recipe number: ");
            int recipeNumber;
            if (int.TryParse(Console.ReadLine(), out recipeNumber))
            {
                if (recipeNumber >= 1 && recipeNumber <= recipes.Count)
                {
                    Recipe recipe = recipes[recipeNumber - 1];
                    //Changing foreground colour to blue for recipe display
                    Console.ForegroundColor = ConsoleColor.Blue;
                    //Display recipe opening message
                    Console.WriteLine($"\nRecipe {recipe.RecipeName} details");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Ingredients:");
                    foreach (Ingredient ingredient in recipe.ingredients)
                    {
                        Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} \nCalories: {ingredient.Calories} \nFood group: {ingredient.FoodGroup}");
                    }
                    Console.WriteLine("\nSteps:");
                    for (int i = 0; i < recipe.steps.Count; i++)
                    {
                        Console.WriteLine($"{(i + 1)}: {steps[i]}");
                    }
                    Console.WriteLine("----------------------------------------");
                    //Changing foreground colour back to gray
                    Console.ForegroundColor = ConsoleColor.Gray;
                }
                else
                {
                    Console.WriteLine("Invalid recipe number. Please try again.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid recipe number.");
            }

        }

        //Method to calculate and return total calories
        public double CalculateTotalCalories(Recipe recipe)
        {
            int totalCalories = 0;
            //Foreach loop 
            foreach (var ingredient in ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//