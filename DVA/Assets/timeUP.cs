using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class timeUP : MonoBehaviour
{
    public float timer;
    public Text timerText;
    bool flag_timeup;

    // Start is called before the first frame update
    void Start()
    {
        timer = 5.0f;
        flag_timeup = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        timerText.text = timer.ToString("f1") + "秒";

        if(timer <= 0)
        {
            timerText.text = "タイムアップ";
            flag_timeup = true;
            StartCoroutine("Finish");

        }
    }
    IEnumerator Finish()
    {
        yield return new WaitForSeconds(3);
        timerText.text = "Owatta";
    }
}
