using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private ConstructorMaze _constructor;
    [SerializeField] private ProgressMenuGUI _progress;
    [SerializeField] private SaveLoadGame _saveLoadGame;
    [SerializeField] private MenuController _menuController;
    [SerializeField] private PlaySound _playSound;
    [SerializeField] private PlaySound _playSoundPic;
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _starGold;
    [SerializeField] private GameObject _starTreasure;
    [SerializeField] private GameObject _starLife;
    [SerializeField] private Button _buttonRestart;
    [SerializeField] private Button _buttonX2;
    [SerializeField] private Button _buttonNext;
    [SerializeField] private Text _textStartGold;
    [SerializeField] private Text _textGold;
    [SerializeField] private Text _textStartLife;
    [SerializeField] private Text _textLife;
    [SerializeField] private Image _treasureImage;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private GameObject _krestImage;
    [SerializeField] private Text _textResultateGold;
    [SerializeField] private Text _textResultateDimond;
    [SerializeField] private Text _textResultateExp;
    [SerializeField] private Text _textTimeLevel;
    [SerializeField] private Text _textRecord;
    [SerializeField] private Text _textTimeLevelInRecord;

    private int _startGold;
    private int _gold;
    private int _startLife;
    private int _life;
    private int _treasure;

    private int _resultateGold;
    private int _resultateDimond;
    private int _resultateExp;

    private int _sizeMaze;
    private int _recordLevel;
    private int _timeLevel;
    private int _minutes;
    private int _seconds;
    private int _minRecord;
    private int _secRecord;

    private bool _isEndGold;
    private bool _isEndExp;

    private const string gold = "Gold";
    private const string life = "Life";
    private const string exp = "Exp";
    private const string resGold = "ResGold";
    private const string resDimond = "ResDimond";
    private const string resEXP = "ResEXP";
    private const string goldRecord = "GoldRecord";

    private void Start()
    {
        _animator.enabled = false;
    }

    public void SetTimeAndSize(int minutes, int seconds, int size)
    {
        _sizeMaze = size;
        _timeLevel = minutes * 100 + seconds;
        _minutes = minutes;
        _seconds = seconds;
        _recordLevel = _saveLoadGame.recordEasy;
        if (_sizeMaze > 23)
        {
            _recordLevel = _saveLoadGame.recordMedium;
        }
        if (_sizeMaze > 33)
        {
            _recordLevel = _saveLoadGame.recordHard;
        }
        if (_sizeMaze > 45)
        {
            _recordLevel = _saveLoadGame.recordCrazy;
        }
        _minRecord = _recordLevel / 100;
        _secRecord = _recordLevel % 100;
    }

    public void StartAnimator()
    {
        _animator.enabled = true;
        _animator.SetTrigger("Victory");
        _buttonRestart.enabled = false;
        _buttonX2.enabled = false;
        _buttonNext.enabled = false;
        _isEndExp = false;
        _isEndGold = false;
        _starGold.SetActive(false);
        _starLife.SetActive(false);
        _starTreasure.SetActive(false);
    }

    public void SetGoldResultate(int start, int end)
    {
        _startGold = start;
        _gold = end;
    }

    public void SetLifeResultate(int start, int end)
    {
        _startLife = start;
        _life = end;
    }

    public void SetTreasure(int value)
    {
        _treasure = value;
    }

    public void SetExp(int value)
    {
        _resultateExp = value;
    }


    public void Gold()
    {
        _resultateGold = _gold;
        _animator.speed = 0;
        StartCoroutine(Timer(gold, 0));
    }

    public void EndGold()
    {
        if (_startGold == _gold)
        {
            _particleSystem.Play();
            _starGold.SetActive(true);
            _playSound.Play("Treasure");
            _animator.speed = 0;
            StartCoroutine(Timer(exp, _resultateExp + 10));
        }
        else
        {
            _starGold.SetActive(false);
            _krestImage.SetActive(true);
            _playSound.Play("Error");
        }
    }

    public void Treasure()
    {
        _resultateDimond = _treasure;
        
        if (_treasure > 0)
        {
            Color color = _treasureImage.color;
            color.a = 1f;
            _treasureImage.color = color;
            _particleSystem.Play();
        }
        else
        {
            _krestImage.SetActive(true);
        }
    }

    public void EndTreasure()
    {
        _isEndGold = false;
        if (_treasure > 0)
        {
            _starTreasure.SetActive(true);
            _playSound.Play("Treasure");
            _animator.speed = 0;
<<<<<<< HEAD
            StartCoroutine(Timer(resDimond, _resultateDimond + 2));
=======
            StartCoroutine(Timer(exp, _resultateExp + 10));
>>>>>>> parent of 2ed1a08 (тестрелиз)
        }
        else
        {
            _starTreasure.SetActive(false);
            _playSound.Play("Error");
        }
    }

    public void Life()
    {
        _animator.speed = 0;
        StartCoroutine(Timer(life, 0));
    }

    public void EndLife()
    {
        if (_startLife == _life)
        {
            _particleSystem.Play();
            _starLife.SetActive(true);
            _playSound.Play("Treasure");
            _animator.speed = 0;
            StartCoroutine(Timer(exp, _resultateExp + 10));
        }
        else
        {
            _starLife.SetActive(false);
            _krestImage.SetActive(true);
            _playSound.Play("Error");
        }
        
        if (_timeLevel < _recordLevel && _startGold == _gold && _treasure > 0 && _startLife == _life)
        {
            NewRecord();
        }

        _buttonRestart.enabled = true;
        _buttonX2.enabled = true;
        _buttonNext.enabled = true;
    }

    public void NewRecord()
    {
        _animator.SetTrigger("Record");
        _recordLevel = _timeLevel;
        if (_sizeMaze <= 23)
        {
            _saveLoadGame.recordEasy = _recordLevel;
        }
        if (_sizeMaze > 23 && _sizeMaze <= 33)
        {
            _saveLoadGame.recordMedium = _recordLevel;
        }
        if (_sizeMaze > 33 && _sizeMaze <= 45)
        {
            _saveLoadGame.recordHard = _recordLevel;
        }
        if (_sizeMaze > 45)
        {
            _saveLoadGame.recordCrazy = _recordLevel;
        }
        _saveLoadGame.Save();
    }

    public void MidleRecord()
    {
        _particleSystem.Play();
        _playSound.Play("Treasure");
        _animator.speed = 0;
        StartCoroutine(Timer(goldRecord, _gold + 100));
    }

    public void EndRecord()
    {
        _minRecord = _minutes;
        _secRecord = _seconds;
    }


    private IEnumerator Timer(string name, int limit)
    {
        yield return null;
        var wait = new WaitForSeconds(0.05f);
        switch (name)
        {
            case gold:
                while (true)
                {
                    if (_gold == limit)
                    {
                        break;
                    }
                    _startGold--;
                    _gold--;
                    _playSoundPic.Play("Pic");
                    yield return wait;
                }
                _animator.speed = 1;
                break;

            case goldRecord:
                while (true)
                {
                    if (_gold == limit)
                    {
                        break;
                    }
                    _gold += 10;
                    _playSoundPic.Play("Pic");
                    yield return wait;
                }
                _animator.speed = 1;
                break;

            case life:
                while (true)
                {
                    if (_life == limit)
                    {
                        break;
                    }
                    _startLife--;
                    _life--;
                    _playSoundPic.Play("Pic");
                    yield return wait;
                }
                _animator.speed = 1;
                break;

            case exp:
                while(true)
                {
                    if (_resultateExp == limit)
                    {
                        break;
                    }
                    _resultateExp++;
                    _playSoundPic.Play("Pic");
                    yield return wait;
                }
                _animator.speed = 1;
                break;

            case resGold:
                while (true)
                {
                    if (_resultateGold == limit)
                    {
                        break;
                    }
                    _resultateGold++;
                    _playSoundPic.Play("Pic");
                    yield return wait;
                }
<<<<<<< HEAD
                _animator.speed = 1;
                _isEndGold = true;
                EndX2();
=======
>>>>>>> parent of 2ed1a08 (тестрелиз)
                break;

            case resDimond:
                while (true)
                {
                    if (_resultateDimond == limit)
                    {
                        break;
                    }
                    _resultateDimond++;
                    _playSoundPic.Play("Pic");
                    yield return wait;
                }
                break;

            case resEXP:
                while (true)
                {
                    if (_resultateExp == limit)
                    {
                        break;
                    }
                    _resultateExp++;
                    _playSoundPic.Play("Pic");
                    yield return wait;
                }
<<<<<<< HEAD
                _animator.speed = 1;
                _isEndExp = true;
                EndX2();
=======
>>>>>>> parent of 2ed1a08 (тестрелиз)
                break;
        }
    }

    public void EndKrest()
    {
        _krestImage.SetActive(false);
    }

    public void X2()
    {
        _buttonNext.enabled = false;
        _buttonRestart.enabled = false;
        _buttonX2.enabled = false;
        int x2Gold = _resultateGold * 2;
        int x2Dimond = _resultateDimond * 2;
        int x2EXP = _resultateExp * 2;
        StartCoroutine(Timer(resGold, x2Gold));
        StartCoroutine(Timer(resDimond, x2Dimond));
        StartCoroutine(Timer(resEXP, x2EXP));
    }

    private void EndX2()
    {
        if (_isEndExp == true && _isEndGold == true)
        {
            _buttonRestart.enabled = true;
            _buttonNext.enabled = true;
        }
    }

    public void Next()
    {
        _progress.SetDataBaseWin(_resultateGold, _resultateDimond, _resultateExp);
        _constructor.DeleteAll();
        _menuController.Global();
        ResetDB();
    }

    public void ResetDB()
    {
        _animator.SetTrigger("EndAnim");
        _animator.enabled = false;
        _startGold = 0;
        _gold = 0;
        _treasure = 0;
        _startLife = 0;
        _life = 0;
        _resultateGold = 0;
        _resultateDimond = 0;
        _resultateExp = 0;
        _minutes = 0;
        _seconds = 0;
        _minRecord = 0;
        _secRecord = 0;
        _recordLevel = 0;
    }

    private void OnGUI()
    {
        _textStartGold.text = $"{_startGold}";
        _textGold.text = $"{_gold}";
        _textStartLife.text = $"{_startLife}";
        _textLife.text = $"{_life}";
        _textResultateGold.text = $"{_resultateGold}";
        _textResultateDimond.text = $"{_resultateDimond}";
        _textResultateExp.text = $"{_resultateExp}";
        _textTimeLevel.text = $"{_minutes:d2} : {_seconds:d2}";
        _textRecord.text = $"{_minRecord:d2} : {_secRecord:d2}";
        _textTimeLevelInRecord.text = $"{_minutes:d2} : {_seconds:d2}";
    }

}
