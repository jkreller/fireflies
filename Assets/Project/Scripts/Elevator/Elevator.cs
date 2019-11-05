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
    [SerializeField] private GameObject finishText;
    private Animator animator;
    private Player player;
    private GameObject room;
    private Collider cabinCollider;
    private ElevatorDoor[] elevatorDoors;
    private Collider factoryCollider;
    private Light[] lights;

    // Logic fields
    [System.NonSerialized] public bool roomFinished;
    private bool attachPlayer;
    private float playerOffsetY;
    private float yRotation = 5.0f;
    private bool trackPlayerPosition;
    private bool finishTextActivated;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        room = GameObject.Find("Room");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        elevatorDoors = GetComponentsInChildren<ElevatorDoor>();
        lights = GetComponentsInChildren<Light>();
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

        if (trackPlayerPosition && Vector3.Distance(transform.position, player.transform.position) > 1.5f)
        {
            CloseDoors();
            factoryCollider.enabled = false;

            if (roomFinished && !finishTextActivated)
            {
                finishText.SetActive(true);
                finishTextActivated = true;
            }

            trackPlayerPosition = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player enters elevator
        if (other.CompareTag("Player") && !trackPlayerPosition)
        {
            CloseDoors();
            attachPlayer = true;
            playerOffsetY = player.transform.position.y - transform.position.y;
            if (roomFinished)
            {
                SetAnimState(3);
            } else
            {
                SetAnimState(1);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            trackPlayerPosition = true;
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

    public void TurnLightOnWhenDown()
    {
        SetAnimState(2);
    }

    public void EnableFactoryCollider()
    {
        factoryCollider.enabled = true;
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
