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

    private GameObject meetObj; // �������ΰ�

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


        rigid.MovePosition(rigid.position + moveInput); // �̵�. ���ν�Ƽ ����ϴ°žƴѰ� �浹ó���Ϸ���?
        anim.SetBool(hashIsWalk, (moveInput.x != 0 || moveInput.y != 0)); // ���ֱ� �ٱ� �ִ� ��ȯ
        if (Mathf.Abs(moveInput.x) != 0) spriteRenderer.flipX = (moveInput.x < 0); // �����ִ� ���� ����

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

    public void OnLook(InputValue value) // ���콺 �������� �ٲ𶧸� �̶� ���콺 ���߰� ĳ���Ͱ� ���콺 ������ �̵������� �¿������ �ߵ� ����.... ������������ ���콺 ��ġ��
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
        if (!value.isPressed || meetObj == null || anim.GetBool(hashIsDuck)) return;  // �������� true ���� false ȣ��


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
