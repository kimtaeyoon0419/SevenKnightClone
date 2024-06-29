using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;
using static UnityEngine.GraphicsBuffer;

/// <summary>
/// 캐릭터의 현재 상태
/// </summary>
public enum CharacterState
{
    move,
    fight,
    die
}

/// <summary>
/// 모든 캐릭터의 최상위 클래스
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    [Header("Stat")]
    public string charName;
    public float maxHp;
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

    protected virtual void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }

    protected virtual void OnEnable()
    {
        curHp = maxHp;
    }

    protected virtual void OnDisable()
    {
        StopAllCoroutines();
    }

    protected virtual void Update()
    {
        if (state != CharacterState.die)
        {
            {
                if (enemy == null || !enemy.activeSelf)
                {
                    enemy = FindEnemy();
                }
                else
                {
                    TakeAttack();
                    Move2Enemy();
                    LookEnemy();
                }
                if (curAtkSpeed > 0)
                {
                    curAtkSpeed -= Time.deltaTime;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPos.transform.position, findRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.transform.position, atkRange);
    }


    #region 이동
    protected virtual void Move2Enemy()
    {
        if (state == CharacterState.move && enemy != null)
        {
            anim.SetBool(hashRun, true);
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
        }
    }

    protected virtual void LookEnemy()
    {
        //Vector2 newPos = enemy.transform.position - transform.position;
        //float rotZ = Mathf.Atan2(newPos.y, newPos.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Euler(0, 0, rotZ);

        Vector2 rotPos = enemy.transform.position - transform.position;
        if(rotPos.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(rotPos.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);   
        }
    }

    #endregion


    #region 적 or 팀 찾기
    protected abstract GameObject FindEnemy();
    #endregion

    #region 데미지 처리

    #region Attack
    /// <summary>
    /// 공격 시전
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
                }
            }
            else if (distace > atkRange)
            {
                state = CharacterState.move;
            }
        }
        if (enemy == null)
        {
        }
    }

    /// <summary>
    /// 공격
    /// </summary>
    /// <param name="enemy">현재 가장 가까운 적 or 팀</param>
    protected abstract void Attack(GameObject enemy);
    #endregion

    /// <summary>
    /// 피격 데미지
    /// </summary>
    /// <param name="damage"></param>
    public virtual void TakeDmg(float damage)
    {
        curHp -= damage;
        if (curHp <= 0 && state != CharacterState.die)
        {
            curHp = 0;
            StartCoroutine(Die());
        }
        if(curHp > maxHp)
        {
            curHp = maxHp;
        }
    }

    /// <summary>
    /// 죽음
    /// </summary>
    #region Die
    protected virtual IEnumerator Die()
    {
        if (state != CharacterState.die)
        {
            anim.SetTrigger(hashDie);
            state = CharacterState.die;

            yield return new WaitForSeconds(1.5f);

            gameObject.SetActive(false);
        }
        else
        {
            yield return null;
        }
    }
    #endregion

    #endregion
}
