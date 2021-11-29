using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WinMenu : MonoBehaviour
{
    [SerializeField] private ConstructorMaze _constructor;
    [SerializeField] private ProgressMenuGUI _progress;
    [SerializeField] private MenuController _menuController;
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

    private int _startGold;
    private int _gold;
    private int _startLife;
    private int _life;
    private int _treasure;

    private int _resultateGold;
    private int _resultateDimond;
    private int _resultateExp;

    private const string gold = "Gold";
    private const string life = "Life";
    private const string exp = "Exp";
    private const string resGold = "ResGold";
    private const string resDimond = "ResDimond";
    private const string resEXP = "ResEXP";

    private void Start()
    {
        _animator.enabled = false;
    }

    public void StartAnimator()
    {
        _animator.enabled = true;
        _animator.SetTrigger("Victory");
        _buttonRestart.enabled = false;
        _buttonX2.enabled = false;
        _buttonNext.enabled = false;
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

            _animator.speed = 0;
            StartCoroutine(Timer(exp, _resultateExp + 10));
        }
        else
        {
            _starGold.SetActive(false);
            _krestImage.SetActive(true);
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
        if (_treasure > 0)
        {
            _starTreasure.SetActive(true);

            _animator.speed = 0;
            StartCoroutine(Timer(exp, _resultateExp + 10));
        }
        else
        {
            _starTreasure.SetActive(false);
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

            _animator.speed = 0;
            StartCoroutine(Timer(exp, _resultateExp + 10));
        }
        else
        {
            _starLife.SetActive(false);
            _krestImage.SetActive(true);
        }
        _buttonRestart.enabled = true;
        _buttonX2.enabled = true;
        _buttonNext.enabled = true;
    }


    private IEnumerator Timer(string name, int limit)
    {
        yield return null;
        var wait = new WaitForSeconds(0.1f);
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
                    yield return wait;
                }
                break;

            case resDimond:
                while (true)
                {
                    if (_resultateDimond == limit)
                    {
                        break;
                    }
                    _resultateDimond++;
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
                    yield return wait;
                }
                break;
        }
    }

    public void EndKrest()
    {
        _krestImage.SetActive(false);
    }

    public void X2()
    {
        int x2Gold = _resultateGold * 2;
        int x2Dimond = _resultateDimond * 2;
        int x2EXP = _resultateExp * 2;
        StartCoroutine(Timer(resGold, x2Gold));
        StartCoroutine(Timer(resDimond, x2Dimond));
        StartCoroutine(Timer(resEXP, x2EXP));

        _buttonX2.enabled = false;
    }

    public void Next()
    {
        _progress.SetDataBaseWin(_resultateGold, _resultateDimond, _resultateExp);
        ResetDB();
        _constructor.DeleteAll();
        _menuController.Global();
    }

    public void ResetDB()
    {
        _animator.SetTrigger("EndAnim");
        _animator.enabled = false;
        _startGold = 0;
        _gold = 0;
        _startLife = 0;
        _life = 0;
        _resultateGold = 0;
        _resultateDimond = 0;
        _resultateExp = 0;
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
    }

}
