using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public static Flashlight instance;

    [SerializeField] FieldOfView fieldOfView = default;
    private Camera cam;
    [SerializeField] public float batteryMaxAmount;
    private float _currentAmount;
    public float currentAmount { get { return _currentAmount; } }
    public int batteryCount;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        cam = Camera.main;
        _currentAmount = batteryMaxAmount;
    }

    void Update()
    {
        if (_currentAmount <= 0)
        {
            if(batteryCount > 0)
            {
                RefillBattery();
                return;
            }

            fieldOfView.Disable();
        }
        else
        {
            _currentAmount -= Time.deltaTime;
        }
    }

    public void RefillBattery()
    {
        _currentAmount = batteryMaxAmount;
        batteryCount--;
    }

    public void HandleAim(Vector3 playerPosition)
    {
        Vector2 aimDir = (cam.ScreenToWorldPoint(Input.mousePosition) - playerPosition).normalized;
        fieldOfView.SetAimDirection(aimDir);
        fieldOfView.SetOrigin(playerPosition);

        float angle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
