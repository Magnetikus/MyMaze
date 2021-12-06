using UnityEngine;

public class MovetCube : MonoBehaviour
{
    [SerializeField] private FpsMovement _fpsMovement;
    [SerializeField] private GameObject _cubeInHundle;

    private GameObject _prefabWoll;
    private GameControler _gameController;
    private SaveProgress _saveProgress;
    private bool _firstStepMovetCube = false;
    private bool _secondStepMovetCube = false;
    private GameObject _wollMovet;
    private GameObject _cellForCube;
    private float _speedPlayer;


    private static string movet = "Movet";
    private static string cell = "Cell";

    private void Start()
    {
        _gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControler>();
        _saveProgress = _gameController.GetComponent<SaveProgress>();

    }

    public void SetCubeInHundle(GameObject prefabNew)
    {
        _prefabWoll = Instantiate(prefabNew);
        _prefabWoll.transform.SetParent(_cubeInHundle.transform, false);
        _prefabWoll.GetComponent<VisibleOnn>().SetVisible(true);
        _cubeInHundle.SetActive(false);
    }

    public void CubeMovetFirstStep()
    {
        _firstStepMovetCube = true;
        _speedPlayer = _fpsMovement.GetSpeed();
    }

    public void CubeDesactive()
    {
        VisibleOnn visibleOnnScript = _wollMovet.GetComponent<VisibleOnn>();
        WollMovet wollMovetScript = _wollMovet.GetComponentInChildren<WollMovet>();
        visibleOnnScript.OffVisible();
        visibleOnnScript.OffSpriteMap();
        wollMovetScript.DesactivCube();
        wollMovetScript.ActivCell();
        _wollMovet.SetActive(false);
        _fpsMovement.SetSpeedWithOrNotCube(_speedPlayer * (0.5f + _saveProgress.PowerPlayer * 0.05f));
        _cubeInHundle.SetActive(true);
        _secondStepMovetCube = true;
    }

    public void CubeActive()
    {
        VisibleOnn visibleOnnScript = _cellForCube.GetComponent<VisibleOnn>();
        CellMovet cellMovetScript = _cellForCube.GetComponentInChildren<CellMovet>();
        visibleOnnScript.OffVisible();
        visibleOnnScript.OffSpriteMap();
        cellMovetScript.DeactivCell();
        cellMovetScript.ActivWoll();
        _cellForCube.SetActive(false);
        _fpsMovement.SetSpeedWithOrNotCube(_speedPlayer);
        _cubeInHundle.SetActive(false);
        _gameController.MinusCube();
        _firstStepMovetCube = false;
        _secondStepMovetCube = false;
    }

    public void EscapeMovet()
    {
        _firstStepMovetCube = false;
    }

    public void ResetEndGame()
    {
        _firstStepMovetCube = false;
        _secondStepMovetCube = false;
    }

    public void ResetFromMinusLife()
    {
        if (_firstStepMovetCube)
        {
            _cubeInHundle.SetActive(false);
            _fpsMovement.SetSpeedWithOrNotCube(_speedPlayer);
            if (!_secondStepMovetCube)
            {
                if (_wollMovet != null)
                {
                    WollMovet wollMovetScript = _wollMovet.GetComponentInChildren<WollMovet>();
                    wollMovetScript.DesactivCube();
                }
            }
            else
            {
                if (_cellForCube != null)
                {
                    CellMovet cellMovetScript = _cellForCube.GetComponentInChildren<CellMovet>();
                    cellMovetScript.DeactivCell();
                }
            }
        }
        _firstStepMovetCube = false;
        _secondStepMovetCube = false;
    }

    private void Update()
    {
        if (!_gameController.pausedGame)
        {

            if (_firstStepMovetCube && !_secondStepMovetCube)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)), out RaycastHit hit, 6f))
                {
                    if (hit.collider.CompareTag(movet))
                    {
                        _wollMovet = hit.collider.gameObject;
                        if (_wollMovet != null) _wollMovet.GetComponentInChildren<WollMovet>().ActivCube();
                    }
                }
            }
            if (_firstStepMovetCube && _secondStepMovetCube)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)), out RaycastHit hit, 6f))
                {
                    if (hit.collider.CompareTag(cell))
                    {
                        _cellForCube = hit.collider.gameObject;
                        if (_cellForCube != null) _cellForCube.GetComponentInChildren<CellMovet>().ActivCell();
                    }
                }
            }
        }
    }
}
