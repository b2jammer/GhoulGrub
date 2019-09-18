using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ghoul Grub/Ingredient")]
public class Ingredient : FoodItem
{
    public enum IngredientType
    {
        Veggie,
        Oil,
        Grain,
        Spice,
        Fruit,
        Dairy,
        Meat
    }
    public IngredientType type;
    public bool isCore;
}
