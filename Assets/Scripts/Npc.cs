using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Npc : MonoBehaviour
{

    enum Name
    {
        slug,
        plant
    }
    [SerializeField] Name name;


    Dictionary<Name, string> npcNames = new Dictionary<Name, string>
    {
    { Name.plant, "충식이" },
    { Name.slug, "에스카르고" },
     };


    [HideInInspector] public Dictionary<int, DialogueSetting> talkData;
    [SerializeField] GameObject signal;

    void Awake()
    {
        if (name == Name.plant)
        {
            talkData = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting(npcNames[name], "너는 파리 먹을 때 뭘 뿌려서 먹니?", 0.02f) },
               { 1, new DialogueSetting(npcNames[name], "뭐? 파리를 안 먹는다고?!", 0.02f) },
               { 2, new DialogueSetting(npcNames[name], "말도 안 돼!! 그건 식문화의 붕괴라구!!!!", 0.02f) }
            };
        }
        else
        {
            talkData = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting(npcNames[name], "처음 보는 녀석이구나! 반가워", 1f) },
               { 1, new DialogueSetting(npcNames[name], "나는 세상에서 가장 빠르게 말하는 달팽이. 달팽구야", 1f) },
               { 2, new DialogueSetting(npcNames[name], "지금은 세상에서 가장 느리게 말하는 달팽이가 되려고 노력 중이야!", 1f)},
               { 3, new DialogueSetting(npcNames[name], "심심하면 내 연습 상대가 되어줄래?", 1f)}
            };


        }



    }
    private void OnEnable()
    {
        Idle();
    }

    public void Idle()
    {
        signal.SetActive(true);
    }

    public void Talk()
    {
        signal.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Idle();
    }
}
