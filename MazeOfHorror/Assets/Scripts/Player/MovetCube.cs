using UnityEngine;

public class MovetCube : MonoBehaviour
{
    [SerializeField] private FpsMovement _fpsMovement;
    [SerializeField] private GameObject _cubeInHundle;

    private GameControler _gameController;
    private bool _firstStepMovetCube = false;
    private bool _secondStepMovetCube = false;
    
    

    private void Start()
    {
        _gameController = GameObject.Find("Controller").GetComponent<GameControler>();
        
    }

    private void Update()
    {
        if (!_gameController.pausedGame && _gameController.GetCube() > 0)
        {
            if (!_firstStepMovetCube && Input.GetKeyDown(KeyCode.F))
            {
                _gameController.cursorOnOff = true;
                _firstStepMovetCube = true;
            }

            if (_firstStepMovetCube && !_secondStepMovetCube)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 6f))
                {
                    if (hit.collider.CompareTag("Movet"))
                    {
                        WollMovet wollMovetScript = hit.collider.GetComponentInChildren<WollMovet>();
                        wollMovetScript.ActivCube();
                        if (Input.GetMouseButtonDown(0))
                        {
                            VisibleOnn visibleOnnScript = hit.collider.GetComponent<VisibleOnn>();
                            GameObject thisCube = hit.collider.gameObject;
                            visibleOnnScript.OffVisible();
                            visibleOnnScript.OffSpriteMap();
                            wollMovetScript.DesactivCube();
                            wollMovetScript.ActivCell();
                            thisCube.SetActive(false);
                            _fpsMovement.SetIsMovetCube(true);
                            _gameController.cursorOnOff = false;
                            //_gameController.cubeMovet = true;
                            _cubeInHundle.SetActive(true);
                            _secondStepMovetCube = true;
                        }
                    }
                }
            }
            if (_firstStepMovetCube && _secondStepMovetCube)
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 6f))
                {
                    if (hit.collider.CompareTag("Cell"))
                    {
                        CellMovet cellMovetScript = hit.collider.GetComponentInChildren<CellMovet>();
                        VisibleOnn visibleOnnScript = hit.collider.GetComponent<VisibleOnn>();
                        cellMovetScript.ActivCell();
                        if (Input.GetMouseButtonDown(0) && visibleOnnScript.GetBusy() == false)
                        {
                            
                            GameObject thisCell = hit.collider.gameObject;
                            visibleOnnScript.OffVisible();
                            visibleOnnScript.OffSpriteMap();
                            cellMovetScript.DeactivCell();
                            cellMovetScript.ActivWoll();
                            thisCell.SetActive(false);
                            _fpsMovement.SetIsMovetCube(false);
                            //_gameController.cubeMovet = false;
                            _cubeInHundle.SetActive(false);
                            _gameController.MinusCube();
                            _firstStepMovetCube = false;
                            _secondStepMovetCube = false;
                        }
                    }
                }
            }
        }
    }
}
