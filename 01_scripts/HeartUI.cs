using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    [SerializeField] private Image[] _heartImage;
    [SerializeField] private Health _playerHeart;

    private void Awake()
    {
        _playerHeart.HealthChangeEvent += HandleHealthChange;
    }

    public void HandleHealthChange(int prev, int now)
    {
        for(int i = 0;  i < _heartImage.Length; i++)
        {
            if(i < now)
            {
                _heartImage[i].enabled = true;
            }
            else
            {
                _heartImage[i].enabled = false;
            }
        }
    }
}
