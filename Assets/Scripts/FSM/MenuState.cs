using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuState : FSMState
{
   
    private void Awake()
    {
        stateID = StateID.Menu;//设置状态ID
        AddTransition(Transition.startbuttonClick, StateID.Play);//状态转换
    }

    public override void DoBeforeEntering()
    {
        ctrl.view.ShowMenu();
        
    }

    public override void DoBeforeLeaving()
    {
        ctrl.view.HideMenu();
    }
    public void OnstartbuttonClick()
    {
        ctrl.audioManager.Playbuttonclick();
        fsm.PerformTransition(Transition.startbuttonClick);//不能隐藏菜单，实例未申明error
    }
    public void OnMenuRestartButton()
    {
        ctrl.audioManager.Playbuttonclick();
        ctrl.model.NumbersGameAdd();
        ctrl.model.SaveDate(ctrl.model.HighScore,ctrl.model.NumbersGame);
        ctrl.model.Restart();
        ctrl.gameManager.RestartResetShape();
        ctrl.gameManager.StartGame();
        ctrl.view.UpdateScore(0, ctrl.model.HighScore);
        ctrl.gameManager.itemtime = 20;
        ctrl.gameManager.TotalTime1 = 20;
        fsm.PerformTransition(Transition.startbuttonClick);
    }
    public void OnRankButtonClick()
    {
        ctrl.audioManager.Playbuttonclick();
        ctrl.view.ShowRankUI(ctrl.model.Score, ctrl.model.HighScore, ctrl.model.NumbersGame);
    }
    //清空数据
    public void OnRankDeleButtonClick()
    {        
       ctrl.model.ClearDate();
        OnRankButtonClick();
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
    public void OnRealMusicButton()
    {
        if (!ctrl.audioManager.isMute)
        {
            ctrl.audioManager.isMute = true;
            return;
        }

        if (ctrl.audioManager.isMute)
        {
            ctrl.audioManager.isMute = false;
            return;
        }
    }
    void Update()
    {
        if (ctrl.view.IsRankExitClick)
        {
            ctrl.audioManager.Playbuttonclick();
            ctrl.view.IsRankExitClick = false;
        }
    }
}


