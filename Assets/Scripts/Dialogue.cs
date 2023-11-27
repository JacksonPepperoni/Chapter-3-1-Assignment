using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class Dialogue : MonoBehaviour
{

    [HideInInspector] public GameManager gameManager;

    private Animator anim;

    [SerializeField]
    private TMP_Text lineText;

    [SerializeField]
    private Animator cursorAnim;

    private bool isTyping = false;
    private bool isclickDelay; //Ŀ�� ������ �ִ� ���� �Է¾ȹް�
    private float clickDelay; // Ŀ�� �����¾ִ� ���� �޴¿�
    private int line;
    private WaitForSeconds typingTime = new WaitForSeconds(0.02f); // Ÿ���� �ӵ�
    [SerializeField] AnimationClip clickClip; // Ŀ�� �ִϱ��̹޴¿�


    private Dictionary<int, DialogueSetting> talkDic; // ��ȭ���� �޴� ������



    void Awake()
    {
        anim = GetComponent<Animator>();

        talkDic = new Dictionary<int, DialogueSetting>
        {
            { 0, new DialogueSetting("������", "�ȳ糪�� ������") },
            { 1, new DialogueSetting("������", "�氡�氡") },
            { 2, new DialogueSetting("������", "3333333333333333333333333333333333333333333") },
            { 3, new DialogueSetting("������", "44444") }
        };

        clickDelay = clickClip.length;

    }

    //�����������������������������������������������������������������������������������������������


    public void Talk() //  �������� �ѱ�� ��ư�� ����.
    {
        if (isclickDelay) return;


        if (isTyping)  //��� �帣���߿� Ŭ���ϸ� ��� �ٶ�
        {
            CancelInvoke();
            StopAllCoroutines();
            lineText.maxVisibleCharacters = 999;
            line += 1;

            isTyping = false;

            cursorAnim.gameObject.SetActive(true);

            return;
        }

        isclickDelay = true; // Ŀ��Ŭ�� �ִ� �����߿� ���� ����̾��� bool �����ۿ�...
        cursorAnim.SetTrigger("Click");
        Invoke("TalkEvent", clickDelay); // Ŀ�� Ŭ���ִ� �����Ŀ� ����

    }


    public void TalkEvent()
    {
        isclickDelay = false;

        if (!talkDic.ContainsKey(line)) // ����ųʸ��� �������� ������
        {
            Close();
            return;
        }

        cursorAnim.gameObject.SetActive(false);

        lineText.text = talkDic[line].line;
        StartCoroutine(TextVisible());

    }


    private IEnumerator TextVisible()
    {
        isTyping = true;

        lineText.ForceMeshUpdate();

        lineText.maxVisibleCharacters = 0; // ù�پȳ����°� �ð������ ��ü�� �ٳ����� ����Ǽ� ����
        yield return new WaitForSeconds(0.02f); // ���â ���� �ִ϶� ���ÿ� ��� �ֱⰡ �ȵǴ��� ù��簡 ��� �ȶ��� �̰��ִϱ� �����.  new WaitForEndOfFrame(); ���� �ȵǳ� ����   yield return null; ���ȵ�


        int totalVisibleCharacters = lineText.textInfo.characterCount;
        int counter = 0;


        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);   // a % b   a�� b�� ������ ���� ���� ��������
            lineText.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters) // �۾� �� ��������
            {

                line += 1;
                isTyping = false;
                cursorAnim.gameObject.SetActive(true);
                break;
            }

            counter += 1;
            yield return typingTime;
        }
    }




    //������������������������������������������������������������������

    public void Set()
    {
        anim.SetTrigger("Set");
        anim.SetBool("isOpen", false);

    }
    public void Open()
    {
        cursorAnim.gameObject.SetActive(false);
        isclickDelay = false;
        isTyping = false;
        lineText.text = "";
        line = 0;

        anim.SetBool("isOpen", true);

        Talk();

    }

    public void Close()
    {
        StopAllCoroutines();
        CancelInvoke();
        isTyping = false;

        gameManager.PlayerState(Player.State.Play);


        anim.SetBool("isOpen", false);

    }



}