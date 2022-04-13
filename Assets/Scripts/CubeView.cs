using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class CubeView : MonoBehaviour
{
    public Action<GameObject, GameObject, int> Collider;
    public Action<GameObject> DESTROY;

    [SerializeField]
    private List<TMP_Text> _texts;
    [SerializeField]
    private Rigidbody _rigidbody;
    [SerializeField]
    private MeshRenderer _renderer;

    private int _count;
    private Bounds _bounds;


    public bool isMove;

    public void SetCubeProperty(int count, Color color, Bounds bounds)
    {
        _bounds = bounds;
        SetCubeProperty(count, color);
    }

    public void SetCubeProperty(int count, Color color)
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _count = count;
        foreach (var t in _texts)
        {
            t.text = count.ToString();
        }

        _renderer.material.color = color;

        name =  color.ToString();
        SetMoveCheck(false);
    }

    public void AddForce(Vector3 force)
    {
        _rigidbody.AddForce(force, ForceMode.Force);
        if (force != Vector3.zero)
            SetMoveCheck(true);
    }

    public void SetMoveCheck(bool value)
    {
        isMove = value;
    }

    public void Move(Vector3 direction)
    {
        var pos = transform.position + direction;
        pos.x = Mathf.Clamp(pos.x, _bounds.min.x, _bounds.max.x);
        transform.position = pos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == name && isMove && collision.gameObject.activeSelf && gameObject.activeSelf)
        {
            if (Collider != null)
                Collider.Invoke(gameObject, collision.gameObject, _count);
        }
        else if (collision.gameObject.tag == "Cube")
        {
            var cubeView = collision.gameObject.GetComponent<CubeView>();
            cubeView.SetMoveCheck(true);
        }
        
        if (collision.gameObject.tag == "Destroy")
        {
            if (DESTROY != null)
                DESTROY.Invoke(gameObject);
        }
        
    }
}
