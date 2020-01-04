using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape1 : MonoBehaviour
{
    [HideInInspector]
    public GameManager gameManager;
    private Ctrl ctrl;
    private bool isMouseDown = false;
    private bool InStartPlace = true;
    private Vector3 lastMousePosition = Vector3.zero;
    private static Vector3 startplace = Vector3.zero;

    //设置颜色
    public void Init(Color color, Ctrl ctrl)
    {
        foreach (Transform t in transform)
        {
            if (t.tag == "Block")
            {
                t.GetComponent<SpriteRenderer>().color = color;
            }
        }
        this.ctrl = ctrl;
    }

    void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnMouseDown()//记录选中物体时物体的初始位置
    {
        startplace = transform.position;
    }
    void OnMouseDrag()//物体同步鼠标位置
    {       
        if(InStartPlace==true)//禁止移动已经放下去的积木
        {
            if (ctrl.gameManager.Psd() != true)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
                transform.position = pos.Round();
            }
        }
    }
    void OnMouseUp()//放下物体后的操作与判断
    {
        if (ctrl.model.IsValidMapPosition(this.transform) == true)//若可以放进去
        {
            ctrl.model.PlaceInMap(this.transform);//放进去地图中
            ctrl.gameManager.ResetShape1();//置空物体
            InStartPlace = false;
        }
        else
        {  
            this.transform.position = startplace;
        }
        if(!ctrl.model.IsInsideMap(this.transform.position.Round()))
        {
            this.transform.position = startplace;
        }
        //分数更新时间重置
        ctrl.gameManager.ResetTime();
    }
    
}



/*标按下
        if (Input.GetMouseButtonDown(0))
        {
          
                isMouseDown1 = true;
            
        }
        //鼠标松开
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDown1 = false;
            lastMousePosition = Vector3.zero;
        }
        //拖拽事件
        if (isMouseDown1)
        { 
            if (lastMousePosition != Vector3.zero)
            {
                Vector3 offset = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastMousePosition;
                this.transform.position += offset;
            }
            lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         }

  if (ctrl.gameManager.Psd() == true)
            isMouseDown = false;
        if (isMouseDown)
        {
               
        }

        void OnMouseDown()//若鼠标点击在物体上
    {   //当点击触发，将ismousedown置为true
        if (!isMouseDown)
            isMouseDown = true;//从非拖拽状态切换到拖拽状态
        else
        {
            isMouseDown = false;//从拖拽状态切换到放置状态            
        }
    }
*/
