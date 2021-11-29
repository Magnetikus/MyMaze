using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{

    private ConstructorMaze generator;

    private int sizeRows;
    private int sizeCols;
    private int lifes;
    private int keys, startKeys;
    private int gold, startGold;
    private int treasure;
    private int cubes;
    private float gameTime;
    private int seconds;
    private int minutes;

    private int EXP = 30;

    [SerializeField] private MenuController menuContr;
    [SerializeField] private SaveProgress saveProgress;
    [SerializeField] private WinMenu winMenu;
    [SerializeField] private GameObject menuBackground;
    [SerializeField] private GameObject joystick;
    [SerializeField] private GameObject joystickLooket;
    [SerializeField] private GameObject iconePower;
    [SerializeField] private GameObject iconeHandle;
    [SerializeField] private GameObject iconeZell;
    [SerializeField] private AmountHeartOut heartOut;
    [SerializeField] private AmountHeartOut keyOut;
    [SerializeField] private GameObject minusLifeImage;
    [SerializeField] private GameObject[] heartsMinusLife = new GameObject[10];
    [SerializeField] private Image treasureOut;
    [SerializeField] private Text textGoldStart;
    [SerializeField] private Text textGold;
    [SerializeField] private Text textCubes;
    [SerializeField] private Text textTimes;
    [SerializeField] private GameObject imageFindKey;
    [SerializeField] private Text textFindKey;
    [SerializeField] private GameObject key1;
    [SerializeField] private GameObject key2;
    [SerializeField] private GameObject key3;
    [SerializeField] private GameObject mimimap;


    public bool pausedGame { get; private set; }

    private void Start()
    {
        generator = GetComponent<ConstructorMaze>();
    }


    public void StartNewGame(List<int> constrMaze)
    {
        generator.GenerateNewMaze(constrMaze);
        sizeRows = constrMaze[0];
        sizeCols = constrMaze[1];
        startKeys = constrMaze[2];
        startGold = (sizeRows + sizeCols) / 2;
        ResetDataBase();
    }

    public void PausedGame(bool chek)
    {
        pausedGame = chek;
    }


    public void RestartGame()
    {
        ResetDataBase();
        generator.Restart();
    }

    private void ResetDataBase()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<FpsMovement>().SetSpeed(saveProgress.SpeedPlayer);
        player.GetComponent<MovetCube>().ResetEndGame();
        menuBackground.SetActive(false);
        mimimap.SetActive(true);
        joystick.SetActive(true);
        joystick.GetComponent<JoystickController>().Zero();
        joystickLooket.SetActive(true);
        joystickLooket.GetComponent<JoystickLooket>().Zero();
        iconePower.SetActive(true);
        seconds = 0;
        minutes = 0;
        lifes = saveProgress.LifePlayer;
        for (int i = 1; i <= lifes; i++)
        {
            heartOut.SetColorHeart(i, 1);
        }
        keys = startKeys;
        for (int i = 1; i <= keys; i++)
        {
            keyOut.SetColorHeart(i, 0.5f);
        }
        gold = 0;
        cubes = saveProgress.AmountMovetWoll;
        treasure = 0;
        Color colorTreasure = treasureOut.color;
        colorTreasure.a = 0.4f;
        treasureOut.color = colorTreasure;
        textGoldStart.color = new Color(250, 39, 13);
        textGold.color = Color.white;
        iconeHandle.SetActive(false);
        iconeZell.SetActive(false);
        pausedGame = false;
    }

    public int[] GetSizeMaze()
    {
        int[] array = { sizeRows, sizeCols };
        return array;
    }

    private void OnGUI()
    {
        textGoldStart.text = $"( {startGold} )";
        textGold.text = $" {gold} ";
        textCubes.text = $" {cubes} ";
        textTimes.text = $" {minutes:d2} : {seconds:d2} ";
    }

    public void PlayerEnterExit()
    {
        if (keys == 0)
        {
            PlayerWin();
        }
        else
        {
            imageFindKey.SetActive(true);
            textFindKey.text = $"{keys}";
            if (keys % 10 == 1)
            {
                key1.SetActive(true);
            }
            else if (keys % 10 < 5)
            {
                key2.SetActive(true);
            }
            else
            {
                key3.SetActive(true);
            }
        }
    }

    public void PlayerExitExit()
    {
        key1.SetActive(false);
        key2.SetActive(false);
        key3.SetActive(false);
        imageFindKey.SetActive(false);
    }

    public void MinusLifes()
    {
        pausedGame = true;
        minusLifeImage.SetActive(true);
        minusLifeImage.GetComponent<Animator>().enabled = true;
        for (int i = 0; i < lifes; i++)
        {
            heartsMinusLife[i].SetActive(true);
        }
    }

    public void EndLifePanelOut()
    {
        heartsMinusLife[lifes - 1].GetComponent<Animator>().enabled = true;
    }

    public void EndMinusLife()
    {
        heartsMinusLife[lifes - 1].SetActive(false);
        minusLifeImage.GetComponent<Animator>().enabled = false;
        minusLifeImage.SetActive(false);
        heartOut.SetColorHeart(lifes, 0);
        lifes--;
        if (lifes < 0) lifes = 0;
        if (lifes == 0) PlayerLose();
        else
        {
            generator.TransformingMonstersAffterMinusLifePlayer();
            pausedGame = false;
        }
    }

    public void MinusKeys()
    {
        keys--;
        if (keys < 0) keys = 0;

        keyOut.SetColorHeart(startKeys - keys, 1);
        
        if (keys == 0)
        {
            ParticleSystem part = GameObject.FindGameObjectWithTag("Exit").GetComponentInChildren<ParticleSystem>();
            ParticleSystem.MainModule main = part.main;
            main.startColor = Color.green;
        }
    }

    public int GetKeys()
    {
        return keys;
    }

    public void MinusCube()
    {
        cubes--;
        if (cubes < 0) cubes = 0;
    }

    public int GetCube()
    {
        return cubes;
    }

    public void MinusGold()
    {
        gold++;
        if (gold > startGold) gold = startGold;
        if (gold == startGold)
        {
            textGoldStart.color = Color.green;
            textGold.color = Color.green;
        }
    }

    public void MapCollect()
    {
        generator.CreateTreasure();
        SelectionObjectsWithTag("Movet");
        SelectionObjectsWithTag("NoMovet");
        SelectionObjectsWithTag("Cell");
    }

    private void SelectionObjectsWithTag(string tag)
    {
        GameObject[] array = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject e in array)
        {
            e.GetComponent<VisibleOnn>().OnSpriteMap();
        }
    }

    public void TreasureCollection()
    {
        treasure += 1;
        Color colorTreasure = treasureOut.color;
        colorTreasure.a = 1f;
        treasureOut.color = colorTreasure;
    }

    public void PlayerWin()
    {
        mimimap.SetActive(false);
        joystick.SetActive(false);
        joystickLooket.SetActive(false);
        iconePower.SetActive(false);
        pausedGame = true;
        menuContr.Victory();
        winMenu.StartAnimator();
        winMenu.SetGoldResultate(startGold, gold);
        winMenu.SetLifeResultate(saveProgress.LifePlayer, lifes);
        winMenu.SetTreasure(treasure);
        winMenu.SetExp(EXP);
    }

    public void PlayerLose()
    {
        mimimap.SetActive(false);
        joystick.SetActive(false);
        joystickLooket.SetActive(false);
        iconePower.SetActive(false);
        pausedGame = true;
        menuContr.Luser();
    }

    public void GameAfterLose()
    {
        mimimap.SetActive(true);
        joystick.SetActive(true);
        joystickLooket.SetActive(true);
        iconePower.SetActive(true);
        lifes++;
        generator.TransformingMonstersAffterMinusLifePlayer();
        pausedGame = false;
    }


    private void Update()
    {
        if (!pausedGame)
        {
            gameTime += Time.deltaTime;
            if (gameTime >= 1)
            {
                seconds += 1;
                gameTime = 0;
            }
            if (seconds >= 60)
            {
                minutes += 1;
                seconds = 0;
            }
        }
    }
}
