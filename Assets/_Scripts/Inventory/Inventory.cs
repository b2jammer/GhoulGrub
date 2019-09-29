using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<FoodItem> items;
    public List<Sprite> itemSprites;

    private void Awake() {
        items = new List<FoodItem>();
        itemSprites = new List<Sprite>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClearInventory() {
        items.Clear();
        itemSprites.Clear();
    }

    public void RemoveInventoryItem(FoodItem item, Sprite itemSprite) {
        items.Remove(item);
        itemSprites.Remove(itemSprite);
    }

    public void AddInventoryItem(FoodItem item, Sprite itemSprite) {
        items.Add(item);
        itemSprites.Add(itemSprite);
    }
}
