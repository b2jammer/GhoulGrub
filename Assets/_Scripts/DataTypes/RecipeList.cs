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
            [Tooltip("Minimum amount of this ingredient per batch.\nIf 0, ingredient is optional.")]
            public int quantity;
            [Tooltip("Maximum amount of this ingredient per batch.")]
            public int quantityMax;
        }
        public FoodItem target;
        public int targetQuantity;
        public IngredientEntry[] ingredients;
        public bool Contains(Ingredient ingredient)
        {
            foreach (IngredientEntry entry in ingredients)
            {
                if (entry.type == ingredient)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public string listTitle;

    public Recipe[] recipes;

    public static Recipe FindMatchingRecipe(Dictionary<FoodItem, int> itemList, RecipeList recipeList, out int numBatches)
    {
        foreach (Recipe testRecipe in recipeList.recipes)
        {
            if (CompareItemsToRecipe(itemList, testRecipe, out int copies))
            {
                numBatches = copies;
                return testRecipe;
            }
        }
        numBatches = 0;
        return null;
    }

    private static bool CompareItemsToRecipe(Dictionary<FoodItem, int> itemList, Recipe testRecipe, out int mostBatches)
    {
        Dictionary<FoodItem, int> recipeIngredients = compileRecipeIngredients(testRecipe);
        //Check for would-be optional ingredients that DON'T belong.
        foreach (FoodItem item in itemList.Keys)
        {
            if (!recipeIngredients.ContainsKey(item)) {
                mostBatches = 0;
                return false;
            }
        }

        //Iterate to find the most batches one can make with the given REQUIRED ingredients.
        //Assume we use the least number of ingredients possible.
        mostBatches = int.MaxValue;
        foreach (Recipe.IngredientEntry ingredient in testRecipe.ingredients)
        {
            if (ingredient.quantity > 0) {
                //Count ingredients
                int ingredientCount = 0;
                if (itemList.ContainsKey(ingredient.type))
                {
                    ingredientCount = itemList[ingredient.type];
                }

                int batches = ingredientCount / ingredient.quantity;
                if (batches == 0)//Cannot make ANY batches with the given required ingredients, back out immediately.
                {
                    return false;
                }
                mostBatches = Mathf.Min(mostBatches, batches);
            }
        }
        //Iterate again, to make sure we don't have too many extras (this is where we count optional ingredients).
        foreach (Recipe.IngredientEntry ingredient in testRecipe.ingredients)
        {
            //Count ingredients
            int ingredientCount = 0;
            if (itemList.ContainsKey(ingredient.type))
            {
                ingredientCount = itemList[ingredient.type];
            }

            //Now assume we use as many ingredients as possible.
            int leastExtras = ingredientCount - (ingredient.quantityMax * mostBatches);
            if (leastExtras > 0)
            {
                return false;
            }
        }

        return true;
    }

    public static Dictionary<FoodItem, int> compileRecipeIngredients(Recipe testRecipe)
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
