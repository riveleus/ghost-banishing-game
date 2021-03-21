using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public Ghost ghostReference;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(ghostReference.isBinded == true && collision.tag == "Ghost")
        {
            Debug.Log("You Win");
        }
    }
}
