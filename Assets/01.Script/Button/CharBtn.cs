using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharBtn : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshProUGUI;

    [Header("ButtonSetting")]
    [SerializeField] private int btnIndex;
    [SerializeField] private PlayerCharater curChar;
    [SerializeField] private Image hpBar;
    [SerializeField] private Image skillBar;

    private void Start()
    {
        if (btnIndex <= SquadManager.instance.memberList.Count)
        {
            curChar = SquadManager.instance.memberList[btnIndex].GetComponent<PlayerCharater>();
        }
        else
        {
            Debug.Log("스쿼드매니저에 오브젝트가 없음");
        }
    }

    private void LateUpdate()
    {
        if (curChar != null)
        {
            textMeshProUGUI.text = curChar.charName;
            hpBar.fillAmount = curChar.curHp / curChar.maxHp;
            skillBar.fillAmount = curChar.curentSkillTime / curChar.maxSkillTime;
        }
    }

    private void UseSkill()
    {

    }
}
