using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipePanel : MonoBehaviour
{
    #region Private Variables
    [SerializeField]
    private Text header;
    [SerializeField]
    private Text headerCount;
    [SerializeField]
    private Image stationIcon;
    [SerializeField]
    private Image foodIcon;
    [SerializeField]
    private GameObject scrollbar;
    [SerializeField]
    private GameObject usedAsPanel;
    [SerializeField]
    private GameObject previousButton;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject findUsesButton;
    [SerializeField]
    private RectTransform recipeItemContainer;

    [SerializeField]
    private RecipeItemPanel recipeItemPrefab;
    [SerializeField]
    private List<StationInfo> stations;

    private List<RecipeList.Recipe> currentRecipes;
    private List<StationInfo> currentStations;
    private int currentRecipeIndex;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        currentRecipes = new List<RecipeList.Recipe>();
        currentStations = new List<StationInfo>();
        InitialRecipeList();
    }
    #endregion

    #region Script-Specific Methods
    private void DisplayRecipe()
    {
        RecipeList.Recipe recipe = currentRecipes[currentRecipeIndex];
        StationInfo station = currentStations[currentRecipeIndex];

        //Set Up Header
        header.text = recipe.target.itemName;
        headerCount.gameObject.SetActive(true);
        headerCount.text = "x"+recipe.targetQuantity;
        stationIcon.gameObject.SetActive(true);
        stationIcon.sprite = station.icon;
        foodIcon.gameObject.SetActive(true);
        foodIcon.sprite = recipe.target.sprite;

        //Set Up Scrollbar
        scrollbar.SetActive(true);
        usedAsPanel.gameObject.SetActive(false);
        previousButton.SetActive(currentRecipeIndex > 0);
        nextButton.SetActive(currentRecipeIndex < currentRecipes.Count - 1);
        findUsesButton.SetActive(recipe.target is Ingredient);

        //Clear Old Food Items
        int childCount = recipeItemContainer.childCount;
        for (int i = 0; i < childCount; i++)
        {
            Destroy(recipeItemContainer.GetChild(0).gameObject);
        }
        recipeItemContainer.DetachChildren();

        //Add Ingredients
        foreach (RecipeList.Recipe.IngredientEntry ingredient in recipe.ingredients)
        {
            RecipeItemPanel newItem = Instantiate(recipeItemPrefab.gameObject, recipeItemContainer).GetComponent<RecipeItemPanel>();
            newItem.SetFoodType(ingredient.type);
            newItem.SetCount(ingredient.quantity);
            newItem.SetMaxCount(ingredient.quantityMax);
            newItem.owner = this;
        }
    }

    public void DisplayRecipeList(Ingredient candidateIngredient = null, bool mealsOnly = false)
    {
        //Set Up Header and Scrollbar
        if (candidateIngredient == null)
        {
            header.text = mealsOnly ? "Meals" : "All Combinations";
            foodIcon.gameObject.SetActive(false);
            usedAsPanel.gameObject.SetActive(false);
        }
        else
        {
            header.text = candidateIngredient.itemName + (mealsOnly ? " (Meals)" : "");
            foodIcon.gameObject.SetActive(true);
            foodIcon.sprite = candidateIngredient.sprite;
            usedAsPanel.gameObject.SetActive(true);
        }
        headerCount.gameObject.SetActive(false);
        stationIcon.gameObject.SetActive(false);
        scrollbar.SetActive(false);

        //Clear Old Food Items
        int childCount = recipeItemContainer.childCount;
        for (int i=0; i < childCount; i++)
        {
            Destroy(recipeItemContainer.GetChild(0).gameObject);
        }
        recipeItemContainer.DetachChildren();

        foreach (StationInfo station in stations)
        {
            foreach (RecipeList.Recipe recipe in station.recipeList.recipes)
            {
                //Skip if we already found one recipe that makes this item
                //Skip if candidate ingredient not found (never skips if no candidate specified)
                if (candidateIngredient != null && !recipe.Contains(candidateIngredient))
                    continue;
                //Skip if this is not a meal (never skips if mealsOnly is false or not specified)
                if (mealsOnly && !(recipe.target is MealItem))
                    continue;
                
                RecipeItemPanel newItem = Instantiate(recipeItemPrefab.gameObject, recipeItemContainer).GetComponent<RecipeItemPanel>();
                newItem.SetFoodType(recipe.target);
                newItem.SetCount(null);
                newItem.SetMaxCount(null);
                newItem.owner = this;
                
            }
        }
    }

    public void FindRecipes(FoodItem target)
    {
        //Reset current recipes
        currentRecipeIndex = 0;
        currentRecipes.Clear();
        currentStations.Clear();

        bool foundRecipe = false;
        foreach (StationInfo station in stations)
        {
            foreach (RecipeList.Recipe recipe in station.recipeList.recipes)
            {
                if (recipe.target == target)
                {
                    foundRecipe = true;
                    currentStations.Add(station);
                    currentRecipes.Add(recipe);
                }
            }
        }
        if (foundRecipe)
            DisplayRecipe();
        else
            FindUses(target);

    }

    public void FindUses(FoodItem target)
    {
        if (target != null && target is Ingredient)
            DisplayRecipeList((Ingredient)target, false);
    }

    public void FindUses()
    {
        FindUses(currentRecipes[currentRecipeIndex].target);
    }

    public void InitialRecipeList() => DisplayRecipeList(null, true);
    #endregion
}
