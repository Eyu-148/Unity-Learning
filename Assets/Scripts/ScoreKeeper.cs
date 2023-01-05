using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correct_answer = 0;
    int questions_answered = 0;

    public int GetCorrectAnswer() {
        return correct_answer;
    }

    public int GetAnsweredQuestions() {
        return questions_answered;
    }

    public void SetCorrectAnswer() {
        ++correct_answer;
    }

    public void SetAnsweredQuestions() {
        ++ questions_answered;
    }

    public int CalculateScore() {
        return Mathf.RoundToInt(correct_answer / (float) questions_answered* 100);
    }
}
