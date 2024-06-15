using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquadManager : MonoBehaviour
{
    public static SquadManager instance;

    /// <summary>
    /// 현재 스쿼드 사이즈
    /// </summary>
    [SerializeField] private int curSquadSize;
    /// <summary>
    /// 현재 스쿼드 멤버 리스트
    /// </summary>
    [SerializeField] private List<GameObject> memberList = new List<GameObject>();
    public List<GameObject> MemberList { get { return memberList; } }


}
