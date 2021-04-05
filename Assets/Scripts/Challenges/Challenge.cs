using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{
    protected float _interval;

    protected virtual void Awake()
    {
        _interval = 1;
    }
    protected void Start()
    {        
        StartCoroutine("ChallengeCoroutine");
    }

    protected virtual IEnumerator ChallengeCoroutine() {
        while (ChallengeCondition())
        {
            yield return new WaitForSeconds(_interval);
            ChallengeAction();
        }
        Destroy(gameObject);
    }

    protected virtual void ChallengeAction()
    {
        Debug.Log("Do something");
    }

    protected virtual bool ChallengeCondition()
    {
        return false;
    }
}
