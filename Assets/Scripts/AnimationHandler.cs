using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public static AnimationHandler instance;
    [SerializeField] Animator fovAnim;

    void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void GetDamage()
    {
        fovAnim.SetTrigger("GetDamage");
    }
}
