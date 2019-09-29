using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public const int inventorySize = 9;

    public FoodItem[] items = new FoodItem[inventorySize];
    public Image[] itemImages = new Image[inventorySize];
    public Text[] itemCount = new Text[inventorySize]; 
    

    public void ClearInventory() {
        for (int i = 0; i < inventorySize; i++) {
            items[i] = null;
            itemImages[i].sprite = null;
            itemImages[i].enabled = false;
            itemCount[i].text = "0";
        }
    }

    public void RemoveInventoryItem(FoodItem item) {
        for (int i = 0; i < inventorySize; i++) {
            if (items[i] == item) {

                if (itemCount[i].text == "1") {
                    items[i] = null;
                    itemImages[i].sprite = null;
                    itemImages[i].enabled = false;
                    itemCount[i].text = "0";
                }
                else {
                    itemCount[i].text = "" + (int.Parse(itemCount[i].text) - 1);
                }

                return;
            }
        }
    }

    public void AddInventoryItem(FoodItem item) {
        Debug.Log(item.name + " has a sprite named " + item.sprite.name);

        for (int i = 0; i < inventorySize; i++) {
            if (items[i] == null) {
                Debug.Log(item.name + " is being added to index " + i);
                items[i] = item;

                Debug.Log("item image name is " + itemImages[i].name);

                itemImages[i].sprite = item.sprite;
                itemImages[i].enabled = true;
                itemCount[i].text = "1";
                return;
            }
            else {
                if (items[i] == item) {
                    itemCount[i].text = "" + (int.Parse(itemCount[i].text) + 1);
                }
            }
        }
    }
}
