using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject menu1;
    [SerializeField] private GameObject menu2;

    public void Quit()
    {
        Application.Quit();
    }

    public void SwitchMenu()
    {
        menu1.SetActive(!menu1.activeSelf);
        menu2.SetActive(!menu2.activeSelf);
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
}
