using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

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
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [Header("Stat")]
    [SerializeField] protected float maxHp;
    [SerializeField] public float curHp { get; protected set; }
    [SerializeField] protected float atkPower;
    [SerializeField] protected float atkSpeed;
    [SerializeField] protected float curAtkSpeed;
    [SerializeField] protected float findRange = 5f;
    [SerializeField] protected float atkRange;
    [SerializeField] protected float speed = 2f;
    [SerializeField] public CharacterState state;

    [Header("attackPos")]
    [SerializeField] protected Transform attackPos;

    [Header("enemy")]
    [SerializeField] protected GameObject enemy;
    [SerializeField] protected LayerMask enemyLayer;

    [Header("Component")]
    protected Rigidbody2D rigid;
    [SerializeField] protected Animator anim;

    [Header("Animation")]
    protected readonly int hashRun = Animator.StringToHash("Run");
    protected readonly int hashAttack = Animator.StringToHash("Attack");
    protected readonly int hashDie = Animator.StringToHash("Die");

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    private void Start()
    {
        curHp = maxHp;
    }

    private void OnDrawGizmosSelected()
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
            anim.SetBool(hashRun, true);
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
                anim.SetBool(hashRun, false);

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
            curHp = 0;
            Die();
        }
        if(curHp > maxHp)
        {
            curHp = maxHp;
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
