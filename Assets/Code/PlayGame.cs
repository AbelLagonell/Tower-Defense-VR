using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
