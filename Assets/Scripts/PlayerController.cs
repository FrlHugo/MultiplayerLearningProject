using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(BomberMan))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 inputMovement;
    [SerializeField] private Vector3 direction;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float gravity = -9.81f;


    public float playerSpeed = 3.5f;

    //Bomb Info

    [Header("Bomb PowerUp")]

    [SerializeField] private BomberMan bomberManInfo;

    private void Awake()
    {
        bomberManInfo = GetComponent<BomberMan>();
        characterController = GetComponent<CharacterController>();
        inputMovement = Vector3.zero;

    }

    // Update is called once per frame
    void Update()
    {

        HandleGravity();
        HandleRotation();
        Move();

    }

    private void HandleGravity()
    {
        direction.y = gravity * Time.deltaTime;
    }
    public void HandleMovingInput(InputAction.CallbackContext context)
    {
        Debug.Log("HandleMovingInput");
        inputMovement = context.ReadValue<Vector2>();
        inputMovement.Normalize();
        direction = new Vector3(inputMovement.x, 0.0f, inputMovement.y);
    }

    private void Move()
    {
        Debug.Log("Move");
        characterController.Move(direction * playerSpeed * bomberManInfo.speedBoost * Time.deltaTime);  
    }
    
    private void HandleRotation()
    {
      
    }



}
