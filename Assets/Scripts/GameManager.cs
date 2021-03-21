using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private bool _isPaused;
    public bool isPaused { get { return _isPaused; } }

    [SerializeField] Player player;
    [SerializeField] Ghost ghost;
    public GameObject battery;
    public GameObject sanityBar;
    public Flowchart firstDialog;
    public Flowchart flashlightDialog;
    public DialogManager dialog;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            PauseUnpause();
        }

        if (!firstDialog.GetBooleanVariable("firstStory"))
        {
            battery.SetActive(true);
            flashlightDialog.gameObject.SetActive(true);
            if (!flashlightDialog.GetBooleanVariable("flashlightDialog"))
            {
                player.canMove = true;
                ghost.ghostMove = true;
                sanityBar.SetActive(true);
            }
        }

        if (!dialog.bookDialog.GetBooleanVariable("bookDialog"))
            dialog.bookDialog.gameObject.SetActive(false);

        if (!dialog.diaryDialog.GetBooleanVariable("diaryDialog"))
            dialog.diaryDialog.gameObject.SetActive(false);

        if (!dialog.photoDialog.GetBooleanVariable("photoDialog"))
            dialog.photoDialog.gameObject.SetActive(false);


        if (!dialog.bookDialog.gameObject.activeInHierarchy || !dialog.diaryDialog.gameObject.activeInHierarchy ||
            !dialog.photoDialog.gameObject.activeInHierarchy)
        {
            player.canMove = true;
            ghost.ghostMove = true;
        }
    }

    public void PauseUnpause()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);

            Time.timeScale = 1f;
            _isPaused = false;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();

            Time.timeScale = 0f;
            _isPaused = true;
        }
    }
}
