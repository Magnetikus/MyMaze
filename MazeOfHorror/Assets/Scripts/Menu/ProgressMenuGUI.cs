using UnityEngine;
using UnityEngine.UI;

public class ProgressMenuGUI : MonoBehaviour
{

    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private LoadResurces _loadResurces;
    [SerializeField] private Text _textLevel;
    [SerializeField] private Text _textLife;
    [SerializeField] private Text _textGold;
    [SerializeField] private Text _textDimond;

    [SerializeField] private Text _textPriceLevel;
    [SerializeField] private Text _textPriceGold;
    [SerializeField] private Text _textPriceDimond;

    [SerializeField] private AmountUpdate _amountUpdateSpeed;
    [SerializeField] private AmountUpdate _amountUpdatePower;
    [SerializeField] private AmountUpdate _amountUpdateManna;
    [SerializeField] private AmountUpdate _amountUpdateLife;

    [SerializeField] private AmountUpdate _amountUpdateMovetWoll;
    [SerializeField] private AmountUpdate _amountUpdatePassage;
    [SerializeField] private AmountUpdate _amountUpdatePort;
    [SerializeField] private AmountUpdate _amountUpdateImmunity;

    private int _levelPlayer;
    private int _progressLevel;
    private int _amountGold;
    private int _amountDimond;

    private int _speedPlayer;
    private int _powerPlayer;
    private int _mannaPlayer;
    private int _lifePlayer;

    private int _amountMovetWoll;
    private int _amountPassage;
    private int _amountPort;
    private int _amountImmunity;

    private int _priceLevel;
    private int _priceGold;
    private int _priceDimond;
    private int _changeResurce;

    private void Start()
    {
        _changeResurce = 0;
    }

    public int GetLevel()
    {
        return _levelPlayer;
    }

    public void SetColorResurce(AmountUpdate nameAmount, int numberResurce)
    {
        for (int i = 0; i <= numberResurce; i++)
        {
            nameAmount.SetColorRect(i);
        }
    }

    private void ResetColorRect(AmountUpdate nameAmount)
    {
        nameAmount.ResetColor();
    }

    public void ClickHero()
    {
        Invoke("FromClickHero", 0.5f);
    }

    public void ClickMagic()
    {
        Invoke("FromClickMagic", 0.5f);
    }

    public void FromClickHero()
    {
        ResetColorRect(_amountUpdateSpeed);
        ResetColorRect(_amountUpdatePower);
        ResetColorRect(_amountUpdateManna);
        ResetColorRect(_amountUpdateLife);
        SetColorResurce(_amountUpdateSpeed, _speedPlayer);
        SetColorResurce(_amountUpdatePower, _powerPlayer);
        SetColorResurce(_amountUpdateManna, _mannaPlayer);
        SetColorResurce(_amountUpdateLife, _lifePlayer);
    }

    public void FromClickMagic()
    {
        ResetColorRect(_amountUpdateMovetWoll);
        ResetColorRect(_amountUpdatePassage);
        ResetColorRect(_amountUpdatePort);
        ResetColorRect(_amountUpdateImmunity);
        SetColorResurce(_amountUpdateMovetWoll, _amountMovetWoll);
        SetColorResurce(_amountUpdatePassage, _amountPassage);
        SetColorResurce(_amountUpdatePort, _amountPort);
        SetColorResurce(_amountUpdateImmunity, _amountImmunity);
    }

    public void UpdateDataBase()
    {
        _levelPlayer = _saveProgress.LevelPlayer;
        _lifePlayer = _saveProgress.LifePlayer;
        _amountGold = _saveProgress.AmountGold;
        _amountDimond = _saveProgress.AmountDimond;
        _speedPlayer = _saveProgress.SpeedPlayer;
        _powerPlayer = _saveProgress.PowerPlayer;
        _mannaPlayer = _saveProgress.MannaPlayer;
        _amountMovetWoll = _saveProgress.AmountMovetWoll;
        _amountPassage = _saveProgress.AmountPassage;
        _amountPort = _saveProgress.AmountPort;
        _amountImmunity = _saveProgress.AmountImmunity;
    }

