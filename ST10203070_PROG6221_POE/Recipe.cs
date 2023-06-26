using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
        //Declaring variable to hold recipe ID for each recipe
        public int RecipeID { get; private set; }
        //Declaring variable to hold recipe name for each recipe
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
        public void DisplayRecipeList(ListBox recipeListBox)
        {
            // Sorting recipes by recipeName in alphabetical order
            List<Recipe> sortedRecipes = recipes.OrderBy(recipe => recipe.RecipeName).ToList();

            // Clear the recipeListBox to ensure a fresh display
            recipeListBox.Items.Clear();

            // Populate the recipeListBox with the sorted recipe names
            for (int i = 0; i < sortedRecipes.Count; i++)
            {
                recipeListBox.Items.Add($"{i + 1}. {sortedRecipes[i].RecipeName}");
            }
        }

        //Method to display specific recipe based on users choice
        public void DisplaySpecificRecipe(int recipeNumber, TextBlock recipeDetailsTextBlock)
        {
            if (recipeDictionary.ContainsKey(recipeNumber))
            {
                Recipe recipe = recipeDictionary[recipeNumber];
                StringBuilder sb = new StringBuilder();

                // Append recipe details to the StringBuilder
                sb.AppendLine($"Recipe '{recipe.RecipeName}' details");
                sb.AppendLine("----------------------------------------");
                sb.AppendLine("Ingredients:");
                foreach (Ingredient ingredient in recipe.ingredients)
                {
                    sb.AppendLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}");
                    sb.AppendLine($"Calories: {ingredient.Calories}");
                    sb.AppendLine($"Food group: {ingredient.FoodGroup}");
                }
                sb.AppendLine($"\nTotal Calories: {CalculateTotalCalories(recipe)}");
                sb.AppendLine("Calories are the amount of energy the food you've eaten releases in your body once digested");
                sb.AppendLine("Steps:");
                for (int i = 0; i < recipe.steps.Count; i++)
                {
                    sb.AppendLine($"{(i + 1)}: {recipe.steps[i]}");
                }
                sb.AppendLine("----------------------------------------");

                // Set the content of the recipeDetailsTextBlock with the recipe details
                recipeDetailsTextBlock.Text = sb.ToString();
            }
            else
            {
                // Handle the case where the recipe number is invalid
                recipeDetailsTextBlock.Text = "Invalid recipe number. Please try again.";
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

        // Method to filter recipes by maximum calories
        public List<Recipe> FilterRecipesByMaxCalories(int maxCalories)
        {
            List<Recipe> filteredRecipes = new List<Recipe>();

            foreach (Recipe recipe in recipes)
            {
                double totalCalories = CalculateTotalCalories(recipe);
                if (totalCalories <= maxCalories)
                {
                    filteredRecipes.Add(recipe);
                }
            }

            return filteredRecipes;
        }

        // Method to calculate the food group percentages for the selected recipes
        public Dictionary<string, double> CalculateFoodGroupPercentages(List<Recipe> recipes)
        {
            Dictionary<string, double> foodGroupPercentages = new Dictionary<string, double>();

            // Calculate the total quantity for each food group
            Dictionary<string, double> totalQuantities = new Dictionary<string, double>();
            foreach (Recipe recipe in recipes)
            {
                foreach (Ingredient ingredient in recipe.ingredients)
                {
                    if (!totalQuantities.ContainsKey(ingredient.FoodGroup))
                    {
                        totalQuantities[ingredient.FoodGroup] = 0;
                    }
                    totalQuantities[ingredient.FoodGroup] += ingredient.Quantity;
                }
            }

            // Calculate the food group percentages
            foreach (string foodGroup in totalQuantities.Keys)
            {
                double percentage = (totalQuantities[foodGroup] / totalQuantities.Values.Sum()) * 100;
                foodGroupPercentages[foodGroup] = percentage;
            }

            return foodGroupPercentages;
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//