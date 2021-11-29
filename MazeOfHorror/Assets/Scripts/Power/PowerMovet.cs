using UnityEngine;

public class PowerMovet : MonoBehaviour
{
    private MovetCube _movetCube;

    private void Start()
    {
        _movetCube = GameObject.FindGameObjectWithTag("Player").GetComponent<MovetCube>();
    }

    public void CubeMovetFirstStep()
    {
        _movetCube.CubeMovetFirstStep();
    }

    public void CubeDesactive()
    {
        _movetCube.CubeDesactive();
    }

    public void CubeActive()
    {
        _movetCube.CubeActive();
    }

    public void EscapeMovet()
    {
        _movetCube.EscapeMovet();
    }
}
