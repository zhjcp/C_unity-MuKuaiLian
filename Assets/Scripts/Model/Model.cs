using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{ //public const int Maxrows= 20;
  //public const int Maxcolumns= 10;
    public Transform[,] map = new Transform[10,20];//虚拟的地图，用来记录游戏进行的状态
    private int[] DRows = new int[20];//存储要删除的行数
    private int[] DColumns = new int[10];//存储要删除的列数
    public int score = 0;
    public int highScore = 0;
    public int numbersGame = 0;
    public bool isDataUpDate = false;
    public bool isTimeUpDate = false;
    public bool isxiaochuMusic = false;
    public bool isHighScoreUpdate = false;
    //返回得到分数
    public int Score { get { return score; } }
    public int HighScore { get { return highScore; } }
    public int NumbersGame { get { return numbersGame; } }
    private void Awake()
    {
        LoadDate();
    }
    //判断是否能放进去
    public bool IsValidMapPosition(Transform t)
    {
        foreach (Transform child in t)//遍历当前图形的每一个孩子，即组成方块
        {
              if (child.tag != "Block") continue;//标签不是Block就开始下一次循环
              if(IsInsideMap(child.position.Round())==true)//物体在地图内且判断物体对应位置不能放下返回false(已解决bug-索引越界)
            {
                Vector2 pos1 = child.position.Round();
                while(map[(int)pos1.x, (int)pos1.y] != null)
                    return false;
            }
              else if(IsInsideMap(child.position.Round())!=true)//物体出现在地图外则返回false
                return false;
        }
        return true;
    }

    //判断物体对应位置是否在网格内
    public bool IsInsideMap(Vector2 pos)
    {
        return pos.x >=0 && pos.x <=9 && pos.y >= 0 && pos.y <=19;
    }

   //将物体置入地图
   public void PlaceInMap(Transform t)
    {
        foreach (Transform child in t)
        {
           if (child.tag != "Block") continue;
              Vector2 pos2 = child.position.Round();
              map[(int)pos2.x, (int)pos2.y] = child;                          
        }
        CheckMap();
    }

    //满行消除
    private void CheckMap()
    {   int count1 = 0;
        int count2 = 0;
        //遍历行
        for (int i = 0; i < 20; i++)
        {
            bool isRowFall = CheckRows(i);           
            if (isRowFall)
            {
                DRows[i] = 1;              
            }
        }
        //遍历列
        for (int i = 0; i < 10; i++)
        {
            bool isColumnFall = CheckColumns(i);
            if (isColumnFall)
            {
                DColumns[i] = 1;
            }
        }
        for (int i = 0; i < 20; i++)
        {
            if (DRows[i] == 1)
            {
                DeleteRows(i);
                DRows[i] = 0;
                count1++;
                isTimeUpDate = true;
                isxiaochuMusic = true;
            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (DColumns[i] == 1)
            {
                DeleteColumns(i);
                DColumns[i] = 0;
                count2++;
                isTimeUpDate = true;
                isxiaochuMusic = true;
            }
        }
        if (count1 > 0||count2>0)
        {
            if (score < 2000)
            {
                score += count1 * 100;
                score += count2 * 200;
            }
            if (score >= 2000&&score<4000)
            {
                score += count1 * 200;
                score += count2 * 400;
            }
            if (score >= 4000 && score < 6000)
            {
                score += count1 * 300;
                score += count2 * 600;
            }
            if (score >= 6000 && score < 8000)
            {
                score += count1 * 400;
                score += count2 * 800;
            }
            if (score >= 8000 && score < 10000)
            {
                score += count1 * 500;
                score += count2 * 1000;
            }
            if (score >= 10000)
            {
                score += count1 * 666;
                score += count2 * 1334;
            }
            isDataUpDate = true;
            if (score > highScore)
            {
                highScore = score;
                isHighScoreUpdate = true;
            }
        }
    }

    //判断满行
    private bool CheckRows(int row)
    {
        for (int i=0; i<10; i++)
        {
            if (map[i, row] == null)
                return false;
        }
        return true;
    }

    //销毁满行的虚拟地图和游戏物体
    private void DeleteRows(int row)
    {
        for (int i = 0; i < 10; i++)
        {   if(map[i, row]!=null)
            Destroy(map[i, row].gameObject);//删除游戏实际物体
            map[i, row] = null;//删除虚拟地图对应物体
        }
    }

    // 判断满列
    private bool CheckColumns(int column)
    {
        for (int i = 0; i < 20; i++)
        {
            if (map[column, i] == null)
                return false;
        }
        return true;
    }

    //销毁满列的虚拟地图和游戏物体
    private void DeleteColumns(int column)
    {   for (int i = 0; i < 20; i++)
        {   if(map[column, i]!=null)
            Destroy(map[column, i].gameObject);
            map[column, i] = null;              
         }
    }

    //加载数据
    private void LoadDate()
    {
        highScore= PlayerPrefs.GetInt("HighScore",0);
        numbersGame= PlayerPrefs.GetInt("NumberGames",0);
    }
    //保存数据
    public void SaveDate(int highScore,int numbersGame)
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.SetInt("NumbersGame", numbersGame);
    }

    public void Restart()
    {
        for (int i = 0; i < 10; i++)
            for (int j = 0; j < 20; j++)
            {
                if (map[i, j] != null)
                {
                    Destroy(map[i, j].gameObject);
                    map[i, j] = null;
                }
            }
        score = 0;
    }
    public void NumbersGameAdd()
    {
        numbersGame++;
    }
    //清空数据
    public void ClearDate()
    {
        highScore = 0;
        numbersGame = 0;
        SaveDate(0, 0);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
/*
//判断游戏结束:三个物体都不能放进地图中
public bool IsGameOver(Transform t1, Transform t2, Transform t3)
{
    if (!WhetherCurrentshape1Place(t1) && !WhetherCurrentshape2Place(t2) && !WhetherCurrentshape3Place(t3))
        return true;
    else
        return false;
}
//判断currentshape不能放进去地图中
//思路：给当前物体遍历匹配所有位置若可以放进去则true，否则false
//检验currentShape1
public bool WhetherCurrentshape1Place(Transform t)
{
    //循环遍历所有位置
    for (int i = 0; i < 10; i++)
        for (int j = 0; j < 20; j++)
        {
            t.position = new Vector2(i, j);
        }
    bool charge = IsValidMapPosition(t);
    if (charge)
        return true;
    else
        return false;
}
//检验currentShape2
public bool WhetherCurrentshape2Place(Transform t)
{
    //循环遍历所有位置
    for (int i = 0; i < 10; i++)
        for (int j = 0; j < 20; j++)
        {
            t.position = new Vector2(i, j);
        }
    bool charge = IsValidMapPosition(t);
    if (charge)
        return true;
    else
        return false;
}
//检验currentShape3
public bool WhetherCurrentshape3Place(Transform t)
{
    //循环遍历所有位置
    for (int i = 0; i < 10; i++)
        for (int j = 0; j < 20; j++)
        {
            t.position = new Vector2(i, j);
        }
    bool charge = IsValidMapPosition(t);
    if (charge)
        return true;
    else
        return false;
}
*/
