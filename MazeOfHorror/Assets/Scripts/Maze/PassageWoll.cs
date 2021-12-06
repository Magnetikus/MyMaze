using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageWoll : MonoBehaviour
{
    [SerializeField] private GameObject _system;
    [SerializeField] private ParticleSystem[] _arrayParticleSystem;
    [SerializeField] private GameObject _vision;
    private float _timePassage = 4f;

    private GameObject _player;

    public void StartPassage(int powerPlayer)
    {
        _timePassage -= powerPlayer / 3;
        _system.SetActive(true);
        _player = GameObject.FindGameObjectWithTag("Player");
        for (int i = 0; i < 4; i++)
        {
            _arrayParticleSystem[i].Play();
        }
        StartCoroutine(StartPassageCicle());
    }


    private IEnumerator StartPassageCicle()
    {
        var wait = new WaitForSeconds(0.1f);
        float radius = 0.5f;
        while (radius < 4)
        {
            for (int i = 0; i < 4; i++)
            {
                ParticleSystem.ShapeModule shape = _arrayParticleSystem[i].shape;
                shape.radius = radius;
            }
            radius += 3.5f / (_timePassage * 10f);
            yield return wait;
        }
        _vision.SetActive(false);
        GetComponent<BoxCollider>().isTrigger = true;
        _player.GetComponent<Passage>().PassageEnd();
    }

    public void PassageEnd()
    {
        for (int i = 0; i < _arrayParticleSystem.Length; i++)
        {
            ParticleSystem.ShapeModule shape = _arrayParticleSystem[i].shape;
            shape.radius = 0.5f;
            _arrayParticleSystem[i].Stop();
        }
        _system.SetActive(false);
        _vision.SetActive(true);
        
        GetComponent<BoxCollider>().isTrigger = false;
    }

}
