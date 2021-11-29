using UnityEngine;

public class ChangeIsLook : MonoBehaviour
{
    private static string _movet = "Movet";
    private static string _noMovet = "NoMovet";
    private static string _cell = "Cell";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_movet) || other.CompareTag(_noMovet) || other.CompareTag(_cell))
        {
            other.GetComponent<VisibleOnn>().OnVisible();
            other.GetComponent<VisibleOnn>().OnSpriteMap();
        }

        if (other.CompareTag(_movet) || other.CompareTag(_noMovet))
        {
            other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_movet) || other.CompareTag(_noMovet) || other.CompareTag(_cell))
        {
            other.GetComponent<VisibleOnn>().OffVisible();
        }

        if (other.CompareTag(_movet) || other.CompareTag(_noMovet))
        {
            other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
    }
}
