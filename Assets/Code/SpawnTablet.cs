using UnityEngine;

public class SpawnTablet : MonoBehaviour
{
    public Transform spawnPt;
    public GameObject Tablet;
    private GameObject[] Tablets;

    public void Spawn()
    {
        Tablets =  GameObject.FindGameObjectsWithTag("TABLET");
        foreach (GameObject tablet in Tablets)
        {
            Destroy(tablet);
        }

        Instantiate(Tablet,spawnPt.position,spawnPt.rotation);
    }
}
