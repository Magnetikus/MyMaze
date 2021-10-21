using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private MonsterController controller;

    private void Start()
    {
        controller = GetComponentInParent<MonsterController>();
    }

    public void RotationEnd()
    {
        controller.SetState(MonsterController.State.Idle);
    }

    public void RoarEnd()
    {
        controller.SetState(MonsterController.State.Idle);
    }

}
