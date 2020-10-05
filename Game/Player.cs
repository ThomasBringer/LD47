using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 1;
    //[SerializeField] float forwardStep = 2;
    //[SerializeField] float worldWidth = 8;
    //float semiWorldWidth;

    bool playing = false;
    public bool Playing
    {
        //get { return playing; }
        set
        {
            playing = value;
            if (value)
            {
                canScore = true;
            }
            if (!value)
            {
                Pos = Vector2.zero;
                firstTime = true;
                //cam.position = camPos;
                anim.SetTrigger("Restart");
            }
        }
    }
    bool movingVertically = false;

    Animator anim;

    Vector2 pos;
    Vector2 Pos
    {
        get { return pos; }
        set
        {
            //pos = new Vector2((value.x + semiWorldWidth) % worldWidth - semiWorldWidth, value.y);
            pos = new Vector2((value.x + Spawner.spawner.semiWorldWidth) % Spawner.spawner.worldWidth - Spawner.spawner.semiWorldWidth, value.y);
            transform.position = pos;
            //pos = Vector2.up * value.y + Vector2.left * (value.x /*% worldWidth*/);
            //transform.position = pos;
        }
    }

    public static Player player;

    //[SerializeField] Transform cam;
    //Vector3 camPos;

    void Awake()
    {
        player = this;
        anim = GetComponent<Animator>();
        //camPos = cam.position;
        //semiWorldWidth = worldWidth * .5f;
    }

    bool firstTime = true;

    void Update()
    {
        if (!playing)
        {
            if (Input.GetButtonDown("Forward")) { SoundManager.sm.Play1(); Playing = true; Score = 0; }
            return;
        }

        if (!movingVertically) Pos += Vector2.right * speed * Time.deltaTime * ScoreLog;
        //transform.Translate(Vector2.right * speed * Time.deltaTime);

        if (Input.GetButtonDown("Forward"))
        {
            SoundManager.sm.Play1();
            movingVertically = true;
            anim.SetTrigger("Forward");
            if (!firstTime) Pos += Vector2.up * Spawner.spawner.forwardStep;//forwardStep;
            firstTime = false;
        }
    }

    public float ScoreLog
    {
        get
        {
            float log = Mathf.Log(score);
            return log >= 1 ? log : 1;
        }
    }

    public void EndForward()
    {
        movingVertically = false;
        Spawner.spawner.Spawn();
        if (playing) Score++;
        //anim.SetTrigger("EndMove");
    }

    public void Collision()
    {
        SoundManager.sm.Play2();
        BlackScreen.blackScreen.GameOver();
        canScore = false;
    }

    bool canScore = false;

    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highscoreText;

    int score = 0;
    int Score
    {
        get { return score; }
        set
        {
            if (canScore)
            {
                score = value;
                if (value > highscore) Highscore = value;
                scoreText.text = value + "";
            }
        }
    }

    int highscore = 0;
    int Highscore
    {
        //get { return highscore; }
        set
        {
            highscore = value;
            highscoreText.text = value + "";
        }
    }
}