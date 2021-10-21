using UnityEngine;

public class WollMovet : MonoBehaviour
{
    [SerializeField] private GameObject _cubeGreen;

    private GameObject _cell;

    public void SetCell(GameObject go)
    {
        _cell = go;
    }

    public void ActivCube()
    {
        GetComponentInParent<Transform>().tag = "NoMovet";
        GameObject[] arrayGO = GameObject.FindGameObjectsWithTag("Marker");
        if (arrayGO.Length > 0)
        {
            foreach (GameObject e in arrayGO)
            {
                e.tag = "Movet";
                e.GetComponentInChildren<WollMovet>()._cubeGreen.SetActive(false);
            }
        }
        _cubeGreen.SetActive(true);
        GetComponentInParent<Transform>().tag = "Marker";
    }

    public void DesactivCube()
    {
        _cubeGreen.SetActive(false);
        GetComponentInParent<Transform>().tag = "Movet";
    }

    public void ActivCell()
    {
        _cell.SetActive(true);
    }

}
