using UnityEngine;

public class Exit : MonoBehaviour
{
    private GameControler controller;

    void Start()
    {
        controller = GameObject.Find("Controller").GetComponent<GameControler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.PlayerEnterExit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            controller.PlayerExitExit();
        }
    }

}
