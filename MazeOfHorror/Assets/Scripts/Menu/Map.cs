using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Camera _mapCamera;
    [SerializeField] private GameObject _mapPoint;
    [SerializeField] private GameControler _controller;

    private int[] _sizeMaze;
    private int _sizeCamera;

    private void Start()
    {
        _sizeMaze = _controller.GetSizeMaze();
        if (_sizeMaze[0] > _sizeMaze[1])
        {
            _sizeCamera = _sizeMaze[0];
        }
        else _sizeCamera = _sizeMaze[1];
        _mapPoint.transform.position = new Vector3(_sizeMaze[0], 20, _sizeMaze[1]);
        _mapCamera.orthographicSize = _sizeCamera;
    }

    public int[] GetSizeMazeAndMaximum()
    {
        int[] array = { _sizeMaze[0], _sizeMaze[1], _sizeCamera };
        return array;
    }

}
