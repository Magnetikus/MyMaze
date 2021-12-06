using UnityEngine;

public class TriggerBusyMonsters : MonoBehaviour
{
    private GameObject _cell;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            _cell = other.gameObject;
            _cell.GetComponent<VisibleOnn>().SetBusy(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cell"))
        {
            other.GetComponent<VisibleOnn>().SetBusy(false);
        }
    }

    public void NoBusy()
    {
        if (_cell != null)
        {
            _cell.GetComponent<VisibleOnn>().SetBusy(false);
        }
    }
}
