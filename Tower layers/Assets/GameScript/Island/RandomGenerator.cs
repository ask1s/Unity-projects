using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class RandomGenerator : MonoBehaviour
{
    public int min = 0;
    public int max = 9;

    private TextMeshPro txt;

    private int[] arr = new int[4];

    private void Awake()
    {
        txt = GetComponent<TextMeshPro>();
    }

    void Start()
    {
        Generate();
    }
    void Generate()
    {
        int rng = UnityEngine.Random.RandomRange(min, max);
        string str = Convert.ToString(rng);
        txt.SetText(str);
    }
}
