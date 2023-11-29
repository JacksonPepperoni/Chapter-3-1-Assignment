using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customize : MonoBehaviour
{
    [SerializeField] TMP_InputField inputField;
    [SerializeField] GameObject error;

    Animator anim;

    [HideInInspector] public Player player;

    public Customize_Avatar customize_Avatar;


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

    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■


    public void AvatarChange(int i) // 커마창 프사 이미지를 i 번으로 변경
    {
        img.sprite = (i == 0) ? rabbitSp : beeSp;
        player.anim.runtimeAnimatorController = (i == 0) ? rabbitAnim : beeAnim;

    }

    public void CustomizeChange()
    {

        if (inputField.text == "" || inputField.text.Length < 2 || inputField.text.Length > 10)
        {
            error.SetActive(true);
            return;
        }

        player.NameChange(inputField.text);
     
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

        inputField.text = player.name;

        error.SetActive(false);
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
