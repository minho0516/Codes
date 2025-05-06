using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject MainPanel;
    [SerializeField] private GameObject GamePanel;
    [SerializeField] private GameObject EndPanel;

    public static UIManager Instance;
    private GameState gameState = GameState.Main;

    private void Awake()
    {
        if(Instance == null) Instance = this;
        else Destroy(Instance);
    }

    private void Start()
    {
        CheckStateAndOpenPanel();
    }

    public void CheckStateAndOpenPanel()
    {
        if(gameState == GameState.Main)
        {
            MainPanel.SetActive(true);
            GamePanel.SetActive(false);
            EndPanel.SetActive(false);
        }
        else if(gameState == GameState.Game)
        {
            MainPanel.SetActive(false);
            GamePanel.SetActive(true);
            EndPanel.SetActive(false);
        }
        else if(gameState == GameState.End)
        {
            MainPanel.SetActive(false);
            GamePanel.SetActive(false);
            EndPanel.SetActive(true);
        }
    }
}
public enum GameState
{
    Main,
    Game,
    End
}
