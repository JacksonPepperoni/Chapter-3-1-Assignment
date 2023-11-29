using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{

    [HideInInspector] public GameManager gameManager;

    private Animator anim;


    [SerializeField] private TMP_Text nameText; // ��� �̸�
    [SerializeField] private Image nameImg; // ����̸�ĭ


    [SerializeField] private TMP_Text lineText;
    [SerializeField] private Animator cursorAnim;

    private bool isTyping = false;
    private bool isclickDelay; //Ŀ�� ������ �ִ� ���� �Է¾ȹް�
    private float clickDelay; // Ŀ�� �����¾ִ� ���� �޴¿�
    private int line;
    private WaitForSeconds typingSpeed = new WaitForSeconds(0.02f); // Ÿ���� �ӵ�
    [SerializeField] AnimationClip clickClip; // Ŀ�� �ִϱ��̹޴¿�


    [HideInInspector] public Dictionary<int, DialogueSetting> talkDic; // ��ȭ���� �޴� ������



    void Awake()
    {
        anim = GetComponent<Animator>();
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

        isclickDelay = true; // Ŀ��Ŭ�� �ִ� ������
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

        if (nameText.text != talkDic[line].name)
        {
            nameText.text = talkDic[line].name;
            nameImg.rectTransform.sizeDelta = new Vector2(nameText.preferredWidth + 120f, nameImg.rectTransform.sizeDelta.y);
        }


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
            yield return typingSpeed;
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

        nameText.text = talkDic[0].name;
        nameImg.rectTransform.sizeDelta = new Vector2(nameText.preferredWidth + 150f, nameImg.rectTransform.sizeDelta.y);

        typingSpeed = new WaitForSeconds(talkDic[0].speechSpeed);

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