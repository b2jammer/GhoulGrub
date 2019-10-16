using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Ghoul Grub/Recipe List")]
public class RecipeList : ScriptableObject
{
    [System.Serializable]
    public class Recipe
    {
        [System.Serializable]
        public struct IngredientEntry
        {
            public Ingredient type;
            public int quantity;
        }
        public FoodItem target;
        public int targetQuantity;
        public IngredientEntry[] ingredients;
    }
    public string listTitle;
    public Sprite listIcon;
    [System.Serializable]
    public class RecipeEvent : UnityEvent<RecipeList.Recipe> { }

    public Recipe[] recipes;

    public static Recipe FindMatchingRecipe(Dictionary<FoodItem, int> itemList, RecipeList recipeList)
    {
        foreach (Recipe testRecipe in recipeList.recipes)
        {
            if (CompareItemsToRecipe(itemList, testRecipe))
            {
                return testRecipe;
            }
        }
        return null;
    }

    private static bool CompareItemsToRecipe(Dictionary<FoodItem, int> itemList, Recipe testRecipe)
    {
        Dictionary<FoodItem, int> recipeIngredients = compileRecipeIngredients(testRecipe);
        //If lengths do not match, throw out (takes care of excess types in inventory)
        if (recipeIngredients.Count != itemList.Count)
        {
            return false;
        }
        foreach (KeyValuePair<FoodItem, int> entry in recipeIngredients)
        {
            //If inventory is missing ingredient or if the counts differ, not a match.
            if (!itemList.ContainsKey(entry.Key) || itemList[entry.Key] != entry.Value)
            {
                return false;
            }
        }
        return true;
    }

    private static Dictionary<FoodItem, int> compileRecipeIngredients(Recipe testRecipe)
    {
        Dictionary<FoodItem, int> ingredientList = new Dictionary<FoodItem, int>();
        foreach (Recipe.IngredientEntry entry in testRecipe.ingredients)
        {
            if (!ingredientList.ContainsKey(entry.type))
            {
                ingredientList.Add(entry.type, entry.quantity);
            }
            else
            {
                ingredientList[entry.type] += entry.quantity;
            }
        }
        return ingredientList;
    }
}
