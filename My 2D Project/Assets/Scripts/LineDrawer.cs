using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public float Length = 0, Count_int = 0;
    public float total_Length = 0;
    private Vector2 pre, cur;

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
            if (line.positionCount != 0)
            {
                mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Length = Vector2.Distance(mousePosition, pre); // 길이를 구함
                pre = mousePosition;

                if (Length > 0.001f)
                    if (LineCreator.Length >= 0)
                    {
                        line.positionCount++;
                        line.SetPosition(line.positionCount - 1, mousePosition - (Vector2)gameObject.transform.position);
                        colliderPoints.Add(mousePosition - (Vector2)gameObject.transform.position); // 콜라이더를 위한 배열

                        total_Length += Length;
                        Count_int += Length;
                        if (Count_int >= 1) {
                            LineCreator.Length -= 1;
                            Count_int -= 1;
                        }
                        GameObject.Find("LineCreator").SendMessage("SetText");
                    }
            }
            else // 첫번째로 딱 찍었을 때
            {
                if (LineCreator.Length > 0)
                {
                    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    pre = mousePosition;
                    line.positionCount++;
                    line.SetPosition(line.positionCount - 1, mousePosition - (Vector2)gameObject.transform.position);
                    colliderPoints.Add(mousePosition - (Vector2)gameObject.transform.position); // 콜라이더를 위한 배열
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (simplifyLine)
            {
                line.Simplify(simplifyTolerance);
            }
            rigid.WakeUp();
            GameObject.Find("LineCreator").SendMessage("SetText");
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
    void OnBecameInvisible()
    {
        LineCreator.Count--;
        LineCreator.Length += (int)total_Length;
        GameObject.Find("LineCreator").SendMessage("SetText");
        Destroy(gameObject);
    }
}