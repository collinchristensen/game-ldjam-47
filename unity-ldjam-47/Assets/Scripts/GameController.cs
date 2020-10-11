using System;
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
    public static string gamePlayerDefeat = "game-player-defeat";

    public static string gameResetState = "game-reset";

    // entity states

    public static string playerScored = "player-scored";
    public static string playerHealthChanged = "player-health-changed";

    // ui events

    public static string buttonPlay = "ButtonPlay";
    public static string buttonOptions = "ButtonOptions";
    public static string buttonBackToMenu = "ButtonBackToMenu";
    public static string buttonQuit = "ButtonQuit";
    public static string LevelLoaded = "LevelLoaded";
}

public class GameGlobals
{
    public static bool ProjectilesArePooled = true;
    public static bool ObjectsArePooled = true;
}

public class GameController : MonoBehaviour
{

    public GameObject UIMainMenu;
    public GameObject UIOptionsMenu;
    public GameObject UIGame;

    public BendShaderControlYZ bendController;



    // bend transition control for game state
    float startXMargin = -100f;
    float startXCurvature = 1f;

    float midXMargin = -1f;
    float midXCurvature = 0f;

    float finalXMargin = 0f;
    float finalXCurvature = -1f;

    float defeatedXMargin = -20f;

    private void OnEnable()
    {
        // UI listeners
        Messenger.AddListener<GameObject>(ButtonEvents.EVENT_BUTTON_CLICK_OBJECT, OnButtonClicked);

        // Game event listeners
        Messenger.AddListener<int>(GameActionKeys.playerScored, OnPlayerScored);

        Messenger.AddListener(GameActionKeys.gamePlayerDefeat, OnGamePlayerDefeat);


        Messenger.AddListener(GameActionKeys.gamePlayerSpawned, OnPlayerSpawned);

    }

    private void OnDisable()
    {
        // UI listeners
        Messenger.RemoveListener<GameObject>(ButtonEvents.EVENT_BUTTON_CLICK_OBJECT, OnButtonClicked);

        // Game event listeners
        Messenger.RemoveListener<int>(GameActionKeys.playerScored, OnPlayerScored);

        Messenger.RemoveListener(GameActionKeys.gamePlayerDefeat, OnGamePlayerDefeat);



        Messenger.RemoveListener(GameActionKeys.gamePlayerSpawned, OnPlayerSpawned);

    }

    private void OnGamePlayerDefeat()
    {
        AudioManager.instance.efxSource.Stop();
        AudioController.instance.PlayDefeatSound();
        DefeatedBendTransition();
    }

    private void OnPlayerScored(int scoreAmount)
    {
        // Debug.Log("scored: " + scoreAmount);
    }

    private void OnPlayerSpawned()
    {

    }

    private void Awake()
    {
        Application.targetFrameRate = 60;

        ShowMainMenu();

        bendController.SetXFlatMargin(startXMargin);
        bendController.SetXCurvature(startXCurvature);

        //ShowGame();
    }

    private void FirstBendTransition()
    {
        float delay = 0f;
        float duration = .6f;

        // lerp from -100 flat margin to 0, and 1 x curvature to -1

        Vector2 startMarginCurvature = new Vector2(startXMargin, startXCurvature);
        Vector2 finalMarginCurvature = new Vector2(midXMargin, midXCurvature);

        iTween.ValueTo(this.gameObject,
            iTween.Hash(
                "from", startMarginCurvature,
                "to", finalMarginCurvature,
                "delay", delay,
                "time", duration,
                "easetype", iTween.EaseType.easeInQuad,
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
        float duration = .4f;

        // lerp from -100 flat margin to 0, and 1 x curvature to -1

        Vector2 startMarginCurvature = new Vector2(midXMargin, midXCurvature);
        Vector2 finalMarginCurvature = new Vector2(finalXMargin, finalXCurvature);

        iTween.ValueTo(this.gameObject,
            iTween.Hash(
                "from", startMarginCurvature,
                "to", finalMarginCurvature,
                "delay", delay,
                "time", duration,
                "easetype", iTween.EaseType.easeOutQuad,
                "onupdate", "updateMarginCurvature",
                "oncomplete", "SecondBendTransitionComplete"
                )
            );

    }

    private void DefeatedBendTransition()
    {
        float delay = 0f;
        float duration = .3f;

        // lerp from -100 flat margin to 0, and 1 x curvature to -1

        Vector2 startMarginCurvature = new Vector2(finalXMargin, finalXCurvature);
        Vector2 finalMarginCurvature = new Vector2(defeatedXMargin, finalXCurvature);

        iTween.ValueTo(this.gameObject,
            iTween.Hash(
                "from", startMarginCurvature,
                "to", finalMarginCurvature,
                "delay", delay,
                "time", duration,
                "easetype", iTween.EaseType.easeOutQuad,
                "onupdate", "updateMarginCurvature",
                "oncomplete", "DefeatedBendTransitionComplete"
                )
            );

    }

    public void SecondBendTransitionComplete()
    {
        // TODO: broadcast
    }
    public void DefeatedBendTransitionComplete()
    {
        // TODO: broadcast

        Messenger.Broadcast(GameActionKeys.gameResetState);

        ShowMainMenu();
    }


    private void OnButtonClicked(GameObject go)
    {
        if (go == null)
        {
            return;
        }

        string action = go.name;

        Debug.Log("OnButtonClicked: " + action);

        AudioController.instance.PlaySelectSound();

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
        else if (action.Contains(GameActionKeys.buttonQuit))
        {
            DefeatedBendTransition();
        }

    }

    private void ShowMainMenu()
    {
        AudioController.instance.PlayMenuMusic();
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
        AudioController.instance.PlayGameMusic();
        UIMainMenu.SetActive(false);
        UIOptionsMenu.SetActive(false);

        UIGame.SetActive(true);

        //Messenger.Broadcast(GameActionKeys.gameResetState);

        FirstBendTransition();
    }

}
