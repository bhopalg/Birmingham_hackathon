using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class WelcomeUI : MonoBehaviour
{
    public static WelcomeUI instance { get; private set; }
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UIDocument uiDocument = GetComponent<UIDocument>();
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
