using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image _marcker;
    [SerializeField] private Image _background;
    private Vector2 _tuchPosition;
    private Vector2 _inputVector;
    private float _screenWidth;
    private float _screenHeight;

    private void Start()
    {
        _screenHeight = Screen.height;
        _screenWidth = Screen.width;
        GetComponent<RectTransform>().sizeDelta = new Vector2(_screenWidth / 2 * 0.9f, _screenHeight * 0.8f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_background.rectTransform, eventData.position, eventData.pressEventCamera, out _tuchPosition))
        {
            _tuchPosition.x /= _background.rectTransform.sizeDelta.x;
            _tuchPosition.y /= _background.rectTransform.sizeDelta.y;

            _inputVector = new Vector2(_tuchPosition.x * 2, _tuchPosition.y * 2);
            _inputVector = (_inputVector.magnitude > 1f) ? _inputVector.normalized : _inputVector;

            _marcker.rectTransform.anchoredPosition = new Vector2(_inputVector.x * (_background.rectTransform.sizeDelta.x / 2), _inputVector.y * (_background.rectTransform.sizeDelta.y / 2));
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _background.gameObject.SetActive(true);
        _background.transform.position = eventData.position;
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _inputVector = Vector2.zero;
        _background.gameObject.SetActive(false);
    }

    public void Zero()
    {
        _inputVector = Vector2.zero;
    }


    public float Horizontal()
    {
        if (_inputVector.x != 0)
        {
            return _inputVector.x;
        }
        else
        {
            return 0f;
        }
    }

    public float Vertical()
    {
        if (_inputVector.y != 0)
        {
            return _inputVector.y;
        }
        else
        {
            return 0f;
        }
    }

}
