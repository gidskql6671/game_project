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

    // Use this for initialization
    void Start () {
        pre = gameObject.transform.parent.GetComponent<LineDrawer>().pre;
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
            Setpoly();
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
        Debug.Log(other.name + "    " + count);
        if (other.transform.tag != "End")
            count++;
    }
    void OnTriggerExit2D(Collider2D other) // 콜라이더에서 빠져 나올 때마다 count--
    {
        if (other.transform.tag != "End")
            count--;
        if (count <= 0) // count <= 0 이면, 충돌하지 않는 상태
        {
            gameObject.transform.parent.GetComponent<LineDrawer>().iscol = false;
            gameObject.transform.parent.GetComponent<LineDrawer>().istmpline = false;
            LineCreator.Length = preLength;
            Destroy(gameObject);
        }
    }
    void Setpoly()
    {
        col.points = new Vector2[] { mousePosition - (Vector2)gameObject.transform.position, pre - (Vector2)gameObject.transform.position };
    }
}
