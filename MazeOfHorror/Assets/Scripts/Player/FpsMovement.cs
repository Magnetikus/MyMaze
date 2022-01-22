using UnityEngine;

[RequireComponent(typeof(CharacterController))]


public class FpsMovement : MonoBehaviour
{
    [SerializeField] private Camera _headCam;
    [SerializeField] private PlaySound _playSound;

    private const float _normalSpeed = 2.5f;        //max 4.5
    private const float _normalSensitivity = 4.3f;  //max 4.5
    private const float _minimumVert = -45.0f;
    private const float _maximumVert = 45.0f;
    private const string _step = "Step";

    private static float _speed;
    private static float _sensitivity;
    private float _rotationVert = 0;
    private Vector3 _movement;
    private Vector3 _myPosition;
    private float _timer;

    private CharacterController _charController;
    private GameControler _gameContr;
    private JoystickController _joystController;
    private JoystickLooket _joystickLooket;
    private Transform _transformMinimap;

    public void SetSpeedWithOrNotCube(float value)
    {
        _speed = value;
    }

    public float GetSpeed()
    {
        return _speed;
    }

    public void SetSpeed(float value)
    {
        _speed = _normalSpeed + value * 0.2f;
        _sensitivity = _normalSensitivity + value * 0.02f;
    }

    public void SetSensity(float value)
    {
        _sensitivity = value;
    }

    private void Start()
    {
        _charController = GetComponent<CharacterController>();
        _gameContr = GameObject.Find("Controller").GetComponent<GameControler>();
        _joystController = GameObject.FindGameObjectWithTag("JoystickMovet").GetComponent<JoystickController>();
        _joystickLooket = GameObject.FindGameObjectWithTag("JoystickLooket").GetComponent<JoystickLooket>();
        _transformMinimap = GameObject.FindGameObjectWithTag("MinimapPoint").GetComponent<Transform>();
    }

    private void Update()
    {
        if (!_gameContr.pausedGame)
        {
            
            MoveCharacter();
            RotateCharacter();
            RotateCamera();
            MinimapMovet();
        }
    }

    private void MoveCharacter()
    {
#if UNITY_EDITOR
        float deltaX = Input.GetAxis("Horizontal") * _speed;
        float deltaZ = Input.GetAxis("Vertical") * _speed;
#else

        float deltaX = _joystController.Horizontal() * _speed;
        float deltaZ = _joystController.Vertical() * _speed;

#endif

        _movement = new Vector3(deltaX, 0, deltaZ);
        _movement = Vector3.ClampMagnitude(_movement, _speed);
        _movement.y = 0;
        _movement *= Time.deltaTime;
        _movement = transform.TransformDirection(_movement);
        _charController.Move(_movement);
        FootStepSound(Mathf.Abs(_movement.sqrMagnitude));
    }

    private void RotateCharacter()
    {
#if UNITY_EDITOR
        transform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivity * 30f * Time.deltaTime, 0);
#else
        transform.Rotate(0, _joystickLooket.HorizontalRotate() * _sensitivity * Time.deltaTime, 0);
#endif
    }

    private void RotateCamera()
    {
#if UNITY_EDITOR
        _rotationVert -= Input.GetAxis("Mouse Y") * _sensitivity * 30f * Time.deltaTime;
#else
        _rotationVert -= _joystickLooket.VerticalRotate() * _sensitivity * Time.deltaTime;
#endif

        _rotationVert = Mathf.Clamp(_rotationVert, _minimumVert, _maximumVert);

        _headCam.transform.localEulerAngles = new Vector3(_rotationVert, _headCam.transform.localEulerAngles.y, 0);
    }

    private void MinimapMovet()
    {
        _myPosition = transform.position;
        _myPosition.y = 5;
        _transformMinimap.position = _myPosition;
    }

    private void FootStepSound(float movetABS)
    {
        if (movetABS > 0)
        {
            _timer += movetABS * 25;
            if (_timer > _normalSpeed)
            {
                _playSound.Play(_step);
                _timer = 0;
            }
        }
    }
}
