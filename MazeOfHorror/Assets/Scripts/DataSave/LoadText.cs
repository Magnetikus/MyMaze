using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadText : MonoBehaviour
{
    private string[] _textProgress;
    private string _path;

    public string TextSpeed { get; private set; }
    public string TextPower { get; private set; }
    public string TextManna { get; private set; }
    public string TextLife { get; private set; }
    public string TextWoll { get; private set; }
    public string TextPass { get; private set; }
    public string TextPort { get; private set; }
    public string TextImmuny { get; private set; }

    private void Start()
    {
        _path = $"Assets/Resources/ru/Progress.txt";
        _textProgress = File.ReadAllLines(_path);
        TextSpeed = _textProgress[0];
        TextPower = _textProgress[1];
        TextManna = _textProgress[2];
        TextLife = _textProgress[3];
        TextWoll = _textProgress[4];
        TextPass = _textProgress[5];
        TextPort = _textProgress[6];
        TextImmuny = _textProgress[7];
    }
}
