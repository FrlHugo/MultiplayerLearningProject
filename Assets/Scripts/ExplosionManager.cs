using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ExplosionManager : MonoBehaviour
{
    private ParticleSystem particleSystemExplosion;
    
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
    
            other.gameObject.GetComponent<WoodBlock>().BlockDestroyed();
        }
        if(other.gameObject.tag == "PowerUp")
        {
            Debug.Log("Destroy Power UP :" + other.gameObject.name);

            other.gameObject.transform.parent.gameObject.GetComponent<PowerUp>().DestroyPowerUp();
            
        }

    }



    
    IEnumerator WaitExplosionEnd()
    {
        yield return new WaitForSeconds(particleSystemExplosion.main.duration);
        Destroy(gameObject);
    }
}
