using UnityEngine;

public class CubeManager
{
    private const int kMultiplication = 2;
    private Pool _pool;
    private GameConfig _gameConfig;

    public CubeManager(Pool pool)
    {
        _pool = pool;
        _gameConfig = GameConfig.Load();
    }

    public void Add(GameObject gameObject)
    {
        var cubeView = gameObject.GetComponent<CubeView>();
        var cubeProperty = _gameConfig.CubeProperties[0];
        cubeView.SetCubeProperty(cubeProperty._count, cubeProperty._color);
        if (cubeView.Collider == null)
            cubeView.Collider += Change;
        if (cubeView.DESTROY == null)
            cubeView.DESTROY += Destroy;
    }

    public void Add(GameObject gameObject, IPlayer player)
    {
        var cubeView = gameObject.GetComponent<CubeView>();
        int index = Random.Range(0, _gameConfig.CubeProperties.Count - 1);
        var cubeProperty = _gameConfig.CubeProperties[index];
        cubeView.SetCubeProperty(cubeProperty._count, cubeProperty._color, _gameConfig.Map);
        if (cubeView.Collider == null)
            cubeView.Collider += Change;
        player.SetView(cubeView, _gameConfig.ForceByZ);
    }

    private void Change(GameObject destroy, GameObject change, int count)
    {
        Destroy(destroy);
        var cubeProperty = GetProperty(count * kMultiplication);
        if (cubeProperty != null)
        {
            var cubeView = change.GetComponent<CubeView>();
            cubeView.SetCubeProperty(cubeProperty._count, cubeProperty._color);
            var force = Vector3.up * _gameConfig.ForceByY;
            cubeView.AddForce(force);
        }
        else
        {
            Destroy(change);
        }
    }

    private void Destroy(GameObject destroy)
    {
        _pool.Add(destroy);
    }

    private GameConfig.CubeProperty GetProperty(int count)
    {
        for (int i = 0; i <= _gameConfig.CubeProperties.Count - 1; i++)
        {
            var cubeProperty = _gameConfig.CubeProperties[i];
            if (cubeProperty._count == count)
            {
                return cubeProperty;
            }
        }
        return null;
    }
}
