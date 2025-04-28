using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType {
    Base,
    Fast,
    Tank
}

[Serializable]
public struct EnemySet {
    public EnemyType type;
    public int enemyAmount;
}

[Serializable]
public struct Wave {
    public EnemySet[] sets;
}

public class WaveSpawner : MonoBehaviour {
   [Header("Wave Configuration")]
    public Wave[] waveSets;
    public float timeBetweenWaves = 10f;
    public float timeBetweenSpawns = 1f;
    
    [Header("BaseEnemy Prefabs")]
    public GameObject[] enemyTypes;
    
    [Header("Game State")]
    public bool gameStart;
    public bool continuousWaves;  // Auto-start next wave after current one finishes
    public bool waveStart;        // Manual trigger to start the next wave
    public int currentWave;
    
    private bool isSpawning = false;
    private GameObject lastBaseEnemy = null;
    private List<GameObject> currentWaveEnemies = new List<GameObject>();
    
    private void Update() {
        // Check if we can start a new wave
        if (gameStart && !isSpawning && (currentWave < waveSets.Length) && (continuousWaves || waveStart)) {
            // Check if there are no enemies left from previous wave
            if (lastBaseEnemy == null && currentWaveEnemies.Count == 0) {
                StartCoroutine(SpawnCurrentWave());
                
                // Reset manual trigger after starting
                if (waveStart) {
                    waveStart = false;
                }
            } else if (continuousWaves) {
                // Debug message that we're waiting for enemies to be cleared
                if (lastBaseEnemy != null) {
                    //Debug.Log("Waiting for last enemy to be defeated before starting next wave...");
                } else if (currentWaveEnemies.Count > 0) {
                    // Clean up any null references (already destroyed enemies)
                    currentWaveEnemies.RemoveAll(enemy => enemy == null);
                    Debug.Log($"Waiting for {currentWaveEnemies.Count} enemies to be defeated before starting next wave...");
                }
            }
        }
    }
    
    private IEnumerator SpawnCurrentWave() {
        if (currentWave >= waveSets.Length) {
            Debug.Log("All waves completed!");
            yield break;
        }
        
        isSpawning = true;
        currentWaveEnemies.Clear();
        
        Debug.Log($"Starting Wave {currentWave + 1}");
        
        // Spawn the current wave
        Wave currentWaveSet = waveSets[currentWave];
        yield return StartCoroutine(SpawnWave(currentWaveSet));
        
        // Set the last enemy reference to the last spawned enemy
        if (lastBaseEnemy != null && lastBaseEnemy.GetComponent<BaseEnemy>() != null) {
            // Subscribe to the death event of the last enemy if it has an BaseEnemy component
            BaseEnemy enemyComponent = lastBaseEnemy.GetComponent<BaseEnemy>();
            enemyComponent.OnEnemyDeath += OnLastBaseEnemyDefeated;
        }
        
        isSpawning = false;
        
        // Note: We don't increment currentWave here - it will be incremented 
        // when the last enemy is defeated
    }
    
    private IEnumerator SpawnWave(Wave wave) {
        foreach (EnemySet enemySet in wave.sets) {
            for (int i = 0; i < enemySet.enemyAmount; i++) {
                // Ensure the enemy type index is valid
                if ((int)enemySet.type < enemyTypes.Length) {
                    GameObject enemy = Instantiate(enemyTypes[(int)enemySet.type], transform.position, transform.rotation);
                    
                    // Add to tracking list
                    currentWaveEnemies.Add(enemy);
                    
                    // Update last enemy reference
                    lastBaseEnemy = enemy;
                } else {
                    Debug.LogError($"BaseEnemy type {enemySet.type} does not have a corresponding prefab!");
                }
                
                yield return new WaitForSeconds(timeBetweenSpawns);
            }
        }
    }
    
    private void OnLastBaseEnemyDefeated() {
        // Unsubscribe from the event
        if (lastBaseEnemy != null && lastBaseEnemy.GetComponent<BaseEnemy>() != null) {
            lastBaseEnemy.GetComponent<BaseEnemy>().OnEnemyDeath -= OnLastBaseEnemyDefeated;
        }
        
        lastBaseEnemy = null;
        
        // Increment to next wave
        currentWave++;

        if (currentWave >= waveSets.Length) {
            StartCoroutine(ChangeScene());
        }
        
        // If continuous waves, automatically trigger the next wave
        if (continuousWaves && currentWave < waveSets.Length) {
            Debug.Log($"Last enemy defeated! Next wave starts in {timeBetweenWaves} seconds...");
            StartCoroutine(WaitAndStartNextWave());
        } else {
            Debug.Log("Last enemy defeated! Ready for next wave.");
        }
    }

    private IEnumerator ChangeScene() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Win");
    }
    
    private IEnumerator WaitAndStartNextWave() {
        yield return new WaitForSeconds(timeBetweenWaves);
        waveStart = true; // Trigger next wave after waiting
    }
    
    // Public methods to control wave spawning from UI or other scripts
    public void StartGame() {
        gameStart = true;
        waveStart = true;
    }
    
    public void StartNextWave() {
        if (!isSpawning && currentWave < waveSets.Length && lastBaseEnemy == null && currentWaveEnemies.Count == 0) {
            waveStart = true;
        } else {
            Debug.Log("Cannot start next wave until all enemies are defeated!");
        }
    }
    
    public void SetContinuousWaves(bool isContinuous) {
        continuousWaves = isContinuous;
    }
    
    public void ResetWaves() {
        StopAllCoroutines();
        isSpawning = false;
        currentWave = 0;
        lastBaseEnemy = null;
        currentWaveEnemies.Clear();
    }
}