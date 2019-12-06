using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldText : MonoBehaviour
{
    private float height;
    private float xScale;
    private float yScale;

    private void Awake() {
        height = 1.75f;
        xScale = 0.05f;
        yScale = 0.05f;
    }

    public void SetPosition(Vector3 position) {
        transform.position = new Vector3(position.x, height, position.z);
    }

    public void Appear() {
        //gameObject.SetActive(true);
        transform.localScale = new Vector3(xScale, yScale, 1);
    }

    public void Disappear() {
        //gameObject.SetActive(false);
        transform.localScale = new Vector3(0, 0, 0);
    }
}
