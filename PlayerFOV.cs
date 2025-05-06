using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ViewCastInfo
{
    public bool isHit;
    public Vector3 point;
    public float distance;
    public float angle;
}

public struct EdgeInfo
{
    public Vector3 pointA;
    public Vector3 pointB;
}

public class PlayerFOV : MonoBehaviour
{
    [Range(0, 360f)] public float viewAngle = 120f;
    [Range(1f, 12f)] public float viewRadius = 5f;
    [SerializeField] private LayerMask _enemyMask, _obstacleMask;
    [SerializeField] private float _enemyFireDelay = 0.3f;
    [SerializeField, Range(0.1f, 2f)] private float _meshResolution = 1f;
    [SerializeField] private int _iterationCnt = 3;
    [SerializeField] private float _distanceThreadhold = 0.2f;

    public List<Transform> visibleTargets = new List<Transform>();

    private MeshFilter _meshFilter;
    private Mesh _viewMesh;

    private Collider[] _enemiesInView;

    private void Awake()
    {
        _enemiesInView = new Collider[10];
        _meshFilter = GetComponent<MeshFilter>();
        _viewMesh = new Mesh();
        _meshFilter.mesh = _viewMesh;
    }

    private void Start()
    {
        StartCoroutine(FindEnemyWhitDelay());
    }

    private void LateUpdate()
    {
        DrawFOV();
    }

    private EdgeInfo FindEdge(ViewCastInfo minCast, ViewCastInfo maxCast)
    {
        float minAngle = minCast.angle;
        float maxAngle = maxCast.angle;

        Vector3 minPoint = Vector3.zero;
        Vector3 maxPoint = Vector3.zero;

        for (int i = 0; i < _iterationCnt; i++)
        {
            float angle = (minAngle + maxAngle) * 0.5f;
            ViewCastInfo castInfo = ViewCast(angle);

            float distance = Mathf.Abs(minCast.distance - castInfo.distance);
            bool thresholdExceed = distance > _distanceThreadhold;

            if (castInfo.isHit == minCast.isHit && !thresholdExceed)
            {
                minAngle = angle;
                minPoint = castInfo.point;
            }
            else
            {
                maxAngle = angle;
                maxPoint = castInfo.point;
            }
        }

        return new EdgeInfo { pointA = minPoint, pointB = maxPoint };
    }
    private void DrawFOV()
    {
        int stepCount = Mathf.RoundToInt(viewAngle * _meshResolution);
        float stepAngleSize = viewAngle / stepCount;

        Vector3 pos = transform.position;
        List<Vector3> viewPoints = new List<Vector3>();

        ViewCastInfo oldCast = new ViewCastInfo();


        for (int i = 0; i <= stepCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle * 0.5f + stepAngleSize * i;
            //Debug.DrawLine(pos, pos + DirectionFromAngle(angle, true) * viewRadius, Color.red);

            ViewCastInfo info = ViewCast(angle);

            if (i > 0)
            {
                bool threadholdExceed = Mathf.Abs(oldCast.distance - info.distance) > _distanceThreadhold;

                if (oldCast.isHit != info.isHit || (oldCast.isHit && threadholdExceed))
                {
                    EdgeInfo edge = FindEdge(oldCast, info);

                    if (edge.pointA != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointA);
                    }
                    if (edge.pointB != Vector3.zero)
                    {
                        viewPoints.Add(edge.pointB);
                    }
                }
            }
            viewPoints.Add(info.point);
            oldCast = info;
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] verteces = new Vector3[vertexCount];
        int[] trangles = new int[(vertexCount - 2) * 3];

        verteces[0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++)
        {
            verteces[i + 1] = transform.InverseTransformPoint(viewPoints[i]);

            if (i < vertexCount - 2)
            {
                int tIndex = i * 3;
                trangles[tIndex + 0] = 0;
                trangles[tIndex + 1] = i + 1;
                trangles[tIndex + 2] = i + 2;
            }
        }
        _viewMesh.Clear();
        _viewMesh.vertices = verteces;
        _viewMesh.triangles = trangles;
        _viewMesh.RecalculateNormals();
    }

    private ViewCastInfo ViewCast(float angle)
    {
        Vector3 direction = DirectionFromAngle(angle, true);
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, viewRadius, _obstacleMask))
        {
            return new ViewCastInfo { isHit = true, point = hit.point, distance = hit.distance, angle = angle };
        }
        else if(Physics.Raycast(transform.position, direction, out RaycastHit hit2, viewRadius, _enemyMask))
        {
            if(hit2.transform.TryGetComponent<Enemy>(out Enemy compo)) Debug.Log(compo.gameObject.name);

            return new ViewCastInfo { isHit = true, point = hit.point, distance = hit.distance, angle = angle };
        }
        else
        {
            return new ViewCastInfo { isHit = false, point = transform.position + direction * viewRadius, distance = viewRadius, angle = angle };
        } 
    }
    private IEnumerator FindEnemyWhitDelay()
    {
        var time = new WaitForSeconds(_enemyFireDelay);
        while (true)
        {
            yield return time;
            FindVisibleEnemies();
        }
    }

    private void FindVisibleEnemies()
    {
        visibleTargets.Clear();
        int cnt = Physics.OverlapSphereNonAlloc(transform.position, viewRadius, _enemiesInView, _enemyMask);

        for (int i = 0; i < cnt; i++)
        {
            Transform enemy = _enemiesInView[i].transform;
            Vector3 direction = enemy.position - transform.position;

            if (Vector3.Angle(transform.forward, direction.normalized) < viewAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position, direction.normalized, direction.magnitude, _obstacleMask) == false)
                {
                    visibleTargets.Add(enemy);
                }
            }
        }
    }

    public Vector3 DirectionFromAngle(float degree, bool isGlobalAngle)
    {
        if (isGlobalAngle == false)
        {
            degree += transform.eulerAngles.y;
        }

        float radian = degree * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
