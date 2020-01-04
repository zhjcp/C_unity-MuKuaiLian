using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayState : FSMState
{
    private void Awake()
    {
        stateID = StateID.Play;
        AddTransition(Transition.pausebuttonClick, StateID.Menu);
    }//设置状态ID
    public override void DoBeforeEntering()
    {
        ctrl.view.ShowGameUI(ctrl.model.Score,ctrl.model.HighScore);//把最高分输入进去
        ctrl.gameManager.StartGame();
    }
    public override void DoBeforeLeaving()
    {
        ctrl.view.HideGameUI();
        ctrl.view.Showrestartbutton();
        ctrl.gameManager.PauseGame();
    }
    public void OnpausebuttonClick()
    {
        ctrl.audioManager.Playbuttonclick();
        fsm.PerformTransition(Transition.pausebuttonClick);
    }
    public void OnRestartButtonClick()
    {
        ctrl.audioManager.Playbuttonclick();
        ctrl.view.HideGameOverUI();
        ctrl.gameManager.RestartResetShape();
        ctrl.model.Restart();
        ctrl.gameManager.StartGame();
        ctrl.view.UpdateScore(0, ctrl.model.HighScore);
    }
}


