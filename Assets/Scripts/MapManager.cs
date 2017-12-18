using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 控制地图和游戏整体逻辑
/// </summary>
public class MapManager : MonoBehaviour {

    public GameObject[] outWallArray;
    public GameObject[] floorArray;
    public GameObject[] wallArray;

    public int rows;
    public int cols;

    private List<Vector2> positionList;
    private Transform map;

	// Use this for initialization
	void Start () {
        InitMap();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// 初始化围墙 地板 障碍物 食物 敌人
    /// </summary>
    private void InitMap()
    {
        #region 初始化围墙
        map = new GameObject("Map").transform;
        for (int x = 0; x < cols; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                if (x == 0 || y == 0 || x == cols - 1 || y == rows - 1)
                {
                    int index = Random.Range(0, outWallArray.Length);
                    GameObject go = GameObject.Instantiate(outWallArray[index], new Vector3(x, y, 0), Quaternion.identity);
                    go.transform.SetParent(map);
                }
                else
                {
                    int index = Random.Range(0, floorArray.Length);
                    GameObject go = GameObject.Instantiate(floorArray[index], new Vector3(x, y, 0), Quaternion.identity);
                    go.transform.SetParent(map);
                }
            }
        }
        #endregion

        #region 获取要放障碍和敌人等物体的屏幕坐标集合
        positionList = new List<Vector2>();
        for (int x = 2; x < cols-2; x++)
        {
            for (int y = 2; y < rows-2; y++)
            {
                positionList.Add(new Vector2(x,y));
            }
        }
        #endregion

        #region 初始化障碍物
        int wallCount = Random.Range(2, 8);  //随机生成2-8个障碍
        for (int i = 0; i < wallCount; i++)
        {
            int positionIndex = Random.Range(0,positionList.Count);
            Vector2 position = positionList[positionIndex];
            int wallIndex = Random.Range(0, wallArray.Length);
            GameObject.Instantiate(wallArray[wallIndex], position, Quaternion.identity).transform.SetParent(map);
        }
        
        #endregion
    }
}
