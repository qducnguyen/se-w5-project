using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    

    [SerializeField] private TMP_Text statsText;
    [SerializeField] private Player player;
    [SerializeField] private GameObject hook;

    PlayerCollision collision;
    HookCollision fighting;

    private int distance = 0;

    private float maxDistance = 0f;
    private int money;

    public static ScoreManager Instance;
    public const string prefScore = "prefScore";

    private void Awake() {
        Instance = this;
        collision = player.GetComponent<PlayerCollision>();
        fighting = hook.GetComponent<HookCollision>();

    }
    void Start()
    {
    }

    void Update()
    {

        if (player.distance > maxDistance){
            maxDistance = player.distance;
            distance = Mathf.FloorToInt(player.distance);
        }

        money = collision.moneyGet + fighting.moneyGet;
        statsText.text = distance + " m\nMoney " + money;
    }

    public bool CheckNewHighScore()
    {
        if (distance > PlayerPrefs.GetInt(prefScore))
        {
            PlayerPrefs.SetInt(prefScore, distance);
            return true;
        }
        else
        {
            return false;
        }
    }
}

