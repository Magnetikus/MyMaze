using UnityEngine;

public class PlayParticleSystem : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;

    
    private void OnEnable()
    {
        _particleSystem.Play();
    }

    private void OnDisable()
    {
        _particleSystem.Stop();
    }
}
