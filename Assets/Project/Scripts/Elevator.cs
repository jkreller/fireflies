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

    // Logic fields
    private bool attachPlayer;
    private float playerOffsetY;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        room = GameObject.Find("Room");
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        cabinCollider = GameObject.Find("Cabin").GetComponent<Collider>();
        cabinCollider.isTrigger = true;
    }

    void Start()
    {
        if (skipElevator)
        {
            player.transform.position = room.transform.position + Vector3.down * room.transform.localScale.y;
        } else
        {
            ToggleRoom(0);
        }
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
            SetAnimState(2);
            playerOffsetY = player.transform.position.y - transform.position.y;
            attachPlayer = true;
        }
    }

    public void OpenDoors()
    {
        SetAnimState(1);
        cabinCollider.isTrigger = false;
    }

    public void UnattachPlayer()
    {
        attachPlayer = false;
    }

    public void ToggleRoom(int active)
    {
        foreach (Transform child in room.GetComponentInChildren<Transform>())
        {
            child.gameObject.SetActive(active == 0 ? false : true);
        }
    }

    private void SetAnimState(int state)
    {
        animator.SetInteger("AnimState", state);
    }
}
