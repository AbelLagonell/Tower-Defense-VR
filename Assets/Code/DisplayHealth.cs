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
        //0 to display health, 1 to display money
        switch(type)
        {
            case 0:
                if(IsExt)
                {
                    display.text = "Health: " + Entity.health.ToString();
                }
                else
                {
                    display.text = Entity.health.ToString();
                }
            break;
            case 1:
                if(IsExt)
                {
                    display.text = "Money: " + Entity.money.ToString();
                }
                else
                {
                    display.text = Entity.money.ToString();
                }
            break;
        }
    }
}
