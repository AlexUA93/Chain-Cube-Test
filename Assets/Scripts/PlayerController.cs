using System;
using UnityEngine;

public class PlayerController : IPlayer
{
    public Action ACTION { get; set; }

    private CubeView _cubeView;
    private float _force = 150f;

    public void SetView(CubeView view, float force)
    {
        _cubeView = view;
        _force = force;
    }

    public void Action()
    {
        if (_cubeView != null)
        {
            var force = Vector3.forward * _force;
            _cubeView.AddForce(force);
            _cubeView = null;
            if (ACTION != null)
                ACTION.Invoke();
        }
    }

    public void Move(Vector3 direction)
    {
        if (_cubeView != null)
        {
            _cubeView.Move(direction);
        }
    }
}
