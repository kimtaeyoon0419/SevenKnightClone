using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public static SquadManager instance;

    /// <summary>
    /// ���� ������ ������
    /// </summary>
    [SerializeField] private int curSquadSize;
    /// <summary>
    /// ���� ������ ��� ����Ʈ
    /// </summary>
    [SerializeField] private List<GameObject> memberList = new List<GameObject>();
    public List<GameObject> MemberList { get { return memberList; } }


}
