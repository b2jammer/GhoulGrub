using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ghoul Grub/Meal")]
public class MealItem : FoodItem
{
    public enum MealType
    {
        Roast,
        Sandwich,
        Wrap,
        Stir_Fry,
        Soup,
        Salad,
        Pie
    }
    public MealType type;
    [Range(1,10)]
    public int rank = 1;

    [TextArea]
    public string recipe;
}
