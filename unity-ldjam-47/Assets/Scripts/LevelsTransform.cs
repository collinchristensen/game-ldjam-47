using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsTransform : MonoBehaviour
{
    public static LevelsTransform _instance;

    public static LevelsTransform Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<LevelsTransform>();
            }

            return _instance;
        }
    }

    private void Awake()
    {
        // singleton check
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
