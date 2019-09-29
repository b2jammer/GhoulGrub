using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FoodItemPanel : MonoBehaviour
{
    public FoodItem foodItem;
    public int count;

    [SerializeField]
    private Text foodLabel;
    [SerializeField]
    private Image foodImage;

    public void Update()
    {
        if (foodItem != null)
        {
            foodLabel.text = foodItem.itemName + "\nx " + count;
            foodImage.sprite = foodItem.sprite;
        }
    }
}
