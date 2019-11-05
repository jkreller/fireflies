using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for elevator logic
public class Elevator : MonoBehaviour
{
    // Options
    [SerializeField] private float playerOffsetZ = 1;
    [SerializeField] private bool skipElevator;

    // References
    private Animator animator;
    private Player player;
    private GameObject room;
    private Collider cabinCollider;
    private ElevatorDoor[] elevatorDoors;
    private Collider factoryCollider;

    // Logic fields
    private bool attachPlayer;
    private float playerOffsetY;
    float yRotation = 5.0f;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        room = GameObject.Find("Room");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        elevatorDoors = GetComponentsInChildren<ElevatorDoor>();
        cabinCollider = GameObject.Find("Cabin").GetComponent<Collider>();
        cabinCollider.isTrigger = true;
    }

    void Start()
    {
        if (skipElevator)
        {
            player.transform.position = room.transform.position + Vector3.down * 1.5f;
        }
        factoryCollider = GameObject.Find("Factory").GetComponent<Collider>();
    }

    void Update()
    {
        // Attach player to elevator
        if (attachPlayer)
        {
            Vector3 playerPos = player.transform.position;
            player.transform.position = new Vector3(playerPos.x, transform.position.y + playerOffsetY, playerPos.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player enters elevator
        if (other.CompareTag("Player"))
        {
            CloseDoors();
            SetAnimState(1);
            playerOffsetY = player.transform.position.y - transform.position.y;
            attachPlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When player enters elevator
        if (other.CompareTag("Player"))
        {
            CloseDoors();
            factoryCollider.enabled = false;
        }
    }

    public void OpenDoors()
    {
        foreach (ElevatorDoor elevatorDoor in elevatorDoors)
        {
            elevatorDoor.Open();
        }
        cabinCollider.isTrigger = false;
    }

    public void CloseDoors()
    {
        foreach (ElevatorDoor elevatorDoor in elevatorDoors)
        {
            elevatorDoor.Close();
        }
        cabinCollider.isTrigger = true;
    }

    public void UnattachPlayer()
    {
        attachPlayer = false;
    }

    private void SetAnimState(int state)
    {
        animator.SetInteger("AnimState", state);
    }
}
