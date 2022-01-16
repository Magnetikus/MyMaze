using UnityEngine;
using System.Collections.Generic;

public class LoadResurces : MonoBehaviour
{

    [SerializeField] private GameObject[] _resurcesNature;
    [SerializeField] private GameObject[] _resurcesDinopark;
    [SerializeField] private GameObject[] _resurcesCastle;
    [SerializeField] private SaveLoadGame _saveLoadGame;
    [SerializeField] private GameControler _gameController;

    private int _location;
    private int _selectNatura;
    private int _selectDino;
    private int _selectCastle;
    private List<int> _listLocation;
    private int _index;
    private System.Random _random = new System.Random();

    private void Start()
    {
        _listLocation = new List<int>();
    }
    public GameObject[] GetResurces()
    {
        _listLocation.Clear();
        _selectNatura = _saveLoadGame.selectNatura;
        _selectDino = _saveLoadGame.selectDino;
        _selectCastle = _saveLoadGame.selectCastle;
        if (_selectNatura != 0) _listLocation.Add(1);
        if (_selectDino != 0) _listLocation.Add(2);
        if (_selectCastle != 0) _listLocation.Add(3);
        if (_listLocation.Count == 0) _listLocation.Add(1);
        _index = _random.Next(1, _listLocation.Count);
        _location = _listLocation[_index - 1];
        _gameController.SetLocation(_location);
        switch (_location)
        {
            case (1):
                return _resurcesNature;

            case (2):
                if (_resurcesDinopark.Length != 0)
                {
                    return _resurcesDinopark;
                }
                else
                {
                    return _resurcesNature;
                }

            case (3):
                if (_resurcesCastle.Length != 0)
                {
                    return _resurcesCastle;
                }
                else
                {
                    return _resurcesNature;
                }

            default:
                return _resurcesNature;
        }
    }
}
