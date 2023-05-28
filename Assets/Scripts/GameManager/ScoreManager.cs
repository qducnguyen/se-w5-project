using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    
    Player player;
    Text distanceText;
    PlayerCollision collision;

    private int distance;

    public static ScoreManager Instance;
    public const string prefScore = "prefScore";

    private void Awake() {
        Instance = this;
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("ScoreText").GetComponent<Text>();
        collision = GameObject.Find("Player").GetComponent<PlayerCollision>();

    }
    void Start()
    {
    }

    void Update()
    {
        distance = Mathf.FloorToInt(player.distance);
        distanceText.text = distance + " m\nMoney " + collision.moneyGet;
    }

    public bool CheckNewHighScore()
    {
        if (distance > PlayerPrefs.GetInt(prefScore))
        {
            PlayerPrefs.SetInt(prefScore, distance);
            Debug.Log("new highscore: " + distance);
            return true;
        }
        else
        {
            Debug.Log("no new highscore");
            return false;
        }
    }
}

