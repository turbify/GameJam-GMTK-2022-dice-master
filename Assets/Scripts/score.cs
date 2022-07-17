using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class score : MonoBehaviour
{
    public float scoreValue = 0;
    public float maxScoreValue = 100;

    public int sliderSpeed;

    public string sceneName;

    public GameObject sl;
    public Slider sll;

    private void Start()
    {
        sll = sl.GetComponent<Slider>();
    }

    public void addScore(int val)
    {
        scoreValue += (float)val;
        sll.value = (scoreValue / maxScoreValue);

        if(scoreValue >= maxScoreValue) { SwitchScene();}
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
