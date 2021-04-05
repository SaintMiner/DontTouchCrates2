using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Rigidbody _crateRigidBody;
    [SerializeField] private float _speed = 1f;

    private void OnEnable()
    {
        Vector3 target = Platform.RandomActivePart().transform.position;
        Vector3 cross = Vector3.Cross(transform.forward, target);
        Vector3 targetPosition = (target- transform.position) * _speed;
        float angleDiff = Vector3.Angle(transform.forward, target);

        _crateRigidBody.AddForce(targetPosition);
        _crateRigidBody.AddTorque(cross * angleDiff * _speed);
    }

}
