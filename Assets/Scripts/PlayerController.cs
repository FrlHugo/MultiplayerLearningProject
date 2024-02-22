using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


using Unity.Netcode;
using Unity.Netcode.Components;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(BomberMan))]
[RequireComponent(typeof(NetworkObject))]
public class PlayerController : NetworkBehaviour
{
    [SerializeField] private Vector2 inputMovement;
    [SerializeField] private Vector3 direction;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float gravity = -9.81f;

    private float currentVelocity;
    //[SerializeField] private float smoothtime = 0.05f;

    public float playerSpeed = 3.5f;

    [SerializeField] private Vector3 spawnPosition;

    //Bomb Info

    [Header("Bomb PowerUp")]

    [SerializeField] private BomberMan bomberManInfo;

    public static PlayerController instance { get; private set; }

    private void Awake()
    {
        instance = this;
        bomberManInfo = GetComponent<BomberMan>();
        characterController = GetComponent<CharacterController>();
        inputMovement = Vector3.zero;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(IsOwner)
        {
            HandleGravity();
            HandleRotation();
            Move();
        }
  
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
        characterController.Move(direction * playerSpeed * bomberManInfo.speedBoost * Time.deltaTime);  
    }
    
    private void HandleRotation()
    {
        if(inputMovement.sqrMagnitude > 0.0f)
        {
            var targetAngle = MathF.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            //var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocity, smoothtime); // smooth the rotation
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
        }

    }

    /*
    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        GameManager.instance.PlayerDictionnary.Add(OwnerClientId, gameObject);

    }
    */
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        GameManager.instance.PlayerDictionnary.Remove(OwnerClientId);
    }

    [ClientRpc]
    public void TeleportPlayerToSpawnPointClientRpc()
    {
        if (!IsOwner) { return; }
        var spawnPoint = PlayerSpawnManager.instance.listPlayerSpawn[0];
        PlayerSpawnManager.instance.listPlayerSpawn.Remove(spawnPoint);

        Debug.Log("Teleport player  : " + OwnerClientId + " at : " + spawnPoint + " | position : " + spawnPoint.transform.position);

        ClientNetworkTransform cnt = GetComponent<ClientNetworkTransform>();

        characterController.enabled = false;

        cnt.Teleport(spawnPoint.transform.position,new Quaternion(gameObject.transform.rotation.x,180f, gameObject.transform.rotation.z,0), gameObject.transform.localScale);

        characterController.enabled = true;
    }

}
