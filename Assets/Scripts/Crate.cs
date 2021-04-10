using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private Rigidbody _crateRigidBody;
    [SerializeField] private float _speed = 1f;

    public void ChallengeRainCrate()
    {
        GameObject targetObject = Platform.RandomActivePart();
        Vector3 targetBounds = targetObject.GetComponent<MeshRenderer>().bounds.extents;
        Vector3 target = targetObject.transform.position;
        Vector3 cross = Vector3.Cross(transform.forward, target);

        float offsetX = Random.Range(-targetBounds.x, targetBounds.x);
        float offsetZ = Random.Range(-targetBounds.z, targetBounds.z);
        Vector3 offsetVector = new Vector3(offsetX, 0, offsetZ);

        Vector3 targetPosition = (target - transform.position + offsetVector) * _speed;
        float angleDiff = Vector3.Angle(transform.forward, target);

        _crateRigidBody.AddForce(targetPosition);
        _crateRigidBody.AddTorque(cross * angleDiff * _speed);
    }

    public void ChallengeLauchCrate()
    {
        Vector3 cratePost = transform.position;
        Vector3 target = new Vector3(-cratePost.x, cratePost.y, 0);
        Vector3 targetPosition = (target) * _speed;

        _crateRigidBody.AddForce(targetPosition);
        _crateRigidBody.AddTorque(Random.rotation.eulerAngles);
    }

    private void OnDisable()
    {
        _crateRigidBody.angularVelocity = _crateRigidBody.velocity = Vector3.zero;
    }
}
