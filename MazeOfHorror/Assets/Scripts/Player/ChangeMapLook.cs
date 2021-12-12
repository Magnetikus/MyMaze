using UnityEngine;

public class ChangeMapLook : MonoBehaviour
{
    private static string _movet = "Movet";
    private static string _noMovet = "NoMovet";
    private static string _cell = "Cell";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_movet) || other.CompareTag(_noMovet) || other.CompareTag(_cell))
        {
            other.GetComponent<VisibleOnn>().OnSpriteMap();
        }
    }

}
