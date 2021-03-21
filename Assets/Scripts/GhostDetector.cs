using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostDetector : MonoBehaviour
{
    private bool ableToDetect = true;
    [SerializeField] Ghost ghost;
    [SerializeField] int exposeDelay;
    private float exposeTime;
    private bool exposeTimeStarted;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Ghost")
        {
            if (ableToDetect)
            {
                ghost.RunAway(true);
            }

            if(!exposeTimeStarted)
            {
                exposeTime = exposeDelay;
                exposeTimeStarted = true;
            }

            if(exposeTime <= 0)
            {
                ghost.Disappear();
                exposeTimeStarted = false;
            }
            else
            {
                exposeTime -= Time.deltaTime;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Ghost")
        {
            ghost.RunAway(false);
        }
    }
}
