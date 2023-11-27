using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeManager : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;

    Animator anim;

    [HideInInspector] public Player player;


    public Customize_Avatar customize_Avatar;

    [SerializeField] GameObject error;

    [SerializeField] Image img;




    [SerializeField] Sprite rabbitSp;
    [SerializeField] AnimatorOverrideController rabbitAnim;
    [SerializeField] Sprite beeSp;
    [SerializeField] AnimatorOverrideController beeAnim;



    void Awake()
    {
        anim = GetComponent<Animator>();
        error.SetActive(false);

        customize_Avatar.customizeManager = this;


    }

    private void Start()
    {
        player.NameChange("끼토산야끼토"); // 기본이름

    }


    public void AvatarChange(int i) // 커마창 프사 이미지를 i 번으로 변경
    {
        switch (i)
        {
            case 0:
                img.sprite = rabbitSp;
                break;

            case 1:
                img.sprite = beeSp;
                break;
        }
    }




    public void CustomizeChange()
    {

        if (inputField.text == "" || inputField.text.Length < 2 || inputField.text.Length > 10)
        {
            error.SetActive(true);
            return;
        }

        player.NameChange(inputField.text);

        if (img.sprite == rabbitSp)
            player.anim.runtimeAnimatorController = rabbitAnim;
        else
            player.anim.runtimeAnimatorController = beeAnim;


        Close();

    }




    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

    public void Set()
    {
        error.SetActive(false);

        anim.SetTrigger("Set");
        anim.SetBool("isOpen", false);

    }

    public void Open()
    {
        error.SetActive(false);

        inputField.text = player.name;

        player.state = Player.State.Stop;
        anim.SetBool("isOpen", true);
    }

    public void Close()
    {
        error.SetActive(false);
        player.state = Player.State.Play;
        anim.SetBool("isOpen", false);

    }
}
