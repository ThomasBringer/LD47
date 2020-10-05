using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollison : MonoBehaviour
{
    Player player;

    void Awake()
    {
        player = GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        player.Collision();
    }
}