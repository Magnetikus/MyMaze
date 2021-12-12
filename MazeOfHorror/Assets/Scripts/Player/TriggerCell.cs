using UnityEngine;

public class TriggerCell : MonoBehaviour
{
    [HideInInspector]
    public Vector3 positionCell;

    private int _levelPlayer;
    private int _timeScent = 500;

    private void Start()
    {
        _levelPlayer = GameObject.FindGameObjectWithTag("GameController").GetComponent<SaveProgress>().LevelPlayer;
        _timeScent += _levelPlayer * 10;
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            positionCell = other.transform.position;
            other.GetComponent<VisibleOnn>().SetBusy(false);
            other.GetComponent<TimerScentPayer>().CounterScentStart(_timeScent);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            other.GetComponent<VisibleOnn>().SetBusy(true);
        }
    }
}
