using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerPort : MonoBehaviour
{
    [SerializeField] private GameControler _gameController;
    [SerializeField] private Transform _mapPoint;
    [SerializeField] private Button _button;
    [SerializeField] private Image _imagePower;
    [SerializeField] private Image _mapRecttangle;
    [SerializeField] private Slider _mapSlider;
    [SerializeField] private GameObject _teleportActivity;
    private GameObject[] _arrayAllCell;
    private GameObject _player;
    private List<GameObject> _arrayTargetCell;
    private Vector2 _positionInRectangle;
    private Vector3 _positionForTeleport;
    private float _distanceForTeleport = 15f;
    private bool _firstStep = false;
    private float _timeRevers = 60f;

    private static readonly string Player = "Player";
    private static readonly string Cell = "Cell";


    private void Start()
    {
        _arrayTargetCell = new List<GameObject>();
    }

    public void SetTimeRevers(int mannaPlayer)
    {
        _timeRevers -= mannaPlayer * 4f;
    }


    public void StartTeleport()
    {
        _firstStep = true;
        _gameController.IsVisibleIconesPower(false);
        _player = GameObject.FindGameObjectWithTag(Player);
        Vector3 positionPlayer = _player.transform.position;
        float distance;
        _distanceForTeleport += 3 * _gameController.GetComponent<SaveProgress>().PowerPlayer; 
        _arrayAllCell = GameObject.FindGameObjectsWithTag(Cell);
        for (int i = 0; i < _arrayAllCell.Length; i++)
        {
            distance = (positionPlayer - _arrayAllCell[i].transform.position).magnitude;
            if (distance < _distanceForTeleport)
            {
                if (_arrayAllCell[i].name != "Vision")
                {
                    _arrayAllCell[i].GetComponent<VisibleOnn>().OnSpriteTeleport();
                    _arrayTargetCell.Add(_arrayAllCell[i]);
                }
            }
        }
    }

    public void EscapeTeleport()
    {
        _teleportActivity.SetActive(false);
        _gameController.IsVisibleIconesPower(true);
        for (int i = 0; i < _arrayAllCell.Length; i++)
        {
            if (_arrayAllCell[i].name != "Vision")
            {
                _arrayAllCell[i].GetComponent<VisibleOnn>().OffSpriteTeleport();
            }
        }
        ChekButton();
    }

    public void TeleportActivity()
    {
        for (int i = 0; i < _arrayAllCell.Length; i++)
        {
            if (_arrayAllCell[i].name != "Vision")
            {
                _arrayAllCell[i].GetComponent<VisibleOnn>().OffSpriteTeleport();
            }
        }
        _player.GetComponent<Teleport>().StartTeleport(_positionForTeleport);
        _gameController.MinusPort();
        _gameController.PausedGame(true);
        Invoke("EndTeleport", 7.5f);
    }



    public void EndTeleport()
    {

        _teleportActivity.SetActive(false);
        _gameController.IsVisibleIconesPower(true);
        _button.enabled = false;
        _firstStep = false;
        _gameController.PausedGame(false);
        _imagePower.fillAmount = 0;
        StartCoroutine(FillImage());
    }

    public void ResetAfterMinusLife()
    {
        _teleportActivity.SetActive(false);
        _button.enabled = true;
        _firstStep = false;
        _imagePower.fillAmount = 1;
        if (_arrayAllCell != null)
        {
            for (int i = 0; i < _arrayAllCell.Length; i++)
            {
                if (_arrayAllCell[i].name != "Vision")
                {
                    _arrayAllCell[i].GetComponent<VisibleOnn>().OffSpriteTeleport();
                }
            }
        }
        ChekButton();
    }

    private IEnumerator FillImage()
    {
        var wait = new WaitForSeconds(_timeRevers / 100);
        Color color = _imagePower.color;
        color.a = 0.5f;
        _imagePower.color = color;
        while (_imagePower.fillAmount < 1)
        {
            _imagePower.fillAmount += 0.01f;
            yield return wait;
        }
        color.a = 1f;
        _imagePower.color = color;
        ChekButton();
    }

    private void Update()
    {
        if (_firstStep)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_mapRecttangle.rectTransform,
                    Input.mousePosition, null, out _positionInRectangle))
                {
                    _positionInRectangle.x /= (_mapRecttangle.rectTransform.sizeDelta.x / 2) / _mapSlider.value;
                    _positionInRectangle.y /= (_mapRecttangle.rectTransform.sizeDelta.y / 2) / _mapSlider.value;
                    Vector3 targetPosition = new Vector3(_mapPoint.position.x + _positionInRectangle.x - 1,
                        0, _mapPoint.position.z + _positionInRectangle.y - 1);
                    for (int i = 0; i < _arrayTargetCell.Count; i++)
                    {
                        if ((_arrayTargetCell[i].transform.position - targetPosition).magnitude < 0.9f)
                        {
                            _arrayTargetCell[i].GetComponent<VisibleOnn>().OnBlueColorTeleport();
                            _teleportActivity.SetActive(true);
                            _positionForTeleport = _arrayTargetCell[i].transform.position;
                        }
                        else
                        {
                            _arrayTargetCell[i].GetComponent<VisibleOnn>().OnGreenColorTeleport();
                        }
                    }
                }
            }
        }
    }

    public void ChekButton()
    {
        if (_gameController.GetPort() > 0)
        {
            _button.enabled = true;
        }
        else _button.enabled = false;
    }

    public void StopCourotine()
    {
        StopCoroutine(FillImage());
    }
}
