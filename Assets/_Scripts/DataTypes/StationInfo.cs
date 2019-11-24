using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ghoul Grub/Station Info")]
public class StationInfo : ScriptableObject
{
    public string title;
    public string action;
    public Sprite icon;
    public RecipeList recipeList;
}
