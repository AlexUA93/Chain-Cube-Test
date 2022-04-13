using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "data/gameconfig")]
public class GameConfig : ScriptableObject
{
    public static GameConfig Load()
    {
        var result = Resources.Load<GameConfig>("GameConfig");
        return result;
    }

    public List<CubeProperty> CubeProperties;
    public Bounds Map;
    public float ForceByZ;
    public float ForceByY;


    [Serializable]
    public class CubeProperty
    {
        public Color _color;
        public int _count;
    }
}
