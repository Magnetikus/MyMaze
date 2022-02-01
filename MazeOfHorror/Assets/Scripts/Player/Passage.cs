using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{
    private GameControler _gameController;
    private GameObject _woll;
    private SaveProgress _saveProgress;
    private bool _firstStep = false;
    private bool _secondStep = false;

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControler>();
        _saveProgress = _gameController.GetComponent<SaveProgress>();
    }


    public void PassageFirstStep()
    {
        _firstStep = true;
    }

    public void PassageActivate()
    {
        WollMovet wollMovetScript = _woll.GetComponentInChildren<WollMovet>();
        wollMovetScript.DesactivCube();
        _gameController.MinusPassage();
        _woll.GetComponent<PassageWoll>().StartPassage(_saveProgress.PowerPlayer);
        _secondStep = true;
    }

    public void PassageEnd()
    {
        _firstStep = false;
        _secondStep = false;
    }

    public void ResetFromMinusLife()
    {
        if (_firstStep)
        {
            if (_woll != null)
            {
                WollMovet wollMovetScript = _woll.GetComponentInChildren<WollMovet>();
                wollMovetScript.DesactivCube();
            }
        }
        _firstStep = false;
        _secondStep = false;
    }


    private void Update()
    {
        if (!_gameController.pausedGame)
        {

            if (_firstStep && !_secondStep)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)), out RaycastHit hit, 6f))
                {
                    if (hit.collider.CompareTag("Movet"))
                    {
                        _woll = hit.collider.gameObject;
                        if (_woll != null)
                        {
                            _woll.GetComponentInChildren<WollMovet>().ActivCube();
                        }
                    }
                }
            }
        }
    }


}
