using UnityEditor;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Vector3 moveInput;

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(x, 0, z);
    }
}
//Ŭ���� = �߻�ȭ = context = å(��ü) , �����ý����� å(�̸� ���� �������� ��)