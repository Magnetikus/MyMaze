using UnityEngine;

[RequireComponent(typeof(CharacterController))]


public class FpsMovement : MonoBehaviour
{
    [SerializeField] private Camera _headCam;

    private static float _speed = 3.0f; // max 7
    private static float _gravity = -9.8f;
    private static float _sensitivityHor = 4.5f;
    private static float _sensitivityVert = 4.5f;
    private static float _minimumVert = -45.0f;
    private static float _maximumVert = 45.0f;

    private float _rotationVert = 0;

    private CharacterController _charController;
    private GameControler _gameContr;
    private JoystickController _joystController;
    private JoystickLooket _joystickLooket;

    public void SetSpeedWithOrNotCube(float value)
    {
        _speed *= value;
    }

    public void SetSpeed(float value)
    {
        if (value != 0)
        {
            _speed = value;
        }
        else
        {
            _speed = 3f;
        }
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

        float deltaX = _joystController.Horizontal() * _speed;
        float deltaZ = _joystController.Vertical() * _speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, _speed);

        movement.y = _gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        _charController.Move(movement);

        var pos = transform.position;
        if (pos.y != 0) pos.y = 0;
        transform.position = pos;
    }

    private void RotateCharacter()
    {
        transform.Rotate(0, _joystickLooket.HorizontalRotate() * _sensitivityHor * Time.deltaTime,0);
    }

    private void RotateCamera()
    {
        _rotationVert -= _joystickLooket.VerticalRotate() * _sensitivityVert * Time.deltaTime;
        _rotationVert = Mathf.Clamp(_rotationVert, _minimumVert, _maximumVert);

        _headCam.transform.localEulerAngles = new Vector3(_rotationVert, _headCam.transform.localEulerAngles.y, 0);
    }
}
