using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour {

    public static GameplayManager control; //Do Not delete Controll

    [SerializeField] //Using for Exteral access (drag and drop like public)
    GameObject PauseMenu; //Pause menu buttons group

    bool Paused = false;
    public bool Ingame = false; // Keep track if at main menu

    [SerializeField]
    GameObject playerobject;

    [SerializeField]
    Transform Spawn;

    void Awake()
    {
        Game.current = new Game();
        Instantiate(playerobject, Spawn.position, Spawn.rotation);
        //Keep control going always run GameManager
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }



    void Update()
    {
        if (InGame())
        {
            CheckPausing();
        }
    }

    bool InGame()
    {
        //Enable GUI
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Ingame = false;
            return false;
        }
        else
        {
            Ingame = true;
            return true;
        }
    }
    void CheckPausing()
    {
        //Game Control While In Main Menu and Pausing
        if (Ingame)
        {
            //Pause In Game on Esc Press toggle----------------
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Paused = !Paused;
            }


            if (Paused)
            {
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                PauseMenu.SetActive(true);
            }
            else
            {
                Time.timeScale = 1;
                PauseMenu.SetActive(false);
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        //------------------------------------------------------
    }

    public void LoadInfo()
    {

    }



    public void SaveNow()
    {
        playerobject.GetComponent<PlayerManager>().CompileSaveInformation();
        SaveLoad.Save();
    }


}
