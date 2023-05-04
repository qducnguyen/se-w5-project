using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TMP_Text scoreText;
    private int current_score = 0;
    // Start is called before the first frame update


    // Update is called once per frame
    public void UpdateScore(int score)
    {
        // if (score > current_score)
        // {
        //     scoreText.text = "Score: " + score.ToString();
        //     current_score = score;
        // }

        scoreText.text = "Score: " + score.ToString();
        current_score = score;
    }
}
