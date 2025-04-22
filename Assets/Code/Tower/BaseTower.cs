using System.Numerics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(SphereCollider), typeof(XRGrabInteractable))]
public abstract class BaseTower : Entity {
    public float detectionRadius = 10;
    public int damage = 1;
    public int cost = 1;
    public float attackCooldown = 1f; //in seconds
    public float currentAttackCooldown;
    public GameObject spawnedObject;
    public bool active = false;
   //Set to the direction the enemy is in
    private XRGrabInteractable interactable;

    public void Start() {
        GetComponent<SphereCollider>().radius = detectionRadius;
        interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnSocketEnter);
        interactable.selectExited.AddListener(OnSocketExit);
    }

    private void OnDrawGizmos() {
        MoreDebug.DrawCircle(transform.position, detectionRadius * transform.localScale.x, 32, Color.red);
    }

    protected abstract void Attack();

    private void Update() {
        currentAttackCooldown -= Time.deltaTime;
    }

    public void OnSocketEnter(SelectEnterEventArgs args) {
        // Check if the interactor is a socket
        if (args.interactorObject is XRSocketInteractor) {
            active = true;
            transform.rotation = Quaternion.identity;
        }
    }

    public void OnSocketExit(SelectExitEventArgs args) {
        if (args.interactorObject is XRSocketInteractor) {
            active = false;
        }
    }

    void OnDestroy() {
        if (interactable != null) {
            interactable.selectEntered.RemoveListener(OnSocketEnter);
            interactable.selectExited.RemoveListener(OnSocketExit);
        }
    }
}