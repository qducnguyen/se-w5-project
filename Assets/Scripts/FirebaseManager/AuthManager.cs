using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using TMPro;

public class AuthManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;    
    public FirebaseUser User;

    //Login variables
    [Header("Login")]
    public TMP_InputField usernameLoginField;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;


    private const string EMAIL_ADDRESS = "w5.com";
    private const string DEFAULT_PASSWORD = "123456";


    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
    }


    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(usernameLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(usernameRegisterField.text));
    }

    private IEnumerator Login(string _username)
    {
        //Call the Firebase auth signin function passing the email and password
        string _email = string.Concat(_username, "@", EMAIL_ADDRESS);

        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, DEFAULT_PASSWORD);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Username";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Username";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            Debug.LogWarning(message);
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result.User;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            Debug.Log("Logged In");
        }
    }

    private IEnumerator Register(string _username)
    {
        if (_username == "")
        {
            //If the username field is blank show a warning
            Debug.LogWarning("Missing Username");
        }
        else 
        {
            //Call the Firebase auth signin function passing the email and password
            string _email = string.Concat(_username, "@", EMAIL_ADDRESS);
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, DEFAULT_PASSWORD);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Username";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Username Already In Use";
                        break;
                }
                Debug.LogWarning(message);
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result.User;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile{DisplayName = _username};

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        Debug.LogWarning("Username Set Failed!");

                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen
                        StartScreenUIManager.instance.LoginScreen();
                    }
                }
            }
        }
    }

}