    public void SetDataBaseWin(int gold, int dimond, int exp)
    {
        _amountGold += gold;
        _amountDimond += dimond;
        SetProgressLevel(exp);
        _saveProgress.SetGold(_amountGold);
        _saveProgress.SetDimond(_amountDimond);
        _saveProgress.SetLevel(_levelPlayer);
        _saveProgress.Save();
    }


    public void SetProgressLevel(int value)
    {
        int endProgressLevel = (_levelPlayer + 1) * 100;
        if (_progressLevel + value >= endProgressLevel)
        {
            int toEnd = endProgressLevel - _progressLevel;
            int surplus = value - toEnd;
            _levelPlayer++;
            _progressLevel = 0;
            SetProgressLevel(surplus);
        }
        else
        {
            _progressLevel += value;
        }
    }

    private int[] CalculationPriceForSpeedPowerManna(int nameValue)
    {
        int[] arrayLevelGoldDimond = new int[3];
        int startGold = 10;
        int startDimond = 1;
        if (nameValue < 3)
        {
            arrayLevelGoldDimond[0] = 0;
        }
        else if (nameValue < 6)
        {
            arrayLevelGoldDimond[0] = 5;
        }
        else if (nameValue < 9)
        {
            arrayLevelGoldDimond[0] = 10;
        }
        else
        {
            arrayLevelGoldDimond[0] = 15;
        }
        arrayLevelGoldDimond[1] = (nameValue + 1) * (nameValue + 1) * startGold;
        arrayLevelGoldDimond[2] = (nameValue + 1) * (nameValue + 1) * startDimond;
        return arrayLevelGoldDimond;
    }

    private int[] CalculationPriceForLife()
    {
        int[] arrayLevelGoldDimond = new int[3];
        if (_lifePlayer < 6)
        {
            arrayLevelGoldDimond[0] = 10;
            arrayLevelGoldDimond[1] = 200;
            arrayLevelGoldDimond[2] = 20;
        }
        else if (_lifePlayer < 9)
        {
            arrayLevelGoldDimond[0] = 20;
            arrayLevelGoldDimond[1] = 500;
            arrayLevelGoldDimond[2] = 50;
        }
        else
        {
            arrayLevelGoldDimond[0] = 30;
            arrayLevelGoldDimond[1] = 1000;
            arrayLevelGoldDimond[2] = 100;
        }
        return arrayLevelGoldDimond;
    }

    private int[] CalculationPriceMagic(int nameValue, int numberString)
    {
        int[] array = new int[3];
        int startLevel = 0;
        if (nameValue < 6)
        {
            array[0] = startLevel + 5 * (numberString - 1);
        }
        else
        {
            array[0] = startLevel + 5 + 5 * (numberString - 1);
        }
        array[1] = 1000;
        array[2] = 100;
        return array;
    }

    public void SetChangeResurce(int value)
    {
        _changeResurce = value;
    }

    public void SetPrice(int value)
    {
        SetChangeResurce(value);
        int vertical = value / 10;
        int horizontal = value % 10;
        int temp = 0;
        if (vertical == 1)
        {
            if (horizontal == 1)
            {
                temp = _speedPlayer;
            }
            else if (horizontal == 2)
            {
                temp = _powerPlayer;
            }
            else if (horizontal == 3)
            {
                temp = _mannaPlayer;
            }
            _priceLevel = CalculationPriceForSpeedPowerManna(temp)[0];
            _priceGold = CalculationPriceForSpeedPowerManna(temp)[1];
            _priceDimond = CalculationPriceForSpeedPowerManna(temp)[2];
            if (horizontal == 4)
            {
                _priceLevel = CalculationPriceForLife()[0];
                _priceGold = CalculationPriceForLife()[1];
                _priceDimond = CalculationPriceForLife()[2];
            }
        }
        else if (vertical == 2)
        {
            if (horizontal == 1)
            {
                temp = _amountMovetWoll;
            }
            else if (horizontal == 2)
            {
                temp = _amountPassage;
            }
            else if (horizontal == 3)
            {
                temp = _amountPort;
            }
            else if (horizontal == 4)
            {
                temp = _amountImmunity;
            }
            _priceLevel = CalculationPriceMagic(temp, horizontal)[0];
            _priceGold = CalculationPriceMagic(temp, horizontal)[1];
            _priceDimond = CalculationPriceMagic(temp, horizontal)[2];
        }
    }

