using UnityEngine;

public class ChangeIsLook : MonoBehaviour
{
    private static string _movet = "Movet";
    private static string _noMovet = "NoMovet";
    private static string _cell = "Cell";
    private static string _monster = "Monster";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_movet) || other.CompareTag(_noMovet) || other.CompareTag(_cell) || other.CompareTag(_monster))
        {
            other.GetComponent<VisibleOnn>().OnVisible();

            if (other.CompareTag(_movet) || other.CompareTag(_noMovet))
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(_movet) || other.CompareTag(_noMovet) || other.CompareTag(_cell))
        {
            if (other.GetComponent<VisibleOnn>().GetPassage() == false)
            {
                other.GetComponent<VisibleOnn>().OffVisible();
            }
            if (other.CompareTag(_movet) || other.CompareTag(_noMovet))
            {
                other.gameObject.GetComponent<BoxCollider>().isTrigger = true;
            }
        }
        if (other.CompareTag(_monster))
        {
            other.GetComponent<VisibleOnn>().OffVisible();
        }
    }
}
