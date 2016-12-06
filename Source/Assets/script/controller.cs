using UnityEngine;
using System.Collections;
using System;

public class controller : MonoBehaviour
{
    private int score_sum;
    public int score;
    public int score_2nd;
    public int game_mode;
    private int dual;
    private DateTime start_time;
    private TimeSpan duration;
    private DateTime dual_start_time;
    private TimeSpan dual_duration;
    private TimeSpan dual_finished;
    private GameObject btn1;
    private GameObject btn2;
    private GameObject btnta;
    private GameObject player_1;
    private GameObject player_2;
    private player player_1_com;
    private player2 player_2_com;
    private AudioSource[] audio;
    public AudioClip bgm;
    public AudioClip bgm_win;
    public AudioClip bgm_lose;
    private bool audio_oneshot;

    // Use this for initialization
    void Start()
    {
        audio = GetComponents<AudioSource>();
        score_sum = 17 * 100;
        score = 0;
        score_2nd = 0;
        game_mode = 0;
        dual = 0;
        dual_finished = new TimeSpan(0, 0, 30);
        audio_oneshot = false;

        btn1 = GameObject.Find("Button1");
        btn2 = GameObject.Find("Button2");
        btnta = GameObject.Find("ButtonTryAgain");

        btnta.SetActive(false);

        player_1 = GameObject.Find("player");
        player_1_com = player_1.GetComponent<player>();
        player_2 = GameObject.Find("player2");
        player_2_com = player_2.GetComponent<player2>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game_mode == 1 || game_mode == 2)
        {
            duration = System.DateTime.Now - start_time;
        }
        OnDie();    
        OnDual();
    }

    public void ScorePlus()
    {
        score += 100;
        Debug.Log("score += 100");
    }

    public void Score2Plus()
    {
        score_2nd += 100;
        Debug.Log("score_2nd += 100");
    }
    
    void OnGUI()
    {
        if (game_mode == 1)
        {
            GUI.Box(new Rect(0, 0, 120f, 80f), "");
            GUI.Label(new Rect(0, 0, 120f, 40f), "  Current time is: \n" + "  " + duration/*ToLongTimeString()*/);
            GUI.Label(new Rect(0, 40, 120f, 40f), "  Current score is: \n" + "  " + (score));
        }
        else if (game_mode == 2)
        {
            GUI.Box(new Rect(0, 0, 120f, 80f), "");
            GUI.Label(new Rect(0, 0, 120f, 40f), "  Current time is: \n" + "  " + duration/*ToLongTimeString()*/);
            GUI.Label(new Rect(0, 40, 120f, 40f), "  Current score is: \n" + "  " + (score));

            GUI.Box(new Rect(Screen.width - 120, 0, 120f, 80f), "");
            GUI.Label(new Rect(Screen.width - 120, 0, 120f, 40f), "  Current time is: \n" + "  " + duration/*ToLongTimeString()*/);
            GUI.Label(new Rect(Screen.width - 120, 40, 120f, 40f), "  Current score is: \n" + "  " + (score_2nd));

            if (dual == 1)
            {
                //-----------------------------------
                GUI.Box(new Rect(Screen.width / 2 - 60, 0, 200f, 80f), "Now, Red has 30 seconds\n to defeat Green");
                GUI.Label(new Rect(Screen.width / 2 - 60, 40, 200f, 40f), "  Current time spent is: \n" + "  " + dual_duration/*ToLongTimeString()*/);
            }
            else if (dual == 2)
            {
                //-----------------------------------
                GUI.Box(new Rect(Screen.width / 2 - 60, 0, 200f, 80f), "Now, Green has 30 seconds\n to defeat Red");
                GUI.Label(new Rect(Screen.width / 2 - 60, 40, 200f, 40f), "  Current time is: \n" + "  " + dual_duration/*ToLongTimeString()*/);
            }
        }
        else if (game_mode == -1)
        {
            if (!audio_oneshot)
            {
                audio_oneshot = true;
                audio[1].clip = bgm_win;
                audio[1].PlayOneShot(bgm_win);
            }

            GUI.Box(new Rect(0, 0, 120f, 80f), "");
            GUI.Label(new Rect(0, 0, 120f, 40f), "  Current time is: \n" + "  " + duration/*ToLongTimeString()*/);
            GUI.Label(new Rect(0, 40, 120f, 40f), "  Current score is: \n" + "  " + (score));

            GUI.Box(new Rect(Screen.width - 120, 0, 120f, 80f), "");
            GUI.Label(new Rect(Screen.width - 120, 0, 120f, 40f), "  Current time is: \n" + "  " + duration/*ToLongTimeString()*/);
            GUI.Label(new Rect(Screen.width - 120, 40, 120f, 40f), "  Current score is: \n" + "  " + (score_2nd));

            GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 40, 120f, 22f), "Green Beats Red");
            btnta.SetActive(true);
        }
        else if (game_mode == -2)
        {
            if (!audio_oneshot)
            {
                audio_oneshot = true;
                audio[1].clip = bgm_win;
                audio[1].PlayOneShot(bgm_win);
            }

            GUI.Box(new Rect(0, 0, 120f, 80f), "");
            GUI.Label(new Rect(0, 0, 120f, 40f), "  Current time is: \n" + "  " + duration/*ToLongTimeString()*/);
            GUI.Label(new Rect(0, 40, 120f, 40f), "  Current score is: \n" + "  " + (score));

            GUI.Box(new Rect(Screen.width - 120, 0, 120f, 80f), "");
            GUI.Label(new Rect(Screen.width - 120, 0, 120f, 40f), "  Current time is: \n" + "  " + duration/*ToLongTimeString()*/);
            GUI.Label(new Rect(Screen.width - 120, 40, 120f, 40f), "  Current score is: \n" + "  " + (score_2nd));

            GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 40, 120f, 22f), "Red Beats Green");
            btnta.SetActive(true);
        }
        else if (game_mode == -3)//lose
        {
            if (!audio_oneshot)
            {
                audio_oneshot = true;
                audio[1].clip = bgm_lose;
                audio[1].PlayOneShot(bgm_lose);
            }

            GUI.Box(new Rect(0, 0, 120f, 80f), "");
            GUI.Label(new Rect(0, 0, 120f, 40f), "  Current time is: \n" + "  " + duration/*ToLongTimeString()*/);
            GUI.Label(new Rect(0, 40, 120f, 40f), "  Current score is: \n" + "  " + (score));

            GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 40, 120f, 22f), "You Died");
            btnta.SetActive(true);
        }
        else if (game_mode == -4)
        {
            if (!audio_oneshot)
            {
                audio_oneshot = true;
                audio[1].clip = bgm_win;
                audio[1].PlayOneShot(bgm_win);
            }

            GUI.Box(new Rect(0, 0, 120f, 80f), "");
            GUI.Label(new Rect(0, 0, 120f, 40f), "  Current time is: \n" + "  " + duration/*ToLongTimeString()*/);
            GUI.Label(new Rect(0, 40, 120f, 40f), "  Current score is: \n" + "  " + (score));

            GUI.Box(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 40, 120f, 22f), "You Beat the Game!");
            btnta.SetActive(true);
        }
    }

    public void SetGameMode1()
    {
        audio[0].clip = bgm;
        audio[0].loop = true;
        audio[0].Play();

        game_mode = 1;
        player_1_com.alive = true;
        player_2_com.alive = true;
        start_time = DateTime.Now;
        btn1.SetActive(false);
        btn2.SetActive(false);
    }

    public void SetGameMode2()
    {
        audio[0].clip = bgm;
        audio[0].loop = true;
        audio[0].Play();

        game_mode = 2;
        player_1_com.alive = true;
        player_2_com.alive = true;
        start_time = DateTime.Now;
        btn1.SetActive(false);
        btn2.SetActive(false);
    }

    public void OnDie()
    {
        if (game_mode == 1)
        {
            if (player_1_com.alive == false || player_2_com.alive == false)
            {
                game_mode = -3;
            }
            else if (score + score_2nd == 1700)
            {
                game_mode = -4;
            }
        }
        if (game_mode == 2)
        {
            if (dual == 1 && dual_duration >= dual_finished)
            {
                player_1_com.die();
            }
            else if (dual == 2 && dual_duration >= dual_finished)
            {
                player_2_com.die();
            }

            if (player_1_com.alive == false)
            {
                game_mode = -1;
            }
            else if (player_2_com.alive == false)
            {
                game_mode = -2;
            }
        }
    }

    public void TryAgain()
    {
        Application.LoadLevel("scene1");
    }

    public void OnDual()
    {
        if (score > score_sum / 2 && dual != 1)
        {
            dual = 1;
            dual_start_time = DateTime.Now;  
        }
        else if (score_2nd > score_sum / 2 && dual != 2)
        {
            dual = 2;
            dual_start_time = DateTime.Now;
        }
        if (dual == 1 || dual == 2)
        {
            dual_duration = DateTime.Now - dual_start_time;
        }
    }
}