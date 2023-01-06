using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SecretCode : MonoBehaviour
{
    [SerializeField]
    private TMP_Text one;
    [SerializeField]
    private TMP_Text two;
    [SerializeField]
    private TMP_Text three;
    [SerializeField]
    private TMP_Text four;

    private int code;

    private bool once = false;

    private void Update()
    {
        if (!once)
        {
            MakeCode();
            once = true;
        }
    }

    private void MakeCode()
    {
        int q = Reverse(Convert.ToInt32(four.text))*1000;
        int w = Reverse(Convert.ToInt32(three.text))*100;
        int e = Reverse(Convert.ToInt32(two.text))*10;
        int r = Reverse(Convert.ToInt32(one.text));
        code = q + w + e + r;
    }

    int Reverse(int n)
    {
        if (n == 5)
            return 2;
        else if (n == 2)
            return 5;
        else if (n == 9)
            return 6;
        else if (n == 6)
            return 9;
        else
            return n;
    }
}
