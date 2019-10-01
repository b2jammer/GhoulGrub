using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractAgent : MonoBehaviour
{
    #region Private Variables
    private Interactable currentInteractable;
    #endregion

    /// <summary>
    /// Calls the functions tied to the other colliding objects OnApproach event if it is an interactable object
    /// </summary>
    /// <param name="other"></param>
    #region MonoBehaviour Methods
    private void OnTriggerEnter(Collider other)
    {
        Interactable otherInteract;
        if ((otherInteract = other.GetComponentInChildren<Interactable>()) != null)
        {
            currentInteractable = otherInteract;
            otherInteract.OnApproach.Invoke();
            Debug.Log("Approached.");
        }
    }

    /// <summary>
    /// Calls the functions tied to the other colliding objects OnRetreat event if it is an interactable object
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        Interactable otherInteract;
        if ((otherInteract = other.GetComponentInChildren<Interactable>()) != null && otherInteract == currentInteractable)
        {
            currentInteractable = null;
            otherInteract.OnRetreat.Invoke();
            Debug.Log("Retreated.");
        }
    }
    #endregion

    #region Script Specific Methods
    /// <summary>
    /// Calls the functions tied to an objects OnInteract event  
    /// </summary>
    public void Interact()
    {
        if (currentInteractable != null) {
            currentInteractable.OnInteract.Invoke();
        }
    }

    public void OpenStationInventory() {
        if (currentInteractable != null) {
            currentInteractable.OnStationInventoryOpened.Invoke();
        }
    }
    #endregion
}
