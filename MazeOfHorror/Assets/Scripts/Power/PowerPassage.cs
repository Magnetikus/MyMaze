using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerPassage : MonoBehaviour
{
    private Passage _passage;
    [SerializeField] private GameControler _gameController;
    [SerializeField] private Button _button;
    [SerializeField] private Image _imagePower;
    private float _timeRevers = 60f;

    private void Start()
    {
        _passage = GameObject.FindGameObjectWithTag("Player").GetComponent<Passage>();
    }

    public void SetTimeRevers(int mannaPlayer)
    {
        _timeRevers -= mannaPlayer * 4f;
    }

    public void PassageFirstStep()
    {
        _passage.PassageFirstStep();
        _gameController.IsVisibleIconesPower(false);
    }

    public void PassageActivate()
    {
        _passage.PassageActivate();
        _gameController.IsVisibleIconesPower(true);
        _button.enabled = false;
        _imagePower.fillAmount = 0;
        StartCoroutine(FillImage());
    }


    public void EscapePassage()
    {
        _gameController.IsVisibleIconesPower(true);
        _passage.PassageEnd();
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
        ChekButton();
    }

    public void ChekButton()
    {
        if (_gameController.GetPassage() > 0)
        {
            _button.enabled = true;
        }
        else _button.enabled = false;
    }

    public void StopCourotine()
    {
        StopCoroutine(FillImage());
    }
}
