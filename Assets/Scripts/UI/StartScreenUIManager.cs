using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Firebase.Auth;
public class StartScreenUIManager : MonoBehaviour
{
    public static StartScreenUIManager instance;

    public GameObject startScreenUI;

    public GameObject loginUI;

    public GameObject registerUI;

    public GameObject settingsUI;

    public Button loginButton;

    public Button logoutButton;

    public Button syncButton;


    public TMP_Text WelcomeText;

    public TMP_Text TotalMoneyText;

    public TMP_Text HighScoreText;


    public void ClearScreen()
    {
        startScreenUI.SetActive(false);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        settingsUI.SetActive(false);
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

    public void SettingsScreen()
    {
        ClearScreen();
        settingsUI.SetActive(true);
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

    public void UpdateUserInformation(FirebaseUser user){
        if (user != null){
            WelcomeText.text =  "Welcome " + user.DisplayName + "!";

            loginButton.interactable = false;
            logoutButton.interactable = true;
            syncButton.interactable = true;
            // loginButton.SetActive(false);
            // logoutButton.SetActive(true);
            // syncButton.SetActive(true);
        }
        else{
            WelcomeText.text =  "You are playing as Anonymous";

            loginButton.interactable = true;
            logoutButton.interactable = false;
            syncButton.interactable = false;

            // loginButton.SetActive(true);
            // logoutButton.SetActive(false);
            // syncButton.SetActive(false);
        }
        TotalMoneyText.text = "Total Money: " + PlayerPrefs.GetInt("prefTotalMoney").ToString();
        HighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("prefScore").ToString() + " m";

    }

}
