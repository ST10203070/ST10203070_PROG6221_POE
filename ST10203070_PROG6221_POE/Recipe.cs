using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10203070_PROG6221_POE
{
    public class Recipe
    {
        //Declaring list of Ingredients class to store ingredients
        public List<Ingredient> ingredients;
        //Declaring string list to store steps
        private List<string> steps;
        //Initializing Recipes List of Recipe class to store recipes
        public static List<Recipe> recipes = new List<Recipe>();
        //Initializing dictionary to map recipe numbers to recipe objects
        private Dictionary<int, Recipe> recipeDictionary = new Dictionary<int, Recipe>();

        public int RecipeID { get; private set; }
        public string RecipeName { get; set; }

        //Recipe constructor
        public Recipe(int recipeId, string recipeName)
        {
            //Initializing ingredients list
            ingredients = new List<Ingredient>();
            //Initializing steps list
            steps = new List<string>();
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
            Console.WriteLine($"\nRecipe '{recipe.RecipeName}' details");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Ingredients:");
            //Using loop to print ingredient name, quantity, unit, calories, and food group for each ingredient in ingredients list
            foreach (var ingredient in recipe.ingredients)
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} \n\nCalories: {ingredient.Calories} \n\nFood group: {ingredient.FoodGroup}");
            }
            //Displaying total calories
            Console.WriteLine($"\nTotal Calories: {CalculateTotalCalories(recipe)}\nCalories are the amount of energy the food you've eaten releases in your body once digested");

            //Using loop to print each step from steps list
            Console.WriteLine("\nSteps:");
            for (int i = 0; i < recipe.steps.Count; i++)
            {
                Console.WriteLine($"{(i + 1)}: {steps[i]}");
            }
            Console.WriteLine("----------------------------------------");
            //Changing foreground colour back to gray
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        //Method to scale recipe
        public void ScaleRecipe(double factor, Recipe recipe)
        {
            //For loop to scale each ingredient quantity in ingredients list by factor
            foreach (Ingredient ingredient in recipe.ingredients)
            {
                ingredient.Quantity *= factor;
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
        public void ClearRecipe(Recipe recipe)
        {
            //Clearing ingredients list
            ingredients.Clear();
            //Clearing steps list
            steps.Clear();
            //Removing object 'recipe' from recipes list
            recipes.Remove(recipe);
        }

        //Method to display list of recipes in alphabetical order by name
        public void DisplayRecipeList()
        {
            //Changing foreground colour to red for recipe list
            Console.ForegroundColor= ConsoleColor.Red;
            //Sorting recipes by list by recipeNmae in alphabetical order
            List<Recipe> sortedRecipes = recipes.OrderBy(recipe => recipe.RecipeName).ToList();
            //Populate the recipe dictionary with the sorted recipes
            recipeDictionary.Clear();
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                recipeDictionary[i + 1] = sortedRecipes[i];
            }
            //Concatenating each recipeName and displaying it to the user
            Console.WriteLine("\nRecipe list: ");
            for(int i = 0; i < sortedRecipes.Count; i++) 
            {
                Console.WriteLine($"{i+1}. {sortedRecipes[i].RecipeName}");
            }
            //Changing foreground colour back to grey
            Console.ForegroundColor= ConsoleColor.Gray;
        }

        //Method to display specific recipe based on users choice
        public void DisplaySpecificRecipe() 
        {
            //Getting number corresponding to recipe name 
            Console.WriteLine("Enter the number of the recipe to view: ");
            int recipeNumber;
            if (int.TryParse(Console.ReadLine(), out recipeNumber))
            {
                //Checking if the given recipe number exists in the recipeDictionary
                if (recipeDictionary.ContainsKey(recipeNumber))
                {
                    //Assigning recipe object associated with recipeNumber from the recipeDictionary to variable recipe
                    Recipe recipe = recipeDictionary[recipeNumber];
                    //Changing foreground colour to blue for recipe display
                    Console.ForegroundColor = ConsoleColor.Blue;
                    //Display recipe opening message
                    Console.WriteLine($"\nRecipe '{recipe.RecipeName}' details");
                    Console.WriteLine("----------------------------------------");
                    Console.WriteLine("Ingredients:");
                    foreach (Ingredient ingredient in recipe.ingredients)
                    {
                        Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit} \n\nCalories: {ingredient.Calories} \n\nFood group: {ingredient.FoodGroup}");
                    }
                    //Displaying total calories
                    Console.WriteLine($"\nTotal Calories: {CalculateTotalCalories(recipe)}\nCalories are the amount of energy the food you've eaten releases in your body once digested");
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
            //Foreach loop to add calories of each ingredient to totalCalories
            foreach (var ingredient in recipe.ingredients)
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//