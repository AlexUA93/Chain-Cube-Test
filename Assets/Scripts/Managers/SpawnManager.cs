using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const float kTime = 5f;

    [SerializeField]
    private List<GameObject> _spawnPoints;
    [SerializeField]
    private GameObject _playerSpawnPoint;
    [SerializeField]
    private GameObject _spawnObject;
    [SerializeField]
    private Pool _pool;
    private CubeManager _cubeManager;

    private float timer;
    private bool isCanSpawn;

    private IPlayer _player;

    public void SetProperty(IPlayer player)
    {
        _player = player;
        player.ACTION += SetTimer;
    }

    private void Awake()
    {
        _cubeManager = new CubeManager(_pool);
        _playerSpawnPoint.SetActive(false);

        foreach (var point in _spawnPoints)
        {
            point.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (isCanSpawn)
        {
            isCanSpawn = false;
            SpawnPlayerCube();
        }
    }

    public void SetTimer()
    {
        timer = kTime;
        isCanSpawn = true;
    }

    private void SpawnPlayerCube()
    {
        var cube = Inst(_playerSpawnPoint.transform);
        _cubeManager.Add(cube, _player);
    }

    public void SpawnAtStart()
    {
        foreach (var point in _spawnPoints)
        {
            _cubeManager.Add(Inst(point.transform));
        }
        SpawnPlayerCube();
    }

    private GameObject Inst(Transform position)
    {
        GameObject gb = _pool.Take();
        if (gb == null)
        {
            gb = Instantiate(_spawnObject, position.position, position.rotation);
        }
        else
        {
            gb.transform.position = position.position;
            gb.transform.rotation = position.rotation;
        }
        return gb;
    }
}
