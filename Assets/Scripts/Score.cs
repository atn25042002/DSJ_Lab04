using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    private int score;
    private TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh= GetComponent<TextMeshProUGUI>();
        score= 0;
    }
    public void AddScore(int points ){
        score+= points;
        if (score < 0){
            score= 0;
        }
        textMesh.text= score.ToString("0");
    }
}
