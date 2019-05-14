using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject lineCreator;
    UImanager UI;
    [SerializeField] GameObject endbackground;
    [SerializeField] GameObject player;

    Vector3 StartingPos;

    public static float difsize = 0.1f; // 스프라이트랑 콜라이더 크기 및 오프셋 설정에 필요함...
    // 아직 해상도 같은게 미정이라 테스트 하기 편하라고...

    void Awake()
    {
        Time.timeScale = 0; // 일시정지 기능
    }

    void Start()
    {
        lineCreator = GameObject.Find("LineCreator");
        StartingPos = GameObject.FindGameObjectWithTag("Start").transform.position;
        UI = gameObject.GetComponent<UImanager>();

        UI.NomalUI.SetActive(false); // 게임 버튼 비활성화
    }

    public void StartGame()
    {
        GameObject.Find("But_Start").SetActive(false); // 스타트 버튼 비활성화
        UI.NomalUI.SetActive(true); // 게임 버튼 활성화
        UI.StartEndUI.SetActive(false); // 스타트 엔드 판낼 비활성화

        player = Instantiate(player, StartingPos, Quaternion.Euler(0.0f, 0.0f, 0.0f)); // 플레이어 생성
        player.GetComponent<PlayerMove>().enabled = true; // 플레이어 이동 스크립트 온

        lineCreator.GetComponent<LineCreator>().enabled = true; // 라인 그리기 스크립트 온 
        gameObject.GetComponent<Control_Manager>().enabled = true; // 컨트롤 매니저 스크립트 온
        Time.timeScale = 1f; // 일시정지 해제
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
        player.GetComponent<PlayerMove>().enabled = false; // 플레이어 이동 스크립트 비활성화
        UI.StartEndUI.SetActive(true);
        UI.StartEndUI.transform.Find("But_End").gameObject.SetActive(true); // end 버튼 활성화

        Instantiate(endbackground, Vector2.zero, Quaternion.Euler(0.0f, 0.0f, 0.0f)); // 끝나면 배경... 인스턴스 생성
    }
}
