using UnityEngine;
using UnityEngine.UI;

public class MapSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Map _map;
    [SerializeField] private Transform _mapPoint;
    [SerializeField] private MovetMap _movetMap;
    private GameObject _player;
    private Vector3 _position;
    private int _sizeRows;
    private int _sizeCols;
    private int _step;
    private int _maximum;
    private const int _minimum = 15;


    private void OnEnable()
    {
        int[] array = _map.GetSizeMazeAndMaximum();
        _sizeRows = array[0];
        _sizeCols = array[1];
        _maximum = array[2];
        _slider.minValue = _minimum;
        _slider.maxValue = _maximum;
        _slider.value = _maximum;
        _step = _maximum / _minimum;
    }

    public void PlusMap()
    {
        _slider.value -= _step;
    }

    public void MinusMap()
    {
        _slider.value += _step;
    }

    public void LookInPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        Vector3 positionPlayer = _player.transform.position;
        _position.x = positionPlayer.x;
        _position.z = positionPlayer.z;
        _mapPoint.position = _position;
    }

    private void MovetMap()
    {
        float deltaX = _movetMap.HorizontalMovetMap();
        float deltaZ = _movetMap.VerticalMovetMap();
        _position.x += deltaX;
        _position.z += deltaZ;
    }

    private void CorrectionMovet()
    {
        float deltaSize = _maximum - _slider.value;
        if (_slider.value < _sizeRows)
        {
            if (_maximum - (int)_position.x > 0 && _position.x < _maximum - deltaSize)
            {
                _position.x = _maximum - deltaSize;
            }
            if (_maximum - (int)_position.x < 0 && _position.x > _maximum + deltaSize)
            {
                _position.x = _maximum + deltaSize;
            }
        }
        else
        {
            _position.x = _sizeRows;
        }
        if (_slider.value < _sizeCols)
        {
            if (_maximum - (int)_position.z > 0 && _position.z < _maximum - deltaSize)
            {
                _position.z = _maximum - deltaSize;
            }
            if (_maximum - (int)_position.z < 0 && _position.z > _maximum + deltaSize)
            {
                _position.z = _maximum + deltaSize;
            }
        }
        else
        {
            _position.z = _sizeCols;
        }
    }

    private void CorrectionZoom()
    {
        if (_slider.value < _minimum)
        {
            _slider.value = _minimum;
        }
        if (_slider.value > _maximum)
        {
            _slider.value = _maximum;
        }
    }

    private void Update()
    {
        _position = _mapPoint.position;
        MovetMap();
        CorrectionMovet();
        CorrectionZoom();
        _mapPoint.position = _position;
    }
}
