using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerImmuny : MonoBehaviour
{
    [SerializeField] private GameControler _gameController;
    [SerializeField] private Button _button;
    [SerializeField] private Image _imagePower;
    private float _timeRevers = 60f;

    public void SetTimeRevers(int mannaPlayer)
    {
        _timeRevers -= mannaPlayer * 4f;
    }

    public void StartHurricane()
    {
        _button.enabled = false;
        _gameController.IsVisibleIconesPower(false);
    }

    public void ActivateHurricane()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Immuny>().StartEffect();
        _gameController.GetComponent<ConstructorMaze>().TransformingMonstersAffterMinusLifePlayer(_gameController.GetComponent<SaveProgress>().PowerPlayer);
        _gameController.IsVisibleIconesPower(true);
        _button.enabled = false;
        _imagePower.fillAmount = 0;
        StartCoroutine(FillImage());
    }

    public void EscapeHurricane()
    {
        _gameController.IsVisibleIconesPower(true);
        ChekButton();
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
        if (_gameController.GetInvisy() > 0)
        {
            _button.enabled = true;
        }
        else _button.enabled = false;
    }
}
