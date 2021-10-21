using UnityEngine;

public class ChangeColliderPlayer : MonoBehaviour
{
    private CharacterController _controller;
    private bool _isMonsterEarNearby = false;

    public void SetIsMonsterEarNearby(bool value)
    {
        _isMonsterEarNearby = value;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isMonsterEarNearby == true)
        {
            if (other.CompareTag("Movet") || other.CompareTag("NoMovet"))
            {
                _controller.radius = 0.2f;
            }
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Movet") || other.CompareTag("NoMovet"))
        {
            _controller.radius = 0.5f;
        }
    }

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

}
