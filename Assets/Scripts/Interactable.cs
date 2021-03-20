using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private DialogManager dialog;
    private bool touched;

    private void Start() {
        dialog = FindObjectOfType<DialogManager>();    
    }

    private void Update() {
        if(touched == true && Input.GetKeyDown(KeyCode.Space)){
            GetInteract();
        }
    }

    public void GetInteract()
    {
        dialog.GetDialog(this.tag);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.name == "Player"){
            touched = true;
        }
    }
    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.name=="Player"){
            touched = false;
        }
    }

}
