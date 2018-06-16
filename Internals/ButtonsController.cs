using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    
    public void NewGame()
    {
        Game.current = new Game();
        LoadSceneOnClick(1);
    }

    //Buttons Control ----------------
    public void LoadSceneOnClick(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {

        SaveLoad.Load();
    }
}
