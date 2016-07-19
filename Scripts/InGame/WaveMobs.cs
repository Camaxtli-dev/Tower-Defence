using UnityEngine;
using System.Collections;

public class WaveMobs : MonoBehaviour {
    
    private GameObject[] enemyPrefabs; // мобы
    private Transform[] SpawnMobsPoints;
    public int enemyChoice = 0; // выбор волны
    public int enemyPerWave; // сколько будет мобов за волну
    public float respanwInterval = 2.5f;
    private float lastSpawnTime = 0;

    public bool waveActive = false;

    private int waveLevel = 0;
    private float intermissionTime = 15;
    private float waitEndWave;
    
    public void Mobs(GameObject[] mobs)
    {
        enemyPrefabs = new GameObject[mobs.Length];
        for(int i = 0; i < mobs.Length; i++)
        {
            enemyPrefabs[i] = mobs[i];
            PoolManager.Instance.CreatePool(enemyPrefabs[i], 5);
        }
    }

    public void SpawnPoints(GameObject spawn)
    {
        SpawnMobsPoints = new Transform[spawn.transform.childCount];
        int i = 0;
        foreach (Transform spawnPoint in spawn.transform)
        {
            SpawnMobsPoints[i] = spawnPoint;
            i++;
        }
    }

    public void StartNewWave()
    {
        waveActive = true;
        SetNextWave();
        enemyChoice = Random.Range(0, enemyPrefabs.Length);
        enemyPerWave = 4;
    }

    public void nextWave()
    {
        if (Time.time >= waitEndWave)
        {
            if (enemyPerWave >= 0)
            {
                if (Time.time > (lastSpawnTime + respanwInterval))
                {
                    SpawnNewEnemy();
                }
            }
            if (enemyPerWave < 0)
            {
                waitEndWave = Time.time + intermissionTime;
                FinishWave();
            }
        }
    }

    public void FinishWave()
    {
        waveActive = false;
        StartNewWave();
    }
    
    public void SpawnNewEnemy()
    {
        int spawnChoice = Random.Range(0, SpawnMobsPoints.Length);
        PoolManager.Instance.ReuseObject(enemyPrefabs[enemyChoice], SpawnMobsPoints[spawnChoice].position, SpawnMobsPoints[spawnChoice].rotation);

        enemyPerWave--;
        lastSpawnTime = Time.time;
        GameMaster.Instance.enemyCount++;
        GameMaster.Instance.UpdateHUD();
    }

    public void SetNextWave()
    {
        waveLevel++;
        GameMaster.Instance.waveCount = waveLevel;
        GameMaster.Instance.UpdateHUD();
    }
}
