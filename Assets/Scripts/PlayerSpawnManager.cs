using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    public List<GameObject> listPlayerSpawn =  new List<GameObject>();

    public NetworkVariable<List<GameObject>> networkListSpawn = new NetworkVariable<List<GameObject>>();

    public static PlayerSpawnManager instance {  get; set; }
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
