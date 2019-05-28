using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UImanager : MonoBehaviour {

    [HideInInspector] public GameObject NomalUI;
    [HideInInspector] public GameObject StartEndUI;

    void Awake()
    {
        NomalUI = GameObject.Find("Canvas").transform.Find("NomalUI").gameObject;
        StartEndUI = GameObject.Find("Canvas").transform.Find("StartEndUI").gameObject;
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
