using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameRunning;
    private bool isPaused;
    [SerializeField] Player player;
    [SerializeField] Ghost ghost;
    public GameObject battery;
    public GameObject sanityBar;
    public Flowchart firstDialog;
    public Flowchart flashlightDialog;
    public DialogManager dialog;
    private bool firstStoryPassed;
    [SerializeField] Candle[] candles;
    private Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        respawnPosition = player.transform.position;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseUnpause();
        }

        if (!firstDialog.GetBooleanVariable("firstStory") && !firstStoryPassed)
        {
            battery.SetActive(true);
            flashlightDialog.gameObject.SetActive(true);
            if (!flashlightDialog.GetBooleanVariable("flashlightDialog"))
            {
                isGameRunning = true;
                sanityBar.SetActive(true);
                firstStoryPassed = true;
            }
        }

        if (!isGameRunning)
        {
            if (!dialog.DialogIsActive())
            {
                dialog.bookDialog.gameObject.SetActive(false);
                dialog.diaryDialog.gameObject.SetActive(false);
                dialog.photoDialog.gameObject.SetActive(false);
                dialog.candleDialog.gameObject.SetActive(false);
                dialog.activeCandleDialog.gameObject.SetActive(false);
                isGameRunning = true;
            }
        }
    }

    public bool AllCandleIsActivated()
    {
        for(int i = 0; i < candles.Length; i++)
        {
            if(!candles[i].isActive)
                return false;
        }

        return true;
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);

            Time.timeScale = 1f;
            isPaused = false;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();

            Time.timeScale = 0f;
            isPaused = true;
        }
    }

    public bool isStopped()
    {
        if(!isGameRunning || isPaused)
            return true;

        return false;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        isGameRunning = false;
        UIManager.instance.gameOverScreen.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0f;
        isGameRunning = false;
        UIManager.instance.winScreen.SetActive(true);
    }

    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);

        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        UIManager.instance.fadeFromBlack = true;

        player.transform.position = respawnPosition;

        isGameRunning = false;

        player.gameObject.SetActive(true);
    }
}
