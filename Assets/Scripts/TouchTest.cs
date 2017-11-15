using UnityEngine;
using System.Collections;

public class TouchTest : MonoBehaviour
{
    delegate void listener(ArrayList touches);
    event listener touchBegin, touchMove, touchEnd;

    // 처음 좌표, 움직일 떄의 좌표
    Vector2 beginPos, movePos;

    public GameObject store, setting, achive,select;

    public Tile.TileMap tilemap = new Tile.TileMap();
    public bool isTouch = false;
    public static bool isTouchOn = true;
    private Assets.Scripts.SpriteManager sm;
    public Animator skill_anim;
    private bool CheckSkill()
    {
        return skill_anim.GetCurrentAnimatorStateInfo(0).IsName("skill_ani");
    }

    public static void setTouch(bool touch)
    {
        isTouchOn = touch;
    }

    void Start()
    {
        new AdmobManager().ShowBannerAdFunc();

        touchBegin += (touches) =>
        {
            Touch touch = (Touch)touches[0];
            beginPos = touch.position;

            Debug.Log("begin");
            isTouch = true;
        };
        touchEnd += (touches) =>
        {
            Debug.Log("end");
        };
        touchMove += (touches) =>
        {
            Touch touch = (Touch)touches[0];
            movePos = touch.position;

            Vector2 pos = new Vector2(movePos.x - beginPos.x, movePos.y - beginPos.y);

            if (!isTouch)
            {
                return;
            }
            if (Mathf.Pow((pos.x * pos.x + pos.y * pos.y), 0.5f) > 1)
            {
                // ↖
                if (pos.x < 0 && pos.y > 0)
                {
                    if (!tilemap.isMoving)
                    {
                        isTouch = false;
                        tilemap.UpLeft();
                    }
                    Debug.Log("↖");
                }
                // ↗
                else if (pos.x > 0 && pos.y > 0)
                {
                    if (!tilemap.isMoving)
                    {
                        isTouch = false;
                        tilemap.UpRight();
                    }
                    Debug.Log("↗");
                }
                // ↙
                else if (pos.x < 0 && pos.y < 0)
                {
                    if (!tilemap.isMoving)
                    {
                        isTouch = false;
                        tilemap.DownLeft();
                    }
                    Debug.Log("↙");
                }
                // ↘
                else if (pos.x > 0 && pos.y < 0)
                {
                    if (!tilemap.isMoving)
                    {
                        isTouch = false;
                        tilemap.DownRight();
                    }
                    Debug.Log("↘");
                }
            }
        };
        //터치하면 각각 begin, end, move 호출
    }

    void Update()
    {
        if (!isTouchOn || !CheckSkill() || store.activeSelf || setting.activeSelf || achive.activeSelf || select.activeSelf)
            return;

#if UNITY_STANDALONE || true
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if(!tilemap.isMoving)
                tilemap.DownLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if(!tilemap.isMoving)
                tilemap.UpRight();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(!tilemap.isMoving)
                tilemap.UpLeft();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (!tilemap.isMoving) 
                tilemap.DownRight();
        }
        
#endif

#if UNITY_ANDROID || UNITY_IOS || UNITY_WP8 || UNITY_WP8_1
        int count = Input.touchCount;
        if (count == 0) return;

        //이벤트를 체크할 플래그
        bool begin, move, end;
        begin = move = end = false;

        //인자로 보낼 ArrayList;
        ArrayList result = new ArrayList();

        for (int i = 0; i < count; i++)
        {

            Touch touch = Input.GetTouch(i);
            result.Add(touch); //보낼 인자에 추가
            if (touch.phase == TouchPhase.Began && touchBegin != null) begin = true;
            else if (touch.phase == TouchPhase.Moved && touchMove != null) move = true;
            else if (touch.phase == TouchPhase.Ended && touchEnd != null) end = true;
        }

        //포인트중 하나라도 상태를 가졌다면..
        if (begin) touchBegin(result);
        if (end) touchEnd(result);
        if (move) touchMove(result);
#endif
    }
}