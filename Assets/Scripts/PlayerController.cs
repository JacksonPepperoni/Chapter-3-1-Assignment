using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    private float speed = 6;

    private Camera _camera;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    private Animator anim;
    private int hashIsWalk = Animator.StringToHash("isWalk");
    private int hashIsDuck = Animator.StringToHash("isDuck");



    private Vector2 moveInput;
    private Vector2 mousePos;


    private GameObject meetObj; // 접촉중인거


    private void Awake()
    {
        _camera = Camera.main;
        anim = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = anim.gameObject.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        if (anim.GetBool(hashIsDuck)) return;


        rigid.MovePosition(rigid.position + moveInput); // 이동. 벨로시티 써야하는거아닌가 충돌처리하려면?
        anim.SetBool(hashIsWalk, (moveInput.x != 0 || moveInput.y != 0)); // 서있기 뛰기 애니 전환
        if (Mathf.Abs(moveInput.x) != 0) spriteRenderer.flipX = (moveInput.x < 0); // 가고있는 방향 보기

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        meetObj = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        meetObj = null;
    }


    public void OnMove(InputValue value) // wsad
    {
        moveInput = value.Get<Vector2>().normalized * speed * Time.deltaTime; // 이걸 바로 애니메이터 트리 파라미터로 보내버려도될듯 
    }

    public void OnLook(InputValue value) // 마우스 포지션이 바뀔때만 이라서 마우스 멈추고 ㅋ캐릭터가 마우스 옆으로 이동했을땐 좌우반전이 발동 안함.... 가만있을때만 마우스 위치봄
    {
        if (anim.GetBool(hashIsDuck)) return;

        mousePos = _camera.ScreenToWorldPoint(value.Get<Vector2>());
        spriteRenderer.flipX = (rigid.position.x > mousePos.x);
    }

    public void OnDuck(InputValue value) // shift키
    {
        anim.SetBool(hashIsDuck, value.isPressed);

    }



    public void OnTalk(InputValue value) // 스페이스바  
    {
        if (!value.isPressed || meetObj == null) return;  // 눌렀을때 땔때 호출은 다 되니까 걸러야함


        if (meetObj.CompareTag("NPC"))
        {
            Debug.Log(" npc와 대화!!");
        }
        // 내범위안에 들어온 상호작용가능 물체 실행
        // 이미 상호작용 중일땐 상호작용 다음 상황 진행 다 끝나면 종료

    }

}

/*

    public void OnLook(InputValue value)
    {
        newAim = value.Get<Vector2>();
        worldPos = _camera.ScreenToWorldPoint(newAim);
        newAim = (worldPos - (Vector2)transform.position).normalized;


        rotZ = Mathf.Atan2(newAim.y, newAim.x) * Mathf.Rad2Deg;
    }

*/


/*
 마우스 안쓰는거 백업
 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    [SerializeField] private Animator anim;

    private int hashWalk = Animator.StringToHash("Walk");
    private float speed = 5;

 
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    Vector2 moveInput;
    Vector2 nextVec;

    private void Awake()
    {
        spriteRenderer = anim.gameObject.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
    
        nextVec = moveInput.normalized * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + nextVec); // 벨로시티 써야하는거아닌가 충돌할라면

        if (Mathf.Abs(nextVec.x) != 0)
            spriteRenderer.flipX = (nextVec.x < 0);

        anim.SetBool(hashWalk, (nextVec.x != 0 || nextVec.y != 0));

    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); // 이걸 바로 애니메이터 트리 파라미터로 보내버려도될듯 
    }

}
 
 
 
 */