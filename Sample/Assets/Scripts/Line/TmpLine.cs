using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TmpLine : MonoBehaviour
{
    private LineRenderer line;
    EdgeCollider2D col;
    Vector2 mousePosition;
    Vector2 pre;
    private int count = 0;
    private float preLength, length;

    void Start () {
        if (gameObject.transform.parent.gameObject.name == "LineDrawer(Clone)")
            pre = gameObject.transform.parent.GetComponent<LineDrawer>().pre; // 부모 객체의 LineDrawer 스크립트에서 pre를 가져옴
        else
            pre = gameObject.transform.parent.GetComponent<TramLineDrawer>().pre; // 부모 객체의 TramLineDrawer 스크립트에서 pre를 가져옴
        line = gameObject.GetComponent<LineRenderer>();
        line.SetPosition(0,new Vector3(0,0,0));
        col = gameObject.GetComponent<EdgeCollider2D>();
        preLength = LineCreator.Length;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButton(0)) //Or use GetKey with key defined with mouse button
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(1, mousePosition - (Vector2)gameObject.transform.position);
            length = Vector2.Distance(mousePosition, pre);
            LineCreator.Length = preLength - (int)length;
            SetCol();
            GameObject.Find("LineCreator").SendMessage("SetText");
        }
        if (Input.GetMouseButtonUp(0))
        {
            LineCreator.Length = preLength;
            GameObject.Find("LineCreator").SendMessage("SetText");
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other) // 콜라이더에 접촉할 때마다 count++
    {
        count++;
    }
    void OnTriggerExit2D(Collider2D other) // 콜라이더에서 빠져 나올 때마다 count--
    {
        count--;
        if (count <= 0) // count <= 0 이면, 충돌하지 않는 상태
        {
            if (gameObject.transform.parent.transform.name == "LineDrawer(Clone)")
            {
                gameObject.transform.parent.GetComponent<LineDrawer>().iscol = false;
                gameObject.transform.parent.GetComponent<LineDrawer>().istmpline = false;
            }
            else
            {
                gameObject.transform.parent.GetComponent<TramLineDrawer>().iscol = false;
                gameObject.transform.parent.GetComponent<TramLineDrawer>().istmpline = false;
            }
            LineCreator.Length = preLength;
            Destroy(gameObject);
        }
    }
    void SetCol()
    {
        col.points = new Vector2[] { mousePosition - (Vector2)gameObject.transform.position, pre - (Vector2)gameObject.transform.position };
    }
}
