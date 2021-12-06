using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
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

    public enum NameMonster
    {
        Nose = 0,
        Ear = 1,
        Eye = 2,
    }

    public State state;
    public NameMonster nameMonster;
    public Transform scanerTransform;
    public Transform scanerForward;
    public Transform scanerLeft;
    public Transform scanerRight;


    private static float _speedNormal = 10f;
    private static float _speedAttack = 2 * _speedNormal;
    private static float _speedMaximum = 4 * _speedNormal;
    private static float _speedRotation = 10f;
    private static float _speedPlayerForChangeInAlert = 0.05f;
    private static float _distanceToPlayerForChangeInAlert = 8f;
    private static float _distanceToPlayerForChangeInRunTarget = 0.5f * _distanceToPlayerForChangeInAlert;
    private static float _distanceInAttack = 0.25f * _distanceToPlayerForChangeInAlert;


    private List<Vector3> listPoint;
    private List<float> listScentPlayer;
    private GameObject player;
    private ChangeColliderPlayer collaiderPlayer;
    private Vector3 positionPlayer;
    private Vector3 positionPlayerOld;
    private Vector3 positionMovet;
    private Vector3 positionPlayerOutSight;
    private GameControler gameContr;
    private Animator animator;
    private Rigidbody rb;
    private static readonly int RotationAnim = Animator.StringToHash("Rotation");
    private static readonly int SpeedAnim = Animator.StringToHash("Speed");
    private static readonly int AttackAnim = Animator.StringToHash("Attack");
    private static readonly int RoarAnim = Animator.StringToHash("Roar");


    public void SetSpeedNormal(float value)
    {
        _speedNormal += value * 0.1f;
        _distanceToPlayerForChangeInAlert += value * 0.1f;
    }

    public void Restart()
    {
        Start();
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    private void Start()
    {
        state = State.Idle;
        listPoint = new List<Vector3>();
        listScentPlayer = new List<float>();
        player = GameObject.Find("Player(Clone)");
        collaiderPlayer = player.GetComponent<ChangeColliderPlayer>();
        gameContr = GameObject.Find("Controller").GetComponent<GameControler>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    private bool Movet(Vector3 targetPosition, float speedMovet, float speedRotation)
    {
        animator.SetFloat(SpeedAnim, speedMovet / _speedNormal);

        Vector3 distance = targetPosition - transform.position;
        if (distance.magnitude < 0.01f)
        {
            rb.isKinematic = true;
            transform.position = targetPosition;
            rb.velocity = Vector3.zero;
            rb.isKinematic = false;
            return true;
        }
        Vector3 direction = distance.normalized;

        // Rotation

        rb.rotation = Quaternion.LookRotation(direction);

        if (RaycastForwardToMonsters())
        {
            rb.velocity = Vector3.zero;
            return true;
        }

        // Movet

        float velocity = rb.velocity.sqrMagnitude;

        if (distance.magnitude > 0.1f)
        {
            if (velocity < _speedMaximum)
            {
                rb.AddRelativeForce(new Vector3(0, 0, speedMovet));
            }
            else rb.AddRelativeForce(Vector3.zero);
            return false;
        }
        else rb.AddRelativeForce(Vector3.zero);

        transform.position = targetPosition;
        return true;
    }

    private bool AnglePlayerAndForwardMonster(Vector3 playerPosition, Vector3 myPosition)
    {
        Vector3 distance = playerPosition - myPosition;

        float angle = Mathf.Abs(Vector3.Angle(transform.forward, distance));
        if (angle < 120f) return true;
        return false;
    }

    private List<Vector3> Scaner(Vector3 direction)
    {
        Vector3 ray = scanerTransform.TransformDirection(direction);
        if (Physics.Raycast(scanerTransform.position, ray, out RaycastHit hit, 3f))
        {
            if (hit.collider.CompareTag("Cell"))
            {
                GameObject go = hit.collider.gameObject;
                if (go.GetComponent<VisibleOnn>().GetBusy() == false)
                {
                    listPoint.Add(go.GetComponent<Transform>().position);
                    listScentPlayer.Add(go.GetComponent<VisibleOnn>().GetAmountScent());
                }
                
            }
        }
        return listPoint;
    }

    private List<Vector3> ScanerForward()
    {
        bool iMonster = false;
        iMonster = RaycastForwardToMonsters();
        if (!iMonster) Scaner(Vector3.forward);
        else Scaner(Vector3.back);
        return listPoint;
    }

    private bool RaycastToPlayer()
    {
        if (Physics.Raycast(transform.position, (positionPlayer - transform.position).normalized, out RaycastHit hitRay))
        {
            if (hitRay.collider.CompareTag("Player"))
            {
                return true;
            }
            else return false;
        }
        else return false;
    }

    private void CorrectionMovet()
    {

        if (RaycastForwardToWoll())
        {
            Vector3 direction = (positionPlayer - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, direction);
            if (angle < 5f)
            {
                state = State.Idle;
            }
        }
        else
        {
            float scanerLeftAndRight = ScanerLeftAndRight();
            if (scanerLeftAndRight != 0)
            {
                rb.AddRelativeForce(new Vector3(scanerLeftAndRight * _speedAttack, 0, 0));
            }
        }

    }

    private bool RaycastForwardToWoll()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, 1.5f))
        {
            if (hit.collider.CompareTag("Movet"))
            {
                
                return true;
            }
            else return false;
        }
        else return false;
    }

    private bool RaycastForwardToMonsters()
    {

        RaycastHit[] hits;
        hits = Physics.RaycastAll(scanerTransform.position, scanerTransform.TransformDirection(Vector3.forward), 6f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            if (hit.collider.CompareTag("Monster"))
            {
                return true;
            }
        }
        return false;
    }

    private float ScanerLeftAndRight()
    {
        Vector3 ray = transform.TransformDirection(Vector3.forward);
        RaycastHit hit;
        float hitLeft = 0;
        float hitRight = 0;
        float resultat;
        if (Physics.Raycast(scanerLeft.position, ray, out hit, 1f))
        {
            if (hit.collider.CompareTag("Movet"))
            {
                Vector3 direction = scanerLeft.position - hit.transform.position;
                hitLeft = Mathf.Sign(Vector3.Dot(transform.up, Vector3.Cross(transform.forward, direction)));
            }
        }
        if (Physics.Raycast(scanerRight.position, ray, out hit, 1f))
        {
            if (hit.collider.CompareTag("Movet"))
            {
                Vector3 direction = scanerRight.position - hit.transform.position;
                hitRight = Mathf.Sign(Vector3.Dot(transform.up, Vector3.Cross(transform.forward, direction)));
            }
        }
        resultat = hitLeft + hitRight;
        return resultat;
    }

    private void FixedUpdate()
    {
        if (player == null) player = GameObject.FindGameObjectWithTag("Player");
        if (!gameContr.pausedGame)
        {
            positionPlayer = player.transform.position;
            float speedPlayerReal = (positionPlayer - positionPlayerOld).magnitude;
            float distanceToPlayer = (positionPlayer - transform.position).magnitude;


            if (nameMonster == NameMonster.Ear)
            {
                if (RaycastToPlayer())
                {
                    collaiderPlayer.SetIsMonsterEarNearby(true);
                }
                else
                {
                    collaiderPlayer.SetIsMonsterEarNearby(false);
                }
            }

            switch (state)
            {
                case State.Idle:
                    transform.position = transform.position;
                    positionMovet = transform.position;
                    animator.SetFloat(SpeedAnim, 0f);
                    switch (nameMonster)
                    {
                        case NameMonster.Ear:

                            if (distanceToPlayer < _distanceToPlayerForChangeInAlert && speedPlayerReal > 0f)
                            {
                                if (RaycastToPlayer())
                                {
                                    state = State.Alert;

                                }
                            }
                            else if (Random.value < 0.05f)
                            {

                                animator.SetTrigger(RotationAnim);
                                state = State.Rotation;
                            }
                            else if (Random.value > 0.95f)
                            {
                                animator.SetTrigger(RoarAnim);
                                state = State.Roar;
                            }
                            else
                            {
                                state = State.Scaner;
                            }

                            break;

                        case NameMonster.Eye:
                            if (distanceToPlayer < _distanceToPlayerForChangeInAlert &&
                                AnglePlayerAndForwardMonster(positionPlayer, transform.position) && RaycastToPlayer())
                            {
                                state = State.Alert;

                            }
                            else if (Random.value < 0.05f)
                            {

                                animator.SetTrigger(RotationAnim);
                                state = State.Rotation;
                            }
                            else if (Random.value > 0.95f)
                            {
                                animator.SetTrigger(RoarAnim);
                                state = State.Roar;
                            }
                            else
                            {
                                state = State.Scaner;
                            }
                            break;

                        case NameMonster.Nose:
                            if (Random.value < 0.05f)
                            {

                                animator.SetTrigger(RotationAnim);
                                state = State.Rotation;
                            }
                            else if (Random.value > 0.95f)
                            {
                                animator.SetTrigger(RoarAnim);
                                state = State.Roar;
                            }
                            else
                            {
                                state = State.Scaner;
                            }
                            break;

                    }


                    break;

                case State.Scaner:

                    animator.SetFloat(SpeedAnim, 0f);

                    listPoint.Clear();
                    listScentPlayer.Clear();
                    ScanerForward();
                    Scaner(Vector3.left);
                    Scaner(Vector3.right);
                    Scaner(Vector3.back);

                    int lenghtListPoint = listPoint.Count;
                    float randomValue1 = Random.value;
                    float randomValue2 = Random.value;
                    switch (lenghtListPoint)
                    {
                        case (1):
                            positionMovet = listPoint[0];
                            break;
                        case (2):
                            positionMovet = randomValue1 < 0.99f ? listPoint[0] : listPoint[1];
                            break;
                        case (3):
                            positionMovet = randomValue1 < 0.99f ? listPoint[0] : randomValue2 > 0.5f ? listPoint[1] : listPoint[2];
                            break;
                        case (4):
                            positionMovet = randomValue1 < 0.99f ? listPoint[0] : randomValue2 > 0.95f ? listPoint[3] :
                                randomValue1 < 0.5f ? listPoint[1] : listPoint[2];
                            break;
                        default:
                            state = State.Idle;
                            break;
                    }

                    switch (nameMonster)
                    {
                        case NameMonster.Ear:
                        case NameMonster.Eye:
                            state = State.Movet;

                            break;
                        case NameMonster.Nose:
                            float summa = 0;
                            foreach (var scent in listScentPlayer) summa += scent;
                            if (summa > 0)
                            {
                                state = State.Alert;
                            }
                            else
                            {
                                state = State.Movet;
                            }
                            break;
                    }
                    break;

                case State.Movet:

                    switch (nameMonster)
                    {
                        case NameMonster.Ear:

                            if (distanceToPlayer < _distanceToPlayerForChangeInAlert && speedPlayerReal > 0f)
                            {
                                if (RaycastToPlayer())
                                {
                                    state = State.Alert;

                                }
                            }
                            break;

                        case NameMonster.Eye:
                            if (distanceToPlayer < _distanceToPlayerForChangeInAlert &&
                                AnglePlayerAndForwardMonster(positionPlayer, transform.position) && RaycastToPlayer())
                            {
                                state = State.Alert;

                            }
                            break;

                        case NameMonster.Nose:
                            break;

                    }

                    if (Movet(positionMovet, _speedNormal + nameMonster.GetHashCode(), _speedRotation))
                    {
                        state = State.Idle;

                        break;
                    }

                    break;

                case State.Alert:

                    if (RaycastToPlayer()) positionPlayerOutSight = player.GetComponentInChildren<TriggerCell>().positionCell;

                    switch (nameMonster)
                    {
                        case NameMonster.Ear:

                            if (RaycastToPlayer() && distanceToPlayer < _distanceToPlayerForChangeInRunTarget && speedPlayerReal > _speedPlayerForChangeInAlert)
                            {
                                state = State.RunTarget;

                            }
                            else
                            {
                                CorrectionMovet();
                            }

                            if (Movet(positionPlayerOutSight, _speedAttack, _speedRotation))
                            {
                                state = State.Scaner;

                            }

                            break;

                        case NameMonster.Eye:

                            if (RaycastToPlayer() && distanceToPlayer < _distanceToPlayerForChangeInRunTarget)
                            {
                                state = State.RunTarget;

                            }
                            else
                            {
                                CorrectionMovet();
                            }

                            if (Movet(positionPlayerOutSight, _speedAttack, _speedRotation))
                            {
                                state = State.Scaner;

                            }

                            break;

                        case NameMonster.Nose:

                            int index = 0;
                            float temp = 0;
                            for (int i = 0; i < listScentPlayer.Count; i++)
                            {
                                if (listScentPlayer[i] > temp)
                                {
                                    index = i;
                                    temp = listScentPlayer[i];
                                }
                            }

                            positionMovet = listPoint[index];
                            if (RaycastToPlayer() && distanceToPlayer < _distanceToPlayerForChangeInRunTarget)
                            {
                                state = State.RunTarget;

                            }

                            if (Movet(positionMovet, _speedAttack, _speedRotation))
                            {
                                state = State.Scaner;

                            }

                            break;
                    }

                    break;

                case State.RunTarget:

                    positionPlayerOutSight = player.GetComponentInChildren<TriggerCell>().positionCell;

                    if (RaycastToPlayer())
                    {
                        if (distanceToPlayer < _distanceInAttack)
                        {
                            rb.velocity = Vector3.zero;
                            state = State.Attack;
                        }

                        if (Movet(positionPlayer, _speedAttack + _speedNormal, _speedRotation))
                        {
                            rb.velocity = Vector3.zero;
                            state = State.Attack;
                        }
                    }
                    else
                    {
                        CorrectionMovet();
                        if (Movet(positionPlayerOutSight, _speedAttack + _speedNormal, _speedRotation))
                        {
                            state = State.Alert;
                        }
                    }

                    break;

                case State.Attack:
                    animator.SetTrigger(AttackAnim);
                    gameContr.PausedGame(true);
                    Invoke("MinusLife", 1f);

                    break;

                case State.Rotation:

                    if (nameMonster == NameMonster.Eye)
                    {
                        Vector3 ray = scanerForward.TransformDirection(Vector3.forward);
                        if (Physics.Raycast(scanerForward.position, ray, out RaycastHit hit, _distanceToPlayerForChangeInAlert))
                        {
                            if (hit.collider.CompareTag("Player"))
                            {
                                state = State.Alert;
                            }
                        }
                    }

                    break;

                case State.Roar:
                    animator.SetTrigger(RoarAnim);

                    break;
            }
        }
    }

    private void MinusLife()
    {
        gameContr.MinusLifes();
    }

    private void LateUpdate()
    {
        if (!gameContr.pausedGame)
        {
            positionPlayerOld = positionPlayer;
        }
    }

}
