using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private Camera mapCamera;
    [SerializeField] private GameObject mapPoint;
    [SerializeField] private GameObject map;
    [SerializeField] private GameControler controller;

    private int[] sizeMaze;
    private int sizeCamera;
    private bool isActive = false;

    private void Start()
    {
        sizeMaze = controller.GetSizeMaze();
        if (sizeMaze[0] > sizeMaze[1])
        {
            sizeCamera = sizeMaze[0];
        }
        else sizeCamera = sizeMaze[1];
        mapPoint.transform.position = new Vector3(sizeMaze[0], 20, sizeMaze[1]);
        mapCamera.orthographicSize = sizeCamera + 2;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            isActive = !isActive;
            map.SetActive(isActive);
            mapPoint.SetActive(isActive);
        }

    }
}
