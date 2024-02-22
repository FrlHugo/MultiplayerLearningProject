using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TestingNetworkUI : MonoBehaviour
{

    public static TestingNetworkUI instance;
    // Start is called before the first frame update
    public void StartHosting()
    {
        Debug.Log("Start Host");
        NetworkManager.Singleton.StartHost();
        GameManager.instance.LevelGenerator2ServerRpc();
        
    }

    public void StartClient()
    {
        Debug.Log("Start Client");
        NetworkManager.Singleton.StartClient();
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);    
    }
    

    public void StartCountDown()
    {

    }
}
