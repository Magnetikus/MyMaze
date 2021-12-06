using UnityEngine;

public class TriggerPassegeExit : MonoBehaviour
{
    [SerializeField] private PassageWoll _passageWoll;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _passageWoll.PassageEnd();
            if (other != null)
            {
                other.GetComponent<Passage>().PassageEnd();
            }
        }
    }
}
