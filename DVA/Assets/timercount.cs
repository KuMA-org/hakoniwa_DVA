using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timercount : MonoBehaviour
{
    public float timer = 60.0f; 
    public Text TimeCounter;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        TimeCounter.text = "残り時間 ："+timer.ToString("n2"); //残り時間の表示

        if(timer <= 0.0f)
        {
            TimeCounter.text = "残り時間 ： 0.00"; //0で止めてマイナスにいかないように
        }
    }
}
