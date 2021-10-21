using UnityEngine;

public class CellMovet : MonoBehaviour
{
    [SerializeField] private GameObject _cell;

    private GameObject _woll;

    public void SetWoll(GameObject go)
    {
        _woll = go;
    }

    public void ActivCell()
    {
        GetComponentInParent<Transform>().tag = "NoMovet";
        GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("Marker");
        if (arrayGO.Length > 0)
        {
            foreach (GameObject e in arrayGO)
            {
                e.tag = "Cell";
                e.GetComponentInChildren<CellMovet>()._cell.SetActive(false);
            }
        }
        _cell.SetActive(true);
        GetComponentInParent<Transform>().tag = "Marker";
    }

    public void DeactivCell()
    {
        _cell.SetActive(false);
        GetComponentInParent<Transform>().tag = "Cell";
    }

    public void ActivWoll()
    {
        _woll.SetActive(true);
    }

    
}
