﻿using System;
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
        //Declaring array of Ingredients class to store ingredients
        private Ingredient[] ingredients;
        //Declaring string array to store steps
        private string[] steps;

        //Recipe constructor used for creating ingredients and steps arrays
        public Recipe() 
        {
            //Creating ingredients array
            ingredients = new Ingredient[0];
            //Creating steps array
            steps = new string[0];
        }

        //Method to add ingredient object to Ingredient class array
        public void AddIngredient(Ingredient ingredient) 
        {
            //Resizing ingredients array to fit a new ingredient object
            Array.Resize(ref ingredients, ingredients.Length +1);
            //Storing ingredient object into last index of ingredients array
            ingredients[ingredients.Length - 1] = ingredient;
        }

        //Method to add step to steps array array
        public void AddStep(string step)
        {
            //Resizing steps array to fit a new step
            Array.Resize(ref steps, steps.Length +1);
            //Storing step into last index of steps array
            steps[steps.Length - 1] = step;
        }

        //Method to display recipe
        public void DisplayRecipe() 
        {
            //Display recipe opening message
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Recipe details:");
            Console.WriteLine("Ingredients:");
            //Printing ingredient name, quantity, and unit for each ingredient in ingredients array in loop
            foreach(var ingredient in ingredients) 
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}");
            }

            //Printing each step from steps array in for loop
            Console.WriteLine("\nSteps:");
            for(int i = 0; i < steps.Length; i++) 
            {
                Console.WriteLine($"{steps[i]}");
            }
            Console.WriteLine("----------------------------------------");
        }

        //Method to scale recipe
        public void ScaleRecipe(double factor) 
        {
            //For loop to scale each ingredient quantity in ingredients array by scallingFactor
            for (int i = 0; i < program.numIngredients; i++) 
            {
                ingredients[i].Quantity *= factor;
            }
        }

        //Method to reset quantiies to default
        public void ResetQuantities()
        {
            //For loop to resetl each ingredient quantity in ingredients array to original quantity
            for (int i = 0; i < program.numIngredients; i++)
            {
                ingredients[i].Quantity = ingredients[i].OriginalQuantity;
            }
        }

        //Method to clear recipe
        public void ClearRecipe()
        {
            //Setting numIngredients in Program class to zero
            program.numIngredients = 0;
            //Setting numSteps in Program class to zero
            program.numSteps = 0;
            //Setting ingredients array to null
            ingredients = null;
            //Setting steps array to null
            steps = null;
        }
    }
}
