using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monsters
{
    public GameObject parentsObject;
    public string monsterName;
    public GameObject monster;
}

public class MonsterSpawnManager : MonoBehaviour
{
    public static MonsterSpawnManager instance;

    [Header("MonsterList")]
    [SerializeField] private List<Monsters> monsterList = new List<Monsters>();
    public List<GameObject> spawnedMonsters = new List<GameObject>();

    [Header("SpawnRange")]
    [SerializeField] private GameObject rangeObject;
    private BoxCollider2D rangeCollider;

    [Header("SpawnIndex")]
    [SerializeField] private int spawnIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        rangeCollider = rangeObject.GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if(spawnedMonsters.Count <= 0)
        {
            StartCoroutine(Co_SpawnMonster());
        }
    }

    private IEnumerator Co_SpawnMonster()
    {
        int monsterType;
        Debug.Log("몬스터 스폰을 시작합니다.");

        monsterType = Random.Range(0, monsterList.Count);

        for (int i = 0; i < spawnIndex; i++)
        {
            GameObject spawnMonster = Instantiate(monsterList[monsterType].monster, spawnPos(), Quaternion.identity, monsterList[monsterType].parentsObject.transform);
            Debug.Log("몬스터를 스폰했습니다 / 스폰된 몬스터의 이름 : " + monsterList[monsterType].monsterName);
            spawnedMonsters.Add(spawnMonster);
            yield return null;
        }
    }

    private Vector2 spawnPos()
    {
        Vector2 originPosition = rangeObject.transform.position;
        // 콜라이더의 사이즈를 가져오는 bound.size 사용
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);
        Vector2 RandomPostion = new Vector2(range_X, range_Y);

        Vector2 respawnPosition = originPosition + RandomPostion;
        return respawnPosition;
    }
}
