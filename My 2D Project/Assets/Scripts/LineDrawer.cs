using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    private LineRenderer line;
    private Rigidbody2D rigid;
    private PolygonCollider2D poly;
    private Vector2 mousePosition;
    private List<Vector2> colliderPoints = new List<Vector2>();

    [SerializeField] private bool simplifyLine = false; // true 일시, 선의 포인트가 너무 많지 않게 해줌
    [SerializeField] private float simplifyTolerance = 0.02f; // 높을수록 더 단순화
    void Start()
    {
        line = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        poly = GetComponent<PolygonCollider2D>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0)) //Or use GetKey with key defined with mouse button
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.positionCount++;
            line.SetPosition(line.positionCount - 1, mousePosition - (Vector2)gameObject.transform.position);
            colliderPoints.Add(mousePosition - (Vector2)gameObject.transform.position); // 콜라이더를 위한 배열
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (simplifyLine)
            {
                line.Simplify(simplifyTolerance);
            }
            rigid.WakeUp();
            enabled = false; //Making this script stop
            addCollider();
        }
    }
    void addCollider()
    {
        List<Vector2> colliderPoints2 = new List<Vector2>();

        for (int i = 0; i < colliderPoints.Count; i++)
        {
            colliderPoints2.Add(colliderPoints[i] + new Vector2(0.05f, 0.05f));
        }

        for (int i = colliderPoints.Count - 1; i > 0; i--)
        {
            colliderPoints2.Add(colliderPoints[i] - new Vector2(0.05f,0.05f));
        }

        poly.SetPath(0, colliderPoints2.ToArray());
    }
}