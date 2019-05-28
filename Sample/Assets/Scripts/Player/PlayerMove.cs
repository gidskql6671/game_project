using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {

    [HideInInspector] public bool isleft = false;
    [HideInInspector] public bool isright = false;
    [HideInInspector] public bool isjump = false;

    public float movePower = 5f;
    public float jumpPower = 6f;

    Rigidbody2D rigid;
    Animator animator;

    bool island = false; // 땅에 있는가?

	// Use this for initialization
	void Start () {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        // 점프를 위한 콜라이더
        BoxCollider2D col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        col.offset = new Vector2(0, -0.8f) * GameManager.difsize;
        col.size = new Vector2(0.3f, 0.1f) * GameManager.difsize;
	}
	
	void Update () {
		if (isjump) // 점프 버튼을 누를 때 실행
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
        if (SystemInfo.deviceType.ToString().Equals("Desktop"))
        {
            if (Input.GetKey(KeyCode.LeftArrow))
                transform.position += Vector3.left * movePower * Time.deltaTime;
            if (Input.GetKey(KeyCode.RightArrow))
                transform.position += Vector3.right * movePower * Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
                Jump();
        }
        if (isleft)
        {
            transform.position += Vector3.left * movePower * Time.deltaTime;
            animator.SetInteger("direction", -1);
            animator.SetBool("walking", true);
        }
        else if (isright)
        {
            transform.position += Vector3.right * movePower * Time.deltaTime;
            animator.SetInteger("direction", 1);
            animator.SetBool("walking", true);
        }
        else
            animator.SetBool("walking", false);
    }
    
    public void Jump()
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
        if (other.tag == "floor" || other.tag == "makefloor")
        {
            island = true;
        }
    }
}
