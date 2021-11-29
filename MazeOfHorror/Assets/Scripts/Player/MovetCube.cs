using UnityEngine;

public class MovetCube : MonoBehaviour
{
    [SerializeField] private FpsMovement _fpsMovement;
    [SerializeField] private GameObject _cubeInHundle;

    private GameControler _gameController;
    private bool _firstStepMovetCube = false;
    private bool _secondStepMovetCube = false;
    private GameObject _wollMovet;
    private GameObject _cellForCube;


    private static string movet = "Movet";
    private static string cell = "Cell";

    private void Start()
    {
        _gameController = GameObject.Find("Controller").GetComponent<GameControler>();
    }

    public void CubeMovetFirstStep()
    {
        _firstStepMovetCube = true;
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
        _fpsMovement.SetSpeedWithOrNotCube(0.5f);
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
        _fpsMovement.SetSpeedWithOrNotCube(2f);
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

    private void Update()
    {
        if (!_gameController.pausedGame && _gameController.GetCube() > 0)
        {

            if (_firstStepMovetCube && !_secondStepMovetCube)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2)), out RaycastHit hit, 6f))
                {
                    if (hit.collider.CompareTag(movet))
                    {
                        _wollMovet = hit.collider.gameObject;
                        _wollMovet.GetComponentInChildren<WollMovet>().ActivCube();
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
                        _cellForCube.GetComponentInChildren<CellMovet>().ActivCell();
                    }
                }
            }
        }
    }
}
