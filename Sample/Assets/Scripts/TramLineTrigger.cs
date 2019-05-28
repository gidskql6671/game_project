using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TramLineTrigger : MonoBehaviour {

    private PolygonCollider2D poly;
    public PhysicsMaterial2D mat;
    // Use this for initialization
    void Start () {
        poly = GetComponent<PolygonCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void makeTrigger(List<Vector2> points)
    {
        List<Vector2> TriggerPoints = new List<Vector2>();
        for(int i = 0; i < points.Count; i++)
        {
            TriggerPoints.Add(points[i] + new Vector2(0.1f, 0.1f));
        }
        for (int i = points.Count - 1; i > 0; i--)
        {
            TriggerPoints.Add(points[i] + new Vector2(-0.1f, -0.1f));
        }
        poly.SetPath(0, TriggerPoints.ToArray());
        poly.isTrigger = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) return;
        if (collision.gameObject.GetComponent<Rigidbody2D>().transform.tag != "floor")//2D 강체면 튕기게 함
        {
            mat.bounciness = 1f;
            collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = mat;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() == null) return;
        if (collision.gameObject.GetComponent<Rigidbody2D>().transform.tag != "floor")//2D 강체면 튕기게 함
        {
            collision.gameObject.GetComponent<Rigidbody2D>().sharedMaterial = null;
        }
    }
}
