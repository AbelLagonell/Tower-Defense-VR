using UnityEngine;
using UnityEngine.Events;

public class ButtonVR : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    private GameObject presser;
    //private AudioSource sound;
    bool isPressed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //sound = GetComponent<AudioSource>();
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isPressed)
        {
            button.transform.localPosition = new Vector3(0, -0.05f, 0);
            presser = other.gameObject;
            onPress.Invoke();
            //sound.Play();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == presser)
        {
            button.transform.localPosition = new Vector3(0, -0.015f, 0);
            onRelease.Invoke();
            isPressed = false;
        }
    }

    public void StartWave()
    {
        //Start wave
        WaveSpawner waveSpawner = FindAnyObjectByType<WaveSpawner>();
        waveSpawner.StartNextWave();
        Debug.Log("Starting Next Wave");
    }
}