    public void Buy()
    {
        if (_priceLevel <= _levelPlayer && _priceGold <= _amountGold && _priceDimond <= _amountDimond)
        {
            _amountGold -= _priceGold;
            _amountDimond -= _priceDimond;
            switch (_changeResurce)
            {
                case (11):
                    _speedPlayer++;
                    _saveProgress.SetSpeed(_speedPlayer);
                    _amountUpdateSpeed.SetColorRect(_speedPlayer);
                    SetPrice(11);
                    break;
                case (12):
                    _powerPlayer++;
                    _saveProgress.SetPower(_powerPlayer);
                    _amountUpdatePower.SetColorRect(_powerPlayer);
                    SetPrice(12);
                    break;
                case (13):
                    _mannaPlayer++;
                    _saveProgress.SetManna(_mannaPlayer);
                    _amountUpdateManna.SetColorRect(_mannaPlayer);
                    SetPrice(13);
                    break;
                case (14):
                    _lifePlayer++;
                    _saveProgress.SetLife(_lifePlayer);
                    _amountUpdateLife.SetColorRect(_lifePlayer);
                    SetPrice(14);
                    break;
                case (21):
                    _amountMovetWoll++;
                    _saveProgress.SetWoll(_amountMovetWoll);
                    _amountUpdateMovetWoll.SetColorRect(_amountMovetWoll);
                    SetPrice(21);
                    break;
                case (22):
                    _amountPassage++;
                    _saveProgress.SetPass(_amountPassage);
                    _amountUpdatePassage.SetColorRect(_amountPassage);
                    SetPrice(22);
                    break;
                case (23):
                    _amountPort++;
                    _saveProgress.SetPort(_amountPort);
                    _amountUpdatePort.SetColorRect(_amountPort);
                    SetPrice(23);
                    break;
                case (24):
                    _amountImmunity++;
                    _saveProgress.SetImmuny(_amountImmunity);
                    _amountUpdateImmunity.SetColorRect(_amountImmunity);
                    SetPrice(24);
                    break;

                default:
                    break;
            }

        }
    }

    public void LocationCastle()
    {
        _loadResurces.SetLocation(1);
    }

    public void LocationNature()
    {
        _loadResurces.SetLocation(2);
    }

    public void LocationSpace()
    {
        _loadResurces.SetLocation(3);
    }

    private void OnGUI()
    {
        _textLevel.text = $"{_levelPlayer}";
        _textLife.text = $"{_lifePlayer}";
        _textGold.text = $"{_amountGold}";
        _textDimond.text = $"{_amountDimond}";

        _textPriceLevel.text = $"{_priceLevel}";
        if (_priceLevel > _levelPlayer)
        {
            _textPriceLevel.color = Color.red;
        }
        else _textPriceLevel.color = Color.white;

        _textPriceGold.text = $"{_priceGold}";
        if (_priceGold > _amountGold)
        {
            _textPriceGold.color = Color.red;
        }
        else _textPriceGold.color = Color.white;

        _textPriceDimond.text = $"{_priceDimond}";
        if (_priceDimond > _amountDimond)
        {
            _textPriceDimond.color = Color.red;
        }
        else _textPriceDimond.color = Color.white;
    }
}
