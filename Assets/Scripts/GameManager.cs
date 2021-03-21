using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool isGameRunning;
    [SerializeField] Player player;
    [SerializeField] Ghost ghost;
    public GameObject battery;
    public GameObject sanityBar;
    public Flowchart firstDialog;
    public Flowchart flashlightDialog;
    public DialogManager dialog;
    private bool firstStoryPassed;

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
            if (!DialogIsActive())
            {
                dialog.bookDialog.gameObject.SetActive(false);
                dialog.diaryDialog.gameObject.SetActive(false);
                dialog.photoDialog.gameObject.SetActive(false);
                isGameRunning = true;
            }
        }

        // if (!dialog.bookDialog.gameObject.activeInHierarchy || !dialog.diaryDialog.gameObject.activeInHierarchy ||
        //     !dialog.photoDialog.gameObject.activeInHierarchy)
        // {
        //     isGameRunning = true;
        // }
    }

    bool DialogIsActive()
    {
        if(dialog.startDialog.GetBooleanVariable("firstStory"))
            return true;
        else if(dialog.senterDialog.GetBooleanVariable("flashlightDialog"))
            return true;
        else if (dialog.bookDialog.GetBooleanVariable("bookDialog"))
            return true;
        else if (dialog.diaryDialog.GetBooleanVariable("diaryDialog"))
            return true;
        else if (dialog.photoDialog.GetBooleanVariable("photoDialog"))
            return true;

        return false;
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);

            Time.timeScale = 1f;
            isGameRunning = false;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();

            Time.timeScale = 0f;
            isGameRunning = true;
        }
    }

    public void GameOver()
    {
        UIManager.instance.gameOverScreen.SetActive(true);
    }

    public IEnumerator RespawnCo()
    {
        player.gameObject.SetActive(false);

        UIManager.instance.fadeToBlack = true;

        yield return new WaitForSeconds(2f);

        UIManager.instance.fadeFromBlack = true;

        player.transform.position = respawnPosition;

        player.gameObject.SetActive(true);
    }
}
