using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//1
public class Movement : MonoBehaviour
{
    //Essentials
    public Transform mainCam;
    CharacterController controller;
    float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    //Animation
    public Animator PlayerAnimator;

    //Movement
    Vector2 movement;
    public float walkSpeed = 4;
    public float sprintSpeed = 6;
    float trueSpeed;

    //Jumping
    public float jumpHeight = 1;
    public float gravity = (int)9.82;
    bool isGrounded;
    Vector3 velocity;
    private int counter = 2;




    void Start()
    {
        //Animator
        //animator = GetComponent<Animator>();

        //No cursor & Move
        trueSpeed = walkSpeed;
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Grounding
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1;
        }

        //Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = sprintSpeed;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = walkSpeed;
        }


        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector3 direction = new Vector3(movement.x, 0, movement.y).normalized;

        //Set rotation equal to the look direction
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (direction.magnitude >= 0.1f)
        {
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDirection.normalized * trueSpeed * Time.deltaTime);
        }

        //Jumping (max doublejump)
        if (isGrounded)
        {
            counter = 2;
        }

        if (Input.GetKeyDown(KeyCode.Space) && counter > 0)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
            counter = counter - 1;
        }


        if (velocity.y > -20)
        {
            velocity.y += (gravity * 30) * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);

        //Animation
        if (direction.magnitude <= 0)
        {
            PlayerAnimator.SetBool("Moving", false);
        }
        else
        {
            PlayerAnimator.SetBool("Moving", true);
        }

    }
}

