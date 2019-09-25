using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InteractAgent : MonoBehaviour
{
    #region Private Variables
    private Interactable currentInteractable;
    #endregion

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
    public void Interact()
    {
        currentInteractable.OnInteract.Invoke();
    }
    #endregion
}
