using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using TMPro;

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
    public int InventorySize = 3;
    public int CurrentItem = 0;
    public GameObject R_Hand;

    [Header("UI")]
    public TMP_Text ItemText;


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
        Movement();
        CameraMovement();
        InventoryMangement();
        //InfoRaycast();
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5))
        {
            if(hit.collider.tag == "PickupAble")
            {
                ItemText.text = "Press E to pickup " + hit.collider.name;
            }
            else
            {
                ItemText.text = "";
            }
        }
        else
        {
            ItemText.text = "";
        }
        if (Inventory[CurrentItem] != null){
            Inventory[CurrentItem].transform.position = R_Hand.transform.position;
            Inventory[CurrentItem].transform.rotation = R_Hand.transform.rotation;
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeItem(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeItem(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeItem(2);
        }
    }
    public void InventoryMangement()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            for (int i = 0; i < Inventory.Length; i++)
            {
                if (Inventory[i].activeSelf)
                {
                    DropItem(Inventory[i]);
                    break;
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 5))
            {
                if(hit.collider.tag == "PickupAble")
                {
                    PickupItem(hit.collider.gameObject);
                }
            }
        }
    }
    public void ChangeItem(int index)
    {
        if (Inventory[index] != null)
        {
            Inventory[CurrentItem].SetActive(false);
            Inventory[index].SetActive(true);
            CurrentItem = index;
            Inventory[CurrentItem].transform.gameObject.SendMessage("Equip");
        }
    }
    public void DropItem(GameObject item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == item)
            {
                Inventory[i] = null;
                item.transform.position = transform.position + transform.forward * 2;
                item.SetActive(true);
                item.GetComponent<Collider>().enabled = true;
                ChangeItem(i);
                break;
            }
        }
    }
    public void PickupItem(GameObject item)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null)
            {
                Debug.Log("Picked up " + item.name);
                Inventory[i] = item;
                item.transform.position = R_Hand.transform.position;
                item.transform.rotation = R_Hand.transform.rotation;
                item.GetComponent<Collider>().enabled = false;
                item.SetActive(false);
                ChangeItem(i);
                break;
            }
        }
    }
    public void Movement()
    {
        // Movement
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        GetComponent<Rigidbody>().MovePosition(transform.position + transform.TransformDirection(new Vector3(moveX, 0, moveZ) * moveSpeed * Time.deltaTime));
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
