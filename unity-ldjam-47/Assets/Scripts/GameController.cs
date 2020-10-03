using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameActionKeys
{
    //// EVENTS

    //// game states
    //public static string gameStart = "game-start";
    //public static string gamePause = "game-pause";
    //public static string gameResume = "game-resume";
    //public static string gameOver = "game-over";
    //public static string gameRestart = "game-restart";

    //// scoring/UI events
    //public static string gamePlayerScored = "game-player-scored";
    //public static string gamePlayerScoredHighScore = "game-player-scored-high-score";
    //public static string gamePlayerLoadedHighScore = "game-player-loaded-high-score";
    //public static string gamePlayerResetScore = "game-player-reset-score";

    //// input events
    //public static string gamePlayerTapped = "game-player-tapped";

    //// collision events
    //public static string gamePlayerEnemyCollided = "game-player-enemy-collided";
    //public static string gameProjectileMismatch = "game-projectile-mismatch";
    //public static string gameProjectileMatch = "game-projectile-match";

    //// settings events
    //public static string gameMusicToggle = "game-music-toggle";
    //public static string gameSFXToggle = "game-sfx-toggle";

    //// graphics/performance for background graphics

    //public static string detectedLowPerformance = "detected-low-performance";
    //public static string detectedHighPerformance = "detected-high-performance";


    //// TODO
    //public static string displayAd = "display-ad";
    //public static string analyticsUpdate = "analytics-update";


    //// BUTTONS

    //// button names
    //public static string buttonTap = "ButtonTap";
    //public static string buttonStart = "ButtonStart";
    //public static string buttonPause = "ButtonPause";
    //public static string buttonResume = "ButtonResume";
    //public static string buttonRestart = "ButtonRestart";

    //public static string buttonSettings = "ButtonSettings";
    //public static string buttonMusicToggle = "ButtonMusicToggle";
    //public static string buttonSFXToggle = "ButtonSFXToggle";
    //public static string buttonCloseSettings = "ButtonCloseSettings";

    //// player projectile buttons
    //public static string buttonGem1 = "ButtonGem1";
    //public static string buttonGem2 = "ButtonGem2";
    //public static string buttonGem3 = "ButtonGem3";

    //// admin/debug
    //public static string buttonDebugStats = "ButtonDebugStats";


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

    }

    private void OnDisable()
    {
        // UI listeners
        Messenger.RemoveListener<GameObject>(ButtonEvents.EVENT_BUTTON_CLICK_OBJECT, OnButtonClicked);

    }

    private void Awake()
    {
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
