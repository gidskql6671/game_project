using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour {

    void Start()
    {
        int child_count = gameObject.transform.childCount;
        for(int i = 0; i < child_count; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            BoxCollider2D col = child.AddComponent<BoxCollider2D>();
            Rigidbody2D rigid = child.AddComponent<Rigidbody2D>();
            rigid.bodyType = RigidbodyType2D.Static;
            col.size = gameObject.transform.localScale * 1.6f;
        }

    }
}
