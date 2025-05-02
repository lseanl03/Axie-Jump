using UnityEngine;

public class LevelTransiton : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        if(!animator) animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    public void TransitionState(bool isOpen)
    {
        animator.SetBool("isOpen", isOpen);
    }
}
