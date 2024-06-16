using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : PlayerCharater
{
    private void Update()
    {
        if (enemy == null || !enemy.activeSelf)
        {
            enemy = FindEnemy();
        }
        if (curAtkSpeed > 0) curAtkSpeed -= Time.deltaTime;

        TakeAttack();
        Move2Enemy();
    }
}
