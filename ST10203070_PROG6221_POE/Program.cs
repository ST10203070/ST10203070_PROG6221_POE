﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ST10203070_PROG6221_POE
{
    class Program
    {
        public int numIngredients = 0;
        public int numSteps = 0;

        //Program constructor
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

            //Welcome message
            Console.WriteLine("Welcome to recipe manager!");
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
            for(int i = 0; i < prog.numSteps; i++) 
            {
                //Getting description for steps sequentially
                Console.WriteLine("Enter description for step " + (i+1) + ": ");
                //Saving description to variable description
                string description  = Console.ReadLine();   
                //Adding description to steps array
                recipe.AddStep(description);
            }

            //Confirmation of added recipe statement
            Console.WriteLine("\nRecipe added successfully");
            //Displaying the recipe added using DisplayRecipe method
            recipe.DisplayRecipe();

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
                switch(choice) 
                {
                    //Scale recipe
                    case 1:
                        //Getting scaling factor
                        Console.WriteLine("Enter scalling factor (0.5, 2, or 3): ");
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
                        //Calling ClearRecipe method
                        recipe.ClearRecipe();
                        //Statement confriming recipe cleared
                        Console.WriteLine("Recipe cleared successfully");
                        break;
                    //Exit application
                    case 4:
                        //Setting run varible to false to exit while loop
                        run = false;
                        break;
                    //Default case for if the user enters an invalid choice
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}