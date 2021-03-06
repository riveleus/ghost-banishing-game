using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Image blackScreen;

    public float fadeSpeed = 2f;
    public bool fadeToBlack, fadeFromBlack;

    public GameObject blackScreenObject, pauseScreen, optionsScreen, gameOverScreen, winScreen;

    public string mainMenu;

    public Slider musicVolSlider, sfxVolSlider;
    [SerializeField] Image sanityBar;
    [SerializeField] Slider candleFillBar;
    [SerializeField] Text objectIndicator;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        // kalau mau fade to black
        if (fadeToBlack)
        {
            blackScreenObject.SetActive(true);
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 1f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 1f)
            {
                fadeToBlack = false;
            }

        }
        if (fadeFromBlack)
        {
            blackScreen.color = new Color(blackScreen.color.r, blackScreen.color.g, blackScreen.color.b, Mathf.MoveTowards(blackScreen.color.a, 0f, fadeSpeed * Time.deltaTime));

            if (blackScreen.color.a == 0f)
            {
                fadeFromBlack = false;
                blackScreenObject.SetActive(false);
            }
        }
    }

    public void Resume()
    {
        GameManager.instance.PauseUnpause();
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
        Time.timeScale = 1;
    }

    public void SetMusicLevel()
    {
        AudioManager.instance.SetMusicLevel();
    }

    public void SetSFXLevel()
    {
        AudioManager.instance.SetSFXLevel();
    }

    public void InitializeSanityBar()
    {
        sanityBar.fillAmount = 1;
    }

    public void UpdateSanityBar(float value)
    {
        sanityBar.fillAmount = value;
    }

    public void InitializeCandleBar(int maxAmount)
    {
        candleFillBar.maxValue = maxAmount;
        candleFillBar.value = 0;
        candleFillBar.gameObject.SetActive(true);
    }

    public void HideCandleBar()
    {
        candleFillBar.gameObject.SetActive(false);
    }

    public void UpdateCandleBarAmount(float amount)
    {
        candleFillBar.value = amount;
    }

    public void TryAgain()
    {
        gameOverScreen.SetActive(false);
        Time.timeScale = 1f;
        StartCoroutine(GameManager.instance.RespawnCo());
    }

    public void ShowObjectIndicator(string objName)
    {
        objectIndicator.text = objName;
        objectIndicator.gameObject.SetActive(true);
    }

    public void HideObjectIndicator()
    {
        objectIndicator.gameObject.SetActive(false);
    }
}
