using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnManager : MonoBehaviour
{
    [Header("MonsterList")]
    [SerializeField] private List<GameObject> monsterList = new List<GameObject>();
    [SerializeField] private List<GameObject> spawnedMonsters = new List<GameObject>();

    [Header("SpawnRange")]
    [SerializeField] private GameObject rangeObject;
    private BoxCollider2D rangeCollider;

    private void Awake()
    {
        rangeCollider = rangeObject.GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        StartCoroutine(SpawnMonster());
    }

    private IEnumerator SpawnMonster()
    {
        int monsterCount = Random.Range(0, 3);
        int monsterType;

        while (true)
        {
            monsterType = Random.Range(0, monsterList.Count);

            for (int i = 0; i < monsterCount; i++)
            {
                GameObject spawnMonster = Instantiate(monsterList[monsterType], spawnPos(), Quaternion.identity);

                spawnedMonsters.Add(spawnMonster);
            }

            yield return new WaitForSeconds(3f);
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
