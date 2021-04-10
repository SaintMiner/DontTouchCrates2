using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxSpeed = 10f;
    private Rigidbody playerRigidBody;
    private SphereCollider playerCollider;
    private bool isGrounded;
    public event System.Action OnPlayerLose;
    
    private void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<SphereCollider>();
        GameManager.OnPlayerPickupTrigger += GameManager_OnPlayerPickupTrigger;        
    }

    private void GameManager_OnPlayerPickupTrigger(Pickup obj)
    {
        Debug.Log("Player Picked challenge");
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            Vector3 direction = Vector3.up * 0.5f + playerRigidBody.velocity.normalized;
            playerRigidBody.AddForce(direction * _acceleration, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
        
    private void MovePlayer()
    {
        if (isGrounded) {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 direction = new Vector3(moveHorizontal * _acceleration, 0.0f, moveVertical * _acceleration);

            playerRigidBody.AddForce(direction);
        }
        
        if (playerRigidBody.velocity.magnitude > _maxSpeed)
        {
            playerRigidBody.velocity = Vector3.ClampMagnitude(playerRigidBody.velocity, _maxSpeed * 1.5f);
        }

        playerRigidBody.angularVelocity *= 0.9f;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Crate"))
        {
            gameObject.SetActive(false);
            GameManager.TriggerPlayerCrateTouch();
        }
    }

    private void OnDisable()
    {
        OnPlayerLose?.Invoke();
    }

    private void OnCollisionStay(Collision other) {
        if (other.gameObject.CompareTag("Platform Part")) {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision other) {
        isGrounded = false;
    }

}
