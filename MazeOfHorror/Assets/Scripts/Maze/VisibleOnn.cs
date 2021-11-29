using UnityEngine;
using System.Collections;

public class VisibleOnn : MonoBehaviour
{
    [SerializeField] private GameObject _vision;
    [SerializeField] private GameObject _spriteMap;

    private GameObject _go;
    private int _amountScent = 0;
    private bool _busy = false;
    private const string Movet = "Movet";
    private const string NoMovet = "NoMovet";
    private const string Cell = "Cell";
    
   
    public void SetGo (GameObject go)
    {
        _go = go;
    }

    public GameObject GetGo()
    {
        return _go;
    }

    public void CounterScentStart(int value)
    {
        _amountScent = value;
        StartCoroutine(CounterScent());
    }

    private void CounterScentStop()
    {
        _amountScent = 0;
        StopCoroutine(CounterScent());
    }

    private IEnumerator CounterScent()
    {
        var wait = new WaitForSeconds(1f);
        while (_amountScent > 0)
        {
            _amountScent -= 1;
            if (_amountScent < 0)
            {
                CounterScentStop();
            }
            yield return wait;
        }
    }

    public float GetAmountScent()
    {
        return _amountScent;
    }

    public void OnVisible()
    {
        _vision.SetActive(true);
        if (tag == Movet || tag == NoMovet)
        {
            _vision.GetComponent<WollMovet>().SetCell(_go);
        }
        else if (tag == Cell)
        {
            _vision.GetComponent<CellMovet>().SetWoll(_go);
        }
    }

    public void OffVisible()
    {
        _vision.SetActive(false);
    }

    public void OnSpriteMap()
    {
        _spriteMap.SetActive(true);
    }

    public void OffSpriteMap()
    {
        _spriteMap.SetActive(false);
    }

    public bool GetBusy()
    {
        return _busy;
    }

    public void SetBusy(bool value)
    {
        _busy = value;
    }



    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player") || other.CompareTag("Monster"))
    //    {
    //        _busy = true;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player") || other.CompareTag("Monster"))
    //    {
    //        _busy = false;
    //    }
    //}
}
