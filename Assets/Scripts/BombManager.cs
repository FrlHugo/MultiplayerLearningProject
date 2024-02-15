using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[RequireComponent(typeof(BoxCollider))]
public class BombManager : MonoBehaviour
{
    private BoxCollider boxCollider;
    public float delayExplosion = 3f;

    public GameObject bombMesh;

    
    private ParticleSystem bombFire;

    [SerializeField] private GameObject prefabExplosion;

    public float explodeRange = 1f;

    /// <summary>
    /// raycast for explosion
    /// </summary>
    private RaycastHit hitLeft;
    private RaycastHit hitRight;
    private RaycastHit hitForward;
    private RaycastHit hitBack;

    private float hitRightDistance;
    private float hitLeftDistance;
    private float hitForwardDistance;
    private float hitBackDistance;

    private bool rightHit;
    private bool leftHit;
    private bool forwardHit;
    private bool backHit;

    private bool canDrawGizmos = false;


    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        bombMesh = transform.GetChild(0).gameObject;

    }

    private void Start()
    {
        StartCoroutine(CountDownExplosion());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            boxCollider.isTrigger = false;
        }
    }

    IEnumerator CountDownExplosion()
    {
        yield return new WaitForSeconds(delayExplosion);
        canDrawGizmos = true;
        Explode();//

    }

    public void RaycastHitAllDirection()
    {
        int layerMask = LayerMask.GetMask("Walls");
        //right
        if(rightHit = Physics.BoxCast(transform.position, new Vector3(0.45f, 0.4f, 0.45f), transform.right, out hitRight, Quaternion.identity, explodeRange, layerMask))
        {
            hitRightDistance = Mathf.Round(hitRight.collider.gameObject.transform.position.x) - Mathf.Round(transform.position.x) - 1;

            if (hitRight.collider.gameObject.tag == "WoodBlock")
            {
                hitRightDistance += 1;
            }
        }
        else
        {
            hitRightDistance = explodeRange;
        }

        //Left
        if(leftHit = Physics.BoxCast(transform.position, new Vector3(0.45f, 0.4f, 0.45f), - transform.right, out hitLeft, Quaternion.identity, explodeRange, layerMask))
        {
            hitLeftDistance =  - (Mathf.Round(hitLeft.collider.gameObject.transform.position.x) - Mathf.Round(transform.position.x)) - 1;
            if (hitLeft.collider.gameObject.tag == "WoodBlock")
            {
                hitLeftDistance += 1;
            }
        }
        else
        {
            hitLeftDistance = explodeRange;
        }

        //Forward
        if (forwardHit = Physics.BoxCast(transform.position, new Vector3(0.45f, 0.4f, 0.45f), transform.forward, out hitForward, Quaternion.identity, explodeRange, layerMask))
        {
            hitForwardDistance =  Mathf.Round(hitForward.collider.gameObject.transform.position.z) - Mathf.Round(transform.position.z) -1;
            if (hitForward.collider.gameObject.tag == "WoodBlock")
            {
                Debug.Log("WoodBlock Forward Cast");
                hitForwardDistance += 1;
            }
        }
        else
        {
            hitForwardDistance = explodeRange;
        }

        //Back
        if (backHit = Physics.BoxCast(transform.position, new Vector3(0.45f, 0.4f, 0.45f), -transform.forward, out hitBack, Quaternion.identity, explodeRange, layerMask))
        {
            hitBackDistance = - ( Mathf.Round(hitBack.collider.gameObject.transform.position.z) - Mathf.Round(transform.position.z)) - 1;
            if (hitBack.collider.gameObject.tag == "WoodBlock")
            {
                hitBackDistance += 1;
            }
        }
        else
        {
            hitBackDistance = explodeRange;
        }

        Debug.Log("HitBack " + hitBackDistance);


    }

    public void Explode()
    {
        RaycastHitAllDirection();

       

        Destroy(bombMesh);
        Destroy(bombFire);

        PlayExplosionSound();
        SpawnVFXExplositon();

        Destroy(gameObject);
    }

    public void PlayExplosionSound()
    {

    }

    public void SpawnVFXExplositon()
    {
        Instantiate(prefabExplosion, transform.position, Quaternion.identity);

        for (int i = 1; i <= hitRightDistance; i++) 
        {
            Instantiate(prefabExplosion, transform.position + transform.right * i, Quaternion.identity);
        }
        for (int i = 1; i <= hitLeftDistance; i++)
        {
            Instantiate(prefabExplosion, transform.position + -transform.right * i, Quaternion.identity);
        }
        for (int i = 1; i <= hitForwardDistance; i++)
        {
            Instantiate(prefabExplosion, transform.position + transform.forward * i, Quaternion.identity);
        }
        for (int i = 1; i <= hitBackDistance; i++)
        {
            Instantiate(prefabExplosion, transform.position + -transform.forward * i, Quaternion.identity);
        }

    }

    /*
    void OnDrawGizmos()
    {
        if(canDrawGizmos)
        {
           
            if (rightHit)
            {
                for(int i = 1; i < hitRightDistance; i++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(transform.position + transform.right * i, new Vector3(0.9f, 0.9f, 0.9f));
                }
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(hitRight.collider.gameObject.transform.position, new Vector3(0.9f, 0.9f, 0.9f));
            }
            else
            {
                Gizmos.color = Color.green;
                for (int i = 1; i <= explodeRange; i++)
                {
                    Gizmos.DrawWireCube(transform.position + transform.right * i, new Vector3(0.9f, 0.9f, 0.9f));
                }
            }

            if (leftHit)
            {
                for (int i = 1; i < hitLeftDistance; i++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(transform.position + -transform.right * i, new Vector3(0.9f, 0.9f, 0.9f));
                }
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(hitLeft.collider.gameObject.transform.position, new Vector3(0.9f, 0.9f, 0.9f));
            }
            else
            {
                Gizmos.color = Color.green;
                for (int i = 1; i <= explodeRange; i++)
                {
                    Gizmos.DrawWireCube(transform.position + -transform.right * i, new Vector3(0.9f, 0.9f, 0.9f));
                }
            }

            if (forwardHit)
            {
                for (int i = 1; i < hitForwardDistance; i++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(transform.position + transform.forward * i, new Vector3(0.9f, 0.9f, 0.9f));
                }
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(hitForward.collider.gameObject.transform.position, new Vector3(0.9f, 0.9f, 0.9f));
            }
            else
            {
                Gizmos.color = Color.green;
                for (int i = 1; i <= explodeRange; i++)
                {
                    Gizmos.DrawWireCube(transform.position + transform.forward * i, new Vector3(0.9f, 0.9f, 0.9f));
                }
            }

            if (backHit)
            {
                for (int i = 1; i < hitBackDistance; i++)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawWireCube(transform.position + -transform.forward * i, new Vector3(0.9f, 0.9f, 0.9f));
                }
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireCube(hitBack.collider.gameObject.transform.position, new Vector3(0.9f, 0.9f, 0.9f));
            }
            else
            {
                Gizmos.color = Color.green;
                for (int i = 1; i <= explodeRange; i++)
                {
                    Gizmos.DrawWireCube(transform.position + -transform.forward * i, new Vector3(0.9f, 0.9f, 0.9f));
                }
            }
        }

    }
    */
}
