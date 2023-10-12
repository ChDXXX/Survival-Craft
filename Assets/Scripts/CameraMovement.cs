using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private float upDownSpeed = 4f;

    [SerializeField]
    private float minRotation = -40f;

    [SerializeField]
    private float maxRotation = 40f;

    private float mouseY = 10f;

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        //마우스의 상하 움직임 감지
        mouseY += Input.GetAxis("Mouse Y") * upDownSpeed;

        //최소 최대값을 설정
        mouseY = Mathf.Clamp(mouseY, minRotation, maxRotation);

        //로컬 회전 값 변경
        transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
}
