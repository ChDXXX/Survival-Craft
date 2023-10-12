using System.Collections;
using UnityEngine;

public class Scoped : MonoBehaviour
{
    public GameObject scopeImage;
    public GameObject gunCamera;
    public Camera mainCamera;
    public float scopeFov = 15f;
    private float normalFov;

    private Animator animator;
    private bool isScoped = false;

    private void Start()
    {
        normalFov = mainCamera.fieldOfView;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //오른쪽 마우스 클릭시 
        if (Input.GetMouseButtonDown(1))
        {
            isScoped = !isScoped;
            animator.SetBool("isScoped", true);

            StartCoroutine(OnScoped());
        }
        else if (Input.GetMouseButtonUp(1))
        {
            UnScoped();
        }
    }

    private void UnScoped()
    {
        mainCamera.fieldOfView = normalFov;
        scopeImage.SetActive(false);
        gunCamera.SetActive(true);
        animator.SetBool("isScoped", false);
    }

    private IEnumerator OnScoped()
    {
        yield return new WaitForSeconds(0.15f);

        scopeImage.SetActive(true);
        gunCamera.SetActive(false);
        mainCamera.fieldOfView = scopeFov;
    }
}
