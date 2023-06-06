using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEditor.Experimental.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public Image whiteImage;
    private bool effectControl;
    public List<GameObject> setActiveObjects;
    public GameObject finishPanel, rewardedIcon, adsBtn, noThanksBtn;
    public Text rewardedText;
    public Slider levelBarSlider;

    
    private void Awake() => Instance = this;

    private void Update()
    {
        levelBarSlider.value =
            PlayerController.Player.transform.position.z / Finish.FinishLine.transform.position.z;
    }

    private IEnumerator WhiteEffect(bool isEffect = false)
    {
        whiteImage.gameObject.SetActive(true);
        if (isEffect)
        {
            effectControl = false;
        }

        while (!effectControl)
        {
            yield return new WaitForSeconds(0.001f);
            if (isEffect)
            {
                whiteImage.color -= new Color(0, 0, 0, 0.1f);
            }
            else
            {
                whiteImage.color += new Color(0, 0, 0, 0.1f);
            }

            if (whiteImage.color != new Color(1, 1, 1, 1)) continue;
            effectControl = true;
            if (!isEffect)
            {
                StartCoroutine(WhiteEffect(true));
            }
        }
    }

    public void WhiteEffectCall() => StartCoroutine(WhiteEffect());

    public IEnumerator FinishUpdate()
    {
        finishPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        rewardedIcon.SetActive(true);
        rewardedText.text = "+" + 100;
        GameManager.Instance.CoinCalculator(100);
        yield return new WaitForSeconds(0.8f);
        adsBtn.SetActive(true);
        yield return new WaitForSeconds(2f);
        noThanksBtn.SetActive(true);
    }

    public void Restart()
    {
        Variables.FirstTouch = 0;
        Variables.RoundOver = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UIStart()
    {
        foreach (var t in setActiveObjects)
        {
            t.SetActive(false);
        }
    }
}