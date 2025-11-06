using Goldmetal.UndeadSurvivor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public PoolManager poolManager;
    public PlayerController playerController;

    private void Awake()
    {
        instance = this;
    }
}
