using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    /// <summary>
    /// ���� ������ ������
    /// </summary>
    [SerializeField] private int curSquadSize;
    /// <summary>
    /// ���� ������ ��� ����Ʈ
    /// </summary>
    [SerializeField] private List<GameObject> memberList = new List<GameObject>();
}
