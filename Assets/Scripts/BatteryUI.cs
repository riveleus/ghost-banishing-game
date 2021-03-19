using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryUI : MonoBehaviour
{
    [SerializeField] List<GameObject> batteryImage;
    public Flashlight flashlight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetImageActive();
    }

    void GetImageActive(){
        for(int i =0;i<4;i++){
            batteryImage[i].SetActive(false);
        }

        int image = 0;
        if(flashlight.currentAmount>((0.6)*flashlight.batteryMaxAmount)){
             image = 0;
        }else if(flashlight.currentAmount>((0.3)*flashlight.batteryMaxAmount)){
            image=1;
        }else if(flashlight.currentAmount>0){
            image=2;
        } else{
            image=3;
        }

        switch(image){
            case 0:
                batteryImage[0].SetActive(true);
                break;
            case 1:
                batteryImage[1].SetActive(true);
                break;
            case 2:
                batteryImage[2].SetActive(true);
                break;
            case 3:
                batteryImage[3].SetActive(true);
                break;
        }
    }
}
