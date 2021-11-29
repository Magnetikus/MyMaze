using UnityEngine;
using UnityEngine.UI;

public class MenuStartRandomGame : MonoBehaviour
{
    [SerializeField] private Button _buttonMedium;
    [SerializeField] private Image _imageMedium;
    [SerializeField] private GameObject _textMedium;
    [SerializeField] private Button _buttonHard;
    [SerializeField] private Image _imageHard;
    [SerializeField] private GameObject _textHard;
    [SerializeField] private Button _buttonCrazy;
    [SerializeField] private Image _imageCrazy;
    [SerializeField] private GameObject _textCrazy;

    private int _levelPlayer;

    public void SetDB(int value)
    {
        _levelPlayer = value;
        AllFalse();
        if (_levelPlayer >= 10)
        {
            _buttonMedium.enabled = true;
            _imageMedium.enabled = false;
        }
        else
        {
            _buttonMedium.enabled = false;
            _imageMedium.enabled = true;
        }

        if (_levelPlayer >= 20)
        {
            _buttonHard.enabled = true;
            _imageHard.enabled = false;
        }
        else
        {
            _buttonHard.enabled = false;
            _imageHard.enabled = true;
        }

        if (_levelPlayer >= 30)
        {
            _buttonCrazy.enabled = true;
            _imageCrazy.enabled = false;
        }
        else
        {
            _buttonCrazy.enabled = false;
            _imageCrazy.enabled = true;
        }
    }

    public void ClickMedium()
    {
        AllFalse();
        _textMedium.SetActive(true);
    }

    public void ClickHard()
    {
        AllFalse();
        _textHard.SetActive(true);
    }

    public void ClickCrazy()
    {
        AllFalse();
        _textCrazy.SetActive(true);
    }

    public void AllFalse()
    {
        _textMedium.SetActive(false);
        _textHard.SetActive(false);
        _textCrazy.SetActive(false);
    }

}
