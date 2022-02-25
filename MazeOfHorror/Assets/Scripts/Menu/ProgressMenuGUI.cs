using UnityEngine;
using UnityEngine.UI;

public class ProgressMenuGUI : MonoBehaviour
{

    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private SaveLoadGame _saveLoadGame;
    [SerializeField] private MenuController _menuController;
    [SerializeField] private PlaySound _playSound;
    [SerializeField] private GameObject _windowPrice;
    [SerializeField] private GameObject _ramkaNatura;
    [SerializeField] private GameObject _ramkaDino;
    [SerializeField] private GameObject _ramkaCastle;
    [SerializeField] private GameObject _buttonShop_1;
    [SerializeField] private GameObject _buttonShop_2;
    [SerializeField] private GameObject _buttonShop_3;
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

    private int _dinoBuy;
    private int _castleBuy;
    private int _selectNatura;
    private int _selectDino;
    private int _selectCastle;

    private int _priceLevel;
    private int _priceGold;
    private int _priceDimond;
    private int _changeResurce;

    private void Start()
    {
        _changeResurce = 0;
        UpdateDataBase();
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

    public void ClickLocation()
    {
        _selectNatura = _saveLoadGame.selectNatura;
        _selectDino = _saveLoadGame.selectDino;
        _selectCastle = _saveLoadGame.selectCastle;
        if (_selectNatura == 1) _ramkaNatura.SetActive(true);
        else _ramkaNatura.SetActive(false);
        if (_selectDino == 1) _ramkaDino.SetActive(true);
        else _ramkaDino.SetActive(false);
        if (_selectCastle == 1) _ramkaCastle.SetActive(true);
        else _ramkaCastle.SetActive(false);
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
        _progressLevel = _saveProgress.ProgressLevel;
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
        _dinoBuy = _saveProgress.DinoBuy;
        _castleBuy = _saveProgress.CastleBuy;
        _selectNatura = _saveLoadGame.selectNatura;
        _selectDino = _saveLoadGame.selectDino;
        _selectCastle = _saveLoadGame.selectCastle;
        if (_selectNatura == 1) _ramkaNatura.SetActive(true);
        else _ramkaNatura.SetActive(false);
        if (_selectDino == 1) _ramkaDino.SetActive(true);
        else _ramkaDino.SetActive(false);
        if (_selectCastle == 1) _ramkaCastle.SetActive(true);
        else _ramkaCastle.SetActive(false);
    }

    public void SetDataBaseWin(int gold, int dimond, int exp)
    {
        _amountGold += gold;
        _amountDimond += dimond;
        SetProgressLevel(exp);
        _saveProgress.SetGold(_amountGold);
        _saveProgress.SetDimond(_amountDimond);
        _saveProgress.SetLevel(_levelPlayer);
        _saveProgress.SetProgressLevel(_progressLevel);
        _saveProgress.Save();
        _menuController.ExitInMenu();
    }


    public void SetProgressLevel(int value)
    {
        int endProgressLevel = 50 + _levelPlayer * 30;
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
        _windowPrice.SetActive(true);
        if (vertical == 1)
        {
            if (horizontal == 1)
            {
                temp = _speedPlayer;
                if (_speedPlayer == 10)
                {
                    _windowPrice.SetActive(false);
                }
            }
            else if (horizontal == 2)
            {
                temp = _powerPlayer;
                if (_powerPlayer == 10)
                {
                    _windowPrice.SetActive(false);
                }
            }
            else if (horizontal == 3)
            {
                temp = _mannaPlayer;
                if (_mannaPlayer == 10)
                {
                    _windowPrice.SetActive(false);
                }
            }
            _priceLevel = CalculationPriceForSpeedPowerManna(temp)[0];
            _priceGold = CalculationPriceForSpeedPowerManna(temp)[1];
            _priceDimond = CalculationPriceForSpeedPowerManna(temp)[2];
            if (horizontal == 4)
            {
                if (_lifePlayer == 10)
                {
                    _windowPrice.SetActive(false);
                }
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
                if (_amountMovetWoll == 10)
                {
                    _windowPrice.SetActive(false);
                }
            }
            else if (horizontal == 2)
            {
                temp = _amountPassage;
                if (_amountPassage == 10)
                {
                    _windowPrice.SetActive(false);
                }
            }
            else if (horizontal == 3)
            {
                temp = _amountPort;
                if (_amountPort == 10)
                {
                    _windowPrice.SetActive(false);
                }
            }
            else if (horizontal == 4)
            {
                temp = _amountImmunity;
                if (_amountImmunity == 10)
                {
                    _windowPrice.SetActive(false);
                }
            }
            _priceLevel = CalculationPriceMagic(temp, horizontal)[0];
            _priceGold = CalculationPriceMagic(temp, horizontal)[1];
            _priceDimond = CalculationPriceMagic(temp, horizontal)[2];
        }
        else if (vertical == 3)
        {
            if (horizontal == 1)
            {
                _windowPrice.SetActive(false);
            }
            if (horizontal == 2)
            {
                if (_dinoBuy == 1)
                {
                    _windowPrice.SetActive(false);
                }
                _priceGold = 200;
                _priceDimond = 20;
            }
            if (horizontal == 3)
            {
                if (_castleBuy == 1)
                {
                    _windowPrice.SetActive(false);
                }
                _priceGold = 500;
                _priceDimond = 50;
            }
            _priceLevel = 0;
        }
    }

    public void Buy()
    {
        if (_priceLevel <= _levelPlayer && _priceGold <= _amountGold && _priceDimond <= _amountDimond)
        {
            _playSound.Play("Okey");
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
                    if (_amountPassage > 0)
                    {
                        _amountPort++;
                        _saveProgress.SetPort(_amountPort);
                        _amountUpdatePort.SetColorRect(_amountPort);
                        SetPrice(23);
                    }
                    break;
                case (24):
                    if (_amountPort > 0)
                    {
                        _amountImmunity++;
                        _saveProgress.SetImmuny(_amountImmunity);
                        _amountUpdateImmunity.SetColorRect(_amountImmunity);
                        SetPrice(24);
                    }
                    break;

                case (32):
                    _dinoBuy = 1;
                    _saveProgress.SetDino(1);
                    SetPrice(32);
                    break;
                case (33):
                    _castleBuy = 1;
                    _saveProgress.SetCastle(1);
                    SetPrice(33);
                    break;

                default:
                    break;
            }
        }
        else
        {
            _playSound.Play("Error");
        }
    }

