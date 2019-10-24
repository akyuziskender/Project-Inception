using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingWoodController : MonoBehaviour
{
    private Animator animator;

    public void StartAnim()
    {
        animator = gameObject.GetComponent<Animator>();
        animator.SetBool("Broken", true);
    } 

    public void DestroyWood()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
