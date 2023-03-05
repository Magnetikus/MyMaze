using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private enum Screen
    {
        Game,
        Global,
        Progress,
        Setting,
        GameChange,
        EasyMediumHard,
        Paused,
        SettingInPause,
        Constructor,
        Victory,
        Loser,
        Shop,
        Tutorial,
        Title
    }
    private Screen screen;
    private List<int> constrMaze;

    [SerializeField] private GameControler gameContr;
    [SerializeField] private GameObject menuBackground;
    [SerializeField] private UsingConstructorMaze usingConstr;
    [SerializeField] private SaveLoadGame saveLoadGame;
    [SerializeField] private SaveProgress saveProgress;
    [SerializeField] private LosMenu loseMenu;
    [SerializeField] private MenuStartRandomGame _menuRandomGame;
    [SerializeField] private PlaySound _playMusic;
    [SerializeField] private PlaySound _playSound;
    [SerializeField] private Button _buttonNewGame;
    [SerializeField] private Button _buttonConstruktor;
    [SerializeField] private ConstruktorMenu _constrMenu;

    [SerializeField] private CanvasGroup gameCanvas;
    [SerializeField] private CanvasGroup globalMenu;
    [SerializeField] private CanvasGroup progressMenu;
    [SerializeField] private CanvasGroup shopMenu;
    [SerializeField] private CanvasGroup settingMenu;
    [SerializeField] private CanvasGroup gameChangeMenu;
    [SerializeField] private CanvasGroup easyMediumHardMenu;
    [SerializeField] private CanvasGroup pausedMenu;
    [SerializeField] private CanvasGroup settingInPause;
    [SerializeField] private CanvasGroup constructor;
    [SerializeField] private CanvasGroup victory;
    [SerializeField] private CanvasGroup loser;
    [SerializeField] private CanvasGroup tutorial;
    [SerializeField] private CanvasGroup title;

    private int _levelPlayer;
    private int _coefficient;
    private int _notFirstEnterGame;

    private void SetCurrentScreen(Screen screen)
    {
        Utility.SetCanvasGroupEnabled(gameCanvas, screen == Screen.Game);
        Utility.SetCanvasGroupEnabled(globalMenu, screen == Screen.Global);
        Utility.SetCanvasGroupEnabled(progressMenu, screen == Screen.Progress);
        Utility.SetCanvasGroupEnabled(settingMenu, screen == Screen.Setting);
        Utility.SetCanvasGroupEnabled(gameChangeMenu, screen == Screen.GameChange);
        Utility.SetCanvasGroupEnabled(easyMediumHardMenu, screen == Screen.EasyMediumHard);
        Utility.SetCanvasGroupEnabled(pausedMenu, screen == Screen.Paused);
        Utility.SetCanvasGroupEnabled(settingInPause, screen == Screen.SettingInPause);
        Utility.SetCanvasGroupEnabled(constructor, screen == Screen.Constructor);
        Utility.SetCanvasGroupEnabled(victory, screen == Screen.Victory);
        Utility.SetCanvasGroupEnabled(loser, screen == Screen.Loser);
        Utility.SetCanvasGroupEnabled(shopMenu, screen == Screen.Shop);
        Utility.SetCanvasGroupEnabled(tutorial, screen == Screen.Tutorial);
        Utility.SetCanvasGroupEnabled(title, screen == Screen.Title);
    }

    private void Start()
    {
        saveLoadGame.Load();
        saveProgress.Load();
        if (saveProgress.LifePlayer == 0)
        {
            saveProgress.SetLife(3);
        }
        if (saveProgress.AmountMovetWoll == 0)
        {
            saveProgress.SetWoll(3);
        }
        _levelPlayer = saveProgress.LevelPlayer;
        Global();
    }

    public void Global()
    {
        _playMusic.Play("Menu");
        menuBackground.SetActive(true);
        SetCurrentScreen(Screen.Global);
        screen = Screen.Global;
    }

    public void NewGame()
    {
        SetCurrentScreen(Screen.GameChange);
        screen = Screen.GameChange;
        _notFirstEnterGame = saveLoadGame.notFirstEnterGame;
        if (_notFirstEnterGame < 0.5f)
        {
            _buttonNewGame.enabled = false;
            _buttonConstruktor.enabled = false;
        }
        else
        {
            _buttonNewGame.enabled = true;
            _buttonConstruktor.enabled = true;
        }
    }

    public void Setting()
    {
        SetCurrentScreen(Screen.Setting);
        screen = Screen.Setting;
    }

    public void Progress()
    {
        SetCurrentScreen(Screen.Progress);
        screen = Screen.Progress;
    }

    public void Shop()
    {
        SetCurrentScreen(Screen.Shop);
        screen = Screen.Shop;
    }

    public void ExitProgress()
    {
        SetCurrentScreen(Screen.Global);
        screen = Screen.Global;
    }

    public void Exit()
    {
        saveProgress.Save();
        Application.Quit();
    }

    public void ExitSetting()
    {
        saveLoadGame.Save();
        SetCurrentScreen(Screen.Global);
        screen = Screen.Global;
    }

    public void StartRandomLevel()
    {
        SetCurrentScreen(Screen.EasyMediumHard);
        screen = Screen.EasyMediumHard;
        _levelPlayer = saveProgress.LevelPlayer;
        _menuRandomGame.SetDB(_levelPlayer);
    }

    public void ConstructorLevel()
    {
        SetCurrentScreen(Screen.Constructor);
        screen = Screen.Constructor;
        _levelPlayer = saveProgress.LevelPlayer;
        _constrMenu.SetLevelPlayer(_levelPlayer);
    }

    public void Tutorial()
    {
        SetCurrentScreen(Screen.Tutorial);
        screen = Screen.Tutorial;
        _notFirstEnterGame = 1;
        saveLoadGame.notFirstEnterGame = 1;
        saveLoadGame.Save();
    }

    public void LoadLevel()
    {
        // В разработке
    }

    public void ExitGame()
    {
        SetCurrentScreen(Screen.Global);
        screen = Screen.Global;
    }

    public void Easy()
    {
        _menuRandomGame.AllFalse();

        SetCurrentScreen(Screen.Game);
        screen = Screen.Game;
        if (_levelPlayer < 10)
        {
            _coefficient = 1;
        }
        else
        {
            _coefficient = 2;
        }
        constrMaze = new List<int> { 15, 15, 1 * _coefficient, 1 * _coefficient, 1 * _coefficient, 1 * _coefficient };
        gameContr.StartNewGame(constrMaze);
    }

    public void Medium()
    {
        _menuRandomGame.AllFalse();

        SetCurrentScreen(Screen.Game);
        screen = Screen.Game;
        if (_levelPlayer < 15)
        {
            _coefficient = 1;
        }
        else if (_levelPlayer < 20)
        {
            _coefficient = 2;
        }
        else
        {
            _coefficient = 3;
        }
        constrMaze = new List<int> { 25, 25, 2, 2 * _coefficient, 2 * _coefficient, 2 * _coefficient };
        gameContr.StartNewGame(constrMaze);
    }

    public void Hard()
    {
        _menuRandomGame.AllFalse();

        SetCurrentScreen(Screen.Game);
        screen = Screen.Game;
        if (_levelPlayer < 25)
        {
            _coefficient = 1;
        }
        else if (_levelPlayer < 30)
        {
            _coefficient = 2;
        }
        else
        {
            _coefficient = 3;
        }
        constrMaze = new List<int> { 35, 35, 4, 3 * _coefficient, 3 * _coefficient, 3 * _coefficient };
        gameContr.StartNewGame(constrMaze);
    }

    public void Crazy()
    {
        _menuRandomGame.AllFalse();

        SetCurrentScreen(Screen.Game);
        screen = Screen.Game;
        if (_levelPlayer < 35)
        {
            _coefficient = 1;
        }
        else if (_levelPlayer < 40)
        {
            _coefficient = 2;
        }
        else
        {
            _coefficient = 3;
        }
        constrMaze = new List<int> { 55, 55, 9, 5 * _coefficient, 5 * _coefficient, 5 * _coefficient };
        gameContr.StartNewGame(constrMaze);
    }

    public void ExitEMH()
    {
        SetCurrentScreen(Screen.GameChange);
        screen = Screen.GameChange;
    }

    public void Paused()
    {
        saveLoadGame.Save();
        SetCurrentScreen(Screen.Paused);
        screen = Screen.Paused;
        gameContr.PausedGame(true);

    }

    public void Continue()
    {
        SetCurrentScreen(Screen.Game);
        screen = Screen.Game;
        gameContr.PausedGame(false);
    }

    public void Restart()
    {
        SetCurrentScreen(Screen.Game);
        gameContr.RestartGame();
        screen = Screen.Game;
        gameContr.PausedGame(false);
    }

    public void SettingInPaused()
    {
        SetCurrentScreen(Screen.SettingInPause);
        screen = Screen.SettingInPause;
    }


    public void ExitInMenu()
    {
        saveProgress.Save();
        saveLoadGame.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartConstructorGame()
    {
        constrMaze = usingConstr.UsingConstructor();
        SetCurrentScreen(Screen.Game);
        screen = Screen.Game;
        gameContr.StartNewGame(constrMaze);
    }

    public void Victory()
    {
        _playMusic.Stop();
        SetCurrentScreen(Screen.Victory);
        screen = Screen.Victory;
    }

    public void Luser()
    {
        _playMusic.Stop();
        SetCurrentScreen(Screen.Loser);
        screen = Screen.Loser;
        loseMenu.SetDimond();
    }

    public void GameAfterLoser()
    {
        SetCurrentScreen(Screen.Game);
        screen = Screen.Game;
    }

    public void TitleMenu()
    {
        SetCurrentScreen(Screen.Title);
        screen = Screen.Title;
    }

    public void Click()
    {
        _playSound.Play("Click");
    }

    public void Okey()
    {
        _playSound.Play("Okey");
    }

    public void Perehod()
    {
        _playSound.Play("Perehod");
    }

    public void Escape()
    {
        _playSound.Play("Escape");
    }


}
