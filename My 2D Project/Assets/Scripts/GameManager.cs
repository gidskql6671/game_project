using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject canvas, lineCreator;
    [SerializeField] GameObject endobject;
    [SerializeField] GameObject player;

    Vector3 StartingPos;
    bool isStarted = false;


    void Awake()
    {
        Time.timeScale = 0;
    }

    void Start()
    {
        lineCreator = GameObject.Find("LineCreator");
        StartingPos = GameObject.FindGameObjectWithTag("Start").transform.position;

        canvas = GameObject.Find("Canvas");
        canvas.SetActive(false);
        GameObject.Find("But_End").SetActive(false);

        Screen.SetResolution(Screen.width, Screen.width * 1280 / 720, true);
    }

    public void StartGame()
    {
        GameObject.Find("But_Start").SetActive(false);

        lineCreator.GetComponent<LineCreator>().enabled = true;

        player = Instantiate(player, StartingPos, Quaternion.Euler(0.0f, 0.0f, 0.0f));
        player.GetComponent<PlayerMove>().enabled = true;
        
        canvas.SetActive(true);
        gameObject.GetComponent<Control_Manager>().enabled = true;
        Time.timeScale = 1f;
    }


    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

    }
    public void End()
    {
        player.GetComponent<PlayerMove>().enabled = false;
        canvas.SetActive(false);
        GameObject.Find("But_End").SetActive(true);
        Instantiate(endobject, Vector2.zero, Quaternion.Euler(0.0f, 0.0f, 0.0f)); // 인스턴스 생성
    }
}
