using UnityEngine;

public class VisibleOnn : MonoBehaviour
{
    [SerializeField] private GameObject _vision;
    [SerializeField] private GameObject _spriteMap;
    [SerializeField] private GameObject _spriteTeleport;

    private GameObject _go;
    private bool _busy = false;
    private bool _isPassage = false;
    private const string Movet = "Movet";
    private const string NoMovet = "NoMovet";
    private const string Cell = "Cell";


    public void SetGo(GameObject go)
    {
        _go = go;
    }

    public GameObject GetGo()
    {
        return _go;
    }

    public void SetVisible(bool value)
    {
        _vision.SetActive(value);
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

    public void OnSpriteTeleport()
    {
        _spriteTeleport.SetActive(true);
    }

    public void OffSpriteTeleport()
    {
        _spriteTeleport.SetActive(false);
    }

    public void OnBlueColorTeleport()
    {
        _spriteTeleport.GetComponent<SpriteRenderer>().color = Color.blue;
    }

    public void OnGreenColorTeleport()
    {
        _spriteTeleport.GetComponent<SpriteRenderer>().color = Color.green;
    }

    public void SetPassage(bool value)
    {
        _isPassage = value;
    }

    public bool GetPassage()
    {
        return _isPassage;
    }
}
