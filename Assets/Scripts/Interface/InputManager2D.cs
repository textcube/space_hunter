using UnityEngine;
using System.Collections;

public class InputManager2D : MonoBehaviour
{
    public GameObject player;
    public PlayerControl2D playerScript;
    public bool padEnable = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerControl2D>();
    }

    void Update()
    {
        if (padEnable && Joystick.Instance != null && Joystick.Instance.isPress)
            playerScript.MoveToTarget();
    }
}