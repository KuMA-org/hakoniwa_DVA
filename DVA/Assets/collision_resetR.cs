using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision_resetR : MonoBehaviour
{
    public Score_cal reset;
    public AudioClip se_resetR;
    AudioSource audiosourceR;
    
    // Start is called before the first frame update
    void Start()
    {
        audiosourceR = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(reset.flag_reset == false)
        {
            reset.flag_reset = true;
            audiosourceR.PlayOneShot(se_resetR);
        }

       

    }
}
