﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public int score = 0;

    public TMP_Text scoreText;

    private void OnEnable()
    {
        Messenger.AddListener<int>(GameActionKeys.playerScored, OnPlayerScored);
        Messenger.AddListener(GameActionKeys.gameResetState, OnGameResetState);

    }

    private void OnDisable()
    {
        Messenger.RemoveListener<int>(GameActionKeys.playerScored, OnPlayerScored);
        Messenger.RemoveListener(GameActionKeys.gameResetState, OnGameResetState);

    }

    private void OnGameResetState()
    {
        score = 0;
        scoreText.text = score.ToString("00000");
    }

    private void OnPlayerScored(int val)
    {
        score += val;
        //scoreText.text = String.Format("00000:{0}",score);
        scoreText.text = score.ToString("00000");
    }
}
