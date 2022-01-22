using UnityEngine;
using UnityEngine.UI;

public class ConstruktorMenu : MonoBehaviour
{
    [SerializeField] private Slider _sliderRows;
    [SerializeField] private Slider _sliderCols;

    private int _levelPlayer;

    public void SetLevelPlayer(int value)
    {
        _levelPlayer = value;
        if (_levelPlayer >= 0)
        {
            _sliderRows.maxValue = 17;
            _sliderCols.maxValue = 17;
        }
        if (_levelPlayer >= 20)
        {
            _sliderRows.maxValue = 27;
            _sliderCols.maxValue = 27;
        }
        if (_levelPlayer >= 30)
        {
            _sliderRows.maxValue = 37;
            _sliderCols.maxValue = 37;
        }
        if (_levelPlayer >= 40)
        {
            _sliderRows.maxValue = 55;
            _sliderCols.maxValue = 55;
        }
    }
}
