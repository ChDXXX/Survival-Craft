using UnityEngine;

public class Scope : MonoBehaviour
{
    private Animator animator;
    private bool isScoped = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", isScoped);
        }
    }
}
