using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{

    [HideInInspector] public GameManager gameManager;

    private Animator anim;


    [SerializeField] private TMP_Text nameText; // 상대 이름
    [SerializeField] private Image nameImg; // 상대이름칸


    [SerializeField] private TMP_Text lineText;
    [SerializeField] private Animator cursorAnim;

    private bool isTyping = false;
    private bool isclickDelay; //커서 눌리는 애니 동안 입력안받게
    private float clickDelay; // 커서 눌리는애니 길이 받는용
    private int line;
    private WaitForSeconds typingSpeed = new WaitForSeconds(0.02f); // 타이핑 속도
    [SerializeField] AnimationClip clickClip; // 커서 애니길이받는용


    [HideInInspector] public Dictionary<int, DialogueSetting> talkDic; // 대화정보 받는 껍데기



    void Awake()
    {
        anim = GetComponent<Animator>();
        clickDelay = clickClip.length;

    }

    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■


    public void Talk() //  다음대사로 넘기는 버튼에 연결.
    {
        if (isclickDelay) return;


        if (isTyping)  //대사 흐르는중에 클릭하면 대사 다뜸
        {
            CancelInvoke();
            StopAllCoroutines();
            lineText.maxVisibleCharacters = 999;
            line += 1;

            isTyping = false;

            cursorAnim.gameObject.SetActive(true);

            return;
        }

        isclickDelay = true; // 커서클릭 애니 진행중
        cursorAnim.SetTrigger("Click");
        Invoke("TalkEvent", clickDelay); // 커서 클릭애니 끝난후에 실행

    }


    public void TalkEvent()
    {
        isclickDelay = false;

        if (!talkDic.ContainsKey(line)) // 대사딕셔너리에 다음줄이 없으면
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

        lineText.maxVisibleCharacters = 0; // 첫줄안나오는거 시간줬더니 전체글 다나오고 재생되서 넣음
        yield return new WaitForSeconds(0.02f); // 대사창 오픈 애니랑 동시에 대사 넣기가 안되는지 첫대사가 계속 안떠서 이거주니까 적용됨.  new WaitForEndOfFrame(); 으론 안되네 뭐냐   yield return null; 도안됨


        int totalVisibleCharacters = lineText.textInfo.characterCount;
        int counter = 0;


        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);   // a % b   a를 b로 나누고 나온 값의 나머지값
            lineText.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters) // 글씨 다 나왔으면
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




    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

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