using UnityEngine;

public class SpawnTower : MonoBehaviour
{
    public int TowerType;
    public int Price;
    public GameObject[] Towers;

    public void Spawn()
    {
        if(Entity.money >= Price)
        {
            Instantiate(Towers[TowerType],transform.position,transform.rotation);
            Entity.money -= Price;
        }
    }
}
