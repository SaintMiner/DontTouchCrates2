using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : Singleton<ChallengeManager>
{    
    [SerializeField] private GameObject _challengePrefab;
    [SerializeField] private MeshRenderer _spawnArea;
    private List<Challenge> _activeChallenges;
    private Vector3 _spawnAreaBound;
    private int _completedChallengeCount;

    public int CompletedChallengeCount
    {
        get
        {
            return _completedChallengeCount;
        }
    }

    public static event System.Action<ChallengePickup.ChallengeType> OnChallegeComplete;

    protected override void Awake()
    {
        _completedChallengeCount = 0;
        _persistent = false;
        _activeChallenges = new List<Challenge>();
        _spawnAreaBound = GetSpawnAreaBound();
        base.Awake();
    }

    public void ActivateChallenge(ChallengePickup.ChallengeType challengeType)
    {
        Debug.Log($"Activating challenge {challengeType}");
        Challenge challenge = Instantiate(_challengePrefab).GetComponent<Challenge>();
        challenge.OnChallengeEnd += Challenge_OnChallengeEnd;
    }

    private void Challenge_OnChallengeEnd(ChallengePickup.ChallengeType challengeType)
    {
        _completedChallengeCount++;
        OnChallegeComplete?.Invoke(challengeType);
    }

    private Vector3 GetSpawnAreaBound()
    {
        return _spawnArea.bounds.extents;
    }

    public static Vector3 GenerateSpawnPosition()
    {
        float crateRangeX = Instance._spawnAreaBound.x;
        float crateRangeZ = Instance._spawnAreaBound.z;

        float cratePosX = Random.Range(-crateRangeX, crateRangeX);
        float cratePosZ = Random.Range(-crateRangeZ, crateRangeZ);
        float cratePosY = 15f;

        Vector3 spawnPos = new Vector3(cratePosX, cratePosY, cratePosZ);

        return spawnPos;
    }
}
