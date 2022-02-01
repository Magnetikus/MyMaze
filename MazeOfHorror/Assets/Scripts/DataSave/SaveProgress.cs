using UnityEngine;

public class SaveProgress : MonoBehaviour
{
    public int LevelPlayer
    {
        get; private set;
    }

    public int ProgressLevel
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


    public void Save()
    {
        string data = LevelPlayer + ";" + AmountGold + ";" + AmountDimond + ";" + LifePlayer + ";"
            + SpeedPlayer + ";" + PowerPlayer + ";" + MannaPlayer + ";" + AmountMovetWoll + ";" + AmountPassage + ";"
            + AmountPort + ";" + AmountImmunity + ";" + DinoBuy + ";" + CastleBuy + ";" + ProgressLevel;
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

        PlayerPrefs.Save();
    }



    public void Load()
    {
        string data;
        string[] array = new string[14];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = "0";
        }
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
            array[3] = "3";
            array[7] = "3";
        }
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == null)
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
        SetProgressLevel(int.Parse(array[13]));
    }

    public void SetLevel(int value)
    {
        LevelPlayer = value;
    }

    public void SetProgressLevel(int value)
    {
        ProgressLevel = value;
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

    private void Start()
    {
        Load();
    }
}
