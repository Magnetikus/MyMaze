using UnityEngine;

[RequireComponent(typeof(CharacterController))]

// basic WASD-style movement control
public class FpsMovement : MonoBehaviour
{
    [SerializeField] private Camera headCam;

    public float speed = 5.0f;
    public float gravity = -9.8f;

    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float rotationVert = 0;

    private bool isMovetCube = false;

    private CharacterController charController;
    private GameControler gameContr;

    public void SetIsMovetCube(bool value)
    {
        isMovetCube = value;
    }

    void Start()
    {
        charController = GetComponent<CharacterController>();
        gameContr = GameObject.Find("Controller").GetComponent<GameControler>();
    }

    void Update()
    {
        if (!gameContr.pausedGame)
        {
            SetSpeedPlayer();
            MoveCharacter();
            RotateCharacter();
            RotateCamera();
        }
    }

    private void MoveCharacter()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        charController.Move(movement);

        var pos = transform.position;
        if (pos.y != 0) pos.y = 0;
        transform.position = pos;
    }

    private void RotateCharacter()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
    }

    private void RotateCamera()
    {
        rotationVert -= Input.GetAxis("Mouse Y") * sensitivityVert;
        rotationVert = Mathf.Clamp(rotationVert, minimumVert, maximumVert);

        headCam.transform.localEulerAngles = new Vector3(rotationVert, headCam.transform.localEulerAngles.y, 0);
    }

    private void SetSpeedPlayer()
    {
        if (isMovetCube == true)
        {
            speed = 2.0f;
        }
        else
        {
            speed = 5.0f;
        }
    }
}
