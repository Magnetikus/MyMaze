using UnityEngine;
using UnityEngine.UI;

public class AmountHeartOut : MonoBehaviour
{
    private Transform[] _hearts;

    private void Start()
    {
        _hearts = gameObject.GetComponentsInChildren<Transform>();
    }

    public void SetColorHeart(int index, float alpha)
    {
        Color colorHeart = _hearts[index].GetComponent<Image>().color;
        colorHeart.a = alpha;
        _hearts[index].GetComponent<Image>().color = colorHeart;
    }
}
