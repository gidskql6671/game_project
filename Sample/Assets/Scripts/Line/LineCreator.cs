using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;  // UI 터치를 막아주는 함수가 들어있당

public class LineCreator : MonoBehaviour
{
    public static int Count = 10;
    public static float Length = 100f;
    [SerializeField] private GameObject line; // 기본선
    [SerializeField] private GameObject tramline; // 트램펄린 선
    private Vector2 mousePosition;
    private bool canDraw = true;

    public int mode = 0; // 선 모드, 0이면 기본선, 1이면 트램펄린 선

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
                if (Count > 0 && Length > 0 && canDraw)
                {
                    mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                    if (mode == 0) 
                        Instantiate(line, mousePosition, Quaternion.Euler(0.0f, 0.0f, 0.0f)); // 인스턴스 생성 (라인 객체 생성)
                    else if (mode == 1)
                        Instantiate(tramline, mousePosition, Quaternion.Euler(0.0f, 0.0f, 0.0f)); // 인스턴스 생성 (트램펄린 라인 객체 생성)
                    Count--;
                }
            }
            Length = (Length > 100) ? 100 : Length;
            SetText();
            if (mode == 2) // 지움
            {
                Debug.Log("AA");
                if (Input.GetMouseButton(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    int mask = 1 << LayerMask.NameToLayer("Ignore Raycast") + 1 << LayerMask.NameToLayer("UI");  // Ignore Raycast 랑 UI만 보겠다.
                    mask = ~mask;   // mask 설정한걸 안볼거다.
                    RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, mask); // 그리는 도중에 다른 옵젝이랑 충돌하는 지 여부 판단
                    if (hit.collider != null)
                    {
                        if (hit.collider.gameObject.name == "LineDrawer(Clone)" || hit.collider.gameObject.name == "TramLineDrawer(Clone)")
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }
    void SetText()
    { 
        countText.text = "Count : " + Count +"\nLength : " + (Length < 0 ? 0 : Length);
    }
}