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

    public GameObject shoppingUI;

    public GameObject leaderBoardUI;

    public Button loginButton;

    public Button logoutButton;

    public Button syncButton;

    public Button shopButton;

    public Button lbButton;


    public TMP_Text WelcomeText;

    public TMP_Text TotalMoneyText;

    public TMP_Text HighScoreText;

    [HideInInspector] public bool IsInitialized { get; private set;} // For get and set


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

        IsInitialized = true;
    }

    public void ClearScreen()
    {
        startScreenUI.SetActive(false);
        loginUI.SetActive(false);
        registerUI.SetActive(false);
        settingsUI.SetActive(false);
        shoppingUI.SetActive(false);
        leaderBoardUI.SetActive(false);
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

     public void ShoppingScreen()
    {
        ClearScreen();
        shoppingUI.SetActive(true);
        // SceneManager.LoadScene("ShoppingSystem");

    }

     public void leaderBoardScreen()
    {
        ClearScreen();
        leaderBoardUI.SetActive(true);

    }


    public void StartScreen()
    {
        UpdateUserInformation(FirebaseManager.User);
        ClearScreen();
        startScreenUI.SetActive(true);
    }

    public void StartGame()
    {
        // ClearScreen();
        SceneManager.LoadScene("InGameScreen");
    }


    public void UpdateUserInformation(FirebaseUser user){
        if (user != null){
            WelcomeText.text =  "Welcome " + user.DisplayName + "!";

            loginButton.interactable = false;
            logoutButton.interactable = true;
            syncButton.interactable = true;
            lbButton.interactable = true;
        }
        else{
            WelcomeText.text =  "You are playing as Anonymous";

            loginButton.interactable = true;
            logoutButton.interactable = false;
            syncButton.interactable = false;
            lbButton.interactable = false;
        }
        TotalMoneyText.text = "Total Money: " + PlayerPrefs.GetInt("prefTotalMoney").ToString();
        HighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("prefScore").ToString() + " m";

    }

}
