using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ST10203070_PROG6221_POE
{
    public class Program
    {
        //Variable holds number of ingredients
        public int numIngredients = 0;
        //Varible holds number of steps
        public int numSteps = 0;
        //Variable holds users response to clearing recipe (Yes/No)
        public string? clearConfirm = null;
        //Declaring instance of Recipe class
        public Recipe recipe;
        //Variable holds the recipeIDCounter
        static int recipeIDCounter = 1;
        //Declaring delegate specifying RecipeExceededCaloriesHandler method's signature. Delegate include recipeNmae and totalCalories as parameters
        public delegate void RecipeExceededCaloriesEventHandler(string recipeName, double totalCalories);        
        //Declaring event of delegate type
        public event RecipeExceededCaloriesEventHandler RecipeExceededCaloriesEvent;

        //Program class constructor
        public Program()
        {
            //Initializing recipe field with default
            recipe = new Recipe(recipeIDCounter, " ");
            //Subscribing to the RecipeExceededCaloriesEvent by providing the event handler method
            RecipeExceededCaloriesEvent += RecipeExceededCaloriesHandler;
        }

        //Main method
        static void Main(String[] args)
        {
            //Creating object instance of Program class
            Program prog = new Program();

            //Calling GetRecipeDetails method to get ingredients and steps from user and return recipe object to be passed as an argument to Actionsmenu method
            prog.ActionsMenu(prog.GetRecipeDetails());
        }
        public Recipe GetRecipeDetails() 
        {
            //Setting foreground colour to blue for welcome message
            Console.ForegroundColor = ConsoleColor.Blue;
            //Welcome message
            Console.WriteLine("Welcome to recipe manager!", Console.ForegroundColor);
            //Changing foreground colour to gray
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("================================================================================");
            //Request for recipe details
            Console.WriteLine("Please enter details for new recipe");
            //Saving recipe name in varibale recipeName
            string? recipeName = null;
            //Checking if recipe name entered is valid (not null)
            while (string.IsNullOrEmpty(recipeName))
            {
                //Getting name of recipe
                Console.WriteLine("Enter the name of the recipe: ");
                //Saving name in varibale recipeName
                recipeName = Console.ReadLine();

                if (string.IsNullOrEmpty(recipeName))
                {
                    Console.WriteLine("Invalid recipe name. Please enter a valid recipe name.");
                }
            }
            //Creating new recipe object
            Recipe recipe = new Recipe(recipeIDCounter, recipeName);
            //Incrementing recipeIDCounter so each recipe has a unqiue ID
            recipeIDCounter++;

            //Request for number of ingredients
            Console.WriteLine($"Enter the number of ingredients for '{recipeName}': ");
            //Saving number of ingredients in variable numIngredients
            this.numIngredients = Convert.ToInt32(Console.ReadLine());

            //Loop to get and store details for each ingredient: Name, Quantity, Unit of measurement
            for (int i = 0; i < this.numIngredients; i++)
            {
                //Asking for details of specific ingredient number
                Console.WriteLine("Enter details for ingredient " + (i +1));
                string? name = null;
                //Checking if ingredeint name entered is valid (not null)
                while (string.IsNullOrEmpty(name))
                {
                    //Asking for ingredient name
                    Console.WriteLine("Name: ");
                    //Saving name in varibale name
                    name = Console.ReadLine();

                    if (string.IsNullOrEmpty(name))
                    {
                        Console.WriteLine("Invalid ingredient name. Please enter a valid ingredient name.");
                    }
                }
                //Asking for ingredient quantity
                Console.WriteLine("Quantity: ");
                //Saving quantity in varibale quantity
                double quantity = Convert.ToDouble(Console.ReadLine());
                string? unit = null;
                //Checking if unit entered is valid (not null)
                while (string.IsNullOrEmpty(unit))
                {
                    //Asking for unit of measurement
                    Console.WriteLine("Unit of measurement: ");
                    //Saving unit of measurement in varibale unit
                    unit = Console.ReadLine();

                    if (string.IsNullOrEmpty(unit))
                    {
                        Console.WriteLine("Invalid unit of measurement. Please enter a valid unit.");
                    }
                }
                Console.WriteLine("Calories (amount of energy the food you've eaten releases in your body once digested): ");
                int calories = Convert.ToInt32(Console.ReadLine());
                int foodGroupNum = 0;
                string foodGroup = "";
                //Checking if food group number entered is valid
                while (foodGroupNum == 0)
                {
                    //Asking for food group from the given options
                    Console.WriteLine("Enter corresponding food group number from the following options (a food group is an assembly of foods sharing common nutritional properties): \n1. Starchy foods\n2. Vegetables and fruits\n3. Dry beans, peas, lentils and soya\n4. Chicken, fish, meat and eggs\n5. Milk and dairy products\n6. Fats and oil\n7. Water");
                    //Saving food group in varibale foodGroup
                    foodGroupNum = Convert.ToInt32(Console.ReadLine());
                    switch (foodGroupNum) 
                    {
                        case 1:
                            foodGroup = "Starchy foods";
                            break;
                        case 2:
                            foodGroup = "Vegetables and fruits";
                            break;
                        case 3:
                            foodGroup = "Dry beans, peas, lentils and soya";
                            break;
                        case 4:
                            foodGroup = "Chicken, fish, meat and eggs";
                            break;
                        case 5:
                            foodGroup = "Milk and dairy products";
                            break;
                        case 6:
                            foodGroup = "Fats and oil";
                            break;
                        case 7:
                            foodGroup = "Water";
                            break;
                    }

                    if (foodGroupNum == 0)
                    {
                        Console.WriteLine("Invalid food group number. Please enter a valid number corresponding to a food group.");
                    }
                }
                //Creating new object of Ingredient class to hold current name, quantity, unit, calories, and foodGroup
                Ingredient ingredient = new Ingredient(name, quantity, unit, calories, foodGroup);
                //Saving ingredient object to ingredients list
                recipe.AddIngredient(recipe, ingredient);
            }

            //Getting number of steps
            Console.WriteLine("Enter the number of steps: ");
            //Saving number of steps in varibale numSteps
            this.numSteps = Convert.ToInt32(Console.ReadLine());

            //Getting and saving description for each step to steps array based on numSteps the user wants to enter
            for (int i = 0; i < this.numSteps; i++)
            {
                string? description = null;
                //Checking if step description entered is valid (not null)
                while (string.IsNullOrEmpty(description))
                {
                    //Getting description for steps sequentially
                    Console.WriteLine("Enter description for step " + (i+1) + ": ");
                    //Saving description to variable description
                    description = Console.ReadLine();

                    if (string.IsNullOrEmpty(description))
                    {
                        Console.WriteLine("Invalid description. Please enter a valid description.");
                    }
                }
                //Adding description to steps lists
                recipe.AddStep(recipe, description);
            }

            //Calling AddRecipe method to add recipe to recipes list
            recipe.AddRecipe(recipe);
            //If statement checking if total calories is over 300 and raising RecipeExceededCaloriesEvent by invoking the event handler RecipeExceededCaloriesHandler
            if (recipe.CalculateTotalCalories(recipe) > 300)
                RecipeExceededCaloriesEvent?.Invoke(recipeName, recipe.CalculateTotalCalories(recipe));
            //Confirmation of added recipe statement
            Console.WriteLine($"\nRecipe '{recipeName}' added successfully");
            //Displaying the recipe added using DisplayRecipe method
            recipe.DisplayRecipe(recipe);
            Console.WriteLine("================================================================================");
            //Returning recipe object
            return recipe;
        }

        public void ActionsMenu(Recipe recipe) 
        {
            //Boolean variable to keep while loop running
            bool run = true;

            //While loop giving user furhter options such as scaling recipe, resetting quantities, clearing recipe, or exiting
            while (run)
            {
                Console.WriteLine("\nPlease select one of the following:");
                Console.WriteLine("1. Scale recipe");
                Console.WriteLine("2. Reset quantities");
                Console.WriteLine("3. Clear recipe");
                Console.WriteLine("4. Add recipe");
                Console.WriteLine("5. Display recipe list");
                Console.WriteLine("6. Exit");
                Console.WriteLine("Enter choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                //Switch statement used to act on users choice
                switch (choice)
                {
                    //Scale recipe
                    case 1:
                        //Getting scaling factor
                        Console.WriteLine("\nEnter scalling factor (0.5, 2, or 3): ");
                        //Saving scaling factor in variable scallingFactor
                        double scallingFactor = Convert.ToDouble(Console.ReadLine());
                        //Calling ScaleRecipe method with scallingFactor as argument
                        recipe.ScaleRecipe(scallingFactor, recipe);
                        //Displaying scaled recipe
                        recipe.DisplayRecipe(recipe);
                        break;
                    //Reset quantities
                    case 2:
                        //Calling ResetQuantities method 
                        recipe.ResetQuantities();
                        //Displaying recipe after quantities have been reset to default
                        recipe.DisplayRecipe(recipe);
                        break;
                    //Clear recipe
                    case 3:
                        //Checking if response entered is valid (not null)
                        while (string.IsNullOrEmpty(clearConfirm))
                        {
                            //Getting user confirmation for recipe clear
                            Console.WriteLine("Please confirm if you would like to clear the current recipe: Yes/No");
                            //Saving user response in clearConfirm variable
                            clearConfirm = Console.ReadLine();

                            if (string.IsNullOrEmpty(clearConfirm))
                            {
                                Console.WriteLine("Invalid response. Please enter a valid response (Yes/No).");
                            }
                        }
                        
                        //If statement to either clear recipe and add a new one or output recipe not cleared statement
                        if (clearConfirm.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
                        {
                            //Calling ClearRecipe method
                            recipe.ClearRecipe(recipe);
                            //Statement confriming recipe cleared
                            Console.WriteLine("\nRecipe cleared successfully\n");
                            //Calling GetRecipeDetails method to get ingredients and steps from user and return recipe object to be passed as an argument to Actionsmenu method
                            this.ActionsMenu(this.GetRecipeDetails());
                        }
                        else
                        {
                            Console.WriteLine("\nRecipe has NOT been cleared");
                        }
                        break;
                    //Add recipe
                    case 4:
                        //Calling GetRecipeDetails to allow user to add another recipe
                        GetRecipeDetails();
                        break;
                    //Display all recipes
                    case 5:
                        //Calling DisplayRecipeList method to display all recipes
                        recipe.DisplayRecipeList();
                        recipe.DisplaySpecificRecipe();
                        break;
                    //Exit application
                    case 6:
                        //Setting run variable to false to exit while loop
                        run = false;
                        //Setting foreground colour to blue for goodbye message
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\nGoodbye!");
                        //Setting foreground colour back to gray
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    //Default case for if the user enters an invalid choice
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }
                //If statement to break from loop if user decides to clear and enter new recipe
                if (choice == 3 && clearConfirm != null && clearConfirm.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
            }
        }

        //Event handler method to handle the event when a recipe exceeds 300 total calories
        private void RecipeExceededCaloriesHandler(string recipeName, double totalCalories) 
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine($"\nThe total calories of '{recipeName}' exceed 300 with a total of {totalCalories} calories.");
            Console.ForegroundColor= ConsoleColor.Gray;
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//