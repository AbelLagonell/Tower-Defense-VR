using System;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider), typeof(NavMeshAgent))]
public class BaseEnemy : Entity {
    public GameObject path;
    public Transform[] poi;
    public int currentTarget;
    private NavMeshAgent _agent;
    public float speed;


    private void Start() {
        path = GameObject.FindWithTag("Path");
        poi = new Transform[path.transform.childCount];
        for (int i = 0; i < path.transform.childCount; i++) {
            poi[i] = path.transform.GetChild(i);
        }

        _agent = GetComponent<NavMeshAgent>();
        _agent.destination = poi[currentTarget].position;
        _agent.speed = speed;
    }

    private void Update() {
        if (!(_agent.remainingDistance < 0.1)) return;
        currentTarget++;
        if (currentTarget>poi.Length-1) currentTarget = 0;
        _agent.destination = poi[currentTarget].position;
    }


    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("Hitbox")) return;
        var info = other.GetComponent<Hitbox>();
        Debug.Log(other.gameObject.name);
        Debug.Log(info.damage);
        TakeDamage(info.damage);
        if (info.destroyOnHit)
            Destroy(other.gameObject);
    }

    private void OnCollisionEnter(Collision other) {
        Debug.Log("Collision");
    }

    protected override void OnDeath() {
        Destroy(gameObject);
    }
}