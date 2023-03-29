using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    private static int score;
    public UnityEvent startGameEvent = new UnityEvent();
    public static UnityEvent sword_cut_event = new UnityEvent();
    public GameSpawner spawner;
    public AudioSource cut_sound;
    public AudioSource bgm;

    // Start is called before the first frame update
    void Start()
    {
        sword_cut_event.AddListener(PlayCutSound);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        spawner.gameObject.SetActive(true);
        bgm.Play();
    }

    public static void AddScore(int points)
    {
        score += points;
    }

    public static int GetScore()
    {
        return score;
    }

    public void PlayCutSound()
    {
        cut_sound.Play();
    }
}
