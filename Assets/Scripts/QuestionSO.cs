using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(minLines:2, maxLines: 6)]
    [SerializeField] string question = "Enter new question text here.";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correct_answer_index;

    public string GetQuestion() {
        return question;
    }

    public int GetCorrectAnswerIndex() {
        return correct_answer_index;
    }

    public string GetAnswer(int index) {
        return answers[index];
    }
}
