using UnityEngine;

public class TriggerCell : MonoBehaviour
{
    [HideInInspector]
    public Vector3 positionCell;

    private const int _timeScent = 20;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            positionCell = other.transform.position;
            other.GetComponent<VisibleOnn>().CounterScentStart(_timeScent);
        }
    }
}
