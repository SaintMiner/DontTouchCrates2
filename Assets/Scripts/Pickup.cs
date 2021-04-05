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

    protected virtual void TriggeredByPlayer()
    {
        GameManager.TriggerPlayerPickup(this);
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            TriggeredByPlayer();
        }
    }
}
