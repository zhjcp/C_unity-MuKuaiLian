using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ctrl : MonoBehaviour
{   [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;
    [HideInInspector]
    public GameManager gameManager;
    [HideInInspector]
    public AudioManager audioManager;
    private FSMSystem fsm;
    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        gameManager = GetComponent<GameManager>();
        audioManager = GetComponent<AudioManager>();
        MakeFSM();
      
    }

    private void start()//视频错误，makefsm要放在awake中
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

   
    void MakeFSM()
    {
        fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
        foreach (FSMState state in states)//遍历添加状态到状态机
        {
            fsm.AddState(state,this);//this添加当前对象即ctrl的引用
        }
        MenuState s = GetComponentInChildren<MenuState>();//设置菜单状态为默认状态
        fsm.SetCurrentState(s);
    }
   
}
