using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour
{
    
    [SerializeField] RectTransform redBar;
    [SerializeField] RectTransform greyBar;
    private RectTransform RT;

    private float barSize;
    private float hDelta;
    private float canvawidth;

    private PlayerManager playerManager;
    private float initialHealth;
    private float health;

    private void Awake()
    {  
        RT = GetComponent<RectTransform>();
        barSize = (RT.anchorMax - RT.anchorMin).x;
    }

    private void Start()
    {
        playerManager = GameObject.FindObjectOfType<PlayerManager>();
        initialHealth = playerManager.health;
    }
    public void UpdateBars()
    {
        health = playerManager.health;
        UpdateRedBar();
        UpdateGreyBar();
    }

    private void UpdateRedBar()
    {
        //redBar.sizeDelta = new Vector2(barSize * health/initialHealth, hDelta);
        //redBar.offsetMax.x = barSize * health / initialHealth;
        redBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, barSize * health / initialHealth);
    }

    private void UpdateGreyBar()
    {
        greyBar.sizeDelta = new Vector2(barSize * (initialHealth -health)/ initialHealth, hDelta);
    }
}
