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

    // Logic fields
    private bool attachPlayer;
    private float playerOffsetY;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
        room = GameObject.Find("Room");
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    void Start()
    {
        if (skipElevator)
        {
            player.transform.position = transform.position + Vector3.down * 31.9f + Vector3.forward * playerOffsetZ;
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

    public void OpenDoors()
    {
        SetAnimState(1);
    }

    public void FinishOpening()
    {
        player.StartMovement(new Vector3(transform.position.x, player.transform.position.y, transform.position.z + playerOffsetZ), AfterPlayerMovement);
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

    private void AfterPlayerMovement()
    {
        SetAnimState(2);
        playerOffsetY = player.transform.position.y - transform.position.y;
        attachPlayer = true;
    }

    private void SetAnimState(int state)
    {
        animator.SetInteger("AnimState", state);
    }
}
