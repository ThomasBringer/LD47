using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    Animator anim;
    public static BlackScreen blackScreen;

    void Awake()
    {
        blackScreen = this;
        anim = GetComponent<Animator>();
    }

    public void GameOver()
    {
        anim.SetTrigger("Black");
    }

    public void Black()
    {
        Spawner.spawner.Restart();
        Player.player.Playing = false;
    }
}