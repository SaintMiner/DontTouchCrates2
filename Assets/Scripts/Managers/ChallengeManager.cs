using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChallengeManager : Singleton<ChallengeManager>
{
    [SerializeField] private ObjectPool _cratePool;
    [SerializeField] private MeshRenderer _spawnArea;
    private List<Challenge> _activeChallenges;
    private Vector3 _topSpawnAreaBound;
    private int _completedChallengeCount;

    [SerializeField] private GameObject _crateRainChallengeCrateRainPrefab;
    [SerializeField] private GameObject _launchingCratesChallengePrefab;

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
        _topSpawnAreaBound = GetSpawnAreaBound();
        base.Awake();

        _cratePool.FillPool<Crate>();
    }


    public void SpawnCrate(ChallengePickup.ChallengeType challengeType)
    {
        Crate crate = _cratePool.GetPooledObject<Crate>();        

        switch (challengeType)
        {
            case ChallengePickup.ChallengeType.CRATE_RAIN:
                crate.gameObject.transform.position = GenerateTopSpawnPosition();
                crate.gameObject.SetActive(true);
                crate.ChallengeRainCrate();
                break;
            case ChallengePickup.ChallengeType.LAUCHING_CRATES:
                crate.gameObject.transform.position = GenerateSideSpawnPosition();
                crate.gameObject.SetActive(true);
                crate.ChallengeLauchCrate();
                break;
        }
    }
    public void ActivateChallenge(ChallengePickup.ChallengeType challengeType)
    {
        Debug.Log($"Activating challenge {challengeType}");
        Challenge challenge;
        switch (challengeType)
        {
            case ChallengePickup.ChallengeType.CRATE_RAIN:
                challenge = Instantiate(_crateRainChallengeCrateRainPrefab).GetComponent<Challenge>();
                challenge.OnChallengeEnd += Challenge_OnChallengeEnd;
                break;
            case ChallengePickup.ChallengeType.LAUCHING_CRATES:
                challenge = Instantiate(_launchingCratesChallengePrefab).GetComponent<Challenge>();
                challenge.OnChallengeEnd += Challenge_OnChallengeEnd;
                break;
        }
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

    private static Vector3 GenerateTopSpawnPosition()
    {
        float crateRangeX = Instance._topSpawnAreaBound.x;
        float crateRangeZ = Instance._topSpawnAreaBound.z;

        float cratePosX = Random.Range(-crateRangeX, crateRangeX);
        float cratePosZ = Random.Range(-crateRangeZ, crateRangeZ);
        float cratePosY = 15f;

        Vector3 spawnPos = new Vector3(cratePosX, cratePosY, cratePosZ);

        return spawnPos;
    }

    private static Vector3 GenerateSideSpawnPosition()
    {
        float boundX = 50;
        float boundZ = 7;

        float cratePosX = UnityEngine.Random.value < 0.5f ? boundX : -boundX;
        float cratePosZ = Random.Range(-boundZ, boundZ);
        float cratePosY = 1f;

        Vector3 spawnPos = new Vector3(cratePosX, cratePosY, cratePosZ);

        return spawnPos;
    }

}
