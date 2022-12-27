using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{

    public Animator animator { get; private set; }

    private int horizontal;
    private int vertical;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        animator.SetFloat(horizontal, horizontalMovement,0.1f,Time.deltaTime);
        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime);

    }

    public void PlayTargetAnimation(string targetAnimation)
    {
        
        animator.CrossFade(targetAnimation, 0.01f);
        
    }
}
