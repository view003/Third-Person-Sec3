using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCTR : MonoBehaviour
{
    public static PlayerCTR instance;

    public CharacterController characterController;
    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private Camera followCamera;

    [SerializeField] private float rotationSpeed = 10f;

    public Vector3 playerVelocity;
    [SerializeField] private float gravityValue = -13f;

    public bool groundedPlayer;
    [SerializeField] private float jump = 2.5f;

    public Animator animator;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (checkWN.instance.isWinner)
        {
            case true:
                animator.SetBool("Victory", checkWN.instance.isWinner);
                break;
            case false:
                Movement();
                break;
        }
    }

    void Movement()
    {
        groundedPlayer = characterController.isGrounded;

        if(characterController.isGrounded && playerVelocity.y<-2)
        {
            playerVelocity.y = -1f;
        }
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movementInput = Quaternion.Euler(0, followCamera.transform.eulerAngles.y, 0) * new Vector3(inputX, 0, inputY);

        Vector3 movementDirection = movementInput.normalized;

        characterController.Move(movementDirection * playerSpeed * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && groundedPlayer) 
        {
            playerVelocity.y += Mathf.Sqrt(jump * -3.0f * gravityValue);
            animator.SetTrigger("Jumping");
        }

        if (movementDirection != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(movementDirection, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);

        animator.SetFloat("Speed", Mathf.Abs(movementDirection.x) + Mathf.Abs(movementDirection.z));
        animator.SetBool("Ground", characterController.isGrounded);
    }
}
