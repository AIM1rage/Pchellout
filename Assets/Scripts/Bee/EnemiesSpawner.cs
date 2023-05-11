﻿using System;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;


/// <summary>
/// Генерирует врагов (EnemyItem) рандомным образом
/// из разных точек, которые берёт из HoneycombObjectЮ
/// которые летят на Goals рандомно
/// При этом это происходит "волновым" образом
/// </summary>
public class EnemiesSpawner : MonoBehaviour
{
    public List<GameObject> Goals;
    public List<GameObject> EnemyItems;
    public GameObject HoneycombObject;
    public float BeeSpawnInterval = 5f;

    public GameObject ParentForEnemies;


    private Transform[] _honeyCombs;
    private float _timer;
    private int _currentWaveIndex;
    private int _beesToSpawn;
    private float _waveTimer;
    private float _beeSpawnTimer;
    private bool isEnd = false;
    void Start()
    {
        _honeyCombs = HoneycombObject.GetComponentsInChildren<Transform>();
        _beesToSpawn = Globals.AttackWaves[_currentWaveIndex].EnemyCount;
    }

    void Update()
    {
        if (isEnd) return;
        IterateWave();
        if (_beeSpawnTimer >= BeeSpawnInterval && _beesToSpawn > 0)
        {
            SpawnEnemy();
            _timer = 0f;
            _beesToSpawn--;
            _beeSpawnTimer = 0f;
        }
    }

    /// <summary>
    /// Делает так, чтобы _currentWaveIndex указывал на те параметры
    /// из _attackWaves[_currentWaveIndex],
    /// которые нужно применить к врагам в данный момент
    /// </summary>
    private void IterateWave()
    {
        _waveTimer += Time.deltaTime;
        if (_waveTimer >= Globals.AttackWaves[_currentWaveIndex].Duration)
        {
            _waveTimer = 0f;
            _currentWaveIndex++;
            if (_currentWaveIndex >= Globals.AttackWaves.Length)
            {
                isEnd = true;
                return;
            }
            _beesToSpawn = Globals.AttackWaves[_currentWaveIndex].EnemyCount;
            _beeSpawnTimer = BeeSpawnInterval;
        }
        _beeSpawnTimer += Time.deltaTime;
    }

    private void SpawnEnemy()
    {
        Transform honeycomb = _honeyCombs[Random.Range(1, _honeyCombs.Length-1)];
        var speed = Globals.AttackWaves[_currentWaveIndex].Speed;
        var scale = Globals.AttackWaves[_currentWaveIndex].Scale;
        var enemyItem = EnemyItems[Globals.AttackWaves[_currentWaveIndex].EnemyIndex];
        var newEnemy = Instantiate(enemyItem, honeycomb.position, Quaternion.identity);

        newEnemy.transform.SetParent(ParentForEnemies.transform);
        newEnemy.GetComponent<BeeEnemy>().Speed = 
            Random.Range(Math.Max(0, speed - 2), speed + 2);
        newEnemy.transform.localScale = 
            new Vector3(scale, scale, scale);
        
        //TODO исправить баг в присваивании врагу цели (иногда в BeeSource после присваивания лежит null)
        newEnemy.GetComponent<BeeEnemy>().BeesSource = GetRandomObject();
    }

    private GameObject GetRandomObject()
        => Goals[Random.Range(0, Goals.Count)];
}
