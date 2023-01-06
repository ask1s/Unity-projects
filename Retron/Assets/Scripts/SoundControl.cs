using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    [SerializeField] Image soundOff;

    private bool muted = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        }
        else
            Load();

        UpdateIcon();
        AudioListener.pause = muted;
    }
    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else if (muted == true)
        {
            muted = false;
            AudioListener.pause = false;
        }
        Save();
        UpdateIcon();
    }

    private void UpdateIcon()
    {
        if (muted == false)
        {
            soundOff.enabled = false;
        }
        if (muted == true)
        {
            soundOff.enabled = true;
        }
    }
    private void Load()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }
    private void Save()
    {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}