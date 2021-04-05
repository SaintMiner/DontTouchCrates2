using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public  PickupType _pickupType;
    public enum PickupType
    {
        CHALLENGE,
        EFFECT
    }

    private void Start()
    {        
        GameManager.OnPlayerPickupTrigger += GameManager_OnPlayerPickupTrigger;
    }

    private void GameManager_OnPlayerPickupTrigger(Pickup obj)
    {        
        if (obj._pickupType == PickupType.CHALLENGE)
        {
            gameObject.SetActive(false);
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            GameManager.TriggerPlayerPickup(this);
        }
    }
}
