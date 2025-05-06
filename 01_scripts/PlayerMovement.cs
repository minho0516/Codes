using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float cameraDepth = 20f;

    private Rigidbody rigid;
    private PlayerInput playerinput;
    public MeshRenderer meshRenderer;

    private float meshAdded;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        playerinput = GetComponent<PlayerInput>();

        meshAdded = meshRenderer.bounds.size.x;
    }

    private void Update()
    {
        LockedPos();

        Vector3 realion = playerinput.moveInput * moveSpeed;

        rigid.velocity = realion;
    }

    private void LockedPos()
    {
        Camera main = Camera.main;
        Vector3 bl = main.ViewportToWorldPoint(new Vector3(0, 0, cameraDepth));
        Vector3 tr = main.ViewportToWorldPoint(new Vector3(1, 1, cameraDepth));

        Vector3 pos = transform.position;
        //

        pos.x = Mathf.Clamp(pos.x, bl.x + meshAdded, tr.x - meshAdded); //???
        pos.z = Mathf.Clamp(pos.z, bl.z + meshAdded, tr.z - meshAdded);

        transform.position = pos;
    }

    private void OnDrawGizmos()
    {
        Camera main = Camera.main;
        Vector3 bl = main.ViewportToWorldPoint(new Vector3(0, 0, cameraDepth));
        Vector3 tr = main.ViewportToWorldPoint(new Vector3(1, 1, cameraDepth));

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(bl, 1f);

        Gizmos.color = Color.red;
        Gizmos.DrawSphere(tr, 1f);

        Bounds meshBounds = meshRenderer.bounds;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(meshBounds.center, meshBounds.size);
        //메쉬렌더러 퍼블릭으로넣기
    }
}
