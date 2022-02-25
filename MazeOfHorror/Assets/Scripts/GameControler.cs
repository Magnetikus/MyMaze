using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{

    private ConstructorMaze _generator;
    private GameObject _player;

    private int _sizeRowsX;
    private int _sizeColsZ;
    private int _lifes;
    private int _keys, _startKeys;
    private int _gold, _startGold;
    private int _treasure;
    private int _cubes;
    private int _passage;
    private int _port;
    private int _invisy;
    private float _gameTime;
    private int _seconds;
    private int _minutes;
    private int _amountIconesPower;
    private string _nameMusic;

    private int _EXP = 30;

    [SerializeField] private MenuController _menuContr;
    [SerializeField] private SaveProgress _saveProgress;
    [SerializeField] private PlaySound _playMusic;
    [SerializeField] private PlaySound _playSound;
    [SerializeField] private WinMenu _winMenu;
    [SerializeField] private GameObject[] _iconesPower;
    [SerializeField] private GameObject _menuBackground;
    [SerializeField] private GameObject _joystick;
    [SerializeField] private GameObject _joystickLooket;
    [SerializeField] private GameObject _iconePower;
    [SerializeField] private GameObject _escapeFromMaze;
    [SerializeField] private AmountHeartOut _heartOut;
    [SerializeField] private AmountHeartOut _keyOut;
    [SerializeField] private GameObject _minusLifeImage;
    [SerializeField] private GameObject[] _heartsMinusLife = new GameObject[10];
    [SerializeField] private Image _treasureOut;
    [SerializeField] private Text _textGoldStart;
    [SerializeField] private Text _textGold;
    [SerializeField] private Text _textCubes;
    [SerializeField] private Text _textPassage;
    [SerializeField] private Text _textPort;
    [SerializeField] private Text _textInvisy;
    [SerializeField] private Text _textTimes;
    [SerializeField] private GameObject _imageFindKey;
    [SerializeField] private Text _textFindKey;
    [SerializeField] private GameObject _mimimap;

    public InterAd interAd;
    public bool pausedGame { get; private set; }

    private void Start()
    {
        _generator = GetComponent<ConstructorMaze>();
        _nameMusic = "Nature1";
    }


    public void StartNewGame(List<int> constrMaze)
    {
        _generator.GenerateNewMaze(constrMaze);
        _sizeRowsX = constrMaze[0];
        _sizeColsZ = constrMaze[1];
        _startKeys = constrMaze[2];
        _startGold = (_sizeRowsX + _sizeColsZ) / 2;
        ResetDataBase();
        interAd.ShowAd();
    }

    public void PausedGame(bool chek)
    {
        pausedGame = chek;
    }


    public void RestartGame()
    {
        ResetDataBase();
        _generator.Restart();
    }

    private void ResetDataBase()
    {
        _playMusic.Play(_nameMusic);
        _player = GameObject.FindGameObjectWithTag("Player");
        _player.GetComponent<MovetCube>().ResetEndGame();
        _player.GetComponent<Passage>().PassageEnd();
        _menuBackground.SetActive(false);
        _mimimap.SetActive(true);
        _joystick.SetActive(true);
        _joystick.GetComponent<JoystickController>().Zero();
        _joystick.GetComponent<JoystickController>()._isNoInGame = true;
        _joystickLooket.SetActive(true);
        _joystickLooket.GetComponent<JoystickLooket>().Zero();
        _iconePower.SetActive(true);
        _iconePower.GetComponent<PowerMovet>().SetTimeRevers(_saveProgress.MannaPlayer);
        _iconePower.GetComponent<PowerPassage>().SetTimeRevers(_saveProgress.MannaPlayer);
        _iconePower.GetComponent<PowerPort>().SetTimeRevers(_saveProgress.MannaPlayer);
        _iconePower.GetComponent<PowerImmuny>().SetTimeRevers(_saveProgress.MannaPlayer);
        _seconds = 0;
        _minutes = 0;
        _lifes = _saveProgress.LifePlayer;
        for (int i = 1; i <= _lifes; i++)
        {
            _heartOut.SetColorHeart(i, 1);
        }
        _keys = _startKeys;
        for (int i = 1; i <= _keys; i++)
        {
            _keyOut.SetColorHeart(i, 0.5f);
        }
        _gold = 0;
        
        _cubes = _saveProgress.AmountMovetWoll;
        _passage = _saveProgress.AmountPassage;
        _port = _saveProgress.AmountPort;
        _invisy = _saveProgress.AmountImmunity;
        if (_invisy > 0)
        {
            _amountIconesPower = 4;
        }
        else if (_port > 0)
        {
            _amountIconesPower = 3;
        }
        else if (_passage > 0)
        {
            _amountIconesPower = 2;
        }
        else
        {
            _amountIconesPower = 1;
        }
        IsVisibleIconesPower(true);
        foreach (var e in _iconesPower)
        {
            ImageFillIn100(e);
        }
        _treasure = 0;
        Color colorTreasure = _treasureOut.color;
        colorTreasure.a = 0.4f;
        _treasureOut.color = colorTreasure;
        _textGoldStart.color = new Color(250, 39, 13);
        _textGold.color = Color.white;
        //_iconePower.GetComponent<PowerMovet>().AfterMinusLife();
        //_iconePower.GetComponent<PowerPort>().ResetAfterMinusLife();
        //_iconePower.GetComponent<PowerPassage>().ChekButton();
        //_player.GetComponent<MovetCube>().ResetFromMinusLife();
        //_player.GetComponent<Passage>().ResetFromMinusLife();
        pausedGame = true;
    }

    private void ImageFillIn100(GameObject go)
    {
        go.GetComponent<Button>().enabled = true;
        Image[] listImage = go.GetComponentsInChildren<Image>();
        foreach (var e in listImage)
        {
            e.fillAmount = 1f;
        }
    }

    public void IsVisibleIconesPower(bool chek)
    {
        for (int i = 0; i < _amountIconesPower; i++)
        {
            _iconesPower[i].SetActive(chek);
        }
    }

    public int[] GetSizeMaze()
    {
        int[] array = { _sizeRowsX, _sizeColsZ };
        return array;
    }

    private void OnGUI()
    {
        _textGoldStart.text = $"( {_startGold} )";
        _textGold.text = $" {_gold} ";
        _textCubes.text = $" {_cubes} ";
        _textPassage.text = $"{_passage}";
        _textPort.text = $"{_port}";
        _textInvisy.text = $"{_invisy}";
        _textTimes.text = $" {_minutes:d2} : {_seconds:d2} ";
    }

    public void SetLocation(int value)
    {
        switch (value)
        {
            case (1):
                _nameMusic = Random.value == 0.3f ? "Nature1" : Random.value == 0.5f ? "Nature2" : "Nature3";
                break;
            case (2):
                _nameMusic = Random.value == 0.3f ? "Dino1" : Random.value == 0.5f ? "Dino2" : "Dino3";
                break;
            case (3):
                _nameMusic = Random.value == 0.3f ? "Castle1" : Random.value == 0.5f ? "Castle2" : "Castle3";
                break;
            default:
                _nameMusic = "Nature1";
                break;
        }
    }

    public void PlayerEnterExit()
    {
        if (_keys == 0)
        {
            _escapeFromMaze.SetActive(true);
        }
        else
        {
            _imageFindKey.SetActive(true);
            _textFindKey.text = $"{_keys}";
        }
    }

    public void PlayerExitExit()
    {
        if (_keys == 0)
        {
            _escapeFromMaze.SetActive(false);
        }
        else
        {
            _imageFindKey.SetActive(false);
        }
    }

    public void MinusLifes()
    {
        
        pausedGame = true;
        _minusLifeImage.SetActive(true);
        _minusLifeImage.GetComponent<Animator>().enabled = true;
        for (int i = 0; i < _lifes; i++)
        {
            _heartsMinusLife[i].SetActive(true);
        }
    }

    public void EndLifePanelOut()
    {
        _heartsMinusLife[_lifes - 1].GetComponent<Animator>().enabled = true;
    }

    public void EndMinusLife()
    {
        _heartsMinusLife[_lifes - 1].SetActive(false);
        _minusLifeImage.GetComponent<Animator>().enabled = false;
        _minusLifeImage.SetActive(false);
        _heartOut.SetColorHeart(_lifes, 0);
        _lifes--;
        if (_lifes < 0) _lifes = 0;
        if (_lifes == 0) PlayerLose();
        else
        {
            _generator.TransformingMonstersAffterMinusLifePlayer(0);
            pausedGame = false;
        }
        IsVisibleIconesPower(true);
        foreach (var e in _iconesPower)
        {
            ImageFillIn100(e);
        }
        _iconePower.GetComponent<PowerMovet>().AfterMinusLife();
        _iconePower.GetComponent<PowerPort>().ResetAfterMinusLife();
        _iconePower.GetComponent<PowerPassage>().ChekButton();
        _player.GetComponent<MovetCube>().ResetFromMinusLife();
        _player.GetComponent<Passage>().ResetFromMinusLife();
        
    }

    public void MinusKeys()
    {
        _keys--;
        if (_keys < 0) _keys = 0;

        _keyOut.SetColorHeart(_startKeys - _keys, 1);
        
        if (_keys == 0)
        {
            ParticleSystem part = GameObject.FindGameObjectWithTag("Exit").GetComponentInChildren<ParticleSystem>();
            ParticleSystem.MainModule main = part.main;
            main.startColor = Color.green;
        }
    }

    public int GetKeys()
    {
        return _keys;
    }

    public void MinusCube()
    {
        _cubes--;
        if (_cubes <= 0)
        {
            _cubes = 0;
        }
    }

    public int GetCube()
    {
        return _cubes;
    }

    public void MinusPassage()
    {
        _passage--;
        if (_passage <= 0)
        {
            _passage = 0;
        }
    }

    public int GetPassage()
    {
        return _passage;
    }

    public void MinusPort()
    {
        _port--;
        if (_port <= 0)
        {
            _port = 0;
        }
    }

    public int GetPort()
    {
        return _port;
    }

    public void MinusInvisy()
    {
        _invisy--;
        if (_invisy <= 0)
        {
            _invisy = 0;
        }
    }

    public int GetInvisy()
    {
        return _invisy;
    }

    public void MinusGold()
    {
        _gold++;
        if (_gold > _startGold) _gold = _startGold;
        if (_gold == _startGold)
        {
            _textGoldStart.color = Color.green;
            _textGold.color = Color.green;
        }
    }

    public void MapCollect()
    {
        _generator.CreateTreasure();
        SelectionObjectsWithTag("Movet");
        SelectionObjectsWithTag("NoMovet");
        SelectionObjectsWithTag("Cell");
    }

    private void SelectionObjectsWithTag(string tag)
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject e in array)
        {
            if (e.name != "Vision")
            {
                e.GetComponent<VisibleOnn>().OnSpriteMap();
            }
        }
    }

    public void TreasureCollection()
    {
        _treasure += 1;
        Color colorTreasure = _treasureOut.color;
        colorTreasure.a = 1f;
        _treasureOut.color = colorTreasure;
    }

    public void PlayerWin()
    {
        _playMusic.Stop();
        _playSound.Play("Aplomb");
        _mimimap.SetActive(false);
        _joystick.SetActive(false);
        _joystickLooket.SetActive(false);
        _iconePower.SetActive(false);
        interAd.ShowAd();
        pausedGame = true;
        _menuContr.Victory();
        _winMenu.SetTimeAndSize(_minutes, _seconds, _startGold);
        _winMenu.SetGoldResultate(_startGold, _gold);
        _winMenu.SetLifeResultate(_saveProgress.LifePlayer, _lifes);
        _winMenu.SetTreasure(_treasure);
        _winMenu.SetExp(_EXP);
        _winMenu.StartAnimator();
    }

    public void PlayerLose()
    {
        _playMusic.Stop();
        _playSound.Play("Loser");
        _mimimap.SetActive(false);
        _joystick.SetActive(false);
        _joystickLooket.SetActive(false);
        _iconePower.SetActive(false);

        pausedGame = true;
        _menuContr.Luser();
    }

    public void GameAfterLose()
    {
        _playMusic.Play(_nameMusic);
        _mimimap.SetActive(true);
        _joystick.SetActive(true);
        _joystickLooket.SetActive(true);
        _iconePower.SetActive(true);
        _lifes++;
        _generator.TransformingMonstersAffterMinusLifePlayer(0);
        pausedGame = false;
    }


    private void Update()
    {
        if (!pausedGame)
        {
            _gameTime += Time.deltaTime;
            if (_gameTime >= 1)
            {
                _seconds += 1;
                _gameTime = 0;
            }
            if (_seconds >= 60)
            {
                _minutes += 1;
                _seconds = 0;
            }
        }
    }
}
