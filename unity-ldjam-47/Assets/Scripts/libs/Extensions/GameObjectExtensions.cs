using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectExtensions
{
    public static void Show(this GameObject inst)
    {

        if (inst == null)
        {
            return;
        }

        GameObjectHelper.Show(inst);
    }

    public static void Hide(this GameObject inst)
    {

        if (inst == null)
        {
            return;
        }

        GameObjectHelper.Hide(inst);
    }

    public static GameObject CreateGameObject(
        this GameObject go,
        Vector3 pos,
        Quaternion rotate,
        bool pooled)
    {

        return GameObjectHelper.CreateGameObject(go, pos, rotate, pooled);
    }

    //

    public static void DestroyGameObject(this GameObject go, float delay = 0f, bool pooled = true)
    {

        GameObjectHelper.DestroyGameObject(go, delay, pooled);
    }
}