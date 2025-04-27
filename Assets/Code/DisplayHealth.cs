using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealth : MonoBehaviour
{
    public TMP_Text display;
    public int type;
    public bool IsExt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        Player temp = FindAnyObjectByType<Player>();
        //0 to display health, 1 to display money
        switch(type)
        {
            case 0:
                if(IsExt)
                {
                    display.text = "Health: " + temp.health.ToString();
                }
                else
                {
                    display.text = temp.health.ToString();
                }
            break;
            case 1:
                if(IsExt)
                {
                    display.text = "Money: " + temp.money.ToString();
                }
                else
                {
                    display.text = temp.money.ToString();
                }
            break;
        }
    }
}
