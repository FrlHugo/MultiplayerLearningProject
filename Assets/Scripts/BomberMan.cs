using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BomberMan : NetworkBehaviour
{
    public GameObject _prefabBomb;
    [SerializeField] private float cooldownPlantBomb = 3f;

    public int bombPower = 1;
    public int numberOfBombMax = 1;
    public int numberBombLeft = 1;
    public float speedBoost = 1;
    public int rangeThrow = 1;

    public bool canThrow = false;
    public bool canPush = false;
    public bool canKick = false;

    public bool hasShield;

    public GameObject bombInHand;

    public List<GameObject> bombList = new List<GameObject>();

    [SerializeField] private BombListSO bombListSo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void PlantBomb(InputAction.CallbackContext context)
    {
        if(!IsOwner)
        { return; }
        SpawnBombObjectServerRPC() ;
    }



    [ServerRpc(RequireOwnership = false)]
    private void SpawnBombObjectServerRPC()
    { 
        if (numberBombLeft > 0)
        {
            Vector3 pos = new Vector3(Mathf.Round(gameObject.transform.position.x), 0, Mathf.Round(gameObject.transform.position.z));

            var Bomb = Instantiate(_prefabBomb, pos, Quaternion.identity);

            NetworkObject bombNetwork = Bomb.GetComponent<NetworkObject>();
            bombNetwork.Spawn(true);
            //setup bomb info
            Bomb.GetComponent<BombManager>().explodeRange = bombPower;
            numberBombLeft -= 1;
        }

        if (numberBombLeft <= 0)
        {
            StartCoroutine(StartCooldownBomb(cooldownPlantBomb));
        }
    }
    

    //function than will allow the player to first catch a bomb then throw it
    public void ThrowingBomb(InputAction.CallbackContext context)
    {

        /*
        if(bombInHand == null)
        {
            SeizeBomb();
        }
        else
        {
            ThrowBomb();
        }*/

    }

    //function to throw a bomb
    public void ThrowBomb()
    {
        Debug.Log("Throw a Bomb");
    }
    //function to catch/ seize a bomb
    public void SeizeBomb()
    {
        Debug.Log("Seize a bomb");
    }

    public void PunchBomb(InputAction.CallbackContext context)
    {
        Debug.Log("Punch bomb");
    }


    public void TakeDamange()
    {
        if(!hasShield)
        {
            Destroy(gameObject);
        }
        else
        {
            hasShield = false;
            //desactivate shield
        }

    }

    IEnumerator StartCooldownBomb(float duration)
    {
        yield return new WaitForSeconds(duration);
        numberBombLeft = numberOfBombMax;
    }


}
