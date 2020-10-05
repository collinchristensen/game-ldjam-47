using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int health = 10;

    public int maxHealth = 10;

    public List<Image> healthUnits;

    public Sprite fullHealthUnit;
    public Sprite emptyHealthUnit;

    private void OnEnable()
    {
         Messenger.AddListener<int>(GameActionKeys.playerHealthChanged, OnPlayerHealthChanged);

    }

    private void OnDisable()
    {
       Messenger.RemoveListener<int>(GameActionKeys.playerHealthChanged, OnPlayerHealthChanged);

    }

    private void OnPlayerHealthChanged(int amount)
    {
        AddHealth(amount);
    }

    private void Update()
    {

        for (int i = 0; i < healthUnits.Count; i++)
        {

            if (i < health)
            {
                healthUnits[i].sprite = fullHealthUnit;
            }
            else
            {
                healthUnits[i].sprite = emptyHealthUnit;
            }
        }
    }

    private void AddHealth(int input)
    {
        health += input;
        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if (health <= 0)
        {
            Messenger.Broadcast(GameActionKeys.gamePlayerDefeat);
        }
    }

}
