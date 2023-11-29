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
    { Name.plant, "�����" },
    { Name.slug, "����ī����" },
     };


    [HideInInspector] public Dictionary<int, DialogueSetting> talkData;
    [SerializeField] GameObject signal;

    void Awake()
    {
        if (name == Name.plant)
        {
            talkData = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting(npcNames[name], "�ʴ� �ĸ� ���� �� �� �ѷ��� �Դ�?", 0.02f) },
               { 1, new DialogueSetting(npcNames[name], "��? �ĸ��� �� �Դ´ٰ�?!", 0.02f) },
               { 2, new DialogueSetting(npcNames[name], "���� �� ��!! �װ� �Ĺ�ȭ�� �ر���!!!!", 0.02f) }
            };
        }
        else
        {
            talkData = new Dictionary<int, DialogueSetting>
            {
               { 0, new DialogueSetting(npcNames[name], "ó�� ���� �༮�̱���! �ݰ���", 1f) },
               { 1, new DialogueSetting(npcNames[name], "���� ���󿡼� ���� ������ ���ϴ� ������. ���ر���", 1f) },
               { 2, new DialogueSetting(npcNames[name], "������ ���󿡼� ���� ������ ���ϴ� �����̰� �Ƿ��� ��� ���̾�!", 1f)},
               { 3, new DialogueSetting(npcNames[name], "�ɽ��ϸ� �� ���� ��밡 �Ǿ��ٷ�?", 1f)}
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
