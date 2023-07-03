using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using OxyPlot;
using System.Windows.Controls;
using OxyPlot.Annotations;
using OxyPlot.Series;
using System.Runtime.CompilerServices;
using System.Text;
using SelectionMode = System.Windows.Controls.SelectionMode;
using OxyPlot.Wpf;

namespace st10203070_PROG6221_POE
{
    public partial class MainWindow : Window, INotifyPropertyChanged
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
        public event PropertyChangedEventHandler PropertyChanged;


        // Property for the chart model
        private PlotModel _chartModel;
        public PlotModel ChartModel
        {
            get { return _chartModel; }
            set
            {
                _chartModel = value;
                OnPropertyChanged(nameof(ChartModel));
            }
        }

        // Property for StarchyFoodsValues
        private List<DataPoint> _starchyFoodsValues;
        public List<DataPoint> StarchyFoodsValues
        {
            get { return _starchyFoodsValues; }
            set { SetProperty(ref _starchyFoodsValues, value); }
        }

        // Property for VegetablesFruitsValues
        private List<DataPoint> _vegetablesFruitsValues;
        public List<DataPoint> VegetablesFruitsValues
        {
            get { return _vegetablesFruitsValues; }
            set { SetProperty(ref _vegetablesFruitsValues, value); }
        }

        // Property for BeansPeasLentilsValues
        private List<DataPoint> _beansPeasLentilsValues;
        public List<DataPoint> BeansPeasLentilsValues
        {
            get { return _beansPeasLentilsValues; }
            set { SetProperty(ref _beansPeasLentilsValues, value); }
        }

        // Property for MeatEggsValues
        private List<DataPoint> _meatEggsValues;
        public List<DataPoint> MeatEggsValues
        {
            get { return _meatEggsValues; }
            set { SetProperty(ref _meatEggsValues, value); }
        }

        // Property for MilkDairyValues
        private List<DataPoint> _milkDairyValues;
        public List<DataPoint> MilkDairyValues
        {
            get { return _milkDairyValues; }
            set { SetProperty(ref _milkDairyValues, value); }
        }

        // Property for FatsOilValues
        private List<DataPoint> _fatsOilValues;
        public List<DataPoint> FatsOilValues
        {
            get { return _fatsOilValues; }
            set { SetProperty(ref _fatsOilValues, value); }
        }

        // Property for WaterValues
        private List<DataPoint> _waterValues;
        public List<DataPoint> WaterValues
        {
            get { return _waterValues; }
            set { SetProperty(ref _waterValues, value); }
        }

