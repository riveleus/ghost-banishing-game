using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class DialogManager : MonoBehaviour
{
    public Player player;
    public Ghost ghost;

    [SerializeField] public Flowchart bookDialog;
    [SerializeField] public Flowchart diaryDialog;
    [SerializeField] public Flowchart photoDialog;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDialog(string gameTag){
        if(gameTag == "Book"){
            bookDialog.gameObject.SetActive(true);
            bookDialog.SetBooleanVariable("bookDialog", true);
            player.canMove = false;
            ghost.ghostMove = false;
        }
        if(gameTag == "Diary"){
            diaryDialog.gameObject.SetActive(true);
            diaryDialog.SetBooleanVariable("diaryDialog", true);
            player.canMove = false;
            ghost.ghostMove = false;
        }
        if(gameTag == "Photo"){
            photoDialog.gameObject.SetActive(true);
            photoDialog.SetBooleanVariable("photoDialog", true);
            player.canMove = false;
            ghost.ghostMove = false;
        }
    }
}
