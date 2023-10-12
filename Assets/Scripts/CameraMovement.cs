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
        //���콺�� ���� ������ ����
        mouseY += Input.GetAxis("Mouse Y") * upDownSpeed;

        //�ּ� �ִ밪�� ����
        mouseY = Mathf.Clamp(mouseY, minRotation, maxRotation);

        //���� ȸ�� �� ����
        transform.localEulerAngles = new Vector3(-mouseY, 0, 0);
    }
}
