using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10203070_PROG6221_POE
{
    class Program
    {
        //Variable holds number of ingredients
        public int numIngredients = 0;
        //Varible holds number of steps
        public int numSteps = 0;
        //Variable holds users response to clearing recipe (Yes/No)
        public string clearConfirm = "";
        //Declaring instance of Recipe class
        public Recipe recipe;
        //Variable holds the recipeIDCounter
        int recipeIDCounter = 1;
        //Declaring delegate specifying RecipeExceededCaloriesHandler method's signature. Delegate include recipeNmae and totalCalories as parameters
        public delegate void RecipeExceededCaloriesEventHandler(string recipeName, double totalCalories);        
        //Declaring event of delegate type
        public event RecipeExceededCaloriesEventHandler RecipeExceededCaloriesEvent;

        //Program class constructor
        public Program()
        {
            //Subscribing to the RecipeExceededCaloriesEvent by providing the event handler method
            RecipeExceededCaloriesEvent += RecipeExceededCaloriesHandler;
        }

        //Main method
        static void Main(String[] args)
        {
            //Creating object instance of Program class
            Program prog = new Program();

            //Calling GetRecipeDetails method to get ingredients and steps from user
            prog.GetRecipeDetails();

            //Calling ActionsMenu method to get users next action after having entered the first recipe
            prog.ActionsMenu();
        }
        public void GetRecipeDetails() 
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
            //Getting name of recipe
            Console.WriteLine("Enter the name of the recipe: ");
            //Saving recipe name in varibale recipeName
            string recipeName = Console.ReadLine();
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
                Console.WriteLine("Calories: ");
                int calories = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Food group: ");
                string foodGroup = Console.ReadLine();

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
                //Getting description for steps sequentially
                Console.WriteLine("Enter description for step " + (i+1) + ": ");
                //Saving description to variable description
                string description = Console.ReadLine();
                //Adding description to steps lists
                recipe.AddStep(recipe, description);
            }

            //Calling AddRecipe method to add recipe to recipes list
            recipe.AddRecipe(recipe);
            //If statement checking if total calories is over 300 and raising RecipeExceededCaloriesEvent by invoking the event handler RecipeExceededCaloriesHandler
            if (recipe.CalculateTotalCalories(recipe) > 300)
                RecipeExceededCaloriesEvent?.Invoke(recipe.RecipeName, recipe.CalculateTotalCalories(recipe));
            //Confirmation of added recipe statement
            Console.WriteLine($"\nRecipe '{recipeName}' added successfully");
            //Displaying the recipe added using DisplayRecipe method
            recipe.DisplayRecipe(recipe);
            Console.WriteLine("================================================================================");
        }

        public void ActionsMenu() 
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
                        recipe.ScaleRecipe(scallingFactor);
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
                        //Getting user confirmation for recipe clear
                        Console.WriteLine("Please confirm if you would like to clear the current recipe: Yes/No");
                        //Saving user response in clearConfirm variable
                        this.clearConfirm = Console.ReadLine();
                        //If statement to either clear recipe and add a new one or output recipe not cleared statement
                        if (this.clearConfirm.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
                        {
                            //Calling ClearRecipe method
                            recipe.ClearRecipe();
                            //Statement confriming recipe cleared
                            Console.WriteLine("\nRecipe cleared successfully\n");
                            //Calling GetRecipeDetails method to enter new recipe
                            this.GetRecipeDetails();
                            //Calling ActionsMenu to give user further options after having entered new recipe
                            this.ActionsMenu();
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
                        //Setting run varible to false to exit while loop
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
                if (choice == 3 && this.clearConfirm.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
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