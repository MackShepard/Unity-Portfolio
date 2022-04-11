using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControls : MonoBehaviour
{
    private AttackController attackController;
    private playerController playerController;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        attackController = playerObject.GetComponent<AttackController>();
        playerController = playerObject.GetComponent<playerController>();
    }
    public void Attack()
    {
        attackController.AttackHandler();
    }

    public void Jump()
    {
        playerController.Jump();
    }
    public void Interact()
    {
        playerController.Interact();
    }
}
