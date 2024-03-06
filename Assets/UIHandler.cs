using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIHandler : MonoBehaviour
{

    public static UIHandler instance { get; private set; }

    private Label scoreLabel;
    private Label livesLabel;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement scoreBoxLabel = uiDocument.rootVisualElement.Q<VisualElement>("ScoreBoxLabel");
        scoreLabel = scoreBoxLabel.Q<Label>();
        scoreLabel.text = "Score: 0";

        VisualElement livesBoxLabel = uiDocument.rootVisualElement.Q<VisualElement>("LivesBoxLabel");
        livesLabel = livesBoxLabel.Q<Label>();
        livesLabel.text = "Lives: 3";
    }

    public void UpdateScore(int score)
    {
        scoreLabel.text = "Score: " + score;
    }

    public void UpdateLives(int lives)
    {
        livesLabel.text = "Lives: " + lives;
    }


}
