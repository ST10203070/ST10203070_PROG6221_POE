using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ST10203070_PROG6221_POE;

namespace ST10203070_PROG6221_POE.Tests
{
    [TestClass]
    public class CalorieTest
    {
        //Test method checking total calories for 3 ingredients
        [TestMethod]
        public void CalculateTotalCalories_ShouldReturnCorrectTotalCalories()
        {
            //Creating recipe object
            Recipe recipe = new Recipe(1, "Recipe 1");
            //Adding 3 Ingredients to ingredients list
            recipe.ingredients = new List<Ingredient>
            {
                new Ingredient("Ingredient 1", 100, "g", 100, "Starchy foods"),
                new Ingredient("Ingredient 2", 200, "g", 200, "Vegetables and fruits"),
                new Ingredient("Ingredient 3", 300, "g", 300, "Dry beans, peas, lentils and soya")
            };
            //Excpected total calories 
            double expectedTotalCalories = 600;

            //Assigning calorie return from CalculateTotalCalories method to actualTotalCalories
            double actualTotalCalories = recipe.CalculateTotalCalories(recipe);

            // Asserting expected and actual
            Assert.AreEqual(expectedTotalCalories, actualTotalCalories);
        }

        //Test method checking for empty recipe, expected is zero
        [TestMethod]
        public void CalculateTotalCalories_ShouldReturnZeroForEmptyRecipe()
        {
            //Creating recipe object
            Recipe recipe = new Recipe(2, "Recipe 2");

            // Empty ingredient list
            recipe.ingredients = new List<Ingredient>();

            //Expected total calories as 0
            double expectedTotalCalories = 0;

            //Assigning calorie return from CalculateTotalCalories method to actualTotalCalories
            double actualTotalCalories = recipe.CalculateTotalCalories(recipe);

            //Asserting expected and actual
            Assert.AreEqual(expectedTotalCalories, actualTotalCalories);
        }
    }
}