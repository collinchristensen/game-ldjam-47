using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameObjectHelper
{

    public static void ShowRenderers(GameObject inst)
    {
        if (inst == null)
            return;

        Renderer render = inst.GetComponent<Renderer>();

        if (render != null)
        {
            render.enabled = true;
        }

        // Enable rendering:
        foreach (Renderer component in inst.GetComponentsInChildren<Renderer>())
        {
            component.enabled = true;
        }
    }

    public static void HideRenderers(GameObject inst)
    {
        if (inst == null)
            return;

        Renderer render = inst.GetComponent<Renderer>();

        if (render != null)
        {
            render.enabled = false;
        }

        // Enable rendering:
        foreach (Renderer component in inst.GetComponentsInChildren<Renderer>())
        {
            component.enabled = false;
        }
    }

    public static bool Has<T>(GameObject inst) where T : Component
    {
        if (inst == null)
        {
            return false;
        }

        if (inst.GetComponentsInChildren<T>(true).Length > 0
            || inst.GetComponents<T>().Length > 0)
        {
            return true;
        }

        return false;
    }


    public static void Show(GameObject inst)
    {
        //LogUtil.Log("Show:" + inst.name);
        if (inst != null)
        {
            if (!inst.activeSelf)
            {
                inst.SetActive(true);
                ShowRenderers(inst);
            }
        }
    }

    public static void Hide(GameObject inst)
    {
        //LogUtil.Log("Hide:" + inst.name);
        if (inst != null)
        {
            if (inst.activeSelf || inst.activeInHierarchy)
            {
                HideRenderers(inst);
                inst.SetActive(false);
            }
        }
    }
    public static GameObject CleanGameObjectName(
        GameObject go)
    {

        if (go.name.Contains(" (Clone)"))
        {
            go.name = go.name.Replace(" (Clone)", "");
        }
        if (go.name.Contains("(Clone)"))
        {
            go.name = go.name.Replace("(Clone)", "");
        }
        if (go.name.Contains("(clone)"))
        {
            go.name = go.name.Replace("(clone)", "");
        }

        return go;
    }

    // TODO add keyed version of create game object

    public static GameObject CreateGameObject(
        GameObject go,
        Vector3 pos,
        Quaternion rotate,
        bool pooled)
    {

        GameObject obj = null;

        if (!pooled)
        {
            obj = GameObject.Instantiate(go, pos, rotate) as GameObject;
        }
        else
        {
            // single object pooler
            obj = ObjectPooler.SharedInstance.GetPooledObjectByName(go.name);

            if (obj != null)
            {
                obj.transform.position = pos;
                obj.transform.rotation = rotate;
            }
        }

        if (obj != null)
        {
            obj = CleanGameObjectName(obj);
            obj.Show();
        }

        return obj;
    }

    public static void DestroyGameObject(GameObject go, bool pooled = true)
    {
        DestroyGameObject(go, 0f, pooled);
    }

    public static void DestroyGameObject(GameObject go, float delay, bool pooled = true)
    {

        if (go == null)
        {
            return;
        }

        if (pooled)
        {
            go.Hide();
        }
        else
        {
            DestroyDelayed(go, delay);
        }
    }
    public static void DestroyDelayed(GameObject inst, float delay)
    {
        if (inst == null)
            return;

        GameObject.Destroy(inst, delay);
    }

}