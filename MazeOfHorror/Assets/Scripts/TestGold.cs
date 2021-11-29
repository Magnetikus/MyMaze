using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestGold : MonoBehaviour
{
    [SerializeField] Text _textGold;
    [SerializeField] Text _textDimond;
    [SerializeField] Text _textLevel;
    [SerializeField] SaveProgress _saveProgress;

    private int _gold;
    private int _dimond;
    private int _level;

    public void GetResurces()
    {
        _gold = _saveProgress.AmountGold;
        _dimond = _saveProgress.AmountDimond;
        _level = _saveProgress.LevelPlayer;
    }

    public void SetResurces()
    {
        _saveProgress.SetGold(_gold);
        _saveProgress.SetDimond(_dimond);
        _saveProgress.SetLevel(_level);
        _saveProgress.Save();
    }

    public void PlusMinusGold(int value)
    {
        _gold += value;
    }

    public void PlusMinusDimond(int value)
    {
        _dimond += value;
    }

    public void PlusMinusLevel(int value)
    {
        _level += value;
    }

    public void ResetAll()
    {
        _saveProgress.SetGold(0);
        _saveProgress.SetDimond(0);
        _saveProgress.SetManna(0);
        _saveProgress.SetLevel(0);
        _saveProgress.SetSpeed(0);
        _saveProgress.SetPower(0);
        _saveProgress.SetLife(3);
        _saveProgress.SetWoll(3);
        _saveProgress.SetPass(0);
        _saveProgress.SetPort(0);
        _saveProgress.SetImmuny(0);
        _saveProgress.Save();
    }

    private void OnGUI()
    {
        _textGold.text = $"{_gold}";
        _textDimond.text = $"{ _dimond}";
        _textLevel.text = $"{_level}";
    }
}
