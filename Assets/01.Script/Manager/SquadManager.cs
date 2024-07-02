using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public static SquadManager instance;

    [Header("Squad")]
    /// <summary>
    /// ���� ������ ������
    /// </summary>
    [SerializeField] private int curSquadSize;
    /// <summary>
    /// ���� ������ ��� ����Ʈ
    /// </summary>
    public List<GameObject> memberList = new List<GameObject>();

    [Header("CameraUnit")]
    [SerializeField] private GameObject cameraUnit;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void LateUpdate()
    {
        curSquadSize = memberList.Count;
        Debug.Log("���� ������ ������ : " +  curSquadSize);
    }
}
