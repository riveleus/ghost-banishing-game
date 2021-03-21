using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogManager : MonoBehaviour
{
    public Player player;
    public Ghost ghost;

    public Flowchart startDialog;
    public Flowchart senterDialog;
    public Flowchart bookDialog;
    public Flowchart diaryDialog;
    public Flowchart photoDialog;

    public void GetDialog(string gameTag)
    {
        if (gameTag == "Book")
        {
            bookDialog.gameObject.SetActive(true);
            bookDialog.SetBooleanVariable("bookDialog", true);
        }
        if (gameTag == "Diary")
        {
            diaryDialog.gameObject.SetActive(true);
            diaryDialog.SetBooleanVariable("diaryDialog", true);
        }
        if (gameTag == "Photo")
        {
            photoDialog.gameObject.SetActive(true);
            photoDialog.SetBooleanVariable("photoDialog", true);
        }

        GameManager.instance.isGameRunning = false;
        // GameManager.instance.isInteracting = true;
    }
}
