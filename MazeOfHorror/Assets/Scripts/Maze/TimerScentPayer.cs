using UnityEngine;

public class TimerScentPayer : MonoBehaviour
{
    private float _amountScent = 0;

    public void CounterScentStart(int value)
    {
        _amountScent = value;
    }

    public float GetAmountScent()
    {
        return _amountScent;
    }

    private void Update()
    {
        if (_amountScent > 0)
        {
            _amountScent -= 1;
        }
    }
}
