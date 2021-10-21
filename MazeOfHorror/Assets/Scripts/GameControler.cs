using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControler : MonoBehaviour
{

    private ConstructorMaze generator;
    private bool startGame;

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

    [SerializeField] private MenuController menuContr;
    [SerializeField] private GameObject menuBackground;
    [SerializeField] private Text textLifes;
    [SerializeField] private Text textKeys;
    [SerializeField] private Text textGold;
    [SerializeField] private Text textTreasure;
    [SerializeField] private Text textCubes;
    [SerializeField] private Text textTimes;
    [SerializeField] private Text textMessage;
    [SerializeField] private Text resultLife;
    [SerializeField] private Text resultGold;
    [SerializeField] private Text resultTreasure;
    [SerializeField] private Text resultTime;
    [SerializeField] private Text resultLifeLose;
    [SerializeField] private Text resultGoldLose;
    [SerializeField] private Text resultTreasureLose;
    [SerializeField] private Text resultTimeLose;
    [SerializeField] private Image minusLife;
    [SerializeField] private Image cursorHandler;
    [SerializeField] private Image cube;
    [SerializeField] private GameObject mimimap;
    

    [HideInInspector]
    public bool cursorOnOff = false;
    public bool cubeMovet = false;


    public bool pausedGame { get; private set; }

    void Start()
    {
        generator = GetComponent<ConstructorMaze>();
        startGame = false;
    }


    public void StartNewGame(List<int> constrMaze)
    {
        generator.GenerateNewMaze(constrMaze);
        menuBackground.SetActive(false);
        mimimap.SetActive(true);
        sizeRows = constrMaze[0];
        sizeCols = constrMaze[1];
        startKeys = constrMaze[2];
        startGold = (sizeRows + sizeCols) / 2;
        lifes = startGold / 5;
        cubes = lifes;
        keys = startKeys;
        gold = startGold;
        seconds = 0;
        minutes = 0;
        treasure = 1;
        startGame = true;
        pausedGame = false;
    }

    public void PausedGame(bool chek)
    {
        pausedGame = chek;
    }

    public void RestartGame()
    {
        seconds = 0;
        minutes = 0;
        lifes = startGold / 5;
        keys = startKeys;
        gold = startGold;
        cubes = lifes;
        treasure = 0;
        generator.Restart();
    }

    public int[] GetSizeMaze()
    {
        int[] array = { sizeRows, sizeCols };
        return array;
    }

    private void OnGUI()
    {
        if (startGame)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (cursorOnOff)
            {
                cursorHandler.enabled = true;
            }
            if (!cursorOnOff)
            {
                cursorHandler.enabled = false;
            }
            if (cubeMovet)
            {
                cube.enabled = true;
            }
            if (!cubeMovet)
            {
                cube.enabled = false;
            }
        }

        if (pausedGame)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
        }

        textLifes.text = $"Life: {lifes}";
        textKeys.text = $"Keys: {keys} / {startKeys}";
        textGold.text = $"Gold: {gold} / {startGold}";
        textTreasure.text = $"Treasure: {treasure / 1}";
        textCubes.text = $"{cubes}";
        textTimes.text = $"{minutes:d2} : {seconds:d2}";

    }
   

    public void Message(string newText)
    {
        textMessage.text = newText;
    }

    public void MinusLifes()
    {
        lifes--;
        if (lifes < 0) lifes = 0;
        if (lifes == 0) PlayerLose();
        else
        {
            pausedGame = true;
            generator.TransformingMonstersAffterMinusLifePlayer();
            Animation anim = minusLife.GetComponent<Animation>();
            anim.Play();
            //pausedGame = false;
        }
        
    }

    public void MinusKeys()
    {
        keys--;
        if (keys < 0) keys = 0;
        if (keys == 0)
        {
            ParticleSystem part = GameObject.Find("PrefabExit(Clone)").GetComponentInChildren<ParticleSystem>();
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
        gold--;
        if (gold < 0) gold = 0;
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
        treasure -= 1;
    }

    public void PlayerWin()
    {
        mimimap.SetActive(false);
        pausedGame = true;
        menuContr.Victory();
        resultTreasure.text = $"Remaining to collect treasure {treasure} / 1";
        resultGold.text = $"Remaining to collect gold {gold} / {startGold}";
        resultLife.text = $"Remaining lives {lifes} / {startGold / 5}";
        resultTime.text = $"Wasted time {minutes:d2} : {seconds:d2}";
    }

    public void PlayerLose()
    {
        mimimap.SetActive(false);
        pausedGame = true;
        menuContr.Luser();
        resultTreasureLose.text = $"Remaining to collect treasure {treasure} / 1";
        resultGold.text = $"Remaining to collect gold {gold} / {startGold}";
        resultLife.text = $"Remaining lives {lifes} / {startGold / 5}";
        resultTime.text = $"Wasted time {minutes:d2} : {seconds:d2}";
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
