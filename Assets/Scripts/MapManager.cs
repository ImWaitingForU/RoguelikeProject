using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制地图和游戏整体逻辑
/// </summary>
public class MapManager : MonoBehaviour
{

    public GameObject[] outWallArray;
    public GameObject[] floorArray;
    public GameObject[] wallArray;
    public GameObject[] foodArray;
    public GameObject[] enemyArray;
    public GameObject exitObj;

    public int rows;
    public int cols;

    private List<Vector2> positionList;
    private Transform map;
    private GameManager gameManager;

    // Use this for initialization
    void Awake()
    {
        gameManager = this.GetComponent<GameManager>();
        InitMap();
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 初始化围墙 地板 障碍物 食物 敌人
    /// </summary>
    private void InitMap()
    {
        //获取要放障碍和敌人等物体的屏幕坐标集合
        positionList = new List<Vector2>();
        for (int x = 2; x < cols - 2; x++)
        {
            for (int y = 2; y < rows - 2; y++)
            {
                positionList.Add(new Vector2(x, y));
            }
        }

        //初始化围墙
        map = new GameObject("Map").transform;
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1)
                {
                    GameObject.Instantiate(GetRandomObject(outWallArray), new Vector2(x,y), Quaternion.identity).transform.SetParent(map);
                }
                else
                {
                    GameObject.Instantiate(GetRandomObject(floorArray), new Vector2(x, y), Quaternion.identity).transform.SetParent(map);
                }
            }
        }

        //初始化障碍物
        int wallCount = Random.Range(2, 8);  //随机生成2-8个障碍
        InitComponent(wallArray, wallCount);

        //初始化食物
        int foodCount = Random.Range(0, gameManager.level * 2 + 1);
        InitComponent(foodArray, foodCount);

        //初始化敌人
        int enemyCount = gameManager.level / 2 + 1;
        InitComponent(enemyArray, enemyCount);

        //初始化出口
        GameObject.Instantiate(exitObj, new Vector2(cols-2, rows-2), Quaternion.identity).transform.SetParent(map);
    }

    //获取随机位置
    private Vector2 GetRandomPosition()
    {
        int positionIndex = Random.Range(0, positionList.Count);
        Vector2 pos = positionList[positionIndex];
        positionList.RemoveAt(positionIndex);
        return pos;
    }

    //获取随机对象
    private GameObject GetRandomObject(GameObject[] objectPrefabs)
    {
        int index = Random.Range(0, objectPrefabs.Length);
        return objectPrefabs[index];
    }

    //初始化游戏组件,并设置父组件为map
    //objArray:要初始化的地图资源
    private void InitComponent(GameObject[] objArray,int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject.Instantiate(GetRandomObject(objArray), GetRandomPosition(), Quaternion.identity).transform.SetParent(map);
        }
    }
}
