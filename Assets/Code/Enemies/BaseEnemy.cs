using System;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(Collider), typeof(NavMeshAgent))]
public class BaseEnemy : Entity {
    public GameObject path;
    public Transform[] poi;
    public int currentTarget;
    private NavMeshAgent _agent;
    public float speed;
    public EnemyType type;

    public bool lastEnemy { get; private set;}
    public event Action OnEnemyDeath;
    

    private void Start() {
        path = GameObject.FindWithTag("Path");
        poi = new Transform[path.transform.childCount];
        for (int i = 0; i < path.transform.childCount; i++) {
            poi[i] = path.transform.GetChild(i);
        }

        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = poi[currentTarget].position;
        _agent.speed = speed;
        money = 1 + (int)type;
    }

    private void Update() {
        if (!(_agent.remainingDistance < 0.2)) return;
        currentTarget++;
        if (currentTarget>poi.Length-1) currentTarget = 0;
        _agent.destination = poi[currentTarget].position;
    }


    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Finish")) {
            other.gameObject.GetComponent<Player>().TakeDamage((int)type+1);
            OnDeath();
        }
    }

    protected override void OnDeath() {
        if (health <= 0) {
            Player player = FindAnyObjectByType<Player>();
            player.money += money;
        }
        OnEnemyDeath?.Invoke();
        Destroy(gameObject);
    }
}