        // Method to set property value and invoke OnPropertyChanged
        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                field = value;
                OnPropertyChanged(propertyName);
            }
        }

        // Method to handle property changed event
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //Default constructor
        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            // Set the initial values for each food group
            StarchyFoodsValues = new List<DataPoint> { new DataPoint(0, 3) };
            VegetablesFruitsValues = new List<DataPoint> { new DataPoint(0, 3) };
            BeansPeasLentilsValues = new List<DataPoint> { new DataPoint(0, 3) };
            MeatEggsValues = new List<DataPoint> { new DataPoint(0, 3) };
            MilkDairyValues = new List<DataPoint> { new DataPoint(0, 3) };
            FatsOilValues = new List<DataPoint> { new DataPoint(0, 3) };
            WaterValues = new List<DataPoint> { new DataPoint(0, 3) };

            recipe = new Recipe(recipeIDCounter, "");
            RecipeExceededCaloriesEvent += RecipeExceededCaloriesHandler;
            ActionsMenu(GetRecipeDetails());
        }

        //Method to generate labels for data points
        public string PointLabel(Annotation annotation, LineSeries series, int index)
        {
            // Implement the logic for generating the label for each data point
            // You can customize the label format based on your requirements
            return $"{series.Title}: {series.Points[index].Y}";
        }

        //Method to get recipe details from the user
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

            // Add the recipe to the recipeDictionary
            recipe.recipeDictionary.Add(recipe.RecipeID, recipe);

            // If the total calories exceed 300, raise the RecipeExceededCaloriesEvent
            if (recipe.CalculateTotalCalories(recipe) > 300)
            {
                RecipeExceededCaloriesEvent?.Invoke(recipeName, recipe.CalculateTotalCalories(recipe));
            }

            // Display a message box to confirm the added recipe
            MessageBox.Show($"Recipe '{recipeName}' added successfully");

            // Display the recipe by calling DisplayRecipe method
            recipe.DisplayRecipe(recipe);

            // Return the recipe object
            return recipe;
        }

        private void ActionsMenu(Recipe recipe)
        {
            var run = true;
            bool clearAndEnterNewRecipe = false;
            bool addNewRecipe = false;
            // Declare the actionsWindow variable
            Window actionsWindow = null;

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
                    // Create the reset window
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
                    resetStackPanel.Children.Add(resetButton);

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
                        Owner = actionsWindow  // Set the owner window appropriately
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
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Left
                    };
                    yesButton.Click += (confirmSender, confirmArgs) =>
                    {
                        // Set the clearAndEnterNewRecipe variable to true if confirmed
                        clearAndEnterNewRecipe = true;

                        // Call the ClearRecipe method
                        recipe.ClearRecipe(recipe);

                        // Close the actions window
                        actionsWindow.Close();

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
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Right
                    };
                    noButton.Click += (confirmSender, confirmArgs) =>
                    {
                        confirmWindow.Close();
                    };

                    var buttonsStackPanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
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
                        // Close the actions window
                        actionsWindow.Close();

                        // Call the GetRecipeDetails method to get ingredients and steps from the user and return the new recipe object
                        var newRecipe = GetRecipeDetails();

                        // Call the ActionsMenu method with the new recipe object as an argument to continue the process
                        ActionsMenu(newRecipe);
                    }
                };

                var displayAllButton = new Button() { Content = "Display all Recipes" };
                displayAllButton.Click += (sender, e) =>
                {
                    // Create a ListBox control to display the recipe list
                    var listBox = new ListBox
                    {
                        Width = 400,
                        Height = 250
                    };

                    // Call the DisplayRecipeList method to populate the listBox
                    recipe.DisplayRecipeList(listBox);

                    // Create a TextBlock for the prompt
                    var promptTextBlock = new TextBlock
                    {
                        Text = "Enter the number of the recipe to view:",
                        Margin = new Thickness(0, 10, 0, 0)
                    };

                    // Create a TextBox for the user input
                    var recipeNumberTextBox = new TextBox
                    {
                        Width = 150,
                        Margin = new Thickness(0, 5, 0, 0)
                    };

                    // Create an "Enter" button
                    var enterButton = new Button
                    {
                        Content = "Enter",
                        Width = 80,
                        Margin = new Thickness(0, 10, 0, 0)
                    };

                    // Create a StackPanel to hold the prompt, TextBox, and Enter button
                    var stackPanel = new StackPanel
                    {
                        Orientation = Orientation.Vertical,
                        Margin = new Thickness(10)
                    };
                    stackPanel.Children.Add(promptTextBlock);
                    stackPanel.Children.Add(recipeNumberTextBox);
                    stackPanel.Children.Add(enterButton);

                    // Create a Grid to hold the listBox and stackPanel
                    var grid = new Grid();
                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                    grid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                    grid.Children.Add(listBox);
                    grid.Children.Add(stackPanel);
                    Grid.SetRow(listBox, 0);
                    Grid.SetRow(stackPanel, 1);

                    // Create a Window to display the recipe list
                    var recipeListWindow = new Window
                    {
                        Title = "All Recipes",
                        Content = grid,
                        Width = 400,
                        Height = 400
                    };

                    // Event handler for the Enter button click
                    enterButton.Click += (s, args) =>
                    {
                        // Get the recipe number input
                        string recipeNumberInput = recipeNumberTextBox.Text;

                        // Parse the recipe number input
                        if (int.TryParse(recipeNumberInput, out int recipeNumber) && recipe.recipeDictionary.ContainsKey(recipeNumber))
                        {
                            // Call the DisplaySpecificRecipe method with the chosen recipe number
                            recipe.DisplaySpecificRecipe(recipeNumber);
                        }
                        else
                        {
                            // Handle the case where the recipe number input is invalid
                            recipeDetailsTextBlock.Text = "Invalid input. Please enter a valid recipe number.";
                        }
                        recipeListWindow.Close();

                    };

                    // Show the recipeListWindow
                    recipeListWindow.ShowDialog();
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
                // Create a new list box for recipe selection
                ListBox recipeListBox = new ListBox()
                {
                    ItemsSource = Recipe.recipes,
                    SelectionMode = SelectionMode.Multiple
                };

                var menuButton = new Button() { Content = "Select recipes for menu" };
                menuButton.Click += (sender, e) =>
                {
                    // Create a new window for recipe selection
                    Window recipeSelectionWindow = new Window()
                    {
                        Title = "Recipe Selection",
                        Width = 400,
                        Height = 300,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };

                    // Create a stack panel to hold the checkboxes and confirm button
                    StackPanel stackPanel = new StackPanel();

                    // Create checkboxes for each recipe
                    foreach (Recipe recipe in Recipe.recipes)
                    {
                        CheckBox recipeCheckBox = new CheckBox()
                        {
                            Content = recipe.RecipeName
                        };

                        stackPanel.Children.Add(recipeCheckBox);
                    }

                    // Create a button to confirm the recipe selection
                    Button confirmButton = new Button
                    {
                        Content = "OK",
                        Width = 100,
                        Height = 30
                    };

                    // Handle the confirm button click event
                    confirmButton.Click += (confirmSender, confirmEventArgs) =>
                    {
                        // Get the selected recipes from the checkboxes
                        List<Recipe> selectedRecipes = new List<Recipe>();
                        foreach (var child in stackPanel.Children)
                        {
                            if (child is CheckBox checkBox && checkBox.IsChecked == true)
                            {
                                Recipe recipe = Recipe.recipes.FirstOrDefault(r => r.RecipeName == checkBox.Content.ToString());
                                if (recipe != null)
                                {
                                    selectedRecipes.Add(recipe);
                                }
                            }
                        }

                        // Calculate the food group percentages
                        Dictionary<string, double> foodGroupPercentages = Recipe.CalculateFoodGroupPercentages(selectedRecipes);

                        // Update the chart values
                        StarchyFoodsValues.Clear();
                        VegetablesFruitsValues.Clear();
                        BeansPeasLentilsValues.Clear();
                        MeatEggsValues.Clear();
                        MilkDairyValues.Clear();
                        FatsOilValues.Clear();
                        WaterValues.Clear();

                        foreach (var kvp in foodGroupPercentages)
                        {
                            string foodGroup = kvp.Key;
                            double percentage = kvp.Value;

                            switch (foodGroup)
                            {
                                case "Starchy foods":
                                    StarchyFoodsValues.Add(new DataPoint(0, percentage));
                                    break;
                                case "Vegetables and fruits":
                                    VegetablesFruitsValues.Add(new DataPoint(0, percentage));
                                    break;
                                case "Dry beans, peas, lentils and soya":
                                    BeansPeasLentilsValues.Add(new DataPoint(0, percentage));
                                    break;
                                case "Chicken, fish, meat and eggs":
                                    MeatEggsValues.Add(new DataPoint(0, percentage));
                                    break;
                                case "Milk and dairy products":
                                    MilkDairyValues.Add(new DataPoint(0, percentage));
                                    break;
                                case "Fats and oil":
                                    FatsOilValues.Add(new DataPoint(0, percentage));
                                    break;
                                case "Water":
                                    WaterValues.Add(new DataPoint(0, percentage));
                                    break;
                                default:
                                    break;
                            }
                        }

                        // Create and show the pie chart
                        var pieModel = new PlotModel { Title = "Food Group Percentages" };

                        var pieSeries = new PieSeries();

                        if (StarchyFoodsValues.Count > 0)
                            pieSeries.Slices.Add(new PieSlice("Starchy foods", StarchyFoodsValues[0].Y));
                        if (VegetablesFruitsValues.Count > 0)
                            pieSeries.Slices.Add(new PieSlice("Vegetables and fruits", VegetablesFruitsValues[0].Y));
                        if (BeansPeasLentilsValues.Count > 0)
                            pieSeries.Slices.Add(new PieSlice("Dry beans, peas, lentils and soya", BeansPeasLentilsValues[0].Y));
                        if (MeatEggsValues.Count > 0)
                            pieSeries.Slices.Add(new PieSlice("Chicken, fish, meat and eggs", MeatEggsValues[0].Y));
                        if (MilkDairyValues.Count > 0)
                            pieSeries.Slices.Add(new PieSlice("Milk and dairy products", MilkDairyValues[0].Y));
                        if (FatsOilValues.Count > 0)
                            pieSeries.Slices.Add(new PieSlice("Fats and oil", FatsOilValues[0].Y));
                        if (WaterValues.Count > 0)
                            pieSeries.Slices.Add(new PieSlice("Water", WaterValues[0].Y));

                        pieModel.Series.Add(pieSeries);

                        var pieChartWindow = new Window
                        {
                            Title = "Food Group Percentages",
                            Width = 550,
                            Height = 600,
                            Content = new PlotView { Model = pieModel },
                            WindowStartupLocation = WindowStartupLocation.CenterScreen
                        };

                        // Show the pie chart window
                        pieChartWindow.ShowDialog();

                        // Close the recipe selection window
                        recipeSelectionWindow.Close();
                    };

                    // Adding confirmButton to stackPanel
                    stackPanel.Children.Add(confirmButton);

                    recipeSelectionWindow.Content = stackPanel;

                    // Show the recipe selection window
                    recipeSelectionWindow.ShowDialog();
                };

                var exitButton = new Button() { Content = "Exit" };
                exitButton.Click += (sender, e) =>
                {
                    // Set the "run" variable to false to exit the application
                    run = false;
                    // Close the actions window
                    actionsWindow.Close();

                    // Terminate the application
                    Application.Current.Shutdown();
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
                actionsWindow = new Window()
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

        //Method to handle exceeded calories
        private void RecipeExceededCaloriesHandler(string recipeName, double totalCalories)
        {
            MessageBox.Show($"The total calories of '{recipeName}' exceed 300 with a total of {totalCalories} calories.", "Calorie Exceeded");
        }

        //Method to return food group colour
        private Color GetFoodGroupColor(string foodGroup)
        {
            switch (foodGroup)
            {
                case "Starchy foods":
                    return Colors.Green;
                case "Vegetables and fruits":
                    return Colors.Red;
                case "Dry beans, peas, lentils and soya":
                    return Colors.Yellow;
                case "Chicken, fish, meat and eggs":
                    return Colors.Blue;
                case "Milk and dairy products":
                    return Colors.Orange;
                case "Fats and oil":
                    return Colors.Gray;
                case "Water":
                    return Colors.Pink;
                default:
                    return Colors.Gray;
            }
        }
    }
}
//---------------------------------------END OF FILE---------------------------------------//
