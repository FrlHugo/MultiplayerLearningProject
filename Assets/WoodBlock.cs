using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodBlock : MonoBehaviour
{

    [SerializeField] private GameObject[] powerUpTab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BlockDestroyed()
    {
        Debug.Log("BlockDestroyed");
        if(Random.Range(0,100) > 70)
        {
            //Instantiate(powerUpTab[Random.Range(0, powerUpTab.Length)], transform.parent);
        }
        Destroy(gameObject);
    }
}
