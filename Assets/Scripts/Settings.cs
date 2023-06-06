using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Settings : MonoBehaviour
{
    public Sprite closeImage;
    public Sprite settingsImage;
    public GameObject soundBtn;
    public GameObject vibrationsBtn;
    public AudioSource sounds;
    public static bool IsVibration = true;

    private void Start() => SettingsStartControl();

    private void SettingsStartControl()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            sounds.mute = PlayerPrefs.GetString("Sound").JsonDeserialize<bool>();
            soundBtn.transform.GetChild(0).gameObject.SetActive(sounds.mute);
        }

        if (!PlayerPrefs.HasKey("Vibration")) return;
        IsVibration = PlayerPrefs.GetString("Vibration").JsonDeserialize<bool>();
        vibrationsBtn.transform.GetChild(0).gameObject.SetActive(!IsVibration);
    }

    public void SettingsOpen()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetComponent<Image>().sprite = settingsImage;
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetComponent<Image>().sprite = closeImage;
        }
    }

    public void OnOffControl(string type)
    {
        switch (type)
        {
            case "Sound":
                soundBtn.transform.GetChild(0).gameObject.SetActive(!sounds.mute);
                sounds.mute = !sounds.mute;
                PlayerPrefs.SetString("Sound", sounds.mute.JsonSerialize());
                break;
            case "Vibration":
                vibrationsBtn.transform.GetChild(0).gameObject.SetActive(IsVibration);
                IsVibration = !IsVibration;
                PlayerPrefs.SetString("Vibration", IsVibration.JsonSerialize());
                break;
        }
    }
}