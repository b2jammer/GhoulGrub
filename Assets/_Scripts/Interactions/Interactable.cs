using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    #region Public Variables
    public UnityEvent OnInteract;
    public UnityEvent OnApproach;
    public UnityEvent OnRetreat;
    #endregion
}
