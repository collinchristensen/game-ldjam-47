//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class WallBatching : MonoBehaviour
//{
//    private void OnEnable()
//    {
//        Messenger.AddListener(GameActionKeys.LevelLoaded, OnLevelLoad);

//    }

//    private void OnDisable()
//    {
//        Messenger.RemoveListener(GameActionKeys.LevelLoaded, OnLevelLoad);

//    }

//    void OnLevelLoad()
//    {
//        StaticBatchingUtility.Combine(LevelsTransform.Instance.gameObject);
//    }
//}
