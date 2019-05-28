using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInfo : MonoBehaviour {

    void Start()
    {
        int child_count = gameObject.transform.childCount;
        for(int i = 0; i < child_count; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject; // 바닥들

            BoxCollider2D col = child.AddComponent<BoxCollider2D>();
            if (child.GetComponent<SpriteRenderer>().sprite.name == "Basic+Sprites_0")
            { // 왼쪽 끝블록
                col.size = new Vector2(1.4f, 1.6f) * GameManager.difsize;
                col.offset = new Vector2(0.1f, 0) * GameManager.difsize;
            }
            else if (child.GetComponent<SpriteRenderer>().sprite.name == "Basic+Sprites_2")
            { // 오른쪽 끝블록
                col.size = new Vector2(1.4f, 1.6f) * GameManager.difsize;
                col.offset = new Vector2(-0.1f, 0) * GameManager.difsize;
            }
            else // 중간 블록
                col.size = new Vector2(1.6f,1.6f) * GameManager.difsize;

            Rigidbody2D rigid = child.AddComponent<Rigidbody2D>();
            rigid.bodyType = RigidbodyType2D.Static;

            child.tag = "floor";
        }

    }
}
