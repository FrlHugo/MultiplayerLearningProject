using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelOption;
 
    // Start is called before the first frame update
    void Start()
    {
        panelMainMenu.SetActive(true);
        panelOption.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !panelOption.activeSelf)
        {
            OnClickQuit();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && panelOption.activeSelf)
        {
            OnClickCloseOptions();
        }
        

    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void OnClickOpenOptions()
    {

    }

    public void OnClickCloseOptions()
    {

    }

    public void OnClickQuit() 
    {
        Application.Quit();
    }
}
