using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Manager : MonoBehaviour {
    PlayerMove playerScript;

    void OnEnable()
    {
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
    }

    public void InputLeftDown()
    {
        playerScript.isleft = true;
    }
    public void InputLeftUp()
    {
        playerScript.isleft = false;
    }
    public void InputJumpDown()
    {
        playerScript.isjump = true;
    }
    public void InputJumpUp()
    {
        playerScript.isjump = false;
    }
    public void InputRightDown()
    {
        playerScript.isright = true;
    }
    public void InputRightUp()
    {
        playerScript.isright = false;
    }
}
