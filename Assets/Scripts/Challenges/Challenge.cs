using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Challenge : MonoBehaviour
{      
    protected float _interval;
    protected ChallengePickup.ChallengeType _challengeType;

    public event System.Action<ChallengePickup.ChallengeType> OnChallengeEnd;

    public ChallengePickup.ChallengeType ChanllengeType
    {
        get
        {
            return _challengeType;
        }
    }

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
        OnChallengeEnd?.Invoke(ChanllengeType);
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
