using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class Localization : MonoBehaviour
{
    private int ID;
    private bool firstLog = true;
    private void Start()
    {
        ID = PlayerPrefs.GetInt("LocaleKey", 0);
        ChangeLanguage();
    }
    private bool active = false;
    public void ChangeLanguage()
    {
        if (firstLog == true)
            firstLog = false;
        else if (firstLog == false)
            ID++;
        if (active == true)
            return;
        if (ID > 1)
            ID = 0;
        StartCoroutine(SetLocale(ID));
    }


    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];
        PlayerPrefs.SetInt("LocaleKey", _localeID);
        active = false;
    }
}
