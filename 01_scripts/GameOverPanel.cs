using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _playerHealth.HealthChangeEvent += HandleHealthChange;

        _canvasGroup.alpha = 0;
        _canvasGroup.interactable = false;
        _canvasGroup.blocksRaycasts = false;

    }

    public void ReStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void HandleHealthChange(int prev, int now)
    {
        if(now == 0)
        {
            SetOpen();
        }
    }

    public void SetOpen()
    {
        _canvasGroup.alpha = 1;
        _canvasGroup.interactable = true;
        _canvasGroup.blocksRaycasts = true;
    }
}
