using UnityEngine;

public class Customize_Avatar : MonoBehaviour
{
    Animator anim;

    [HideInInspector]
   public CustomizeManager customizeManager;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    public void AvatarChoose(int i) // 아바타 고르는 버튼에 연결
    {
        customizeManager.AvatarChange(i);
        Close();
    }



    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■

    public void Set()
    {
        anim.SetTrigger("Set");
        anim.SetBool("isOpen", false);
    }

    public void Open()
    {
        anim.SetBool("isOpen", true);
    }


    public void Close()
    {
        anim.SetBool("isOpen", false);
    }
}
