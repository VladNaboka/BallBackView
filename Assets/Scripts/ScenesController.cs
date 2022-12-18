using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesController : MonoBehaviour
{
    [SerializeField] string nameSceneGame;
    [SerializeField] string nameSceneMenu;
    public void StartGame()
    {
        SceneManager.LoadScene(nameSceneGame);
    }
    public void OnReturnMenu()
    {
        SceneManager.LoadScene(nameSceneMenu);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
