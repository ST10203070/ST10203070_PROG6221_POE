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
        //
        public string clearConfirm = "";

        //Program class constructor
        public Program()
        {

        }

        //Main method
        static void Main(String[] args)
        {
            //Creating object of Recipe class
            Recipe recipe = new Recipe();
            //Creating object instance of Program
            var prog = new Program();

            //Setting foreground colour to blue for welcome message
            Console.ForegroundColor = ConsoleColor.Blue;
            //Welcome message
            Console.WriteLine("Welcome to recipe manager!", Console.ForegroundColor);
            //Changing foreground colour to gray
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("================================================================================");
            //Request for recipe details
            Console.WriteLine("Please enter details for new recipe");
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

                //Creating new object of Ingredient class to hold current name, quantity, and unit
                Ingredient ingredient = new Ingredient(name, quantity, unit);
                //Saving ingredient object to ingredients array
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

            //Confirmation of added recipe statement
            Console.WriteLine("\nRecipe added successfully");
            //Displaying the recipe added using DisplayRecipe method
            recipe.DisplayRecipe();
            Console.WriteLine("================================================================================");

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
                        prog.clearConfirm = Console.ReadLine();
                        if (prog.clearConfirm.Equals("Yes", StringComparison.InvariantCultureIgnoreCase))
                        {
                            //Calling ClearRecipe method
                            recipe.ClearRecipe();
                            //Statement confriming recipe cleared
                            Console.WriteLine("\nRecipe cleared successfully\n");
                            //Calling main method to enter new recipe
                            Program.Main(args);
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
    }
}