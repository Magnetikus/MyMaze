using UnityEngine;

public class JoystickLooket : MonoBehaviour
{
    private Vector2 _inputVector;
    private float _screenWidth;
    private float _limitInputVector = 10f;

    private void Start()
    {
        _screenWidth = Screen.width;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            foreach (var e in Input.touches)
            {
                if (e.position.x > _screenWidth / 2)
                {

                    if (e.phase == TouchPhase.Moved)
                    {
                        _inputVector = e.deltaPosition;
                    }
                    else
                    {
                        _inputVector = Vector2.zero;
                    }
                }
            }
        }
    }

    public void Zero()
    {
        _inputVector = Vector2.zero;
    }


    public float HorizontalRotate()
    {
        if (_inputVector.x != 0)
        {
            _inputVector.x = _inputVector.x > _limitInputVector ? _limitInputVector : _inputVector.x;
            return _inputVector.x;
        }
        else
        {
            return 0f;
        }
    }

    public float VerticalRotate()
    {
        if (_inputVector.y != 0)
        {
            _inputVector.y = _inputVector.y > _limitInputVector ? _limitInputVector : _inputVector.y;
            return _inputVector.y;
        }
        else
        {
            return 0f;
        }
    }

}
