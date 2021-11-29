using UnityEngine;
using UnityEngine.UI;

public class LosMenu : MonoBehaviour
{
    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private GameControler _gameControler;
    [SerializeField] private Text _textPrice;
    [SerializeField] private Button _buttonShop;

    private int _amountDimond;

    private const int priceDimond = 5;



    public void SetDimond()
    {
        _amountDimond = _saveProgress.AmountDimond;
        _textPrice.text = $"{priceDimond}";
        if (_amountDimond < priceDimond)
        {
            _textPrice.color = Color.red;
            _buttonShop.enabled = false;
        }
        else
        {
            _textPrice.color = Color.white;
            _buttonShop.enabled = true;
        }
    }

    public void Buy()
    {
        _amountDimond -= priceDimond;
        _saveProgress.SetDimond(_amountDimond);
        _gameControler.GameAfterLose();
    }

    public void ADS()
    {
        _gameControler.GameAfterLose();
    }
}
