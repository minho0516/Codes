using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeTextUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeTxt;

    float s;
    int m;

    private void Update()
    {
        s += Time.deltaTime;

        if (s >= 60)
        {
            s = 0;
            m++;
        }

        timeTxt.text = $"{m} : {Mathf.FloorToInt(s)}";
    }
}
