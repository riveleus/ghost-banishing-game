using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    protected DialogManager dialog;
    private bool touched;

    protected virtual void Start() {
        dialog = FindObjectOfType<DialogManager>();    
    }

    protected virtual void Update() {
        if(touched == true && Input.GetKeyDown(KeyCode.Space)){
            GetInteract();
        }
    }

    public virtual void GetInteract()
    {
        dialog.GetDialog(this.tag);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Player"){
            touched = true;
            UIManager.instance.ShowObjectIndicator(this.name);
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.name=="Player"){
            touched = false;
            UIManager.instance.HideObjectIndicator();
        }
    }

}
