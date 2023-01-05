using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Result : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI final_result;
    ScoreKeeper scorekeeper;

    // Start is called before the first frame update
    void Awake()
    {
        scorekeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalResult() {
        final_result.text = "Congratulations!\nYou Win " + scorekeeper.CalculateScore() + "%";
    }
}
