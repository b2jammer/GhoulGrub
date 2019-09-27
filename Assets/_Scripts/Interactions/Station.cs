using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Station : MonoBehaviour
{
    #region Public Variables
    public RecipeList recipeList;
    public Inventory playerInventory;
    #endregion

    #region Private Variables
    private Interactable interactable;
    private Inventory stationInventory;
    
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        stationInventory = GetComponent<Inventory>();
    }

    private void Start()
    {
        
    }
    #endregion

    #region Script Specific Scripts
    public void OnInteract() {
        Debug.Log("Here is some food");

        // TODO: Compare items in the station inventory to recipes,
        // add the most appropriate recipe to the player inventory,
        // and clear the station inventory
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnInventoryOpened() {
        Debug.Log("Here is my inventory");

        // TODO: Open a GUI element where we can place our food
    }
    #endregion
}
