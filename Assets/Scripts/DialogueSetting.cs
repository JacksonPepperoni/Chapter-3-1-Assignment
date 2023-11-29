public class DialogueSetting
{
    public string name;
    public string line;
    public float speechSpeed;

    // 기본 말속도 (0.02f);

    public DialogueSetting(string naa, string daaa, float speed)
    {
        name = naa;
        line = daaa;
        speechSpeed = speed;
    }
}
