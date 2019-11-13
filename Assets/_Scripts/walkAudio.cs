using UnityEngine.Audio;
using UnityEngine;

public class walkAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            GetComponent<AudioSource>().UnPause();
        } else
        {
            GetComponent<AudioSource>().Pause();
        }
    }
}
