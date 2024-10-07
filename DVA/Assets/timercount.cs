using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timercount : MonoBehaviour
{
    public float timer; //制限時間
    public Text TimeCounter;
    public ColorChange colorchange;

    // private ColorChange startcall;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(colorchange.StartCall)
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
