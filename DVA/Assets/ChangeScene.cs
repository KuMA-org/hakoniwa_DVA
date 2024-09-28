using OculusSampleFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //public void change_button()
    //{
    //    SceneManager.LoadScene("DVA");
    //}

    [SerializeField] private ButtonController _buttonController;

    void Update()
    {
        // Button��ActionZone�ɐG��Ă��鎞
        _buttonController.ActionZoneEvent += args => {
            if (args.InteractionT == InteractionType.Enter)
            {
                SceneManager.LoadScene("DVA");
            }
        };
    }
}