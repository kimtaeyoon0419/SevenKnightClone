using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private Character curChar;

    private void Awake()
    {
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        curChar = SquadManager.instance.MemberList[0].GetComponent<Character>();
    }

    private void LateUpdate()
    {
        textMeshProUGUI.text = curChar.charName;
    }
}
