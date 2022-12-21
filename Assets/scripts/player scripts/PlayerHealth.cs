using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float health = 100;
    private playerMovement playerMovement;

    [SerializeField]
    private Slider healthSlider;

    private void Awake()
    {
        playerMovement = GetComponent<playerMovement>();
    }

    public void TakeDamage(float damageAmount)
    {
        if (health <= 0) return;

        health -= damageAmount;

        if (health <= 0f)
        {
            //imform player died
            playerMovement.PlayerDied();

            GameplayController.instance.RestartGame();

        }

        healthSlider.value = health;
    }
}
