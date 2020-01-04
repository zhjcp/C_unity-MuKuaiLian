using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape3 : MonoBehaviour
{
    private Ctrl ctrl;
    private bool isMouseDown = false;
    private Vector3 lastMousePosition = Vector3.zero;
    private Vector3 startplace = Vector3.zero;

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        startplace = transform.position;
    }
    void OnMouseDrag()
    {
        if (ctrl.gameManager.Psd()!= true)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
            transform.position = pos.Round();
        }
    }
    void OnMouseUp()
    {
        if (ctrl.model.IsValidMapPosition(this.transform) == true)//若可以放进去
        {
            ctrl.model.PlaceInMap(this.transform);//放进去地图中
            ctrl.gameManager.ResetShape3();//置空物体
        }
        else
        {
            this.transform.position = startplace;
        }
        if (!ctrl.model.IsInsideMap(this.transform.position.Round()))
        {
            this.transform.position = startplace;
        }
        //分数更新时间重置
        ctrl.gameManager.ResetTime();
    }
    public void Init(Color color,Ctrl ctrl)
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
}
