using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    public float movePower = 5f;
    public float jumpPower = 6f;

    Rigidbody2D rigid;
    BoxCollider2D col;

    Vector3 movement;
    bool island = false; // 땅에 있는가?

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();

        // 점프를 위한 콜라이더
        col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.offset = new Vector2(0, -0.8f);
        col.size = new Vector2(0.3f, 0.1f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Jump") ) // 점프 버튼을 누를 때 실행
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        Vector3 moveVelocity = Vector3.zero;

        if (Input.GetAxisRaw("Horizontal") < 0)
            moveVelocity = Vector3.left;
        else if (Input.GetAxisRaw("Horizontal") > 0)
            moveVelocity = Vector3.right;

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Jump()
    {
        if (!island) // 점프 버튼을 누르지 않았다면 탈출
            return;

        rigid.velocity = Vector2.zero;

        Vector2 jumpVelocity = new Vector2(0, jumpPower);
        rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

        island = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "floor")
        {
            island = true;
        }
    }
}
