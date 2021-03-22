using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : Interactable
{
    private bool _isActive;
    public bool isActive { get { return _isActive; } }
    private Player player;
    public int timeDelay;
    private float currentTime;
    private bool rolling;
    [SerializeField] GameObject lightSource;
    [SerializeField] Sprite activeSprite;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Player>();
    }

    protected override void Update()
    {
        base.Update();
        
        if(rolling)
        {
            currentTime += Time.deltaTime;
            UIManager.instance.UpdateCandleBarAmount(currentTime);

            if(currentTime >= timeDelay)
            {
                Activate();
            }
        }
    }

    public override void GetInteract()
    {
        if(!_isActive)
        {
            if(player.haveSafetyMatches)
            {
                Roll();
            }
            else
            {
                base.GetInteract();
            }
        }
        else
        {
            dialog.GetDialog("ActiveCandle");
        }
    }

    void Roll()
    {
        rolling = true;
        player.RollCandle(this);
        player.StopMovement();
        UIManager.instance.InitializeCandleBar(timeDelay);
    }

    public void StopRolling()
    {
        rolling = false;
        currentTime = 0;
        UIManager.instance.HideCandleBar();
    }

    void Activate()
    {
        _isActive = true;
        lightSource.SetActive(true);
        GetComponent<SpriteRenderer>().sprite = activeSprite;
        player.AllowMovement();
        UIManager.instance.HideCandleBar();
    }

    public void TurnOff()
    {
        _isActive = false;
        currentTime = 0;
        rolling = false;
    }
}