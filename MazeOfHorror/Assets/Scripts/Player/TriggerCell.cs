using UnityEngine;

public class TriggerCell : MonoBehaviour
{
    [HideInInspector]
    public Vector3 positionCell;

    private const int _timeScent = 60;

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
