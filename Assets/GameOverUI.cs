using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{

    public static GameOverUI instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }

        public void GameOver(int score)
    {
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
