using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void LateUpdate()
    {
        textMeshProUGUI.text = SquadManager.instance.MemberList[0].GetComponent<Character>().charName;
    }
}
