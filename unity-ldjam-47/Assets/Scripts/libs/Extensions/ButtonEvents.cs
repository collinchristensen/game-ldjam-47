
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;



[Serializable]
public class ButtonEvenDataItem {
    public string key = "";
    public object data = "";
}

public class ButtonEvents : MonoBehaviour {

    //public List<ButtonEvenDataItem> eventData = new List<ButtonEvenDataItem>();
    public Dictionary<string, string> eventData = new Dictionary<string, string>();

    public static string EVENT_BUTTON_CLICK = "event-button-click";
    public static string EVENT_BUTTON_CLICK_OBJECT = "event-button-click-object";
    public static string EVENT_BUTTON_CLICK_DATA = "event-button-click-data";


    Button btn;

    void Start() {
        btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    void OnClick() {
        Debug.Log("OnClick:" + gameObject.name);

        // TODO
        // GameAudio.PlayEffect(GameAudioEffects.audio_effect_ui_button_1);

        Messenger.Broadcast<GameObject>(ButtonEvents.EVENT_BUTTON_CLICK_OBJECT, gameObject);
        //Messenger<string>.Broadcast(ButtonEvents.EVENT_BUTTON_CLICK, transform.name);

    }
}