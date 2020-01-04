using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform blockHolder;
    private bool isPause = true;
    private bool isTimeDown = false;
    private Ctrl ctrl;
    public Shape1 currentShape1 = null;
    public Shape2 currentShape2 = null;
    public Shape3 currentShape3 = null;
    public Shape1[] shapes;
    public Shape2[] shapEs;
    public Shape3[] shaPes;
    public Color[] colors;
    public int TotalTime1 = 20;
    public int itemtime = 0;//中间变量，实现暂停倒计时静止
    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(CountDown());
    }

    IEnumerator CountDown()
    {
        while (TotalTime1<21)//目的是让倒计时一直执行（但到0会结束游戏）
        {   
            yield return new WaitForSeconds(1);
            TotalTime1--;
        }
    }

    void Awake()
    {
        ctrl = GetComponent<Ctrl>();
        blockHolder = transform.Find("BlockHolder");
    }

    // Update is called once per frame
    void Update()
    {
        ctrl.model.SaveDate(ctrl.model.HighScore, ctrl.model.NumbersGame);
       //暂停状态倒计时静止（通过记录按下暂停键时倒计时的值实现，暂停后其实倒计时还在运行，只是前后效果是静止）
        if (isPause == true)
        {
            isTimeDown = false;
            return;
        }
        //自动产生物体
        if (currentShape1==null)
           SpawnShape1();
        if (currentShape2 == null)
            SpawnShape2();
        if(currentShape3 == null)
            SpawnShape3();
        //在非暂停状态倒计时继续
        if (isPause == false)
        {
            ctrl.view.UpDateTime(TotalTime1);
            isTimeDown = true;
        }
        //倒计时结束暂停并且弹出结算界面
        if(TotalTime1==0)
        {   
            isPause = true;
            if (ctrl.model.isHighScoreUpdate)
                ctrl.audioManager.Playpojilu();
            if(!ctrl.model.isHighScoreUpdate)
                ctrl.audioManager.Playjieshu();
            ctrl.view.ShowGameOverUI(ctrl.model.Score);
            ctrl.model.NumbersGameAdd();
            ctrl.model.SaveDate(ctrl.model.HighScore, ctrl.model.NumbersGame);
            TotalTime1 = -1;//防止多次调用结束
        }
        //销毁BlockHolder里的多余物体
        foreach(Transform t in blockHolder)
        {
            if (t.childCount == 0)
                Destroy(t.gameObject);
        }
    }
    //随机产生物体
    void SpawnShape1()
    {   //Shape1
        int index = Random.Range(0, shapes.Length);
        int indexColor = Random.Range(0, colors.Length);
        currentShape1 = GameObject.Instantiate(shapes[index]);
        currentShape1.Init(colors[indexColor], ctrl);
        currentShape1.transform.parent = blockHolder;
    }
    void SpawnShape2()
    {
        //Shape2
        int index2 = Random.Range(0, shapEs.Length);
        int indexColor2 = Random.Range(0, colors.Length);
        currentShape2 = GameObject.Instantiate(shapEs[index2]);
        currentShape2.Init(colors[indexColor2],ctrl);
        currentShape2.transform.parent = blockHolder;
    }
    void SpawnShape3()
    { 
        //Shape3
        int index3 = Random.Range(0, shapEs.Length);
        int indexColor3 = Random.Range(0, colors.Length);
        currentShape3 = GameObject.Instantiate(shaPes[index3]);
        currentShape3.Init(colors[indexColor3],ctrl);
        currentShape3.transform.parent = blockHolder;
    }

    public void ResetShape1()
    {
        currentShape1 = null;
        ctrl.audioManager.Playputdown();
        if (ctrl.model.isDataUpDate)
        {
            ctrl.view.UpdateScore(ctrl.model.Score, ctrl.model.HighScore);
        }
    }
    public void ResetShape2()
    {
        currentShape2= null;
        ctrl.audioManager.Playputdown();
        if (ctrl.model.isDataUpDate)
        {
            ctrl.view.UpdateScore(ctrl.model.Score, ctrl.model.HighScore);
        }
    }
    public void ResetShape3()
    {
        currentShape3 = null;
        ctrl.audioManager.Playputdown();
        if (ctrl.model.isDataUpDate)
        {
            ctrl.view.UpdateScore(ctrl.model.Score, ctrl.model.HighScore);
        }
    }
    public void ResetTime()
    {
        //分数更新时间重置
        if (ctrl.model.isTimeUpDate)
        {
            if(ctrl.model.score<2000)
               TotalTime1 = 20;
            if (ctrl.model.score >= 2000 && ctrl.model.score < 4000)
                TotalTime1 = 18;
            if (ctrl.model.score >= 4000 && ctrl.model.score < 6000)
                TotalTime1 = 16;
            if (ctrl.model.score >= 6000 && ctrl.model.score < 8000)
                TotalTime1 = 14;
            if (ctrl.model.score >= 8000 && ctrl.model.score < 10000)
                TotalTime1 = 12;
            if (ctrl.model.score >= 10000)
                TotalTime1 = 10;
            ctrl.model.isTimeUpDate = false;
            ctrl.audioManager.Playxiaochu();
            ctrl.model.isxiaochuMusic = false;  //播放完一次消除音乐后重置isxiaochuMusic
        }
    }
   
    public void StartGame()
    {
        isPause = false;
        if (itemtime > 0 && itemtime <= 20)
            TotalTime1 = itemtime;
        else
            TotalTime1 = 20;
    }
    public void RestartResetShape()
    {
        if (currentShape1 != null)
        {
            Destroy(currentShape1.transform.gameObject);
            currentShape1 = null;
        }
        if (currentShape2 != null)
        {
            Destroy(currentShape2.transform.gameObject);
            currentShape2 = null;
        }
        if (currentShape3 != null)
        {
            Destroy(currentShape3.transform.gameObject);
            currentShape3 = null;
        }
    }
    public void PauseGame()
    {
        isPause = true;
        if (TotalTime1 < 20)
            itemtime = TotalTime1;
    }
    public bool Psd()
    {
        return isPause;
    }
 
}
