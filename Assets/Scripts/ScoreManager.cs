using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    // Start is called before the first frame update


    // Update is called once per frame
    public void UpdateScore(int score)
    {
        scoreText.text = "Score: " + score.ToString();
    }
}
