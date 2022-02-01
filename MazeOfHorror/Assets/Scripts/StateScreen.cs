using UnityEngine;

public class StateScreen : MonoBehaviour
{
    [SerializeField] private GameObject _menu;

    private void Awake()
    {
        float scale = Screen.width / 1900f;
        if (scale < 1)
        {
            _menu.transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
