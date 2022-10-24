using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using SysRandom = System.Random;
using UnityRandom = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject entityGO;
    private Spawn[] spawnHolders;
    public List<Entity> entityOnVawe;
    
    public Spawn[] spawns;
    private SysRandom rand = new SysRandom();

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    public int waveIndex = 0;
    private int maxWaveIndex;
    private void Start()
    {
        spawnHolders = GetComponentsInChildren<Spawn>();
        entityGO = LevelManager.instance.currentLevel.entityGO;
        maxWaveIndex = LevelManager.instance.currentLevel.numberOfWaves;
    }

    private void Update()
    {
        if (countdown <= 0)
        {
            if (waveIndex < maxWaveIndex)
            {
                StartCoroutine(SpawnWave());
                countdown = timeBetweenWaves;   
            }
        }

        if (waveIndex == maxWaveIndex && entityOnVawe.Count == 0)
        {
            GameManager.instance.SetGameState(GameState.GameWin);
            UIManager.instance.MenuPanelUIOnClick();
            GameManager.instance.playerSO.score += LevelManager.instance.currentLevel.levelScore;
            LevelManager.instance.SetLevelState();
        }

        countdown -= Time.deltaTime;
    }

    private IEnumerator SpawnWave()
    {
        Debug.Log($"current wave: {waveIndex}");

        waveIndex++;

        for (int i = 0; i < waveIndex; i++)
        {
            entityOnVawe.Clear();
            SpawnEnemy();
            yield return new WaitForSeconds(2f);
        }
    }

    private void SpawnEnemy()
    {
        spawns = spawnHolders.OrderBy(x => rand.Next()).ToArray();
        Array.Resize(ref spawns, UnityRandom.Range(6,10));
        foreach (Spawn spawn in spawns)
        {
            spawn.entityGO = entityGO;
            spawn.hasSpawned = true;
        }
    }
}