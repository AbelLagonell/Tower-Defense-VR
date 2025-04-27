using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public int TowerType;
    public int Price;
    public GameObject[] Towers;

    public void Spawn()
    {
        Player p = FindAnyObjectByType<Player>();
        if(p.money >= Price)
        {
            Instantiate(Towers[TowerType],transform.position,transform.rotation);
            p.money -= Price;
        }
    }
}
