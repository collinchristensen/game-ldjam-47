using System.Collections;
using System.Collections.Generic;
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

        ShowGame();

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
