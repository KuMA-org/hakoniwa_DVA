using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



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

        timerText.text = timer.ToString("f1") + "�b";

        if(timer <= 0)
        {
            timerText.text = "�^�C���A�b�v";
            flag_timeup = true;
            StartCoroutine("Finish");

        }
    }



    IEnumerator Finish()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("finish");
    }
}
