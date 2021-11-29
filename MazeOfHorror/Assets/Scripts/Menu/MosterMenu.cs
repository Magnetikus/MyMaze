using System.Collections.Generic;
using UnityEngine;

public class MosterMenu : MonoBehaviour
{
    public enum State
    {
        Idle = 0,
        Movet = 1,
        Rotation = 2,
        Alert = 3,
        RunTarget = 4,
        Attack = 5,
        Scaner = 6,
        Roar = 7
    }

    public State state;
    public Transform scanerTransform;
    public Transform scanerForward;
    public Transform scanerLeft;
    public Transform scanerRight;

    private const float _speedNormal = 10f;
    private const float _speedMaximum = 40f;
    private const float _speedRotation = 10f;
    private List<Vector3> listPoint;
    private Vector3 positionMovet;
    private Animator animator;
    private Rigidbody rb;
    private static readonly int RotationAnim = Animator.StringToHash("Rotation");
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int RoarAnim = Animator.StringToHash("Roar");

    public void SetState(State newState)
    {
        state = newState;
    }

    private void Start()
    {
        state = State.Idle;
        listPoint = new List<Vector3>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private bool Movet(Vector3 targetPosition, float speedMovet, float speedRotation)
    {
        animator.SetFloat(Speed, speedMovet / _speedNormal);
        Vector3 distance = targetPosition - transform.position;
        if (distance.magnitude < 0.01f)
        {
            rb.MovePosition(targetPosition);
            rb.velocity = Vector3.zero;
            return true;
        }
        Vector3 direction = distance.normalized;

        // Rotation

        rb.rotation = Quaternion.LookRotation(direction);

        // Movet

        float velocity = rb.velocity.sqrMagnitude;

        if (distance.magnitude > 0.02f)
        {
            if (velocity < _speedMaximum)
            {
                rb.AddRelativeForce(new Vector3(0, 0, speedMovet));
            }
            else rb.AddRelativeForce(Vector3.zero);
            return false;
        }
        transform.position = targetPosition;
        return true;
    }


    private List<Vector3> Scaner(Vector3 direction)
    {
        Vector3 ray = scanerTransform.TransformDirection(direction);
        if (Physics.Raycast(scanerTransform.position, ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Cell"))
            {
                GameObject go = hit.collider.gameObject;
                listPoint.Add(go.GetComponent<Transform>().position);
            }
        }
        return listPoint;
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Idle:
                transform.position = transform.position;
                animator.SetFloat(Speed, 0f);

                if (Random.value < 0.2f)
                {
                    animator.SetTrigger(RotationAnim);
                    state = State.Rotation;
                }
                else if (Random.value < 0.2f)
                {
                    animator.SetTrigger(RoarAnim);
                    state = State.Roar;
                }
                else
                {
                    state = State.Scaner;
                }
                break;

            case State.Scaner:

                animator.SetFloat(Speed, 0f);
                listPoint.Clear();
                Scaner(Vector3.forward);
                Scaner(Vector3.left);
                Scaner(Vector3.right);
                Scaner(Vector3.back);
                int lenghtListPoint = listPoint.Count;
                switch (lenghtListPoint)
                {
                    case (1):
                        positionMovet = listPoint[0];
                        break;
                    case (2):
                        positionMovet = Random.value < 0.95f ? listPoint[0] : listPoint[1];
                        break;
                    case (3):
                        positionMovet = Random.value < 0.5f ? listPoint[0] : Random.value < 0.8f ? listPoint[1] : listPoint[2];
                        break;
                    case (4):
                        positionMovet = Random.value > 0.05f ? listPoint[0] : Random.value < 0.5f ? listPoint[3] :
                            Random.value < 0.5f ? listPoint[1] : listPoint[2];
                        break;
                    default:
                        state = State.Idle;
                        break;
                }
                state = State.Movet;
                break;

            case State.Movet:

                if (Movet(positionMovet, _speedNormal, _speedRotation))
                {
                    state = State.Idle;
                    break;
                }
                break;

            case State.Rotation:
                
                break;

            case State.Roar:
                
                break;
        }
    }
}
