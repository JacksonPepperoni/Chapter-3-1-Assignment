using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NameTag : MonoBehaviour
{
    public TMP_Text txt;
    [SerializeField] Image img;

    void Awake()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    public void SizeChange(string name) // 이름칸 사이즈변경
    {
        txt.text = name;
        img.rectTransform.sizeDelta = new Vector2(txt.preferredWidth + 0.25f, txt.preferredHeight + 0.15f);
    }

}
