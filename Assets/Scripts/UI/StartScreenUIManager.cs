using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenUIManager : MonoBehaviour
{
    public static StartScreenUIManager instance;

    public GameObject startScreenUI;

    public GameObject loginUI;

    public GameObject registerUI;

    public void ClearScreen(){
        startScreenUI.SetActive(false);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }


    public void LoginScreen() //Back button
    {
        ClearScreen();
        loginUI.SetActive(true);
    }
    public void RegisterScreen() // Regester button
    {
        ClearScreen();
        registerUI.SetActive(true);
    }

    public void StartScreen()
    {
        ClearScreen();
        startScreenUI.SetActive(true);
    }

    public void StartGame()
    {
        // ClearScreen();
        SceneManager.LoadScene("Prototype");
    }

}
