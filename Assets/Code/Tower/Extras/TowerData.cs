using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class TowerData : MonoBehaviour {
    [Tooltip("The base Buying cost of the item")]
    public int baseCost = 1;
    [Tooltip("Upgrade cost after you bought the tower")]
    public int upgradeCost = 2;
    [Tooltip("Adds to the damage done by the tower")]
    public int upgradeDamage = 1;
    [Tooltip("Adds to the detection Range")]
    public int upgradeRange = 5;
    [Tooltip("This is subtracted from the max attack cooldown")]
    public float upgradeCooldown = 0.5f;
    public UnityEvent onUpgrade;
    
    
    [Tooltip("The multiplicative sell value so if its buying at 10 you sell at 7 with a value of 0.7")]
    [SerializeField] private float depreciationValuePercentage = 0.7f;
    [SerializeField] private SpriteRenderer person;
    [SerializeField] private BaseTower tower;
    [SerializeField] private Sprite upgradeSprite;

    
    private int _stage = 0;

    /// <summary>
    /// Sets the sprite of the tower while giving back the upgrade cost
    /// If it returns -1 then you cannot do it
    /// </summary>
    /// <returns>The player cost</returns>
    public int Upgrade() {
        if (_stage == 1) return -1;
        person.sprite = upgradeSprite;
        _stage++;
        tower.attackCooldown -= upgradeCooldown;
        tower.damage += upgradeDamage;
        tower.detectionRadius += upgradeRange;
        onUpgrade?.Invoke();
        return upgradeCost;
    }

    /// <summary>
    /// Gets the sell value of the Tower depending on the stage
    /// If it returns a -1 then something went wrong with the stage of the tower
    /// </summary>
    /// <returns>The sell value of the tower</returns>
    public int GetSellValue() {
        return _stage switch {
            0 => (int)(baseCost * depreciationValuePercentage),
            1 => (int)((baseCost + upgradeCost) * depreciationValuePercentage),
            _ => -1
        };
    }
}