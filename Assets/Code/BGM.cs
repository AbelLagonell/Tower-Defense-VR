using UnityEngine;

public class BGM : MonoBehaviour
{
    private AudioManager AM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AM = FindAnyObjectByType<AudioManager>();
        AM.PlayBGM("Background");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
