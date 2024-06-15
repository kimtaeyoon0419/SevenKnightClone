using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

/// <summary>
/// ĳ������ ���� ����
/// </summary>
public enum CharacterState
{
    move,
    fight,
    die
}

/// <summary>
/// ��� ĳ������ �ֻ��� Ŭ����
/// </summary>
public abstract class Character : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] protected float maxHp;
    [SerializeField] public float curHp { get; protected set; }
    [SerializeField] protected float atkPower;
    [SerializeField] protected float atkSpeed;
    [SerializeField] protected float curAtkSpeed;
    [SerializeField] protected float findRange;
    [SerializeField] protected float atkRange;
    [SerializeField] protected float speed;
    [SerializeField] public CharacterState state;

    [Header("attackPos")]
    [SerializeField] protected Transform attackPos;

    [Header("enemy")]
    [SerializeField] protected GameObject enemy;
    [SerializeField] protected LayerMask enemyLayer;

    private void Start()
    {
        curHp = maxHp;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.transform.position, findRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.transform.position, atkRange);
    }


    #region �̵�
    protected virtual void Move2Enemy()
    {
        if (state == CharacterState.move && enemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
        }
    }

    #endregion


    #region �� or �� ã��
    protected abstract GameObject FindEnemy();
    #endregion

    #region ������ ó��

    #region Attack
    /// <summary>
    /// ���� ����
    /// </summary>
    protected virtual void TakeAttack()
    {
        if (enemy != null)
        {
            float distace = Vector2.Distance(enemy.transform.position, attackPos.transform.position);

            if (distace <= atkRange)
            {
                state = CharacterState.fight;
                if (curAtkSpeed <= 0)
                {
                    Attack(enemy);
                    curAtkSpeed = atkSpeed;
                    Debug.Log("�����մϴ�!");
                }
            }
            else if (distace > atkRange)
            {
                state = CharacterState.move;
                Debug.Log("���� �ʹ� �־��!");
            }
        }
        if (enemy == null)
        {
            Debug.Log("���� �����ϴ�!");
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    /// <param name="enemy">���� ���� ����� �� or ��</param>
    protected abstract void Attack(GameObject enemy);
    #endregion

    /// <summary>
    /// �ǰ� ������
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDmg(float damage)
    {
        curHp -= damage;
        if (curHp <= 0)
        {
            state = CharacterState.die;
            Die();
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    #region Die
    protected virtual void Die()
    {

    }
    #endregion

    #endregion
}
