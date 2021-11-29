using UnityEngine;
using UnityEngine.UI;

public class MovetMap : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private Vector2 _moveDdirection;
    private float _distance;
    private const float _stepZoom = 0.3f;
    private const float _speedMovet = 0.6f;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (Input.touchCount == 1)
            {
                Movet();
            }
            if (Input.touchCount == 2)
            {
                Zoom();
            }
        }
        else if (_distance != 0)
        {
            _distance = 0;
        }
    }

    private void Movet()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 changePosition = Input.GetTouch(0).deltaPosition;
            _moveDdirection = changePosition.normalized;
        }
        else
        {
            _moveDdirection = Vector2.zero;
        }
    }

    private void Zoom()
    {
        Vector2 firstPosition = Input.GetTouch(0).position;
        Vector2 secondPosition = Input.GetTouch(1).position;

        if (_distance == 0)
        {
            _distance = Vector2.Distance(firstPosition, secondPosition);
        }

        float delta = (Vector2.Distance(firstPosition, secondPosition) - _distance) * _stepZoom;

        _slider.value += - delta;

        _distance = Vector2.Distance(firstPosition, secondPosition);
    }

    public float HorizontalMovetMap()
    {
        return -_moveDdirection.x * _speedMovet;
    }

    public float VerticalMovetMap()
    {
        return -_moveDdirection.y * _speedMovet;
    }
}
