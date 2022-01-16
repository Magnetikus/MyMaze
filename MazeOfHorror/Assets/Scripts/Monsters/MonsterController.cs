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

    private const float _speedMonster = 10f;
    private float _speedNormal;
    private float _speedAttack;
    private float _speedMaximum;
    private const float _speedPlayerForChangeInAlert = 0.05f;
    private const float _distanceToPlayer = 8f;
    private float _distanceToPlayerForChangeInAlert;
    private float _distanceToPlayerForChangeInRunTarget;
    private float _distanceInAttack;
    private float _realDistanceToPlayer;


    private List<Vector3> _listPoint;
    private List<float> _listScentPlayer;
    private GameObject _player;
    private Vector3 _positionPlayer;
    private Vector3 _positionPlayerOld;
    private Vector3 _positionMovet;
    private Vector3 _positionPlayerOutSight;
    private GameControler _gameContr;
    private Animator _animator;
    private Rigidbody _rb;
    private PlaySound _playSound;
    private bool _isAllert;
    private static readonly int RotationAnim = Animator.StringToHash("Rotation");
    private static readonly int SpeedAnim = Animator.StringToHash("Speed");
    private static readonly int AttackAnim = Animator.StringToHash("Attack");
    private static readonly int RoarAnim = Animator.StringToHash("Roar");
    private static readonly int AllertAnim = Animator.StringToHash("Allert");

    public void SetSpeedNormal(float value)
    {
        _speedNormal = _speedMonster + value * 0.1f;
        _speedAttack = 2 * _speedNormal;
        _speedMaximum = 4 * _speedNormal;
        _distanceToPlayerForChangeInAlert = _distanceToPlayer + value * 0.1f;
        _distanceToPlayerForChangeInRunTarget = 0.5f * _distanceToPlayerForChangeInAlert;
        _distanceInAttack = 0.25f * _distanceToPlayerForChangeInAlert;
    }

    public void Restart()
    {
        Start();
    }

    public void SetState(State newState)
    {
        state = newState;
    }

    public void StepSound()
    {
        if (_realDistanceToPlayer < 15f)
        {
            _playSound.Play("Steps");
        }
    }

    private void Start()
    {
        state = State.Idle;
        _listPoint = new List<Vector3>();
        _listScentPlayer = new List<float>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _gameContr = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControler>();
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        GetComponent<VisibleOnn>().OffVisible();
        _playSound = GetComponent<PlaySound>();
    }

    private bool Movet(Vector3 targetPosition, float speedMovet, bool isAllert)
    {
        float speedDel = _speedNormal;
        if (isAllert) speedDel = _speedAttack;
        _animator.SetBool(AllertAnim, isAllert);
        _animator.SetFloat(SpeedAnim, speedMovet / speedDel * 1.3f);

        Vector3 distance = targetPosition - transform.position;
        if (distance.magnitude < 0.01f)
        {
            _rb.isKinematic = true;
            transform.position = targetPosition;
            _rb.velocity = Vector3.zero;
            _rb.isKinematic = false;
            return true;
        }
        Vector3 direction = distance.normalized;

        // Rotation

        _rb.rotation = Quaternion.LookRotation(direction);

        if (RaycastForwardToMonsters())
        {
            _rb.velocity = Vector3.zero;
            return true;
        }

        // Movet

        float velocity = _rb.velocity.sqrMagnitude;

        if (distance.magnitude > 0.1f)
        {
            if (velocity < _speedMaximum)
            {
                _rb.AddRelativeForce(new Vector3(0, 0, speedMovet));
            }
            else _rb.AddRelativeForce(Vector3.zero);
            return false;
        }
        else _rb.AddRelativeForce(Vector3.zero);

        transform.position = targetPosition;
        return true;
    }

    private bool AnglePlayerAndForwardMonster(Vector3 playerPosition, Vector3 myPosition)
    {
        Vector3 distance = playerPosition - myPosition;

        float angle = Mathf.Abs(Vector3.Angle(transform.forward, distance));
        if (angle < 100f) return true;
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
                    _listPoint.Add(go.GetComponent<Transform>().position);
                    _listScentPlayer.Add(go.GetComponent<TimerScentPayer>().GetAmountScent());
                }
                
            }
        }
        return _listPoint;
    }

    private List<Vector3> ScanerForward()
    {
        bool iMonster = false;
        iMonster = RaycastForwardToMonsters();
        if (!iMonster) Scaner(Vector3.forward);
        else Scaner(Vector3.back);
        return _listPoint;
    }

    private bool RaycastToPlayer()
    {
        if (Physics.Raycast(transform.position, (_positionPlayer - transform.position).normalized, out RaycastHit hitRay))
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
            Vector3 direction = (_positionPlayer - transform.position).normalized;
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
                _rb.AddRelativeForce(new Vector3(scanerLeftAndRight * _speedAttack, 0, 0));
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
        hits = Physics.RaycastAll(scanerTransform.position, scanerTransform.TransformDirection(Vector3.forward), 3f);
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
        if (_player == null) _player = GameObject.FindGameObjectWithTag("Player");
        if (!_gameContr.pausedGame)
        {
            _positionPlayer = _player.transform.position;
            float speedPlayerReal = (_positionPlayer - _positionPlayerOld).magnitude;
            _realDistanceToPlayer = (_positionPlayer - transform.position).magnitude;

            switch (state)
            {
                case State.Idle:
                    _isAllert = false;
                    transform.position = transform.position;
                    _positionMovet = transform.position;
                    _animator.SetBool(AllertAnim, _isAllert);
                    _animator.SetFloat(SpeedAnim, 0f);
                    switch (nameMonster)
                    {
                        case NameMonster.Ear:

                            if (_realDistanceToPlayer < _distanceToPlayerForChangeInAlert && speedPlayerReal > 0f)
                            {
                                if (RaycastToPlayer())
                                {
                                    state = State.Alert;

                                }
                            }
                            else if (Random.value < 0.05f)
                            {

                                _animator.SetTrigger(RotationAnim);
                                state = State.Rotation;
                            }
                            else if (Random.value > 0.95f)
                            {
                                _animator.SetTrigger(RoarAnim);
                                state = State.Roar;
                            }
                            else
                            {
                                state = State.Scaner;
                            }

                            break;

                        case NameMonster.Eye:
                            if (_realDistanceToPlayer < _distanceToPlayerForChangeInAlert &&
                                AnglePlayerAndForwardMonster(_positionPlayer, transform.position) && RaycastToPlayer())
                            {
                                state = State.Alert;

                            }
                            else if (Random.value < 0.05f)
                            {

                                _animator.SetTrigger(RotationAnim);
                                state = State.Rotation;
                            }
                            else if (Random.value > 0.95f)
                            {
                                _animator.SetTrigger(RoarAnim);
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

                                _animator.SetTrigger(RotationAnim);
                                state = State.Rotation;
                            }
                            else if (Random.value > 0.95f)
                            {
                                _animator.SetTrigger(RoarAnim);
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

                    _isAllert = false;
                    _animator.SetFloat(SpeedAnim, 0f);
                    _animator.SetBool(AllertAnim, _isAllert);
                    _listPoint.Clear();
                    _listScentPlayer.Clear();
                    ScanerForward();
                    Scaner(Vector3.left);
                    Scaner(Vector3.right);
                    Scaner(Vector3.back);

                    int lenghtListPoint = _listPoint.Count;
                    float randomValue1 = Random.value;
                    float randomValue2 = Random.value;
                    switch (lenghtListPoint)
                    {
                        case (1):
                            _positionMovet = _listPoint[0];
                            break;
                        case (2):
                            _positionMovet = randomValue1 < 0.99f ? _listPoint[0] : _listPoint[1];
                            break;
                        case (3):
                            _positionMovet = randomValue1 < 0.99f ? _listPoint[0] : randomValue2 > 0.5f ? _listPoint[1] : _listPoint[2];
                            break;
                        case (4):
                            _positionMovet = randomValue1 < 0.99f ? _listPoint[0] : randomValue2 > 0.95f ? _listPoint[3] :
                                randomValue1 < 0.5f ? _listPoint[1] : _listPoint[2];
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
                            foreach (var scent in _listScentPlayer) summa += scent;
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

                    _isAllert = false;
                    

                    switch (nameMonster)
                    {
                        case NameMonster.Ear:

                            if (_realDistanceToPlayer < _distanceToPlayerForChangeInAlert && speedPlayerReal > 0f)
                            {
                                if (RaycastToPlayer())
                                {
                                    state = State.Alert;

                                }
                            }
                            break;

                        case NameMonster.Eye:
                            if (_realDistanceToPlayer < _distanceToPlayerForChangeInAlert &&
                                AnglePlayerAndForwardMonster(_positionPlayer, transform.position) && RaycastToPlayer())
                            {
                                state = State.Alert;

                            }
                            break;

                        case NameMonster.Nose:
                            break;

                    }

                    if (Movet(_positionMovet, _speedNormal + nameMonster.GetHashCode(), _isAllert))
                    {
                        state = State.Idle;

                        break;
                    }

                    break;

                case State.Alert:

                    _isAllert = true;

                    if (RaycastToPlayer()) _positionPlayerOutSight = _player.GetComponentInChildren<TriggerCell>().positionCell;

                    switch (nameMonster)
                    {
                        case NameMonster.Ear:

                            if (RaycastToPlayer() && _realDistanceToPlayer < _distanceToPlayerForChangeInRunTarget && speedPlayerReal > _speedPlayerForChangeInAlert)
                            {
                                state = State.RunTarget;

                            }
                            else
                            {
                                CorrectionMovet();
                            }

                            if (Movet(_positionPlayerOutSight, _speedAttack, _isAllert))
                            {
                                state = State.Scaner;

                            }

                            break;

                        case NameMonster.Eye:

                            if (RaycastToPlayer() && _realDistanceToPlayer < _distanceToPlayerForChangeInRunTarget)
                            {
                                state = State.RunTarget;

                            }
                            else
                            {
                                CorrectionMovet();
                            }

                            if (Movet(_positionPlayerOutSight, _speedAttack, _isAllert))
                            {
                                state = State.Scaner;

                            }

                            break;

                        case NameMonster.Nose:

                            int index = 0;
                            float temp = 0;
                            for (int i = 0; i < _listScentPlayer.Count; i++)
                            {
                                if (_listScentPlayer[i] > temp)
                                {
                                    index = i;
                                    temp = _listScentPlayer[i];
                                }
                            }

                            _positionMovet = _listPoint[index];
                            if (RaycastToPlayer() && _realDistanceToPlayer < _distanceToPlayerForChangeInRunTarget)
                            {
                                state = State.RunTarget;

                            }

                            if (Movet(_positionMovet, _speedAttack, _isAllert))
                            {
                                state = State.Scaner;

                            }

                            break;
                    }

                    break;

                case State.RunTarget:

                    _isAllert = true;

                    _positionPlayerOutSight = _player.GetComponentInChildren<TriggerCell>().positionCell;

                    if (RaycastToPlayer())
                    {
                        if (_realDistanceToPlayer < _distanceInAttack)
                        {
                            _rb.velocity = Vector3.zero;
                            state = State.Attack;
                        }

                        if (Movet(_positionPlayer, _speedAttack + _speedNormal, _isAllert))
                        {
                            _rb.velocity = Vector3.zero;
                            state = State.Attack;
                        }
                    }
                    else
                    {
                        CorrectionMovet();
                        if (Movet(_positionPlayerOutSight, _speedAttack + _speedNormal, _isAllert))
                        {
                            state = State.Alert;
                        }
                    }

                    break;

                case State.Attack:

                    _gameContr.PausedGame(true);
                    _playSound.Play("Attack");
                    //_rb.rotation = Quaternion.LookRotation(_positionPlayer);
                    _animator.SetTrigger(AttackAnim);
                    
                    Invoke("MinusLife", 1f);

                    break;

                case State.Rotation:

                    _isAllert = false;

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
                    Invoke("StateIdle", 3f);
                    break;

                case State.Roar:

                    _isAllert = false;
                    if (_realDistanceToPlayer < 10f)
                    {
                        _playSound.Play("Roar");
                    }
                    _animator.SetTrigger(RoarAnim);
                    Invoke("StateIdle", 2f);
                    break;
            }
        }
    }

    private void StateIdle()
    {
        if (state == State.Rotation || state == State.Roar)
        {
            state = State.Idle;
        }
    }

    private void MinusLife()
    {
        _gameContr.MinusLifes();
    }

    private void LateUpdate()
    {
        if (!_gameContr.pausedGame)
        {
            _positionPlayerOld = _positionPlayer;
        }
    }

}
