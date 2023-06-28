using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public Dictionary<int, Recipe> recipeDictionary = new Dictionary<int, Recipe>();
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
            // Create a new window for displaying the recipe details
            var recipeWindow = new Window()
            {
                Title = $"Recipe '{recipe.RecipeName}' details",
                Width = 400,
                Height = 550,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Create a text block to hold the recipe details
            var recipeTextBlock = new TextBlock()
            {
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(20)
            };

            // Construct the recipe details string
            var recipeDetails = $"Recipe '{recipe.RecipeName}' details\n";
            recipeDetails += "----------------------------------------\n";
            recipeDetails += "Ingredients:\n";

            // Iterate over ingredients and append their details to the string
            foreach (var ingredient in recipe.ingredients)
            {
                recipeDetails += $"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}\n";
                recipeDetails += $"Calories: {ingredient.Calories}\n";
                recipeDetails += $"Food group: {ingredient.FoodGroup}\n\n";
            }

            // Append total calories to the string
            recipeDetails += $"Total Calories: {CalculateTotalCalories(recipe)}\n";
            recipeDetails += "Calories are the amount of energy the food you've eaten releases in your body once digested\n\n";

            // Append steps to the string
            recipeDetails += "Steps:\n";
            for (int i = 0; i < recipe.steps.Count; i++)
            {
                recipeDetails += $"{(i + 1)}: {recipe.steps[i]}\n";
            }

            recipeDetails += "----------------------------------------";

            // Set the text block's text to the recipe details
            recipeTextBlock.Text = recipeDetails;

            // Create an OK button
            var okButton = new Button()
            {
                Content = "OK",
                Width = 80,
                Height = 30,
                Margin = new Thickness(20, 0, 0, 20),
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Bottom
            };

            // Set the click event handler for the OK button
            okButton.Click += (sender, e) =>
            {
                // Close the recipe window when the OK button is clicked
                recipeWindow.Close();
            };

            // Create a stack panel to hold the text block and the OK button
            var stackPanel = new StackPanel();
            stackPanel.Children.Add(recipeTextBlock);
            stackPanel.Children.Add(okButton);

            // Set the content of the recipe window as the stack panel
            recipeWindow.Content = stackPanel;

            // Show the recipe window
            recipeWindow.ShowDialog();
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

        // Method to display specific recipe based on user's choice
        public void DisplaySpecificRecipe(int recipeNumber)
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

                // Create a new Window to display the recipe details
                var recipeDetailsWindow = new Window
                {
                    Title = "Recipe Details",
                    Width = 400,
                    Height = 550,
                    Content = new StackPanel
                    {
                        Children =
                        {
                        new TextBlock { Text = sb.ToString() },
                        new Button
                        {
                            Content = "OK",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Margin = new Thickness(0, 10, 0, 0)
                        }
                        }
                    }
                };

                // Get the "OK" button from the StackPanel's Children collection
                Button okButton = (Button)((StackPanel)recipeDetailsWindow.Content).Children[1];

                // Set the click event handler for the "OK" button
                okButton.Click += (sender, e) =>
                {
                    // Close the recipeDetailsWindow when the button is clicked
                    recipeDetailsWindow.Close();
                };

                // Show the recipeDetailsWindow
                recipeDetailsWindow.ShowDialog();
            }
            else
            {
                // Handle the case where the recipe number is invalid
                MessageBox.Show("Invalid recipe number. Please try again.");
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