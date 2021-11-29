using UnityEngine;

public class WollMovet : MonoBehaviour
{
    [SerializeField] private GameObject _cubeGreen;

    private GameObject _cell;

    private static string noMovet = "NoMovet";
    private static string movet = "Movet";
    private static string marker = "Marker";

    public void SetCell(GameObject go)
    {
        _cell = go;
    }

    public void ActivCube()
    {
        GetComponentInParent<Transform>().tag = noMovet;
        GameObject[] arrayGO = GameObject.FindGameObjectsWithTag(marker);
        if (arrayGO.Length > 0)
        {
            foreach (GameObject e in arrayGO)
            {
                e.tag = movet;
                e.GetComponentInChildren<WollMovet>()._cubeGreen.SetActive(false);
            }
        }
        _cubeGreen.SetActive(true);
        GetComponentInParent<Transform>().tag = marker;
    }

    public void DesactivCube()
    {
        _cubeGreen.SetActive(false);
        GetComponentInParent<Transform>().tag = movet;
    }

    public void ActivCell()
    {
        _cell.SetActive(true);
    }

}
