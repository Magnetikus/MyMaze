using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerMovet : MonoBehaviour
{
    private MovetCube _movetCube;
    [SerializeField] private Image _imagePower;
    [SerializeField] private Button _button;
    [SerializeField] private GameControler _gameController;
    [SerializeField] private GameObject _cubeDeactive;
    [SerializeField] private GameObject _cubeActive;
    [SerializeField] private GameObject _buttonCancel;
    private float _timeRevers = 60f;

    private void Start()
    {
        _movetCube = GameObject.FindGameObjectWithTag("Player").GetComponent<MovetCube>();
    }

    public void SetTimeRevers(int mannaPlayer)
    {
        _timeRevers -= mannaPlayer * 4f;
    }


    public void CubeMovetFirstStep()
    {

        _movetCube.CubeMovetFirstStep();
        _gameController.IsVisibleIconesPower(false);
    }

    public void CubeDesactive()
    {
        _movetCube.CubeDesactive();
    }

    public void CubeActive()
    {
        _movetCube.CubeActive();
        _gameController.IsVisibleIconesPower(true);
        _button.enabled = false;
        _imagePower.fillAmount = 0;
        StartCoroutine(FillImage());
    }

    public void EscapeMovet()
    {
        _movetCube.EscapeMovet();
        _gameController.IsVisibleIconesPower(true);
    }

    public void AfterMinusLife()
    {
        _cubeActive.SetActive(false);
        _cubeDeactive.SetActive(false);
        _buttonCancel.SetActive(false);
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
        if (_gameController.GetCube() > 0)
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
