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
        Destroy(gameObject);
 
        int i = Random.Range(0, 100);
        if (i < 90)
        {
            Debug.Log("Spawn power up ");
            int y = Random.Range(0, powerUpTab.Length);
            Instantiate(powerUpTab[y], transform.position, Quaternion.identity);
        }

    }

}