    public void SelectNatura()
    {
        if (_selectNatura == 0)
        {
            _selectNatura = 1;
            _ramkaNatura.SetActive(true);
            _saveLoadGame.selectNatura = 1;
        }
        else
        {
            _selectNatura = 0;
            _ramkaNatura.SetActive(false);
            _saveLoadGame.selectNatura = 0;
        }
    }

    public void SelectDinopark()
    {
        if (_selectDino == 0)
        {
            _selectDino = 1;
            _ramkaDino.SetActive(true);
            _saveLoadGame.selectDino = 1;
        }
        else
        {
            _selectDino = 0;
            _ramkaDino.SetActive(false);
            _saveLoadGame.selectDino = 0;
        }
    }

    public void SelectCastle()
    {
        if (_selectCastle == 0)
        {
            _selectCastle = 1;
            _ramkaCastle.SetActive(true);
            _saveLoadGame.selectCastle = 1;
        }
        else
        {
            _selectCastle = 0;
            _ramkaCastle.SetActive(false);
            _saveLoadGame.selectCastle = 0;
        }
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
            _buttonShop_1.SetActive(true);
        }
        else
        {
            _textPriceLevel.color = Color.white;
            _buttonShop_1.SetActive(false);
        }

            _textPriceGold.text = $"{_priceGold}";
        if (_priceGold > _amountGold)
        {
            _textPriceGold.color = Color.red;
            _buttonShop_2.SetActive(true);
        }
        else
        {
            _textPriceGold.color = Color.white;
            _buttonShop_2.SetActive(false);
        }

            _textPriceDimond.text = $"{_priceDimond}";
        if (_priceDimond > _amountDimond)
        {
            _textPriceDimond.color = Color.red;
            _buttonShop_3.SetActive(true);
        }
        else
        {
            _textPriceDimond.color = Color.white;
            _buttonShop_3.SetActive(false);
        }
    }
}
