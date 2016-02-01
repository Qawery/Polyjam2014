using UnityEngine;
using System.Collections;

public class CharacterMovement : MonoBehaviour
{

    //Variables
    public GameObject player;
    public float turnspeed = 180f;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    private Vector3 moveDirection = Vector3.zero;


    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        // is the controller on the ground?
        if (controller.isGrounded)
        {
            //Feed moveDirection with input.
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            //Multiply it by speed.
            moveDirection *= speed;
            //Jumping
            if (moveDirection != Vector3.zero)
            {
                if (Vector3.Angle(transform.forward, moveDirection) > 179)
                {
                    moveDirection = transform.TransformDirection(new Vector3(.01f, 0, -1));
                }
                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Quaternion.LookRotation(moveDirection), turnspeed * Time.deltaTime);
            }
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
        //Applying gravity to the controller
        moveDirection.y -= gravity * Time.deltaTime;
        //Making the character move
        controller.Move(moveDirection * speed * Time.deltaTime);
    }
}