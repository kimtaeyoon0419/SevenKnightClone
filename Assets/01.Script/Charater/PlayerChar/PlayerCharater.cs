using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerCharType
{
    Tank,
    Dps,
    Healer
}

/// <summary>
/// �÷��̾� ĳ���� ��ũ��Ʈ
/// </summary>
public class PlayerCharater : Character
{
    [Header("Skill")]
    public float maxSkillTime;
    public float curentSkillTime;

    [Header("CharType")]
    [SerializeField] protected PlayerCharType charType;


    protected override void OnEnable()
    {
        base.OnEnable();
        curentSkillTime = maxSkillTime;
    }

    private void Start()
    {
        SquadManager.instance.MemberList.Add(gameObject);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void Update()
    {
        base.Update();
        UseSkill();
    }

    
    /// <summary>
    /// ���� ����� �� Ȥ�� ���� ã��
    /// </summary>
    /// <returns></returns>~
    protected override GameObject FindEnemy()
    {
        GameObject nearObject = null;
        Collider2D[] aroundEnemy;
        float distance = float.MaxValue;

        if (charType == PlayerCharType.Tank || charType == PlayerCharType.Dps) // ��Ŀ�� ������ ���
        {
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
        }
        else //������ ���  
        {
            foreach (GameObject teamObject in SquadManager.instance.MemberList)
            {
                Character curTeam = teamObject.GetComponent<Character>();
                if (curTeam.curHp < distance)
                {
                    distance = curTeam.curHp;
                    nearObject = teamObject;
                }
            }
        }

        return nearObject;
    }

    protected override void Attack(GameObject enemy)
    {
        Character character = enemy.GetComponent<Character>();
        anim.SetTrigger(hashAttack);
        if (charType == PlayerCharType.Tank || charType == PlayerCharType.Dps)
        {
            character.TakeDmg(atkPower);                                // ���� ���ݷ¸�ŭ ���� ü���� ����
        }
        else
        {
            character.TakeDmg(-atkPower);                               // ���� ���ݷ¸�ŭ ������ ü���� ȸ��
        }
    }

    protected virtual void UseSkill()
    {
        if (curentSkillTime >= 0)
        {
            curentSkillTime -= Time.deltaTime;
        }
        else if(0 >= curentSkillTime)
        {
            curentSkillTime = maxSkillTime;
        }
    }
}
