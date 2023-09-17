using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeUI : MonoBehaviour
{

    public TMP_Text messageText;
    Game_Controller gm;

    // Start is called before the first frame update
    void Start() {
        gm = FindObjectOfType<Game_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        messageText.SetText(""+(int)gm.time);
    }
}
