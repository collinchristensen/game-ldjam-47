﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;


public class GameActionKeys
{
    //// EVENTS

    //// game states
    //public static string gameStart = "game-start";

    public static string gamePlayerSpawned = "game-player-spawned";

    // entity states

    public static string playerScored = "player-scored";

    // ui events

    public static string buttonPlay = "ButtonPlay";
    public static string buttonOptions = "ButtonOptions";
    public static string buttonBackToMenu = "ButtonBackToMenu";
}

public class GameController : MonoBehaviour
{

    public GameObject UIMainMenu;
    public GameObject UIOptionsMenu;
    public GameObject UIGame;

    public BendShaderControlYZ bendController;



    float startXMargin = -100f;
    float startXCurvature = 1f;

    float midXMargin = -1f;
    float midXCurvature = 0f;

    float finalXMargin = 0f;
    float finalXCurvature = -1f;


    private void OnEnable()
    {
        // UI listeners
        Messenger.AddListener<GameObject>(ButtonEvents.EVENT_BUTTON_CLICK_OBJECT, OnButtonClicked);

        // Game event listeners
        Messenger.AddListener<int>(GameActionKeys.playerScored, OnPlayerScored);


        // debug
        Messenger.AddListener(GameActionKeys.gamePlayerSpawned, OnPlayerSpawned);

    }

    private void OnDisable()
    {
        // UI listeners
        Messenger.RemoveListener<GameObject>(ButtonEvents.EVENT_BUTTON_CLICK_OBJECT, OnButtonClicked);

        // Game event listeners
        Messenger.RemoveListener<int>(GameActionKeys.playerScored, OnPlayerScored);


        // debug
        Messenger.RemoveListener(GameActionKeys.gamePlayerSpawned, OnPlayerSpawned);

    }

    private void OnPlayerScored(int scoreAmount)
    {
        Debug.Log("scored: " + scoreAmount);
    }

    private void OnPlayerSpawned()
    {

    }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        //ShowMainMenu();

        bendController.SetXFlatMargin(startXMargin);
        bendController.SetXCurvature(startXCurvature);

        ShowGame();

        FirstBendTransition();
    }

    private void FirstBendTransition()
    {
        float delay = 1f;
        float duration = 1.1f;

        // lerp from -100 flat margin to 0, and 1 x curvature to -1

        Vector2 startMarginCurvature = new Vector2(startXMargin, startXCurvature);
        Vector2 finalMarginCurvature = new Vector2(midXMargin, midXCurvature);

        iTween.ValueTo(this.gameObject,
            iTween.Hash(
                "from", startMarginCurvature,
                "to", finalMarginCurvature,
                "delay", delay,
                "time", duration,
                "easetype", iTween.EaseType.easeOutQuad,
                "onupdate", "updateMarginCurvature",
                "oncomplete", "FirstBendTransitionComplete"
                )
            );

    }

    public void updateMarginCurvature(Vector2 val)
    {
        bendController.SetXFlatMargin(val.x);
        bendController.SetXCurvature(val.y);
    }

    public void FirstBendTransitionComplete()
    {
        SecondBendTransition();
    }

    private void SecondBendTransition()
    {
        float delay = 0f;
        float duration = .6f;

        // lerp from -100 flat margin to 0, and 1 x curvature to -1

        Vector2 startMarginCurvature = new Vector2(midXMargin, midXCurvature);
        Vector2 finalMarginCurvature = new Vector2(finalXMargin, finalXCurvature);

        iTween.ValueTo(this.gameObject,
            iTween.Hash(
                "from", startMarginCurvature,
                "to", finalMarginCurvature,
                "delay", delay,
                "time", duration,
                "easetype", iTween.EaseType.easeInQuad,
                "onupdate", "updateMarginCurvature",
                "oncomplete", "SecondBendTransitionComplete"
                )
            );

    }
    public void SecondBendTransitionComplete()
    {
        // broadcast
    }

    private void OnButtonClicked(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        string action = go.name;

        Debug.Log("OnButtonClicked: " + action);

        if (action.Contains(GameActionKeys.buttonPlay))
        {
            ShowGame();
        }
        else if (action.Contains(GameActionKeys.buttonOptions))
        {
            ShowOptionsMenu();
        }
        else if (action.Contains(GameActionKeys.buttonBackToMenu))
        {
            ShowMainMenu();
        }

    }

    private void ShowMainMenu()
    {
        UIOptionsMenu.SetActive(false);
        UIGame.SetActive(false);

        UIMainMenu.SetActive(true);
    }
    private void ShowOptionsMenu()
    {
        UIMainMenu.SetActive(false);
        UIGame.SetActive(false);

        UIOptionsMenu.SetActive(true);
    }
    private void ShowGame()
    {
        UIMainMenu.SetActive(false);
        UIOptionsMenu.SetActive(false);

        UIGame.SetActive(true);
    }

}
