using UnityEngine;
using TMPro;

public class ScoreElement : MonoBehaviour
{
    public TMP_Text usernameText;
    public TMP_Text highScoreText;

    public void NewScoreElement (string _username, int _highscore){
        usernameText.text = _username;
        highScoreText.text = _highscore.ToString();
    }
}
