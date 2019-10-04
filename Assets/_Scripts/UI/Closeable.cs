using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Closeable : MonoBehaviour
{
    #region Public Variables
    [Tooltip("Invoked *before* this object is deactivated.")]
    public UnityEvent OnClose;
    [Tooltip("Invoked *after* this object is activated.")]
    public UnityEvent OnOpen;
    #endregion

    #region Script-Specific Methods
    public void OpenPanel()
    {
        if (!IsOpened())
        {
            gameObject.SetActive(true);
            OnOpen.Invoke();
        }
    }

    public void ClosePanel()
    {
        if (IsOpened())
        {
            OnClose.Invoke();
            gameObject.SetActive(false);
        }
    }

    public bool IsOpened() => gameObject.activeSelf;
    #endregion
}
