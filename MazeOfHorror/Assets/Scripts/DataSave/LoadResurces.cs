using UnityEngine;

public class LoadResurces : MonoBehaviour
{
    [SerializeField] private GameObject[] _resurcesCastle;
    [SerializeField] private GameObject[] _resurcesNature;
    [SerializeField] private GameObject[] _resurcesSpace;

    private int _location;

    public void SetLocation(int value)
    {
        _location = value;
    }

    public GameObject[] GetResurces()
    {
        switch (_location)
        {
            case (1):
                return _resurcesCastle;
            case (2):
                if (_resurcesNature.Length != 0)
                {
                    return _resurcesNature;
                }
                else
                {
                    return _resurcesCastle;
                }
            case (3):
                if (_resurcesSpace.Length != 0)
                {
                    return _resurcesSpace;
                }
                else
                {
                    return _resurcesCastle;
                }
            default:
                return _resurcesCastle;
        }
    }
}
