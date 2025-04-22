using UnityEngine;

public abstract class Entity : MonoBehaviour {
    public int health = 10;
    public int maxHealth = 10;


    public void TakeDamage(int dmg) {
        health -= dmg;
        if (health <= 0) {
            OnDeath();
        }
    }

    protected abstract void OnDeath();

}
