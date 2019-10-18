using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BoolAnimator : MonoBehaviour
{
    #region Private Variables
    private Animator anim;
    #endregion

    #region MonoBehaviour Methods
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    #endregion

    #region Script Specific Methods
    public void SetBoolTrue(string parameter)
    {
        anim.SetBool(parameter, true);
    }

    public void SetBoolTrue(int parameter)
    {
        anim.SetBool(parameter, true);
    }

    public void SetBoolFalse(string parameter)
    {
        anim.SetBool(parameter, false);
    }

    public void SetBoolFalse(int parameter)
    {
        anim.SetBool(parameter, false);
    }
    #endregion
}
