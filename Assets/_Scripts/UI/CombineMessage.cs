using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineMessage : MonoBehaviour
{
    public Text textField;


    public void SetObtainText(RecipeList.Recipe recipe)
    {
        textField.text = "Obtained " + recipe.target.itemName + (recipe.targetQuantity != 1 ? " x" + recipe.targetQuantity : "") + "!";
    }

    public void SetOrderText(Order order) {
        textField.text = order.name + " out!";
    }
}
