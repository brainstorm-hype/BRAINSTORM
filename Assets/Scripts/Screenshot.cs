using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenshot : MonoBehaviour
{
    private string timeNow = DateTime.Now.ToString("ddMMyyyy-Hmm");
    private int counter;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            counter++;
            ScreenCapture.CaptureScreenshot($"screenshot{timeNow}_{counter}.png");
        }
    }
}
