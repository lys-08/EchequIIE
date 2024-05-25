using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pauseMenu;
    [SerializeField] private GameObject target;

    /**
     * Activate or desactivate the pause menu
     */
    public void Button()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        target.SetActive(!target.activeSelf);
    }
    
    /**
     * Allow the user to go back to the main menu
     */
    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
