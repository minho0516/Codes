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
//클래스 = 추상화 = context = 책(실체) , 도서시스템의 책(이름 저자 대출일자 등)