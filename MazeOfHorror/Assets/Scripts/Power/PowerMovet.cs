using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PowerMovet : MonoBehaviour
{
    private MovetCube _movetCube;
    [SerializeField] private Button _button;
    [SerializeField] private Image _imagePower;
    private float _timeRevers = 100f;

    private void Start()
    {
        _movetCube = GameObject.FindGameObjectWithTag("Player").GetComponent<MovetCube>();
    }

    public void SetTimeRevers(int mannaPlayer)
    {
        _timeRevers -= mannaPlayer * 5f;
    }

    public void CubeMovetFirstStep()
    {
        _movetCube.CubeMovetFirstStep();
        Invoke("ButtonDesactiv", 0.5f);
    }

    public void CubeDesactive()
    {
        _movetCube.CubeDesactive();
        
    }

    public void CubeActive()
    {
        _movetCube.CubeActive();
        _imagePower.fillAmount = 0;
        StartCoroutine(FillImage());
    }

    public void EscapeMovet()
    {
        _movetCube.EscapeMovet();
        _button.enabled = true;
    }

    public void ButtonDesactiv()
    {
        _button.enabled = false;
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
