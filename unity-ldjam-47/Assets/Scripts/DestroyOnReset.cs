using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnReset : MonoBehaviour
{
    private void OnEnable()
    {
        Messenger.AddListener(GameActionKeys.gameResetState, OnGameResetState);

    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameActionKeys.gameResetState, OnGameResetState);

    }

    private void OnGameResetState()
    {
        Destroy(gameObject);
    }
}
