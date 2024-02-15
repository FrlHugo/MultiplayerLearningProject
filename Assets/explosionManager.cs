using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class explosionManager : MonoBehaviour
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
            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "Bomb")
        {
            other.gameObject.GetComponent<BombManager>().Explode();
        }
        if(other.gameObject.tag == "WoodBlock")
        {
            Debug.Log("Woodblock On trigger Enter");
            other.gameObject.GetComponent<WoodBlock>().BlockDestroyed();
        }

    }

    IEnumerator WaitExplosionEnd()
    {
        yield return new WaitForSeconds(particleSystemExplosion.main.duration);
        Destroy(gameObject);
    }
}
