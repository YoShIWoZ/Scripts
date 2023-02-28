using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioNumberTracker : MonoBehaviour
{
    public AudioClip defaultAudioClip;
    public AudioClip powerReturnedAudioClip;
    public AudioClip secretTapeClip;

    public GameObject RadioOn;
    private AudioSource AudioController;

    private void Start()
    {
        AudioController = RadioOn.GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GlobalVariables.dayNumber != 3)
        {
            if (!GlobalVariables.powerReturned)
            {
                if (AudioController.clip != defaultAudioClip)
                    AudioController.clip = defaultAudioClip;
            }
            else
            {
                if (AudioController != powerReturnedAudioClip)
                    AudioController.clip = powerReturnedAudioClip;
            }
        }
        else
        {
            if (GlobalVariables.secretTapePlayed)
            {
                if (AudioController.clip != secretTapeClip)
                {
                    AudioController.clip = secretTapeClip;
                    AudioController.loop = false;
                }
            }
            else
            {
                if (AudioController.clip != defaultAudioClip)
                {
                    AudioController.clip = defaultAudioClip;
                    AudioController.loop = true;
                }
            }
        }
    }
}
