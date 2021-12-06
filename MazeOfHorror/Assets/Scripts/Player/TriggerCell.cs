using UnityEngine;

public class TriggerCell : MonoBehaviour
{
    [HideInInspector]
    public Vector3 positionCell;

    private int _levelPlayer;
    private int _timeScent = 60;

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
            VisibleOnn visibleOnn = other.GetComponent<VisibleOnn>();
            visibleOnn.CounterScentStart(_timeScent);
            visibleOnn.SetBusy(false);
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
