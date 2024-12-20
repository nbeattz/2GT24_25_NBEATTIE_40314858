using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static float enemySpeed;

    public static void SetDifficulty(float speed)
    {
        enemySpeed = speed;
    }
}