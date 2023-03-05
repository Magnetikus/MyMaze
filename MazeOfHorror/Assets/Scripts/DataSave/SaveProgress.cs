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

    public int DinoBuy
    {
        get; private set;
    }

    public int CastleBuy
    {
        get; private set;
    }

    private string _save = "Magnetikus";
    (string Type, string Code)[] HackerBack =
    {
        ("0", "rg"),
        ("1", "0s"),
        ("2", "9H"),
        ("3", "MK"),
        ("4", "W8"),
        ("5", "m2"),
        ("6", "cv"),
        ("7", "-4"),
        ("8", "Q4"),
        ("9", "fN"),
        (";", "D1"),
    };

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
    private const string dino = "Dino";
    private const string castle = "Castle";


    public void Save()
    {
        string data = LevelPlayer + ";" + AmountGold + ";" + AmountDimond + ";" + LifePlayer + ";" + SpeedPlayer + ";" + PowerPlayer + ";" +
            MannaPlayer + ";" + AmountMovetWoll + ";" + AmountPassage + ";" + AmountPort + ";" + AmountImmunity + ";" + DinoBuy + ";" + CastleBuy;
        int i = 0;
        string myData = "";
        while (i < data.Length)
        {
            int y = 0;
            while (data[i].ToString() != HackerBack[y].Type)
            {
                y++;
            }
            myData = myData + HackerBack[y].Code;
            i++;
        }
        PlayerPrefs.SetString(_save, myData);

        //PlayerPrefs.SetInt(level, LevelPlayer);
        //PlayerPrefs.SetInt(gold, AmountGold);
        //PlayerPrefs.SetInt(dimond, AmountDimond);
        //PlayerPrefs.SetInt(life, LifePlayer);
        //PlayerPrefs.SetInt(speed, SpeedPlayer);
        //PlayerPrefs.SetInt(power, PowerPlayer);
        //PlayerPrefs.SetInt(manna, MannaPlayer);
        //PlayerPrefs.SetInt(woll, AmountMovetWoll);
        //PlayerPrefs.SetInt(passage, AmountPassage);
        //PlayerPrefs.SetInt(port, AmountPort);
        //PlayerPrefs.SetInt(immuny, AmountImmunity);
        //PlayerPrefs.SetInt(dino, DinoBuy);
        //PlayerPrefs.SetInt(castle, CastleBuy);

        PlayerPrefs.Save();
    }

    public void Load()
    {

        string data;
        string[] array = new string[13];
        if (PlayerPrefs.HasKey(_save))
        {
            data = PlayerPrefs.GetString(_save);
            int i = 0;
            string myData = "";
            while (i < data.Length)
            {
                int y = 0;
                string x = data[i].ToString() + data[i + 1].ToString();
                while (x != HackerBack[y].Code)
                {
                    y++;
                }
                myData = myData + HackerBack[y].Type;
                i = i + 2;
            }
            array = myData.Split(';');
        }
        else
        {
            for (int i = 0; i < 13; i++)
            {
                array[i] = "0";
            }
        }
        SetLevel(int.Parse(array[0]));
        SetGold(int.Parse(array[1]));
        SetDimond(int.Parse(array[2]));
        SetLife(int.Parse(array[3]));
        SetSpeed(int.Parse(array[4]));
        SetPower(int.Parse(array[5]));
        SetManna(int.Parse(array[6]));
        SetWoll(int.Parse(array[7]));
        SetPass(int.Parse(array[8]));
        SetPort(int.Parse(array[9]));
        SetImmuny(int.Parse(array[10]));
        SetDino(int.Parse(array[11]));
        SetCastle(int.Parse(array[12]));

        //if (PlayerPrefs.HasKey(level)) LevelPlayer = UnityEngine.PlayerPrefs.GetInt(level);
        //else LevelPlayer = 0;
        //if (PlayerPrefs.HasKey(gold)) AmountGold = UnityEngine.PlayerPrefs.GetInt(gold);
        //else AmountGold = 0;
        //if (PlayerPrefs.HasKey(dimond)) AmountDimond = UnityEngine.PlayerPrefs.GetInt(dimond);
        //else AmountDimond = 0;
        //if (PlayerPrefs.HasKey(life)) LifePlayer = UnityEngine.PlayerPrefs.GetInt(life);
        //else LifePlayer = 3;
        //if (PlayerPrefs.HasKey(speed)) SpeedPlayer = UnityEngine.PlayerPrefs.GetInt(speed);
        //else SpeedPlayer = 0;
        //if (PlayerPrefs.HasKey(power)) PowerPlayer = UnityEngine.PlayerPrefs.GetInt(power);
        //else PowerPlayer = 0;
        //if (PlayerPrefs.HasKey(manna)) MannaPlayer = UnityEngine.PlayerPrefs.GetInt(manna);
        //else MannaPlayer = 0;
        //if (PlayerPrefs.HasKey(woll)) AmountMovetWoll = UnityEngine.PlayerPrefs.GetInt(woll);
        //else AmountMovetWoll = 3;
        //if (PlayerPrefs.HasKey(passage)) AmountPassage = UnityEngine.PlayerPrefs.GetInt(passage);
        //else AmountPassage = 0;
        //if (PlayerPrefs.HasKey(port)) AmountPort = UnityEngine.PlayerPrefs.GetInt(port);
        //else AmountPort = 0;
        //if (PlayerPrefs.HasKey(immuny)) AmountImmunity = UnityEngine.PlayerPrefs.GetInt(immuny);
        //else AmountImmunity = 0;
        //if (PlayerPrefs.HasKey(dino)) DinoBuy = UnityEngine.PlayerPrefs.GetInt(dino);
        //else DinoBuy = 0;
        //if (PlayerPrefs.HasKey(castle)) CastleBuy = UnityEngine.PlayerPrefs.GetInt(castle);
        //else CastleBuy = 0;
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

    public void SetDino(int value)
    {
        DinoBuy = value;
    }

    public void SetCastle(int value)
    {
        CastleBuy = value;
    }
}
