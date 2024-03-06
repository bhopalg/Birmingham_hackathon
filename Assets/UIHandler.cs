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

    public void GameOver(int score)
    {
        scoreLabel.text = "Game Over";

        UIDocument uiDocument = GetComponent<UIDocument>();
        VisualElement gameOverBoxBackground = uiDocument.rootVisualElement.Q<VisualElement>("GameOverBoxBackground");
        gameOverBoxBackground.style.display = DisplayStyle.Flex;
        Label finalScoreLabel = gameOverBoxBackground.Q<VisualElement>("FinalScoreBoxLabel").Q<Label>();
        finalScoreLabel.text = "Final Score: " + score;

        Button startNewGameButton = uiDocument.rootVisualElement.Q<Button>("StartNewGameButton");
        startNewGameButton.RegisterCallback<ClickEvent>(ev => StartNewGame());

        Button quitButton = uiDocument.rootVisualElement.Q<Button>("QuitButton");
        quitButton.RegisterCallback<ClickEvent>(ev => QuitGame());
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("office");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
