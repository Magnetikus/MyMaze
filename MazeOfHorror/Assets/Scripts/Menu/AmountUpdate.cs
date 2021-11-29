using UnityEngine;
using UnityEngine.UI;

public class AmountUpdate : MonoBehaviour
{
    private Transform[] _arrayChildren;

    private void Start()
    {
        _arrayChildren = gameObject.GetComponentsInChildren<Transform>();
    }

    public void SetColorRect(int index)
    {
        if (index != 0)
        {
            _arrayChildren[index].GetComponent<Image>().color = Color.black;
        }
        
    }

    public void ResetColor()
    {
        for (int i = 1; i < 11; i++)
        {
            _arrayChildren[i].GetComponent<Image>().color = Color.white;
        }
    }
}
