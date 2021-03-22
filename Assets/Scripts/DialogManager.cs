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
    public Flowchart candleDialog;
    public Flowchart activeCandleDialog;
    public Flowchart petunjukDialog;

    public void GetDialog(string gameTag)
    {
        if (DialogIsActive())
            return;

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
        if (gameTag == "Candle")
        {
            candleDialog.gameObject.SetActive(true);
            candleDialog.SetBooleanVariable("candleDialog", true);
        }
        if (gameTag == "ActiveCandle")
        {
            activeCandleDialog.gameObject.SetActive(true);
            activeCandleDialog.SetBooleanVariable("activeCandleDialog", true);
        }
        if(gameTag == "Petunjuk")
        {
            petunjukDialog.gameObject.SetActive(true);
            petunjukDialog.SetBooleanVariable("petunjukDialog", true);
        }

        GameManager.instance.isGameRunning = false;
    }

    public bool DialogIsActive()
    {
        if (bookDialog.GetBooleanVariable("bookDialog"))
            return true;
        else if (diaryDialog.GetBooleanVariable("diaryDialog"))
            return true;
        else if (photoDialog.GetBooleanVariable("photoDialog"))
            return true;
        else if (candleDialog.GetBooleanVariable("candleDialog"))
            return true;
        else if (activeCandleDialog.GetBooleanVariable("activeCandleDialog"))
            return true;
        else if (startDialog.GetBooleanVariable("firstStory"))
            return true;
        else if (senterDialog.GetBooleanVariable("flashlightDialog"))
            return true;
        else if (petunjukDialog.GetBooleanVariable("petunjukDialog"))
            return true;

        return false;
    }
}
