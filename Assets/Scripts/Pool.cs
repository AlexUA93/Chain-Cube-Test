using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    private List<GameObject> _objectsInPool;

    private void Awake()
    {
        _objectsInPool = new List<GameObject>();
    }

    public void Add(GameObject gameObject)
    {
        gameObject.SetActive(false);
        _objectsInPool.Add(gameObject);
    }

    public GameObject Take()
    {
        GameObject gb = null;
        if (_objectsInPool.Count > 0)
        {
            gb = _objectsInPool[0];
            _objectsInPool.Remove(gb);
            gb.SetActive(true);
        }

        return gb;
    }
}
