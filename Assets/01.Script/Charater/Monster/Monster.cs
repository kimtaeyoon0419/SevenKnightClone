using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    protected override GameObject FindEnemy()
    {
        GameObject nearObject = null;
        Collider2D[] aroundEnemy;
        float distance = float.MaxValue;

        aroundEnemy = Physics2D.OverlapCircleAll(attackPos.transform.position, findRange, enemyLayer);
        if (aroundEnemy == null) return null;

        foreach (Collider2D curObject in aroundEnemy)
        {
            float curdistance = Vector2.Distance(transform.position, curObject.transform.position);

            if (curdistance < distance)
            {
                Character curCharacter = curObject.GetComponent<Character>();
                distance = curdistance;
                if (curCharacter != null && curCharacter.state != CharacterState.die)
                    nearObject = curObject.gameObject;
                else
                {
                    return null;
                }
            }
        }

        return nearObject;
    }

    protected override void Attack(GameObject enemy)
    {
        Character character = enemy.GetComponent<Character>();
        anim.SetTrigger(hashAttack);

        character.TakeDmg(atkPower);                                // ���� ���ݷ¸�ŭ ���� ü���� ����
    }
}
