using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;
    public PlayerAttack playerAtk;
    public Hand hand;
    public PoolManager pool;
    public Texture2D cursor;
    public Slider sliderBoss;
    public float gameTime;
    public int stageLevel;
    public bool bossLevel;
    public Golem golemPrefab;
    public Light2D light;

    private void Awake()
    {
        instance = this;
        Cursor.SetCursor(cursor, new Vector2(cursor.width / 2, cursor.height / 2), CursorMode.ForceSoftware);
        Application.targetFrameRate = 120;
    }

    private void Update()
    {
        gameTime += Time.deltaTime;

        Timer();
    }

    void Timer()
    {
        switch (gameTime)
        {
            case 0.01f:
                stageLevel = 0;
                break;
            case 10.0f:
                stageLevel = 1;
                break;
            case 480.0f:
                stageLevel = 2;
                break;
            case 600.0f:
                stageLevel = 3;
                break;
        }
    }
}
