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


    private GameObject meetObj; // �������ΰ�


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


        rigid.MovePosition(rigid.position + moveInput); // �̵�. ���ν�Ƽ ����ϴ°žƴѰ� �浹ó���Ϸ���?
        anim.SetBool(hashIsWalk, (moveInput.x != 0 || moveInput.y != 0)); // ���ֱ� �ٱ� �ִ� ��ȯ
        if (Mathf.Abs(moveInput.x) != 0) spriteRenderer.flipX = (moveInput.x < 0); // �����ִ� ���� ����

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
        moveInput = value.Get<Vector2>().normalized * speed * Time.deltaTime; // �̰� �ٷ� �ִϸ����� Ʈ�� �Ķ���ͷ� �����������ɵ� 
    }

    public void OnLook(InputValue value) // ���콺 �������� �ٲ𶧸� �̶� ���콺 ���߰� ��ĳ���Ͱ� ���콺 ������ �̵������� �¿������ �ߵ� ����.... ������������ ���콺 ��ġ��
    {
        if (anim.GetBool(hashIsDuck)) return;

        mousePos = _camera.ScreenToWorldPoint(value.Get<Vector2>());
        spriteRenderer.flipX = (rigid.position.x > mousePos.x);
    }

    public void OnDuck(InputValue value) // shiftŰ
    {
        anim.SetBool(hashIsDuck, value.isPressed);

    }



    public void OnTalk(InputValue value) // �����̽���  
    {
        if (!value.isPressed || meetObj == null) return;  // �������� ���� ȣ���� �� �Ǵϱ� �ɷ�����


        if (meetObj.CompareTag("NPC"))
        {
            Debug.Log(" npc�� ��ȭ!!");
        }
        // �������ȿ� ���� ��ȣ�ۿ밡�� ��ü ����
        // �̹� ��ȣ�ۿ� ���϶� ��ȣ�ۿ� ���� ��Ȳ ���� �� ������ ����

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
 ���콺 �Ⱦ��°� ���
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
        rigid.MovePosition(rigid.position + nextVec); // ���ν�Ƽ ����ϴ°žƴѰ� �浹�Ҷ��

        if (Mathf.Abs(nextVec.x) != 0)
            spriteRenderer.flipX = (nextVec.x < 0);

        anim.SetBool(hashWalk, (nextVec.x != 0 || nextVec.y != 0));

    }


    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>(); // �̰� �ٷ� �ִϸ����� Ʈ�� �Ķ���ͷ� �����������ɵ� 
    }

}
 
 
 
 */