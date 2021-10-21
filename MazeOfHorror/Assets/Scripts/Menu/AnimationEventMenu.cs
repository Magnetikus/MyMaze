using UnityEngine;

public class AnimationEventMenu : MonoBehaviour
{
    private MosterMenu controller;

    private void Start()
    {
        controller = GetComponentInParent<MosterMenu>();
    }

    public void RotationEnd()
    {
        controller.SetState(MosterMenu.State.Idle);
    }

    public void RoarEnd()
    {
        controller.SetState(MosterMenu.State.Idle);
    }
}
