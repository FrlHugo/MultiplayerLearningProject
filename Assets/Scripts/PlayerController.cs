using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 inputMovement;
    [SerializeField] private Vector3 direction;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float gravity = -9.81f;

    [SerializeField] private float cooldownPlantBomb = 4f;

    [SerializeField] private bool canPlant;

    public GameObject _prefabBomb;

    public float playerSpeed = 3.5f;

    //Bomb Info

    [Header("Bomb PowerUp")]

    [SerializeField] private int bombRange = 1;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        inputMovement = Vector3.zero;
        canPlant = true;
    }

    // Update is called once per frame
    void Update()
    {
        //HandleMovingInput();
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
        inputMovement = context.ReadValue<Vector2>();
        inputMovement.Normalize();
        direction = new Vector3(inputMovement.x, 0.0f, inputMovement.y);
    }

    private void Move()
    {
        characterController.Move(direction * playerSpeed * Time.deltaTime);  
    }
    
    private void HandleRotation()
    {
      
    }


    public void PlantBomb(InputAction.CallbackContext context)
    {
        Debug.Log("Plant the bomb ! :" + canPlant);

        if(canPlant)
        {
            canPlant = false;

            Vector3 pos = new Vector3(Mathf.Round(gameObject.transform.position.x),0, Mathf.Round(gameObject.transform.position.z));

            var Bomb = Instantiate(_prefabBomb,pos, Quaternion.identity);
            Bomb.GetComponent<BombManager>().explodeRange = bombRange;
            StartCoroutine(StartCooldownBomb(cooldownPlantBomb));
        }
       
    }


    IEnumerator StartCooldownBomb(float duration)
    {
        yield return new WaitForSeconds(duration);
        canPlant = true;

    }

}
