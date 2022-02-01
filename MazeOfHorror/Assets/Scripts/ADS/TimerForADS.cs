using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerForADS : MonoBehaviour
{
    [SerializeField] private Text _textTimer;
    [SerializeField] private GameObject _timer;

    private Button _buttonADS;
    private Image _imageButton;
    private float _time;
    private bool _isRun;
    private int _minutes;
    private int _seconds;

    private const int startSeconds = 120;

    private void Start()
    {
        _imageButton = GetComponent<Image>();
        _buttonADS = GetComponent<Button>();
    }

    public void TimerIsRun()
    {
        _buttonADS.enabled = false;
        _timer.SetActive(true);
        _isRun = true;
        _time = 0;
        _seconds = startSeconds;
        _imageButton.fillAmount = 0;
        StartCoroutine(FillImage());
    }

    private void Timer()
    {
        _time += Time.deltaTime;
        if (_time >= 1)
        {
            _seconds--;
            _time = 0;
            _minutes = _seconds / 60;

            _textTimer.text = $"{_minutes:d2} : {_seconds % 60:d2}";
            
        }
        
        if (_seconds <= 0)
        {
            _isRun = false;
            EndTimer();
        }
    }

    private IEnumerator FillImage()
    {
        var wait = new WaitForSeconds(startSeconds / 100);
        
        while (_imageButton.fillAmount < 1)
        {
            _imageButton.fillAmount += 0.01f;
            yield return wait;
        }
    }

    private void EndTimer()
    {
        _buttonADS.enabled = true;
        _timer.SetActive(false);
    }

    private void Update()
    {
        if (_isRun) Timer();
    }
}
