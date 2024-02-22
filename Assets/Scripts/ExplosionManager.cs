using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionManager : NetworkBehaviour
{
    private ParticleSystem particleSystemExplosion;

    private GameObject gameObjectToDestroy;
    
    private void Awake()
    {
        particleSystemExplosion = GetComponent<ParticleSystem>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitExplosionEnd());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter (Collider other)
    {
        Debug.Log("OnTriggerEnter with : " + other.gameObject.name);
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<BomberMan>().TakeDamange();
        }
        if(other.gameObject.tag == "Bomb")
        {
            other.gameObject.GetComponent<BombManager>().Explode();
        }
        if(other.gameObject.tag == "WoodBlock")
        {
            Debug.Log("it is a woodblock");
            //other.gameObject.GetComponent<WoodBlock>().BlockDestroyed();
            gameObjectToDestroy = other.gameObject;
            OnCollisionDestroyWoodBlock();
        }
        if(other.gameObject.tag == "PowerUp")
        {
            Debug.Log("Destroy Power UP :" + other.gameObject.name);

            other.gameObject.transform.parent.gameObject.GetComponent<PowerUp>().DestroyPowerUp();
            
        }

    }

   
    public void OnCollisionDestroyWoodBlock()
    {
        
        Debug.Log("OnCollisionDestroyWoodBlockServerRpc");

        GameManager.instance.DestroyWoodBlockServerRpc(GameManager.instance.listWoodGOBlocks.IndexOf(gameObjectToDestroy));
        gameObjectToDestroy = null;
    }






    
    IEnumerator WaitExplosionEnd()
    {
        yield return new WaitForSeconds(particleSystemExplosion.main.duration);
        Destroy(gameObject);
    }
}
