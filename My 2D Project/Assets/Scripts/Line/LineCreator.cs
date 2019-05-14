using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // UI 터치를 막아주는 함수가 들어있당

public class LineCreator : MonoBehaviour
{
    public static int Count = 0;
    public static float Length = 100f;
    [SerializeField] private GameObject line;
    private Vector2 mousePosition;
    private bool canDraw = true;

    public Text countText;

    void Start()
    {
        SetText();
    }

    public void DrawStop()
    {
        canDraw = false;
    }
    public void DrawStart()
    {
        canDraw = true;
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject() == false) // UI위를 터치한 것이 아닐 때
        {
            if (Input.GetMouseButtonDown(0)) //Or use GetKeyDown with key defined with mouse button
            {
                if (Count < 5 && Length > 0 && canDraw)
                {
                    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Instantiate(line, mousePosition, Quaternion.Euler(0.0f, 0.0f, 0.0f)); // 인스턴스 생성 (라인 객체 생성)

                    Count++;
                }
            }
            Length = (Length > 100) ? 100 : Length;
            SetText();
        }
    }
    void SetText()
    { 
        countText.text = "Count : " + (5 - Count)+"\nLength : " + (Length < 0 ? 0 : Length);
    }
}