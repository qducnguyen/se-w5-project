using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using TMPro;
using System.Linq;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    private DependencyStatus dependencyStatus;
    private FirebaseAuth auth;    
    public static FirebaseUser User;

    private DatabaseReference DBreference;


    [Header("Login")]
    public TMP_InputField usernameLoginField;

    public TMP_Text warningLoginText;
    public TMP_Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public TMP_InputField usernameRegisterField;
    public TMP_Text warningRegisterText;

    [Header("LeaderBoard")]
    public GameObject scoreElement;
    public Transform scoreboardContent;


    private const string EMAIL_ADDRESS = "w5.com";
    private const string DEFAULT_PASSWORD = "123456";

    [HideInInspector] public static FirebaseManager instance;
    [HideInInspector] public bool IsInitialized;


    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        instance = this;
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                // InitializeFirebaseDB();
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });

        IsInitialized = true;
    }

    private void Start() {
        StartScreenUIManager.instance.UpdateUserInformation(User);
    }
    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;

        auth.StateChanged += AuthStateChanged;

    }

    void AuthStateChanged(object sender, System.EventArgs eventArgs) {
        if (auth.CurrentUser != User) {
            bool signedIn = User != auth.CurrentUser && auth.CurrentUser != null;
            if (!signedIn && User != null) {
                Debug.Log("Signed out");
            }
            User = auth.CurrentUser;
            StartScreenUIManager.instance.UpdateUserInformation(User);
            if (signedIn) {
                Debug.Log("Signed in as " + User.DisplayName + "!");
            }
        }
    }

    // Handle removing subscription and reference to the Auth instance.
    // Automatically called by a Monobehaviour after Destroy is called on it.
    // Original Version OnDestroy()
    private void OnDestroy() {
        auth.StateChanged -= AuthStateChanged;
        auth = null;
        DBreference = null;
    }

     public void ClearLoginFields()
    {
        usernameLoginField.text = "";
    }
    public void ClearRegisterFields()
    {
        usernameRegisterField.text = "";
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


    public void SignoutButton()
    {
        ClearLoginFields();
        ClearRegisterFields();
        auth.SignOut();
        // StartScreenUIManager.instance.UpdateUserInformation(User);
    }

    public void SyncButton()
    {

        StartCoroutine(UpdateUserTotalMoney());
        StartCoroutine(UpdateUserHighScore());
        StartCoroutine(UpdateUsernameDatabase());

        Debug.Log("Success Synchronisation!");
    }

    public void ScoreboardButton(){
        StartCoroutine(LoadScoreboardData()); 
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
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result.User;
            // Debug.LogFormat("User signed in successfully: {0} ", User.DisplayName);
            // Debug.Log("Logged In");
            warningLoginText.text = "";
            confirmLoginText.text = "Logged in";
            
            StartCoroutine(LoadUserData());

            yield return new WaitForSeconds(0.5f);
            StartScreenUIManager.instance.UpdateUserInformation(User);
            StartScreenUIManager.instance.StartScreen();
            confirmLoginText.text = "";

            ClearLoginFields();
            ClearRegisterFields();

        }
    }

    private IEnumerator Register(string _username)
    {
        warningLoginText.text = "" ;

        if (_username == "")
        {
            //If the username field is blank show a warning
            Debug.LogWarning("Missing Username");
            warningRegisterText.text = "Missing Username";
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
                warningRegisterText.text = message;
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
                        auth.SignOut();

                        yield return new WaitForSeconds(0.25f);

                        StartScreenUIManager.instance.UpdateUserInformation(User);
                        StartScreenUIManager.instance.LoginScreen();
                        warningRegisterText.text = "";
                        ClearLoginFields();
                        ClearRegisterFields();

                    }
                }
            }
        }
    }

    private IEnumerator UpdateUserTotalMoney(){
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("prefTotalMoney").SetValueAsync(PlayerPrefs.GetInt("prefTotalMoney"));

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateUserHighScore(){
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("prefScore").SetValueAsync(PlayerPrefs.GetInt("prefScore"));

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }

    private IEnumerator UpdateUsernameDatabase()
    {
        //Set the currently logged in user username in the database
        var DBTask = DBreference.Child("users").Child(User.UserId).Child("username").SetValueAsync(auth.CurrentUser.DisplayName);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else
        {
            //Database username is now updated
        }
    }


    private IEnumerator LoadUserData()
    {
        //Get the currently logged in user data
        var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else if (DBTask.Result.Value == null)
        {
            //No data exists yet
            PlayerPrefs.SetInt("prefTotalMoney", 0);
            PlayerPrefs.SetInt("prefScore", 0);
        }
        else
        {
            //Data has been retrieved
            DataSnapshot snapshot = DBTask.Result;

            PlayerPrefs.SetInt("prefTotalMoney", int.Parse(snapshot.Child("prefTotalMoney").Value.ToString()));
            PlayerPrefs.SetInt("prefScore", int.Parse(snapshot.Child("prefScore").Value.ToString()));
    

        }
    }


    private IEnumerator LoadScoreboardData()
    {
        var DBTask = DBreference.Child("users").OrderByChild("prefScore").GetValueAsync();

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null){
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
        else{

            DataSnapshot snapshot = DBTask.Result;

            //Destroy any existing scoreboard elements
            foreach (Transform child in scoreboardContent.transform)
            {
                Destroy(child.gameObject);
            }

            //Loop through every users UID
            foreach (DataSnapshot childSnapshot in snapshot.Children.Reverse<DataSnapshot>())
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int highScore = int.Parse(childSnapshot.Child("prefScore").Value.ToString());

                // //Instantiate new scoreboard elements
                GameObject scoreboardElement = Instantiate(scoreElement, scoreboardContent);
                scoreboardElement.GetComponent<ScoreElement>().NewScoreElement(username, highScore);
            }

            //Go to scoareboard screen
            StartScreenUIManager.instance.leaderBoardScreen();
        }
    
    }

  


}