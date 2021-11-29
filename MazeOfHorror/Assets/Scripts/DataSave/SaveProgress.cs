using UnityEngine;

public class SaveProgress : MonoBehaviour
{
    public int LevelPlayer
    {
        get; private set;
    }
    public int AmountGold
    {
        get; private set;
    }
    public int AmountDimond
    {
        get; private set;
    }
    public int SpeedPlayer
    {
        get; private set;
    }
    public int PowerPlayer
    {
        get; private set;
    }
    public int MannaPlayer
    {
        get; private set;
    }
    public int LifePlayer
    {
        get; private set;
    }

    public int AmountMovetWoll
    {
        get; private set;
    }
    public int AmountPassage
    {
        get; private set;
    }
    public int AmountPort
    {
        get; private set;
    }
    public int AmountImmunity
    {
        get; private set;
    }

    private string _location;

    private const string level = "Level";
    private const string gold = "Gold";
    private const string dimond = "Dimond";
    private const string life = "Life";
    private const string speed = "Speed";
    private const string power = "Power";
    private const string manna = "Manna";
    private const string woll = "Woll";
    private const string passage = "Passage";
    private const string port = "Port";
    private const string immuny = "Immuny";


    public void Save()
    {
        PlayerPrefs.SetInt(level, LevelPlayer);
        PlayerPrefs.SetInt(gold, AmountGold);
        PlayerPrefs.SetInt(dimond, AmountDimond);
        PlayerPrefs.SetInt(life, LifePlayer);
        PlayerPrefs.SetInt(speed, SpeedPlayer);
        PlayerPrefs.SetInt(power, PowerPlayer);
        PlayerPrefs.SetInt(manna, MannaPlayer);
        PlayerPrefs.SetInt(woll, AmountMovetWoll);
        PlayerPrefs.SetInt(passage, AmountPassage);
        PlayerPrefs.SetInt(port, AmountPort);
        PlayerPrefs.SetInt(immuny, AmountImmunity);

        PlayerPrefs.Save();
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(level)) LevelPlayer = PlayerPrefs.GetInt(level);
        else LevelPlayer = 0;
        if (PlayerPrefs.HasKey(gold)) AmountGold = PlayerPrefs.GetInt(gold);
        else AmountGold = 0;
        if (PlayerPrefs.HasKey(dimond)) AmountDimond = PlayerPrefs.GetInt(dimond);
        else AmountDimond = 0;
        if (PlayerPrefs.HasKey(life)) LifePlayer = PlayerPrefs.GetInt(life);
        else LifePlayer = 3;
        if (PlayerPrefs.HasKey(speed)) SpeedPlayer = PlayerPrefs.GetInt(speed);
        else SpeedPlayer = 3;
        if (PlayerPrefs.HasKey(power)) PowerPlayer = PlayerPrefs.GetInt(power);
        else PowerPlayer = 0;
        if (PlayerPrefs.HasKey(manna)) MannaPlayer = PlayerPrefs.GetInt(manna);
        else MannaPlayer = 0;
        if (PlayerPrefs.HasKey(woll)) AmountMovetWoll = PlayerPrefs.GetInt(woll);
        else AmountMovetWoll = 3;
        if (PlayerPrefs.HasKey(passage)) AmountPassage = PlayerPrefs.GetInt(passage);
        else AmountPassage = 0;
        if (PlayerPrefs.HasKey(port)) AmountPort = PlayerPrefs.GetInt(port);
        else AmountPort = 0;
        if (PlayerPrefs.HasKey(immuny)) AmountImmunity = PlayerPrefs.GetInt(immuny);
        else AmountImmunity = 0;
    }

    public void SetLevel(int value)
    {
        LevelPlayer = value;
    }

    public void SetGold(int value)
    {
        AmountGold = value;
    }

    public void SetDimond(int value)
    {
        AmountDimond = value;
    }

    public void SetSpeed(int value)
    {
        SpeedPlayer = value;
    }

    public void SetPower(int value)
    {
        PowerPlayer = value;
    }

    public void SetManna(int value)
    {
        MannaPlayer = value;
    }

    public void SetLife(int value)
    {
        LifePlayer = value;
    }

    public void SetWoll(int value)
    {
        AmountMovetWoll = value;
    }

    public void SetPass(int value)
    {
        AmountPassage = value;
    }

    public void SetPort(int value)
    {
        AmountPort = value;
    }

    public void SetImmuny(int value)
    {
        AmountImmunity = value;
    }
}
