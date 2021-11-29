using UnityEngine;

public class EventMinusLife : MonoBehaviour
{
    private GameControler gameContr;

    private void Start()
    {
        gameContr = GameObject.Find("Controller").GetComponent<GameControler>();
    }

    public void EndAnimation()
    {
        gameContr.EndMinusLife();
    }
}
