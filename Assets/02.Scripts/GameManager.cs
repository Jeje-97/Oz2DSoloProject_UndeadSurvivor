using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 10,  30,  60, 100, 150, 210, 280, 360, 450, 600};

    public PoolManager poolManager;
    public PlayerController playerController;

    private void Awake()
    {
        instance = this;
    }
    void Update()
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

    }

    public void GetExp()
    {
        exp++;

        if (exp == nextExp[level])
        {
            level++;
            exp = 0;
        }
    }
}
