using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Recipe[] recipes;
}
