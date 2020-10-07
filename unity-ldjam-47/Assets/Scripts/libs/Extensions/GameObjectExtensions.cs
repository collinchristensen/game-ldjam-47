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

    //public static void DestroyDelayed(this GameObject go, float delay = 0f)
    //{

    //    GameObjectHelper.DestroyDelayed(go, delay);
    //}

    //

    public static GameObject GetChildWithName(this GameObject go, string withName)
    {
        Transform[] transforms = go.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.gameObject.name == withName)
            {
                return t.gameObject;
            }
        }
        return null;
    }

    public static GameObject GetChildNameContains(this GameObject go, string subString)
    {
        Transform[] transforms = go.transform.GetComponentsInChildren<Transform>();
        foreach (Transform t in transforms)
        {
            if (t.gameObject.name.Contains(subString))
            {
                return t.gameObject;
            }
        }
        return null;
    }

    //public static Transform FindBelow(this GameObject inst, string name) {

    //    if (inst == null) {
    //        return null;
    //    }

    //    if (inst.transform.childCount == 0) {
    //        return null;
    //    }

    //    var child = inst.transform.Find(name);

    //    if (child != null) {
    //        return child;
    //    }

    //    foreach (GameObject t in inst.transform) {

    //        child = FindBelow(t, name);

    //        if (child != null) {
    //            return child;
    //        }
    //    }
    //    return null;
    //}
}