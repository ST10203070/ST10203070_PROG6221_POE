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
        //Declaring instance of Program class
        public Program prog;
        //Declaring instance of Recipe class
        public Recipe recipe;
        //
        public delegate void RecipeExceededCaloriesEventHandler(string recipeName);
        //Declaring Recipes List of Recipe class to store recipes
        private List<Recipe> recipes;
        //
        private RecipeExceededCaloriesEventHandler notifyRecipeExceededCalories;

        //Program class constructor
        public Program()
        {
            recipes = new List<Recipe>();
            //
            notifyRecipeExceededCalories += RecipeExceededCaloriesHandler;
        }

        //Main method
        static void Main(String[] args)
        {
            //Creating object of Recipe class
            Recipe recipe = new Recipe();

            //Creating object instance of Program
            var prog = new Program();

            //Calling GetRecipeDetails method to get ingredients and steps from user
            prog.GetRecipeDetails();

            //Calling ActionsMenu method to get users next action (scaling recipe, resetting quantities, clearing recipe, or exiting)
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
            //Request for number of ingredients
            Console.WriteLine("Enter the number of ingredients for this recipe: ");
            //Saving number of ingredients in variable numIngredients
            prog.numIngredients = Convert.ToInt32(Console.ReadLine());

            //Loop to get and store details for each ingredient: Name, Quantity, Unit of measurement
            for (int i = 0; i < prog.numIngredients; i++)
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
                recipe.AddIngredient(ingredient);
            }

            //Getting number of steps
            Console.WriteLine("Enter the number of steps: ");
            //Saving number of steps in varibale numSteps
            prog.numSteps = Convert.ToInt32(Console.ReadLine());

            //Getting and saving description for each step to steps array based on numSteps the user wants to enter
            for (int i = 0; i < prog.numSteps; i++)
            {
                //Getting description for steps sequentially
                Console.WriteLine("Enter description for step " + (i+1) + ": ");
                //Saving description to variable description
                string description = Console.ReadLine();
                //Adding description to steps array
                recipe.AddStep(description);
            }
            //
            recipes.Add(recipe);
            //Confirmation of added recipe statement
            Console.WriteLine($"\nRecipe '{recipeName}' added successfully");
            //Displaying the recipe added using DisplayRecipe method
            recipe.DisplayRecipe();
            Console.WriteLine("================================================================================");
            //
            if (CalculateTotalCalories(recipe) > 300)
                notifyRecipeExceededCalories(recipeName);
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
                Console.WriteLine("4. Exit");
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
                        recipe.DisplayRecipe();
                        break;
                    //Reset quantities
                    case 2:
                        //Calling ResetQuantities method 
                        recipe.ResetQuantities();
                        //Displaying recipe after quantities have been reset to default
                        recipe.DisplayRecipe();
                        break;
                    //Clear recipe
                    case 3:
                        //Getting user confirmation for recipe clear
                        Console.WriteLine("Please confirm if you would like to clear the current recipe: Yes/No");
                        //Saving user response in clearConfirm variable
                        prog.clearConfirm = Console.ReadLine();
                        //If statement to either clear recipe and add a new one or output recipe not cleared statement
                        if (prog.clearConfirm.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
                        {
                            //Calling ClearRecipe method
                            recipe.ClearRecipe();
                            //Statement confriming recipe cleared
                            Console.WriteLine("\nRecipe cleared successfully\n");
                            //Calling GetRecipeDetails method to enter new recipe
                            prog.GetRecipeDetails();
                            //Calling ActionsMenu to give user further options after having entered new recipe
                            prog.ActionsMenu();
                        }
                        else
                        {
                            Console.WriteLine("\nRecipe has NOT been cleared");
                        }
                        break;
                    //Exit application
                    case 4:
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
                if (choice == 3 && prog.clearConfirm.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
                {
                    break;
                }
            }
        }

        //Method to calculate and return total calories
        private int CalculateTotalCalories(Recipe recipe) 
        {
            int totalCalories = 0;
            foreach (var ingredient in recipe.GetIngredients()) 
            {
                totalCalories += ingredient.Calories;
            }
            return totalCalories;   
        }

        //Method to notify user of exceeded calories in the recipe
        private void RecipeExceededCaloriesHandler(string recipeName) 
        {
            Console.ForegroundColor= ConsoleColor.Red;
            Console.WriteLine($"\nThe total calories of the recipe '{recipeName}' exceed 300");
            Console.ForegroundColor= ConsoleColor.Gray;
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//