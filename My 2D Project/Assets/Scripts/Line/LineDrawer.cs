using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [HideInInspector] public float Length = 0, Count_int = 0;
    [HideInInspector] public float total_Length = 0;
    [HideInInspector] public Vector2 pre;
    [HideInInspector] public bool iscol = false, istmpline = false;
    [SerializeField] private GameObject tmpline;

    private LineRenderer line;
    private Rigidbody2D rigid;
    private PolygonCollider2D poly;
    private Vector2 mousePosition;
    private List<Vector2> colliderPoints = new List<Vector2>();

    [SerializeField] private bool simplifyLine = false; // true 일시, 선의 포인트가 너무 많지 않게 해줌
    [SerializeField] private float simplifyTolerance = 0.02f; // 높을수록 더 단순화
    void Start()
    {
        gameObject.layer = 3;
        line = GetComponent<LineRenderer>();
        rigid = GetComponent<Rigidbody2D>();
        poly = GetComponent<PolygonCollider2D>();
        
        rigid.bodyType = RigidbodyType2D.Static;
    }
    void Update()
    {
        if (Input.GetMouseButton(0)) //Or use GetKey with key defined with mouse button
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity,1); // 그리는 도중에 다른 옵젝이랑 충돌하는 지 여부 판단

            if (iscol == false) // iscol이 false이면 그냥 그리면 됨.
            {
                if (hit.collider == null) // 다른 옵젝이랑 충돌이 없으면 그냥 그림
                {
                    if (LineCreator.Length >= 0) // 길이 제한에 안걸릴 때.
                    {

                        if (line.positionCount != 0) // 그리는 도중일 때
                        {

                            Length = Vector2.Distance(mousePosition, pre); // 길이를 구함
                                                                           //Settmppoly(pre, mousePosition);
                            if (Length > 0.001f) // 마우스 안움직이고 있으면, 포지션 더 안만들도록...
                            {
                                line.positionCount++;
                                line.SetPosition(line.positionCount - 1, mousePosition - (Vector2)gameObject.transform.position);
                                colliderPoints.Add(mousePosition - (Vector2)gameObject.transform.position); // 콜라이더를 위한 배열

                                total_Length += Length; // 해당 선의 총 길이
                                Count_int += Length; // LineCreator.Length 에 그냥 실수를 빼주니, 제대로 출력이 안됨. 그래서 그냥 정수로...
                                if (Count_int >= 1)
                                {
                                    LineCreator.Length -= 1;
                                    Count_int -= 1;
                                }
                                pre = mousePosition;
                                GameObject.Find("LineCreator").SendMessage("SetText");
                            }
                        }
                        else // 첫번째로 딱 찍었을 때
                        {
                            if (LineCreator.Length > 0) // 길이 제한에 안걸릴 때
                            {
                                pre = mousePosition;
                                line.positionCount++;
                                line.SetPosition(line.positionCount - 1, mousePosition - (Vector2)gameObject.transform.position);
                                colliderPoints.Add(mousePosition - (Vector2)gameObject.transform.position); // 콜라이더를 위한 배열
                            }
                        }

                    }
                }
                else // 다른 옵젝이랑 충돌이 있다면?
                {
                    if (line.positionCount != 0)
                    {
                        iscol = true;
                        if (!istmpline)
                        {
                            Instantiate(tmpline, pre, Quaternion.Euler(0.0f, 0.0f, 0.0f)).transform.parent = gameObject.transform; // 인스턴스 생성 (라인 객체 생성)
                            istmpline = true;
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (simplifyLine)
            {
                line.Simplify(simplifyTolerance);
            }

            GameObject.Find("LineCreator").SendMessage("SetText");
            gameObject.layer = 0;
            gameObject.tag = "floor";
            addCollider();
            rigid.bodyType = RigidbodyType2D.Dynamic;
            enabled = false; //Making this script stop
        }
    }
    void addCollider() // 콜라이더 추가
    {
        List<Vector2> colliderPoints2 = new List<Vector2>();

        for (int i = 0; i < colliderPoints.Count; i++)
        {
            colliderPoints2.Add(colliderPoints[i] + new Vector2(0.05f, 0.05f));
        }

        for (int i = colliderPoints.Count - 1; i > 0; i--)
        {
            colliderPoints2.Add(colliderPoints[i] - new Vector2(0.05f, 0.05f));
        }

        poly.SetPath(0, colliderPoints2.ToArray());
    }
    void OnBecameInvisible() // 카메라 밖으로 나갔을 때 -> 삭제해야함
    {
        LineCreator.Count--;
        LineCreator.Length += (int)total_Length;
        GameObject.Find("LineCreator").SendMessage("SetText");
        Destroy(gameObject);
    }
}