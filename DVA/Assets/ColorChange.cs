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
        number = Random.Range(0,4);
        button[number].GetComponent<Renderer>().material.color = Color.red;
        Debug.Log(button[0]);
        Debug.Log(number);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
