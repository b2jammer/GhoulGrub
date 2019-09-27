using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<FoodItem> items;

    private void Awake() {
        items = new List<FoodItem>();
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
    }

    public void RemoveInventoryItem(FoodItem item) {
        items.Remove(item);
    }

    public void AddInventoryItem(FoodItem item) {
        items.Add(item);
    }
}
