using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [TextArea]
    public string message;

    public void GetInteract()
    {
        UIManager.instance.ShowPopupDialog(message);
    }

    public void StopInteract()
    {
        UIManager.instance.HidePopupDialog();
    }
}
