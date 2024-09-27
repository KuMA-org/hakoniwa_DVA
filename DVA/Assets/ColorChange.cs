using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    GameObject[] button;
    public int number;

    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.FindGameObjectsWithTag("sphere");
        StartCoroutine("ChangetheColor1");
        // Debug.Log(button.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangetheColor1 () //はじめの段階，1つボタンの色を変える
    {
        number = Random.Range(0,button.Length); //すべてのボタンからランダムで一つ選択
        button[number].GetComponent<Renderer>().material.color = Color.red; //赤に変更
        yield return new WaitForSeconds(2);
    }
}
