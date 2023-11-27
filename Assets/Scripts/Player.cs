using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
public class Player : MonoBehaviour
{

    public enum State
    {
        Play,
        Stop,
        Talk,
    }
    [HideInInspector] public State state = State.Play;


    private float speed = 8;


    [HideInInspector]
    public GameManager gameManager;

    [HideInInspector]
    public string name = "";


    private Camera _camera;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;


    [HideInInspector]
    public Animator anim;
    private int hashIsWalk = Animator.StringToHash("isWalk");
    private int hashIsDuck = Animator.StringToHash("isDuck");



    private Vector2 moveInput;
    private Vector2 mousePos;

    private GameObject meetObj; // 접촉중인거

    [SerializeField] NameTag NameTagObj;



    private void Awake()
    {
        _camera = Camera.main;
        anim = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = anim.gameObject.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (anim.GetBool(hashIsDuck) || state == State.Stop || state == State.Talk) return;


        rigid.MovePosition(rigid.position + moveInput); // 이동. 벨로시티 써야하는거아닌가 충돌처리하려면?
        anim.SetBool(hashIsWalk, (moveInput.x != 0 || moveInput.y != 0)); // 서있기 뛰기 애니 전환
        if (Mathf.Abs(moveInput.x) != 0) spriteRenderer.flipX = (moveInput.x < 0); // 가고있는 방향 보기

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            meetObj = collision.gameObject;
            NameTagObj.gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        meetObj = null;
        NameTagObj.gameObject.SetActive(true);
    }



    public void NameChange(string newName)
    {
        name = newName;
        NameTagObj.Reload(newName);
    }



    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().normalized * speed * Time.deltaTime;
    }

    public void OnLook(InputValue value) // 마우스 포지션이 바뀔때만 이라서 마우스 멈추고 캐릭터가 마우스 옆으로 이동했을땐 좌우반전이 발동 안함.... 가만있을때만 마우스 위치봄
    {

        if (anim.GetBool(hashIsDuck) || state == State.Stop || state == State.Talk) return;

        mousePos = _camera.ScreenToWorldPoint(value.Get<Vector2>());
        spriteRenderer.flipX = (rigid.position.x > mousePos.x);
    }

    public void OnDuck(InputValue value)
    {
        if (state == State.Stop || state == State.Talk) return;

        anim.SetBool(hashIsDuck, value.isPressed);

    }

    public void OnTalk(InputValue value)
    {
        if (state == State.Stop) return;
        if (!value.isPressed || meetObj == null || anim.GetBool(hashIsDuck)) return;  // 눌렀을때 true 땔때 false 호출


        if (state == State.Talk)
        {
            gameManager.dialogue.Talk();
            return;
        }

        state = State.Talk;
        anim.SetBool(hashIsWalk, false);

        gameManager.dialogue.Open();

    }

}
