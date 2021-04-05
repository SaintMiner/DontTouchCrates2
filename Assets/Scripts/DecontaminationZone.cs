using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecontaminationZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
