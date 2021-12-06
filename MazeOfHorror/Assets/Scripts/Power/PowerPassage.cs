using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPassage : MonoBehaviour
{
    private Passage _passage;
    [SerializeField] private Button _button;
    [SerializeField] private Image _imagePower;
    private float _timeRevers = 100f;

    private void Start()
    {
        _passage = GameObject.FindGameObjectWithTag("Player").GetComponent<Passage>();
    }

    public void SetTimeRevers(int mannaPlayer)
    {
        _timeRevers -= mannaPlayer * 5f;
    }

    public void PassageFirstStep()
    {
        _passage.PassageFirstStep();
        Invoke("ButtonDesactiv", 0.5f);
    }

    public void PassageActivate()
    {
        _passage.PassageActivate();
        _imagePower.fillAmount = 0;
        StartCoroutine(FillImage());
    }

    public void ButtonDesactiv()
    {
        _button.enabled = false;

    }

    public void EscapePassage()
    {
        _button.enabled = true;
        _passage.PassageEnd();
    }

    public void StartRevers()
    {
        StartCoroutine(FillImage());
    }

    private IEnumerator FillImage()
    {
        var wait = new WaitForSeconds(_timeRevers / 100);
        Color color = _imagePower.color;
        color.a = 0.5f;
        _imagePower.color = color;
        while (_imagePower.fillAmount < 1)
        {
            _imagePower.fillAmount += 0.01f;
            yield return wait;
        }
        color.a = 1f;
        _imagePower.color = color;
        _button.enabled = true;
    }

}
