using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static GameManager GetOrCreate()
    {
        return instance ??= new GameManager();
    }

    public int maxSquadSize;

    public int monney;

    void Update()
    {
        
    }
}
