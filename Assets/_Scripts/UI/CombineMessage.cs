using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombineMessage : MonoBehaviour
{
    public Text textField;


    public void SetObtainText(RecipeList.Recipe recipe, int numBatches)
    {
        int totalMade = recipe.targetQuantity * numBatches;
        textField.text = "Obtained " + recipe.target.itemName + (totalMade != 1 ? " x" + totalMade : "") + "!";
    }

    public void SetFailText()
    {
        textField.text = "Oops!";
    }

    public void SetOrderText(Order order) {
        textField.text = order.name + " out!";
    }
}
