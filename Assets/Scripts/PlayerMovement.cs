using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 15f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;

    [Header("Inventory")]
    public GameObject[] Inventory;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 15;
        jumpForce = 5;
        groundCheckRadius = 0.1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.TransformDirection(new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime));
        //Camera
        CameraMovement();
        // Jump
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Jump");
        }
    }
    public void CameraMovement()
    {
        // Camera Movement
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        transform.Rotate(0, mouseX, 0);
        Camera.main.transform.Rotate(-mouseY, 0, 0);
    }
    public bool IsGrounded()
    {
        // Check if the player is grounded but dont check for the player itself
        Collider[] colliders = Physics.OverlapSphere(groundCheck.position, groundCheckRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag != "Player")
            {
                return true;
            }
        }
        return false; 
    }
}
