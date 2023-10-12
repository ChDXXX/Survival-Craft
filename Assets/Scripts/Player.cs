using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Gun gun;
    [SerializeField]
    private float moveSpeed = 4f;
    [SerializeField]
    private float rotationSpeed = 6f;
    private float mouseX = 0f;
    private CharacterController player;

    [SerializeField]
    private Transform groundPos;
    [SerializeField]
    private LayerMask groundMask;
    private float groundRadius = 0.4f;

    [SerializeField]
    private float jumpHeight = 2f;
    private float gravity = -9.81f;      //하강하기 위한 중력 값
    private Vector3 velocity;
    private bool isGrounded;

    private void Start()
    {
        player = GetComponent<CharacterController>();
    }

    private void Update()
    { 
        isGrounded = Physics.CheckSphere(groundPos.position, groundRadius, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveToX = 0f;
        float moveToZ = 0f;

        if (Input.GetKey(KeyCode.W))
        {
            moveToZ += 1f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            moveToX -= 1f;
        }

        if (Input.GetKey(KeyCode.S))
        {
            moveToZ -= 1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            moveToX += 1f;
        }

        Vector3 move = new Vector3(moveToX, 0, moveToZ);

        player.Move(transform.TransformDirection(move) * Time.deltaTime * moveSpeed);

        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;

        transform.eulerAngles = new Vector3(0, mouseX, 0);

        //속도 = 이전속도 + 시간 * 하강 값
        //위치 = 속도 * 시간 
        velocity.y += gravity * Time.deltaTime;
        player.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        { 
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        if (GameManager.Instance.canShooting && Input.GetMouseButtonDown(0))
        {
            gun.Shooting(); 
        }
    }
}
