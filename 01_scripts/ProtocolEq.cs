using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProtocolEq : MonoBehaviour
{
    [SerializeField] RectTransform ThisImage;

    [SerializeField] RectTransform StartButtonImage;
    [SerializeField] RectTransform ButtonImage;
    [SerializeField] RectTransform ExitButtonImage;

    private void Awake()
    {
        
    }
    private void Update()
    {
        Debug.Log(StartButtonImage.anchoredPosition);
        Debug.Log(StartButtonImage.position);

        if (StartButtonImage.position == new Vector3(15, 0, -0.8f))
        {
            Debug.Log("�������������ִ³��ε����ʾ������ᱹ�ʸ��̸³�");
        }
    }
}
