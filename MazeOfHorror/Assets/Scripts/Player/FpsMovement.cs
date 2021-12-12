using UnityEngine;

[RequireComponent(typeof(CharacterController))]


public class FpsMovement : MonoBehaviour
{
    [SerializeField] private Camera _headCam;

    private static float _speed = 2.5f; // max 4.5
    private static float _sensitivity = 3f; //max 4
    private static float _minimumVert = -45.0f;
    private static float _maximumVert = 45.0f;

    private float _rotationVert = 0;

    private CharacterController _charController;
    private GameControler _gameContr;
    private JoystickController _joystController;
    private JoystickLooket _joystickLooket;

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
        _speed += value * 0.2f;
        _sensitivity += value * 0.1f;
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
    }

    private void Update()
    {
        if (!_gameContr.pausedGame)
        {
            
            MoveCharacter();
            RotateCharacter();
            RotateCamera();
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

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);
        movement.y = 0;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        _charController.Move(movement);
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
}
