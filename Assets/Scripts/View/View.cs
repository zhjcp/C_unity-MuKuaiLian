using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;//加载场景用于HomeButton
public class View : MonoBehaviour
{
    private RectTransform logoName;
    private RectTransform menuUI;
    private RectTransform gameUI;
    private GameObject restartButton;
    private GameObject gameOverUI;
    private GameObject rankUI;
    private Text gameOverScore;
    private Text highScore;
    private Text score;
    private Text time;
    private Text title;
    private Text rankScore;
    private Text rankHighScore;
    private Text rankNumbersGame;
    public bool IsRankExitClick = false;
    // Start is called before the first frame update

    void Awake()
    {
        logoName = transform.Find("Canvas/LogoName") as RectTransform;
        menuUI = transform.Find("Canvas/MenuUI") as RectTransform;
        gameUI = transform.Find("Canvas/GameUI/") as RectTransform;
        restartButton = transform.Find("Canvas/MenuUI/restartbutton").gameObject;
        gameOverUI = transform.Find("Canvas/GameOverUI").gameObject;
        rankUI = transform.Find("Canvas/RankUI").gameObject;
        score = transform.Find("Canvas/GameUI/ScoreLabel/Text").GetComponent<Text>();
        highScore = transform.Find("Canvas/GameUI/HighScoreLabel/Text").GetComponent<Text>();
        time = transform.Find("Canvas/GameUI/Time/Text").GetComponent<Text>();
        gameOverScore = transform.Find("Canvas/GameOverUI/Title/Text").GetComponent<Text>();
        title = transform.Find("Canvas/MenuUI/startbutton/Text").GetComponent<Text>();
        rankScore = transform.Find("Canvas/RankUI/ScoreLabel/text").GetComponent<Text>();
        rankHighScore = transform.Find("Canvas/RankUI/HighScoreLabel/text").GetComponent<Text>();
        rankNumbersGame = transform.Find("Canvas/RankUI/NumbersGameLabel/text").GetComponent<Text>();
    }

    public void ShowMenu()
    {
        logoName.gameObject.SetActive(true);
        logoName.DOAnchorPosY(-50.75f, 1f);
        menuUI.gameObject.SetActive(true);
        menuUI.DOAnchorPosY(43.05f, 1f);
    }
    public void HideMenu()
    {
        logoName.DOAnchorPosY(50.75f, 0.5f)
        .OnComplete(delegate { logoName.gameObject.SetActive(false); });
        menuUI.DOAnchorPosY(-43.05f, 0.5f)
        .OnComplete(delegate { menuUI.gameObject.SetActive(false); });
    }

    public void ShowGameUI(int score=0 ,int highScore=0)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
        gameUI.gameObject.SetActive(true);
        gameUI.DOAnchorPosY(-67.6f,1f);
    }
    public void UpdateScore(int score, int highScore)
    {
        this.score.text = score.ToString();
        this.highScore.text = highScore.ToString();
        
    }
    public void UpDateTime(int t)
    {
        this.time.text = t.ToString();
    }
    public void HideGameUI()
    {
     gameUI.DOAnchorPosY(67.6f, 0.5f)
               .OnComplete(delegate { gameUI.gameObject.SetActive(false); });
    }
    public void Showrestartbutton()
    {
        restartButton.SetActive(true);
        string item1 = "继续游戏";
        this.title.text = item1.ToString(); 
    }
    public void ShowGameOverUI(int gameoverscore=0)
    {
        gameOverUI.SetActive(true);
        gameOverScore.text = gameoverscore.ToString();
    }
    public void HideGameOverUI()
    {
        gameOverUI.SetActive(false);
    }
    public void OnHomeButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ShowRankUI(int score,int highscore,int numbersgame)
    {
        this.rankScore.text = score.ToString();
        this.rankHighScore.text = highscore.ToString();
        this.rankNumbersGame.text = numbersgame.ToString();
        rankUI.SetActive(true);
    }
    public void OnRankExitButtonClick()
    {
        IsRankExitClick = true;
        rankUI.SetActive(false);
    }
  
    // Update is called once per frame
    void Update()
    {
        
    }
}
