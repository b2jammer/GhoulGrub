using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OrderStarter : MonoBehaviour
{
    public UnityEvent OnGameStart;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DelayedStart", 5f);
    }
    
    private void DelayedStart() {
        OnGameStart.Invoke();
    }
}
