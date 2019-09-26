using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class Station : MonoBehaviour
{
    #region Public Variables
    public RecipeList recipeList;
    #endregion

    #region Private Variables
    private Interactable interactable;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Start()
    {
        
    }
    #endregion
}
