using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Multiplayer.Samples.Utilities.ClientAuthority;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using static UnityEngine.Rendering.DebugUI;

public class GameManager : NetworkBehaviour
{
    public static GameManager instance { get; set; }

    public List<ulong> listPlayerId = new List<ulong>();

    public Dictionary<ulong, GameObject> PlayerDictionnary = new Dictionary<ulong, GameObject>();

    public List<GameObject> listWoodGOBlocks = new List<GameObject>();

    public List<GameObject> listPowerUpPrefab = new List<GameObject>();

    public List<GameObject> listPowerUp = new List<GameObject>();

    public GameObject prefabWoodBlock;

    public GameObject field;

    public bool gameStarted = false;

    [SerializeField] private GameObject playerPrefab;

    [SerializeField] private GameObject waitPanel;
    [SerializeField] private TMP_Text txt_TimeLeft;
    [SerializeField] private GameObject countDownPanel;
    [SerializeField] private GameObject menuUI;
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        waitPanel.SetActive(true);
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsHost)
        {
            // if not the server will do nothing.
        }
        
    }

    private void OnClientConnected(ulong clientId)
    {
        if(!IsHost) { return; }
        /*
        if(PlayerDictionnary.Count == 1)
        {
            menuUI.SetActive(false);
        }


        */
        // TeleportPlayerToSpawnPointServerRpc(clientId);



        listPlayerId.Add(clientId);
        Debug.Log("PlayerDictionnary.Count : " + listPlayerId);

        if (listPlayerId.Count >= 2)
        {
            countDownPanel.SetActive(true);
            menuUI.SetActive(false);
            waitPanel.SetActive(false);
            StartCoroutine(CountDown(5));
        }
        
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.Log("ClientDisconnected :" + clientId);
    }

    [ServerRpc(RequireOwnership = false)]
    public void TeleportPlayerToSpawnPointServerRpc(ulong clientId)
    {
        if(PlayerDictionnary.ContainsKey(clientId))
        {
            PlayerDictionnary[clientId].GetComponent<PlayerController>().TeleportPlayerToSpawnPointClientRpc();
        }
    }



    [ServerRpc]
    public void DestroyWoodBlockServerRpc(int WoodBlockindex)
    {
        GameObject woodBlock = listWoodGOBlocks[WoodBlockindex];

        woodBlock.GetComponent<NetworkObject>().Despawn(true);
    
        Destroy(woodBlock);

        int i = Random.Range(0, 100);
        if (i < 90)
        {

        }
    }
    
    [ServerRpc]
    public void LevelGenerator2ServerRpc()
    {
        int width = 15;
        int height = 15;

        // spawn woodblocks
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                if (i == 0 || j == 0 || i == 14 || j == 14)
                {

                }
                else if ((i > 0 && i < 14 && j > 0 && j < 14) && ((i % 2 == 0) && (j % 2 == 0)))
                {

                }
                else if (!((i == 1 && j == 1) || (i == 1 && j == 2) || (i == 2 && j == 1) ||
                    (i == 13 && j == 1) || (i == 13 && j == 2) || (i == 12 && j == 1) ||
                    (i == 1 && j == 13) || (i == 2 && j == 13) || (i == 1 && j == 12) ||
                    (i == 13 && j == 13) || (i == 13 && j == 12) || (i == 12 && j == 13)))
                {
                    GameObject go = Instantiate(prefabWoodBlock, new Vector3(i, 0, j), Quaternion.identity);

                    go.GetComponent<NetworkObject>().Spawn(true);
                    listWoodGOBlocks.Add(go);
                    go.name = "WoodBlock_" + i + "/" + j;
                    //go.transform.SetParent(field.transform);
                }


            }
        }
    }

    private void SpawnPlayersGO()
    {
        countDownPanel.SetActive(false);

        foreach(ulong clientId in listPlayerId)
        {

            var spawnPoint = PlayerSpawnManager.instance.listPlayerSpawn[0];

            PlayerSpawnManager.instance.listPlayerSpawn.Remove(spawnPoint);

            GameObject go = Instantiate(playerPrefab, spawnPoint.transform.position, playerPrefab.transform.rotation);

            NetworkObject nto = go.GetComponent<NetworkObject>();

            nto.SpawnWithOwnership(clientId);

            PlayerDictionnary.Add(clientId, go);
            
        }
    }

    IEnumerator CountDown(int duration)
    {
        countDownPanel.SetActive(true);
        int timeleft = duration;
        while(timeleft > 0)
        {
            Debug.Log(timeleft);
            txt_TimeLeft.text = timeleft.ToString();
            yield return new WaitForSeconds(1);
            timeleft--;

        }

        SpawnPlayersGO();
    }
}
