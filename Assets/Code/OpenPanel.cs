using UnityEngine;

public class OpenPanel : MonoBehaviour
{
    public GameObject Panel;

    public void OpenPannel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;
            Panel.SetActive(!isActive);
        }
    }
}
