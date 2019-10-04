using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // Options
    private float playerOffsetZ = 1;

    // References
    private Animator animator;
    private Player player;

    // Logic fields
    private bool attachPlayer;
    private float playerOffsetY;

    void Awake()
    {
        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (attachPlayer)
        {
            Vector3 playerPos = player.transform.position;
            player.transform.position = new Vector3(playerPos.x, transform.position.y + playerOffsetY, playerPos.z);
        }
    }

    public void OpenDoors(Player player)
    {
        SetAnimState(1);
        this.player = player;
    }

    public void FinishOpening()
    {
        player.StartMovement(new Vector3(transform.position.x, player.transform.position.y, transform.position.z + playerOffsetZ), AfterPlayerMovement);
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
