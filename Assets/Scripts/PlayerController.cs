using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector3 velocity;
    CharacterController characterController;

    [Header("Player Speed")]
    [SerializeField] float speed = 12f;
    [SerializeField] float lowSpeedMultiplier = 0.25f;
    [SerializeField] float highSpeedMultiplier = 1.65f;
    float baseSpeed;

    [Header("Ground Check")]
    public Transform groundCheck;
    public LayerMask groundMask;
    RaycastHit hit;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        baseSpeed = speed;
    }

  
    void Update()
    {
        PlayerMove();
    }

    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        if (Physics.Raycast(groundCheck.position, transform.TransformDirection(Vector3.down),
            out hit, 0.4f, groundMask))
        {
            string terrainType;
            terrainType = hit.collider.gameObject.tag;

            switch (terrainType)
            {
                case "Low":
                    speed = baseSpeed * lowSpeedMultiplier;
                    break;
                case "High":
                    speed = baseSpeed * highSpeedMultiplier;
                    break;
                default:
                    speed = baseSpeed;
                    break;
            }
        }


    }
}
