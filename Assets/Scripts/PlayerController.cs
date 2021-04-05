using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody playerRigidBody;

    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();

        GameManager.OnPlayerPickupTrigger += GameManager_OnPlayerPickupTrigger;
        GameManager.OnPlayerCrateTouch += GameManager_OnPlayerCrateTouch;
    }

    private void GameManager_OnPlayerCrateTouch()
    {
        Destroy(gameObject);
    }

    private void GameManager_OnPlayerPickupTrigger(Pickup obj)
    {
        Debug.Log("Player Picked challenge");
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
        
    public void MovePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);

        playerRigidBody.AddForce(direction);
        playerRigidBody.angularVelocity *= 0.9f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Crate"))
        {
            GameManager.TriggerPlayerCrateTouch();
        }
    }
}
