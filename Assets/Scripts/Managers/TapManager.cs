using UnityEngine;
using UnityEngine.EventSystems;

public class TapManager : MonoBehaviour
{
    private BaseInput _input;
    private const int _none = 0;
    private const int _move = 1;
    private const int _up = 2;
    private const float kSensitivity = 5;

    private Vector2 touchPosition;
    private Vector2 lastPosition;

    private IPlayer _player;

    public void SetProperty(IPlayer player)
    {
        _player = player;
    }

    private void Update()
    {
        if (null == _input && EventSystem.current.currentInputModule != null)
        {
            _input = EventSystem.current.currentInputModule.input;
        }

        var touch = GetTouch();

        if (touch.fingerId == _move)
        {
            touchPosition = touch.position;
            MovePlayer();
        }
        else if(touch.fingerId == _up)
        {
            _player.Action();
        }
    }

    private Touch GetTouch()
    {
        Touch touch = new Touch();
        if (_input != null)
        {
            if (_input.GetMouseButton(0))
            {
                touch.position = _input.mousePosition;
                touch.fingerId = _move;
                return touch;
            }

            if (_input.GetMouseButtonUp(0))
            {
                touch.fingerId = _up;
                return touch;
            }

            if (_input.touchCount > 0)
            {
                touch = _input.GetTouch(0);
                touch.fingerId = touch.phase == TouchPhase.Ended ? _up : _move;
                return touch;
            }

            
        }

        touch.fingerId = _none;

        return touch;
    }

    private void MovePlayer()
    {
        lastPosition = lastPosition == Vector2.zero ? touchPosition : lastPosition;
        var direction = lastPosition.x < touchPosition.x ? Vector3.right : lastPosition.x > touchPosition.x ? Vector3.left : Vector3.zero;
        lastPosition = touchPosition;
        _player.Move(direction / kSensitivity);
    }
}
