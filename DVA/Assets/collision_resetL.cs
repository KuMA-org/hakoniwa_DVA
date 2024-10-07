using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_resetL : MonoBehaviour
{
    public Score_cal reset;
    public AudioClip se_resetL;
    AudioSource audiosourceL;
    // Start is called before the first frame update
    void Start()
    {
        audiosourceL = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (reset.flag_reset == false)
        {
            reset.flag_reset = true;
            audiosourceL.PlayOneShot(se_resetL);
        }
    }
}
