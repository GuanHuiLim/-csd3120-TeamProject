using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

public class GameController : MonoBehaviour
{
    private static int score;
    public UnityEvent startGameEvent = new UnityEvent();
    public static UnityEvent sword_cut_event = new UnityEvent();
    public GameSpawner spawner;
    public AudioSource cut_sound;
    public AudioSource bgm;
    public GameObject bladeModePostProcessing;
    private static bool bladeModeActivated = false;
    private static float slowdownFactor = 0.2f;
    //controls
    public static bool leftTriggerDown = false;
    public static bool rightTriggerDown = false;

    //audio
    public AudioMixer mixer;
    public string audioGroupName = "BladeMode";
    private AudioMixerGroup defaultAudioGroup;
    private AudioMixerGroup bladeModeAudioGroup;
    private float initial_bgm_volume;
    //time
    float initial_fixedDeltaTime;

    void Awake()
    {
        // Make a copy of the fixedDeltaTime
        initial_fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the default and blade mode snapshots
        defaultAudioGroup = mixer.FindMatchingGroups("Master")[0];
        bladeModeAudioGroup = mixer.FindMatchingGroups(audioGroupName)[0];
        sword_cut_event.AddListener(PlayCutSound);
    }

    // Update is called once per frame
    void Update()
    {
        if (leftTriggerDown && rightTriggerDown && bladeModeActivated == false)
        {
            bladeModeActivated = true;
            // Activate blade mode
            Time.timeScale = slowdownFactor;
            Time.fixedDeltaTime = Time.timeScale * initial_fixedDeltaTime;
            bladeModePostProcessing.SetActive(true);
            // Switch to the blade mode audio mixer group
            bgm.outputAudioMixerGroup = bladeModeAudioGroup;
            initial_bgm_volume = bgm.volume;
            bgm.volume = bgm.volume * 0.5f;
            Debug.Log("Blade Mode Activated");
        }
        else if (leftTriggerDown == false && rightTriggerDown == false && bladeModeActivated)
        {
            bladeModeActivated = false;
            // Deactivate blade mode
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = initial_fixedDeltaTime;
            if (bladeModePostProcessing.activeInHierarchy == true)
            {
                bladeModePostProcessing.SetActive(false);
            }
            // Switch back to the default audio mixer group
            bgm.outputAudioMixerGroup = defaultAudioGroup;
            bgm.volume = initial_bgm_volume;
            Debug.Log("Blade Mode De-Activated");

        }

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

    public static void OnLeftTrigger(bool activated)
    {
        leftTriggerDown = activated;
        if (activated)
            Debug.Log("LeftTrigger Activated");
        else
            Debug.Log("LeftTrigger De-Activated");

    }

    public static void OnRightTrigger(bool activated)
    {
        rightTriggerDown = activated;
        if (activated)
            Debug.Log("RightTrigger Activated");
        else
            Debug.Log("RightTrigger De-Activated");
    }

    public static void OnLeftThumb(bool activated)
    {
        if (activated)
            Debug.Log("LeftThumb Activated");
        else
            Debug.Log("LeftThumb De-Activated");

    }

    public static void OnRightThumb(bool activated)
    {
        if (activated)
            Debug.Log("RightThumb Activated");
        else
            Debug.Log("RightThumb De-Activated");
    }

    public static void OnMove()
    {
        Debug.Log("Moving");
    }
}
