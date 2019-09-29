using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //public const int inventorySize = 9;

    public FoodItem[] items;
    public Image[] itemImages;
    public Text[] itemCount; 
    

    public void ClearInventory() {
        for (int i = 0; i < items.Length; i++) {
            items[i] = null;
            itemImages[i].sprite = null;
            itemImages[i].enabled = false;
            itemCount[i].text = "0";
        }
    }

    public void RemoveInventoryItem(FoodItem item) {
        for (int i = 0; i < items.Length; i++) {
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
        for (int i = 0; i < items.Length; i++) {
            if (items[i] == null) {
                items[i] = item;
                itemImages[i].sprite = item.sprite;
                itemImages[i].enabled = true;
                itemCount[i].text = "1";
                return;
            }
            else {
                if (items[i] == item) {
                    itemCount[i].text = "" + (int.Parse(itemCount[i].text) + 1);
                    return;
                }
            }
        }
    }
}
