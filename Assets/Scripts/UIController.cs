using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class UIController : MonoBehaviour
{


    public TMP_Text score;


    // Start is called before the first frame update
    void Start()
    {
        if(score!= null) {
            int final_scores = PlayerPrefs.GetInt("score"); ;
            score.SetText("" + final_scores);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void startTheGame()
    {
        SceneManager.LoadScene("MapScene");
    }

    public void endTheGame()
    {
        Application.Quit();
    }
}
