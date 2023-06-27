using ST10203070_PROG6221_POE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace st10203070_PROG6221_POE
{
    /// Interaction logic for MainWindow.xaml

    public partial class MainWindow : Window
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

        public MainWindow()
        {
            InitializeComponent();
            recipe = new Recipe(recipeIDCounter, "");
            RecipeExceededCaloriesEvent += RecipeExceededCaloriesHandler;
            ActionsMenu(GetRecipeDetails());
        }

        private Recipe GetRecipeDetails()
        {
            // Display a message box to welcome the user
            MessageBox.Show("Welcome to recipe manager!");

            // Request recipe name from the user using an input dialog
            string recipeName = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the recipe:", "Recipe Name");

            // Create a new instance of the Recipe class
            Recipe recipe = new Recipe(recipeIDCounter, recipeName);

            // Increment the recipeIDCounter
            recipeIDCounter++;

            // Request number of ingredients from the user using an input dialog
            string numIngredientsInput = Microsoft.VisualBasic.Interaction.InputBox($"Enter the number of ingredients for '{recipeName}':", "Number of Ingredients");
            this.numIngredients = int.Parse(numIngredientsInput);

            // Loop to get and store details for each ingredient
            for (int i = 0; i < this.numIngredients; i++)
            {
                // Create a new window for ingredient details
                var ingredientWindow = new Window()
                {
                    Title = $"Ingredient {i + 1} Details",
                    Width = 400,
                    Height = 400,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };

                // Create controls for ingredient details
                var nameLabel = new Label() { Content = "Name:" };
                var nameTextBox = new TextBox();

                var quantityLabel = new Label() { Content = "Quantity:" };
                var quantityTextBox = new TextBox();

                var unitLabel = new Label() { Content = "Unit of Measurement:" };
                var unitTextBox = new TextBox();

                var caloriesLabel = new Label() { Content = "Calories:" };
                var caloriesTextBox = new TextBox();

                var foodGroupLabel = new Label() { Content = "Food Group:" };
                var foodGroupComboBox = new ComboBox()
                {
                    ItemsSource = new[]
                    {
                    "Starchy foods",
                    "Vegetables and fruits",
                    "Dry beans, peas, lentils and soya",
                    "Chicken, fish, meat and eggs",
                    "Milk and dairy products",
                    "Fats and oil",
                    "Water"
                    }
                };

                var saveButton = new Button() { Content = "Save" };
                saveButton.Click += (sender, e) =>
                {
                    // Get the ingredient details from the input controls
                    string name = nameTextBox.Text;
                    double quantity = double.Parse(quantityTextBox.Text);
                    string unit = unitTextBox.Text;
                    int calories = int.Parse(caloriesTextBox.Text);
                    string foodGroup = foodGroupComboBox.SelectedItem.ToString();

                    // Create an Ingredient object and add it to the recipe
                    Ingredient ingredient = new Ingredient(name, quantity, unit, calories, foodGroup);
                    recipe.AddIngredient(recipe,ingredient);

                    // Close the ingredient window
                    ingredientWindow.Close();
                };

                // Create a stack panel to hold the controls
                var stackPanel = new StackPanel()
                {
                    Margin = new Thickness(20)
                };

                // Add the controls to the stack panel
                stackPanel.Children.Add(nameLabel);
                stackPanel.Children.Add(nameTextBox);
                stackPanel.Children.Add(quantityLabel);
                stackPanel.Children.Add(quantityTextBox);
                stackPanel.Children.Add(unitLabel);
                stackPanel.Children.Add(unitTextBox);
                stackPanel.Children.Add(caloriesLabel);
                stackPanel.Children.Add(caloriesTextBox);
                stackPanel.Children.Add(foodGroupLabel);
                stackPanel.Children.Add(foodGroupComboBox);
                stackPanel.Children.Add(saveButton);

                // Set the content of the ingredient window as the stack panel
                ingredientWindow.Content = stackPanel;

                // Show the ingredient window as a dialog
                ingredientWindow.ShowDialog();
            }

            // Request number of steps from the user using an input dialog
            string numStepsInput = Microsoft.VisualBasic.Interaction.InputBox("Enter the number of steps:", "Number of Steps");
            int numSteps = int.Parse(numStepsInput);

            // Loop to get and store descriptions for each step
            for (int i = 0; i < numSteps; i++)
            {
                string description = Microsoft.VisualBasic.Interaction.InputBox($"Enter description for step {i + 1}:", "Step Description");

                // Add the step description to the recipe
                recipe.AddStep(recipe,description);
            }

            // Call the AddRecipe method to add the recipe to the recipes list
            recipe.AddRecipe(recipe);

            // If the total calories exceed 300, raise the RecipeExceededCaloriesEvent
            if (recipe.CalculateTotalCalories(recipe) > 300)
            {
                RecipeExceededCaloriesEvent?.Invoke(recipeName, recipe.CalculateTotalCalories(recipe));
            }

            // Display a message box to confirm the added recipe
            MessageBox.Show($"Recipe '{recipeName}' added successfully");

            // Display the recipe using a message box
            MessageBox.Show(recipe.ToString());

            // Return the recipe object
            return recipe;
        }

        private void ActionsMenu(Recipe recipe)
        {
            var run = true;
            bool clearAndEnterNewRecipe = false;
            bool addNewRecipe = false;

            while (run)
            {
                // Create the controls for the actions menu
                var titleLabel = new Label() { Content = "Please select one of the following:" };

                var scaleButton = new Button() { Content = "Scale Recipe" };
                scaleButton.Click += (sender, e) =>
                {
                    // Create the scaling window
                    var scalingWindow = new Window()
                    {
                        Title = "Scale Recipe",
                        Width = 300,
                        Height = 200,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };

                    // Create the controls for scaling
                    var scalingLabel = new Label() { Content = "Enter scaling factor (0.5, 2, or 3):" };
                    var scalingTextBox = new TextBox();

                    var scaleButton = new Button() { Content = "Scale" };
                    scaleButton.Click += (innerSender, innerE) =>
                    {
                        // Handle scaling the recipe here...
                        if (double.TryParse(scalingTextBox.Text, out double scalingFactor))
                        {
                            // Call the method to scale the recipe using the scalingFactor
                            recipe.ScaleRecipe(scalingFactor, recipe);

                            // Display the scaled recipe
                            recipe.DisplayRecipe(recipe);

                            // Close the scaling window
                            scalingWindow.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid scaling factor. Please enter a valid number.");
                        }
                    };

                    // Add the controls to a stack panel
                    var scalingStackPanel = new StackPanel()
                    {
                        Margin = new Thickness(20)
                    };
                    scalingStackPanel.Children.Add(scalingLabel);
                    scalingStackPanel.Children.Add(scalingTextBox);
                    scalingStackPanel.Children.Add(scaleButton);

                    // Set the content of the scaling window
                    scalingWindow.Content = scalingStackPanel;

                    // Show the scaling window as a dialog
                    scalingWindow.ShowDialog();
                };

                var resetButton = new Button() { Content = "Reset Quantities" };
                resetButton.Click += (sender, e) =>
                {
                    // Create the scaling window
                    var resetWindow = new Window()
                    {
                        Title = "Reset Recipe Quantities",
                        Width = 300,
                        Height = 200,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };

                    var resetButton = new Button() { Content = "Reset quantities" };
                    resetButton.Click += (innerSender, innerE) =>
                    {
                        //Calling ResetQuantities method 
                        recipe.ResetQuantities();

                        // Display the scaled recipe
                        recipe.DisplayRecipe(recipe);

                        // Close the scaling window
                        resetWindow.Close();
                    };

                    // Add the controls to a stack panel
                    var resetStackPanel = new StackPanel()
                    {
                        Margin = new Thickness(20)
                    };
                    resetStackPanel.Children.Add(scaleButton);

                    // Set the content of the scaling window
                    resetWindow.Content = resetStackPanel;

                    // Show the scaling window as a dialog
                    resetWindow.ShowDialog();
                };

                var clearButton = new Button() { Content = "Clear Recipe" };
                clearButton.Click += (sender, e) =>
                {
                    // Create the confirmation window and controls
                    var confirmWindow = new Window()
                    {
                        Title = "Confirm Clear",
                        Width = 300,
                        Height = 200,
                        WindowStartupLocation = WindowStartupLocation.CenterOwner,
                        Owner = this // Set the owner window appropriately
                    };

                    var confirmTextBlock = new TextBlock()
                    {
                        Text = "Are you sure you want to clear the current recipe?",
                        Margin = new Thickness(20),
                        TextWrapping = TextWrapping.Wrap
                    };

                    var yesButton = new Button()
                    {
                        Content = "Yes",
                        Width = 80,
                        Margin = new Thickness(20),
                        HorizontalAlignment = HorizontalAlignment.Left
                    };
                    yesButton.Click += (confirmSender, confirmArgs) =>
                    {
                        // Set the clearAndEnterNewRecipe variable to true if confirmed
                        clearAndEnterNewRecipe = true;

                        // Call the ClearRecipe method
                        recipe.ClearRecipe(recipe);

                        // Display the statement confirming recipe clearance
                        MessageBox.Show("Recipe cleared successfully");

                        // Call the GetRecipeDetails method to get ingredients and steps from the user and return the new recipe object
                        var newRecipe = GetRecipeDetails();

                        // Close the confirmation window
                        confirmWindow.Close();

                        // Call the ActionsMenu method with the new recipe object as an argument to continue the process
                        ActionsMenu(newRecipe);
                    };

                    var noButton = new Button()
                    {
                        Content = "No",
                        Width = 80,
                        Margin = new Thickness(20),
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    noButton.Click += (confirmSender, confirmArgs) =>
                    {
                        confirmWindow.Close();
                    };

                    var buttonsStackPanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Margin = new Thickness(0, 20, 0, 0)
                    };
                    buttonsStackPanel.Children.Add(yesButton);
                    buttonsStackPanel.Children.Add(noButton);

                    var confirmStackPanel = new StackPanel()
                    {
                        Margin = new Thickness(10)
                    };
                    confirmStackPanel.Children.Add(confirmTextBlock);
                    confirmStackPanel.Children.Add(buttonsStackPanel);

                    confirmWindow.Content = confirmStackPanel;

                    confirmWindow.ShowDialog();
                };

                var addButton = new Button() { Content = "Add Recipe" };
                addButton.Click += (sender, e) =>
                {
                    // Create the confirmation window
                    MessageBoxResult result = MessageBox.Show("Are you sure you want to add a new recipe?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

                    // Check the user's response
                    if (result == MessageBoxResult.Yes)
                    {
                        // Call the GetRecipeDetails method to get ingredients and steps from the user and return the new recipe object
                        var newRecipe = GetRecipeDetails();

                        // Call the ActionsMenu method with the new recipe object as an argument to continue the process
                        ActionsMenu(newRecipe);
                    }
                };

                var displayAllButton = new Button() { Content = "Display all Recipes" };
                displayAllButton.Click += (sender, e) =>
                {
                    // Call the DisplayRecipeList method to populate the recipeListBox
                    recipe.DisplayRecipeList(recipeListBox);

                    // Create a ListBox control to display the recipe list
                    var listBox = new ListBox
                    {
                        Name = "recipeListBox",
                        Width = 400,
                        Height = 300
                    };

                    // Create a ScrollViewer to enable scrolling if needed
                    var scrollViewer = new ScrollViewer
                    {
                        Content = listBox,
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Auto,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Auto
                    };

                    // Create a Window to display the recipe list
                    var recipeListWindow = new Window
                    {
                        Title = "All Recipes",
                        Content = scrollViewer,
                        Width = 400,
                        Height = 300
                    };

                    // Show the recipeListWindow
                    recipeListWindow.ShowDialog();

                    // Prompt the user for the recipe number
                    string recipeNumberInput = Microsoft.VisualBasic.Interaction.InputBox("Enter the number of the recipe to view:", "Recipe Number");

                    // Parse the recipe number input
                    if (int.TryParse(recipeNumberInput, out int recipeNumber))
                    {
                        // Call the DisplaySpecificRecipe method with the chosen recipe number
                        recipe.DisplaySpecificRecipe(recipeNumber, recipeDetailsTextBlock);
                    }
                    else
                    {
                        // Handle the case where the recipe number input is invalid
                        recipeDetailsTextBlock.Text = "Invalid input. Please enter a valid recipe number.";
                    }
                };


                var filterButton = new Button() { Content = "Filter recipes by entering maximum calories" };
                filterButton.Click += (sender, e) =>
                {
                    // Prompt the user for the maximum calories
                    string maxCaloriesInput = Microsoft.VisualBasic.Interaction.InputBox("Enter the maximum calories:", "Maximum Calories");

                    // Parse the maximum calories input
                    if (int.TryParse(maxCaloriesInput, out int maxCalories))
                    {
                        // Filter recipes by maximum calories
                        List<Recipe> filteredRecipes = recipe.FilterRecipesByMaxCalories(maxCalories);

                        // Display the filtered recipes using a MessageBox or another UI element
                        StringBuilder sb = new StringBuilder();
                        sb.AppendLine($"Recipes with maximum calories of {maxCalories}:");
                        foreach (Recipe recipe in filteredRecipes)
                        {
                            sb.AppendLine(recipe.RecipeName);
                        }
                        MessageBox.Show(sb.ToString(), "Filtered Recipes", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        // Handle the case where the maximum calories input is invalid
                        MessageBox.Show("Invalid input. Please enter a valid number for maximum calories.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                };

                var menuButton = new Button() { Content = "Select recipes for menu" };
                menuButton.Click += (sender, e) =>
                {
                    // Create a list box or any other UI element to allow users to select recipes for the menu
                    ListBox recipeListBox = new ListBox();

                    // Populate the recipeListBox with available recipes

                    // Show the menu selection message box and wait for the user to make selections
                    MessageBoxResult result = MessageBox.Show("Select recipes for the menu", "Menu Selection", MessageBoxButton.OKCancel, MessageBoxImage.Information);

                    if (result == MessageBoxResult.OK)
                    {
                        // Get the selected recipes from the recipeListBox
                        List<Recipe> selectedRecipes = new List<Recipe>();
                        foreach (Recipe recipe in recipeListBox.SelectedItems)
                        {
                            selectedRecipes.Add(recipe);
                        }

                        // Calculate the total calories for the menu
                        double totalCalories = 0;
                        foreach (Recipe recipe in selectedRecipes)
                        {
                            totalCalories += recipe.CalculateTotalCalories(recipe);
                        }

                        // Calculate the food group percentages
                        Dictionary<string, double> foodGroupPercentages = recipe.CalculateFoodGroupPercentages(selectedRecipes);

                        // Create a new window to display the pie chart
                        Window pieChartWindow = new Window()
                        {
                            Title = "Menu Pie Chart",
                            Width = 400,
                            Height = 300,
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        };

                        // Create a Grid control to hold the pie chart
                        Grid pieChartGrid = new Grid();

                        // Calculate the angles for the pie slices
                        double startAngle = 0;
                        foreach (var kvp in foodGroupPercentages)
                        {
                            double sweepAngle = 360 * kvp.Value / 100;

                            // Create a pie slice using a PathGeometry
                            PathGeometry sliceGeometry = new PathGeometry();
                            PathFigure sliceFigure = new PathFigure();
                            sliceFigure.StartPoint = new Point(100, 100); // Center of the pie chart
                            sliceFigure.Segments.Add(new LineSegment(new Point(100, 0), isStroked: true));

                            // Calculate the end point for the arc segment
                            double endAngle = startAngle + sweepAngle;
                            double endX = 100 + Math.Sin(endAngle * Math.PI / 180) * 100;
                            double endY = 100 - Math.Cos(endAngle * Math.PI / 180) * 100;

                            // Create an arc segment
                            ArcSegment arcSegment = new ArcSegment(new Point(endX, endY), new Size(100, 100), 0, sweepAngle < 180, SweepDirection.Clockwise, true);
                            sliceFigure.Segments.Add(arcSegment);
                            sliceGeometry.Figures.Add(sliceFigure);

                            // Create a Path object to display the pie slice
                            Path slicePath = new Path()
                            {
                                Fill = new SolidColorBrush(GetRandomColor()), // Random color for each slice
                                Data = sliceGeometry
                            };

                            // Add the slice path to the pie chart grid
                            pieChartGrid.Children.Add(slicePath);

                            // Update the start angle for the next slice
                            startAngle += sweepAngle;
                        }

                        // Add the pie chart grid to the pie chart window's content
                        pieChartWindow.Content = pieChartGrid;

                        // Show the pie chart window
                        pieChartWindow.ShowDialog();
                    }
                };

                var exitButton = new Button() { Content = "Exit" };
                exitButton.Click += (sender, e) =>
                {
                    // Set the "run" variable to false to exit the application
                    run = false;
                };


                // Add the buttons to a stack panel
                var actionsStackPanel = new StackPanel()
                {
                    Margin = new Thickness(20)
                };
                actionsStackPanel.Children.Add(titleLabel);
                actionsStackPanel.Children.Add(scaleButton);
                actionsStackPanel.Children.Add(resetButton);
                actionsStackPanel.Children.Add(clearButton);
                actionsStackPanel.Children.Add(addButton);
                actionsStackPanel.Children.Add(displayAllButton);
                actionsStackPanel.Children.Add(filterButton);
                actionsStackPanel.Children.Add(menuButton);
                actionsStackPanel.Children.Add(exitButton);

                // Create the actions window
                var actionsWindow = new Window()
                {
                    Title = "Actions Menu",
                    Width = 400,
                    Height = 300,
                    WindowStartupLocation = WindowStartupLocation.CenterScreen,
                    Content = actionsStackPanel
                };

                // Show the actions window as a dialog
                actionsWindow.ShowDialog();

                //If statement to break from loop if user decides to clear and enter new recipe
                if (clearAndEnterNewRecipe)
                {
                    break;
                }

                //If statement to break from loop if user decides to add a new recipe
                if (addNewRecipe)
                {
                    break;
                }
            }
        }

        private void RecipeExceededCaloriesHandler(string recipeName, double totalCalories)
        {
            MessageBox.Show($"The total calories of '{recipeName}' exceed 300 with a total of {totalCalories} calories.", "Calorie Exceeded");
        }

        // Helper method to generate a random color
        private Color GetRandomColor()
        {
            Random random = new Random();
            byte[] colorBytes = new byte[3];
            random.NextBytes(colorBytes);
            return Color.FromArgb(255, colorBytes[0], colorBytes[1], colorBytes[2]);
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//