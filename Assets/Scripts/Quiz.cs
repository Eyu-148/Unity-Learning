using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")] // creating layout in Unity editor
    [SerializeField] TextMeshProUGUI question_text;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO current_question;

    [Header("Answers")]
    [SerializeField] GameObject[] answer_buttons; // the game object has been created in Unity
    int correct_answer_index;
    bool is_answer_early = true; // if the player select the answer before the time ends

    [Header("Buttons")]
    [SerializeField] Sprite defalut_answer_sprite;
    [SerializeField] Sprite correct_answer_sprite;

    [Header("Timer")]
    [SerializeField] Image timer_image;
    Timer timer; // Timer object

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scorekeeper;

    [Header("ProgressBar")]
    [SerializeField] Slider progressbar;

    public bool is_complete;

    // Start is called before the first frame update
    void Awake()
    {
        scorekeeper = FindObjectOfType<ScoreKeeper>();
        timer = FindObjectOfType<Timer>(); // get everything from Timer class
        progressbar.maxValue = questions.Count;
        progressbar.value = 0;
    }

    // Update is called once per frame
    void Update() {
        timer_image.fillAmount = timer.timer_image_fill_fraction;
        if (timer.if_load_next_question) { // when showing is done & ready to load the next question
            
            if (progressbar.value == progressbar.maxValue) {
                is_complete = true;
                return; // return when the last question was answered & the answer was showed up
            }

            is_answer_early = false; // reinitialize the bool
            GetNextQuestion(); // display the question
            timer.if_load_next_question = false; //reinitialize the bool
        }
        else if (!timer.is_selceting && !is_answer_early) { // when showing the correct answer
            DisplayAnswers(-1); // a false answer
            SetButtonState(false); // disable the buttons
        }
    }

    /*
        OnAnswerSelected method is called everytime the button is clicked
        it should be connected to each button in Unity
    */
    public void OnAnswerSelected(int index) {  
        is_answer_early = true;
        DisplayAnswers(index);      
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = "Score: " + scorekeeper.CalculateScore() + "%";
    }

    void GetNextQuestion() {
        if (questions.Count > 0) { // when the list is not empty
            SetButtonState(true); // initialize button states
            SetDefalutButtonSprites(); // initialize button images
            GetRandomQuestion();
            DisplayQuestions(); // initialize question/answer text
            ++progressbar.value; // increment the progress bar
            scorekeeper.SetAnsweredQuestions();
        }
    }

    void GetRandomQuestion() {
        int index = Random.Range(0, questions.Count);
        current_question = questions[index];

        if (questions.Contains(current_question)) {
            questions.Remove(current_question);
        }
    }

    void DisplayQuestions() {
        question_text.text = current_question.GetQuestion();
        TextMeshProUGUI button_text;
        // for loop -- get the answers from the question object
        for (int i=0; i<answer_buttons.Length; ++i) {
            button_text = answer_buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            button_text.text = current_question.GetAnswer(i);
        }
    }

    void DisplayAnswers(int index) {
        Image button_image;
        correct_answer_index = current_question.GetCorrectAnswerIndex();

        // if the correct answer is selected
        if (index == correct_answer_index) {
            question_text.text = "Correct!"; // change the question text
            scorekeeper.SetCorrectAnswer();
        }
        else {
            correct_answer_index = current_question.GetCorrectAnswerIndex();
            string correct_answer = current_question.GetAnswer(correct_answer_index);
            question_text.text = "Noop, the answer is this one.";
        }
        button_image = answer_buttons[correct_answer_index].GetComponent<Image>();
        button_image.sprite = correct_answer_sprite; // change the sprite for the correct answer

    }

    void SetButtonState(bool button_state) {
        // disable/enable each buttons
        for (int i=0; i < answer_buttons.Length; ++i) {
            Button button = answer_buttons[i].GetComponent<Button>();
            button.interactable = button_state;
        }
    }

    void SetDefalutButtonSprites() {
        // set up default sprites for each button
        Image button_image;
        for (int i=0; i < answer_buttons.Length; ++i) {
            button_image = answer_buttons[i].GetComponent<Image>();
            button_image.sprite = defalut_answer_sprite;
        }
    }

}
