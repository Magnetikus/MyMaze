using UnityEngine;

public class Immuny : MonoBehaviour
{
    [SerializeField] private GameObject _effect;
    [SerializeField] private PlaySound _playSound;

    public void StartEffect()
    {
        _effect.SetActive(true);
        _effect.GetComponent<ParticleSystem>().Play();
        Invoke("EndEffect", 2f);
        _playSound.Play("Wave");
    }

    private void EndEffect()
    {
        _effect.SetActive(false);
    }
}
