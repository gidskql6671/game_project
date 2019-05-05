using UnityEngine;
using System.Collections;

public class DrawLineTest : MonoBehaviour
{
    private LineRenderer line;
    private GameObject me;
    private Vector2 mousePos;
    private Vector2 startPos;
    private Vector2 endPos;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (line == null)
                createLine();
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            me.transform.position = mousePos;
            line.SetPosition(0, Vector3.zero);
            line.SetPosition(1, Vector3.zero);
            startPos = Vector3.zero;
        }
        else if (Input.GetMouseButtonUp(0) && line)
        {
            if (line)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                line.SetPosition(1, mousePos - (Vector2)me.transform.position);
                endPos = mousePos - (Vector2)me.transform.position;
                addColliderToLine();
                line = null;
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (line)
            {
                mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                line.SetPosition(1, mousePos - (Vector2)me.transform.position);
            }
        }
    }
    // Following method creates line runtime using Line Renderer component
    private void createLine()
    {
        me = new GameObject("Line");
        line = me.AddComponent<LineRenderer>();
        line.material = new Material(Shader.Find("Diffuse"));
        line.SetVertexCount(2); // 점 개수인듯? 2면 무조건 직선, 3이면 꺽이는 점 하나 만들 수 있고...
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.SetColors(Color.black, Color.black);
        line.useWorldSpace = false; // true 일시 World공간의 좌표 사용, false일시 현재 오브젝트의 좌표가 기준
    }
    // Following method adds collider to created line
    private void addColliderToLine()
    {
        Rigidbody2D rigid = me.AddComponent<Rigidbody2D>();
        PolygonCollider2D col = me.AddComponent<PolygonCollider2D>();
        
        float lineLength = Vector2.Distance(startPos, endPos); //length of line
        rigid.mass = 10;

        col.pathCount = 1;
        col.SetPath(0,new Vector2[]{ startPos + Vector2.up * 0.1f, endPos + Vector2.up * 0.1f, endPos + Vector2.down * 0.1f, startPos + Vector2.down * 0.1f });
        Vector2 midPoint = (startPos + endPos) / 2;
        
        
        rigid.gravityScale = 0.5f;
    }
}
