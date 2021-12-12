using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] private GameObject _teleport;
    private Vector3 _positionTarget;

    public void StartTeleport(Vector3 positionTarget)
    {
        _positionTarget = positionTarget;
        _teleport.SetActive(true);
        _teleport.GetComponent<ParticleSystem>().Play();
        Invoke("Teleportation", 4f);
    }

    private void Teleportation()
    {
        transform.position = _positionTarget;
    }

}
