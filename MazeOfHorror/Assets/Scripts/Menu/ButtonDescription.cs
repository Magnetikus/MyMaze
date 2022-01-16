using UnityEngine;

public class ButtonDescription : MonoBehaviour
{
    [SerializeField] private GameObject _activDescription;
    [SerializeField] private GameObject[] _deactivDescriptions;
    [SerializeField] private GameObject _noVisibleWindow;

    public void Click()
    {
        _activDescription.SetActive(true);
        for (int i = 0; i < _deactivDescriptions.Length; i++)
        {
            _deactivDescriptions[i].SetActive(false);
        }
        _noVisibleWindow.SetActive(false);
    }
}
