using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoUI : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private TextMeshProUGUI GradeTxt;
    [SerializeField] private TextMeshProUGUI NameTxt;
    [SerializeField] private TextMeshProUGUI BirthdayTxt;
    [SerializeField] private TextMeshProUGUI GoldTxt;
    [SerializeField] private TextMeshProUGUI NumberTxt;

    [Header("State")]
    [SerializeField] private TextMeshProUGUI CharmTxt;
    [SerializeField] private TextMeshProUGUI KnowledgeTxt;
    [SerializeField] private TextMeshProUGUI StrengthTxt;

    private void Start()    //플레이어 정보를 가져와서 TextMesh에 적용
    {
        Player _player = GameManager.Instance.player;
        GradeTxt.text = "1st Grade";
        NameTxt.text = _player.personName;
        BirthdayTxt.text = "2006.04.15";
        GoldTxt.text = _player.money.ToString();
        NumberTxt.text = "010-XXXX-XXXX";

        CharmTxt.text = _player.charm.ToString();
        KnowledgeTxt.text = _player.knowledge.ToString();
        StrengthTxt.text = _player.strength.ToString();
    }
}
