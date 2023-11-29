using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera VirtualCamera;
    [SerializeField] Player playerPrefab;

    Player player;

    [HideInInspector] public Dialogue dialogue;
    public Customize customize;

    void Awake()
    {
        Init();
    }


    private void Init()
    {
        player = Instantiate(playerPrefab);
        player.gameManager = this;

        VirtualCamera.m_Follow = player.transform;
        VirtualCamera.m_LookAt = player.transform;

        customize.player = player;

        dialogue.gameManager = this;

    }
    public void PlayerState(Player.State stat)
    {
        player.state = stat;
    }


}


