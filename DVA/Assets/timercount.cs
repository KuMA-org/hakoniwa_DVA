using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timercount : MonoBehaviour
{
    private float timer = 60.0f; 
    public Text TimeCounter;
    public ColorChange StartCall;

    // private ColorChange startcall;
    bool startcall;


    // Start is called before the first frame update
    void Start()
    {
        // this.startcall = FindObjectOfType<ColorChange>();
    }

    // Update is called once per frame
    void Update()
    {
        startcall = StartCall.Called;
        while(StartCall)
        {
            timer -= Time.deltaTime;
        }
        TimeCounter.text = "残り時間 : "+timer.ToString("n2");

        if(timer <= 0.0f)
        {
            TimeCounter.text = "残り時間 ： 0.00"; //0で止めてマイナスにいかないように
        }
    }
}
