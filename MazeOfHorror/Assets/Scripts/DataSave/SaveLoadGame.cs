using UnityEngine;
using UnityEngine.UI;

public class SaveLoadGame : MonoBehaviour
{
    public Slider sliderVolume;
    public Slider sliderMusic;
    public Slider sliderSound;

    public int selectNatura;
    public int selectDino;
    public int selectCastle;

    public int notFirstEnterGame;

    public int recordEasy;
    public int recordMedium;
    public int recordHard;
    public int recordCrazy;

    public void Save()
    {
        PlayerPrefs.SetFloat("Volume", sliderVolume.value);
        PlayerPrefs.SetFloat("Music", sliderMusic.value);
        PlayerPrefs.SetFloat("Sound", sliderSound.value);
        PlayerPrefs.SetInt("NaturaSelect", selectNatura);
        PlayerPrefs.SetInt("DinoSelect", selectDino);
        PlayerPrefs.SetInt("CastleSelect", selectCastle);
        PlayerPrefs.SetInt("FirstEnter", notFirstEnterGame);
        PlayerPrefs.SetInt("RecordEasy", recordEasy);
        PlayerPrefs.SetInt("RecordMedium", recordMedium);
        PlayerPrefs.SetInt("RecordHard", recordHard);
        PlayerPrefs.SetInt("RecordCrazy", recordCrazy);

        PlayerPrefs.Save();
    }

    public void Load()
    {

        if (PlayerPrefs.HasKey("Volume")) sliderVolume.value = PlayerPrefs.GetFloat("Volume");
        else sliderVolume.value = 1.0f;
        if (PlayerPrefs.HasKey("Music")) sliderMusic.value = PlayerPrefs.GetFloat("Music");
        else sliderMusic.value = 1.0f;
        if (PlayerPrefs.HasKey("Sound")) sliderSound.value = PlayerPrefs.GetFloat("Sound");
        else sliderSound.value = 1.0f;
        if (PlayerPrefs.HasKey("NaturaSelect")) selectNatura = PlayerPrefs.GetInt("NaturaSelect");
        else selectNatura = 1;
        if (PlayerPrefs.HasKey("DinoSelect")) selectDino = PlayerPrefs.GetInt("DinoSelect");
        else selectDino = 0;
        if (PlayerPrefs.HasKey("CastleSelect")) selectCastle = PlayerPrefs.GetInt("CastleSelect");
        else selectCastle = 0;
        if (PlayerPrefs.HasKey("FirstEnter")) notFirstEnterGame = PlayerPrefs.GetInt("FirstEnter");
        else notFirstEnterGame = 0;
        if (PlayerPrefs.HasKey("RecordEasy")) recordEasy = PlayerPrefs.GetInt("RecordEasy");
        else recordEasy = 1000;
        if (PlayerPrefs.HasKey("RecordMedium")) recordMedium = PlayerPrefs.GetInt("RecordMedium");
        else recordMedium = 2000;
        if (PlayerPrefs.HasKey("RecordHard")) recordHard = PlayerPrefs.GetInt("RecordHard");
        else recordHard = 3000;
        if (PlayerPrefs.HasKey("RecordCrazy")) recordCrazy = PlayerPrefs.GetInt("RecordCrazy");
        else recordCrazy = 5000;
    }

    private void Start()
    {
        Load();
    }
}
