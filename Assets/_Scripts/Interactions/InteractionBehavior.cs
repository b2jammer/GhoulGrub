using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class that allows different kitchen stations (or any interactable object) to have behaviors that
/// differ from one another when used.
/// </summary>
public abstract class InteractionBehavior : ScriptableObject
{
    public abstract FoodItem Interact();
}
