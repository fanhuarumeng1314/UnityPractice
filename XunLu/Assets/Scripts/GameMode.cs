using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

class SearchData
{
    public int distance;

}

public class Pos
{
    public int x;
    public int y;
}

public class AStart
{
    public Pos pos;
    public int G;
    public int H;
    public Pos Parent = null;
    public AStart()
    {

    }
    public AStart(int g,int h)
    {
        G = g;
        H = h;
    }

    public int F
    {
        get
        {
            return G + H;
        }
    }
}

public class GameMode : MonoBehaviour {

    public int[,] map = new int[28,28];
    public GameObject cube;
    public GameObject target;
    public GameObject tmp_Cube;
    public GameObject line_Cube;
    List<Pos> tasks = new List<Pos>();
    List<GameObject> map2 = new List<GameObject>();//存储地图上所有寻路产生的对象
    //Dictionary<int[], int> tmp_Map = new Dictionary<int[], int>();
    public List<Vector3> road = new List<Vector3>();//存储路线
    SearchData[,] sign = new SearchData[28, 28];
    bool isLine = false;

    public bool isWangCheng = true;

    void Start ()
    {
        ReadFlie();
        Print();
    }
	
	
	void Update ()
    {
        if (Input.GetButtonDown("Fire1")&&isWangCheng)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            

            if (Physics.Raycast(ray,out hit))
            {
                if (hit.transform.gameObject.tag =="Finish")
                {
                    isWangCheng = false;

                    Delete();
                    
                    var point = hit.point;
                    var tmp_Map2 = Instantiate(target,new Vector3((int)point.x,0,(int)point.z),Quaternion.identity) as GameObject;
                    map2.Add(tmp_Map2);
                    StartCoroutine(ChaZhao3((int)point.z, (int)point.x));
                    

                }
            }
        }
        #region//废弃方法
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    ChaZhao(15,25);//X Z相反，反向输入
        //}

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    int[] origin = new int[2] { 1, 1 };
        //    tasks.Add(origin);

        //    StartCoroutine(ChaZhao2(15, 15));


        //}
        #endregion


        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(ChaZhao4());
        }
    }

    public void ReadFlie()
    {
        string path = Application.dataPath + "//" + "Map2.txt";

        if (!File.Exists(path))
        {
            return;
        }


        FileStream fs = new FileStream(path,FileMode.Open,FileAccess.Read);

        StreamReader read = new StreamReader(fs, Encoding.Default);

        string strReadline;
        int y = 0;

        while ((strReadline = read.ReadLine()) != null)
        {

            for (int x = 0; x < strReadline.Length; ++x)
            {
                if (strReadline[x] == '#')
                {
                    map[x, y] = 1;
                }
                if (strReadline[x] == 'M')
                {
                    map[x, y] = 2;
                }
            }
            y += 1;
            // strReadline即为按照行读取的字符串
        }

        fs.Close();
        read.Close();
    }

    public void Print()
    {


        for (int i = 0; i < 28; i++)
        {
            for (int j = 0; j < 28; j++)
            {
                if (map[i, j] == 1)
                {
                    Instantiate(cube, new Vector3(j, 0, i), Quaternion.identity);
                }
                //if (map[i, j] == 2)
                //{
                //    Instantiate(target, new Vector3(j, 0, i), Quaternion.identity);
                //}
                
            }
        }

    }
#region//方法废弃1
//    public void ChaZhao(int x, int y)//查找1  废弃方法
//    {
//        int[,] tmp_Map = new int[28, 28];
//        tmp_Map[1, 1] = 1;
//        bool isZhaoDao = false;
//#region//查找目标位置与原点之间的距离，并记录
//        while (!isZhaoDao)
//        {
//            for (int i=0;i<28;i++)
//            {
//                for (int j=0;j<28;j++)
//                {

//                    if (tmp_Map[i,j] !=0)
//                    {
//                        if (i-1>=0)
//                        {
//                            if (map[i-1,j]!=1)
//                            {
//                                if (tmp_Map[i - 1, j]==0)
//                                {
//                                    tmp_Map[i - 1, j] = tmp_Map[i, j] + 1;
//                                }
//                            }
//                            if (i-1==x&&j==y)
//                            {
//                                isZhaoDao = true;
//                            }
//                        }

//                        if (i+1<28)
//                        {
//                            if (map[i + 1, j] != 1)
//                            {
//                                if (tmp_Map[i + 1, j]==0)
//                                {
//                                    tmp_Map[i + 1, j] = tmp_Map[i, j] + 1;
//                                }
//                            }
//                            if (i + 1 == x && j == y)
//                            {
//                                isZhaoDao = true;
//                            }
//                        }

//                        if (j-1>=0)
//                        {
//                            if (map[i,j-1]!=1)
//                            {
//                                if (tmp_Map[i, j - 1]==0)
//                                {
//                                    tmp_Map[i, j - 1] = tmp_Map[i, j] + 1;
//                                }
//                            }

//                            if (j - 1 == x && j == y)
//                            {
//                                isZhaoDao = true;
//                            }

//                        }

//                        if (j + 1 < 28)
//                        {
//                            if (map[i, j + 1] != 1)
//                            {
//                                if (tmp_Map[i, j + 1] == 0)
//                                {
//                                    tmp_Map[i, j + 1] = tmp_Map[i, j] + 1;
//                                }
//                            }

//                            if (j + 1 == x && j == y)
//                            {
//                                isZhaoDao = true;
//                            }
//                        }


//                    }


//                }
//            }
//        }
//        #endregion

//#region//通过目标位置与原点的距离进行画线
//        int tmp_x = x;
//        int tmp_y = y;
//        bool isWanCheng = false;

//        while (!isWanCheng)
//        {
//            if (tmp_Map[tmp_x-1,tmp_y]==tmp_Map[tmp_x,tmp_y]-1)
//            {
//                tmp_x = tmp_x - 1;

//                if (tmp_x==1&&tmp_y==1)
//                {
//                    isWanCheng = true;
//                }
//                Instantiate(tmp_Cube,new Vector3(tmp_y,0,tmp_x),Quaternion.identity);
//                continue;
//            }

//            if (tmp_Map[tmp_x + 1, tmp_y] == tmp_Map[tmp_x, tmp_y] - 1)
//            {
//                tmp_x = tmp_x + 1;

//                if (tmp_x == 1 && tmp_y == 1)
//                {
//                    isWanCheng = true;
//                }
//                Instantiate(tmp_Cube, new Vector3(tmp_y, 0, tmp_x), Quaternion.identity);
//                continue;
//            }

//            if (tmp_Map[tmp_x, tmp_y-1] == tmp_Map[tmp_x, tmp_y] - 1)
//            {
//                tmp_y = tmp_y - 1;
//                if (tmp_x == 1 && tmp_y == 1)
//                {
//                    isWanCheng = true;
//                }
//                Instantiate(tmp_Cube, new Vector3(tmp_y, 0, tmp_x), Quaternion.identity);
//                continue;
//            }

//            if (tmp_Map[tmp_x, tmp_y + 1] == tmp_Map[tmp_x, tmp_y] - 1)
//            {
//                tmp_y = tmp_y + 1;
//                if (tmp_x == 1 && tmp_y == 1)
//                {
//                    isWanCheng = true;
//                }
//                Instantiate(tmp_Cube, new Vector3(tmp_y, 0, tmp_x), Quaternion.identity);
//                continue;

//            }

//        }
//#endregion



//    }
#endregion
#region//方法废弃2
    //public IEnumerator ChaZhao2(int x,int y)//查找2  废弃方法
    //{
    //    int[] origin = new int[2] { 1, 1 };
    //    int distance = 0;
    //    while (tasks.Count>0)
    //    {
    //        ++distance;
    //        if (tasks[0][0]-1>=0)
    //        {
    //            if (map[tasks[0][0] - 1,tasks[0][1]]!=1)
    //            {

    //                int[] tmp_Pos = new int[2] { tasks[0][0] - 1, tasks[0][1] };
    //                if (tasks.Contains(tmp_Pos)|tmp_Pos==origin)
    //                {
    //                    Debug.Log("sasas");
    //                    continue;
    //                }
    //                tasks.Add(tmp_Pos);
    //                tmp_Map.Add(tmp_Pos, distance);
    //                Instantiate(tmp_Cube,new Vector3(tasks[0][1],0,tasks[0][0]-1),Quaternion.identity);
    //                if (tasks[0][0] - 1 == x && tasks[0][1] == y)
    //                {
    //                    tasks.Clear();
    //                    yield return null;
    //                    break;
    //                }
    //                yield return 0;
    //            }
    //        }

    //        if (tasks[0][0] + 1 < 28)
    //        {
    //            if (map[tasks[0][0] + 1, tasks[0][1]] != 1)
    //            {
    //                int[] tmp_Pos = new int[2] { tasks[0][0] + 1, tasks[0][1] };
    //                if (tasks.Contains(tmp_Pos) | tmp_Pos == origin)
    //                {
    //                    Debug.Log("sasas");
    //                    continue;
    //                }
    //                tasks.Add(tmp_Pos);
    //                tmp_Map.Add(tmp_Pos, distance);
    //                Instantiate(tmp_Cube, new Vector3(tasks[0][1], 0, tasks[0][0] + 1), Quaternion.identity);
    //                if (tasks[0][0]+1 == x && tasks[0][1] == y)
    //                {
    //                    tasks.Clear();
    //                    yield return null;
    //                    break;
    //                }
    //                yield return 0;
    //            }
    //        }

    //        if (tasks[0][1]-1>=0)
    //        {
    //            if (map[tasks[0][0], tasks[0][1]-1]!=1)
    //            {
    //                int[] tmp_Pos = new int[2] { tasks[0][0], tasks[0][1]-1 };
    //                if (tasks.Contains(tmp_Pos) | tmp_Pos == origin)
    //                {
    //                    Debug.Log("sasas");
    //                    continue;
    //                }
    //                tasks.Add(tmp_Pos);
    //                tmp_Map.Add(tmp_Pos, distance);
    //                Instantiate(tmp_Cube, new Vector3(tasks[0][1]-1, 0, tasks[0][0]), Quaternion.identity);
    //                if (tasks[0][0] == x && tasks[0][1] - 1 == y)
    //                {
    //                    tasks.Clear();
    //                    yield return null;
    //                    break;
    //                }
    //                yield return 0;
    //            }


    //        }


    //        if (tasks[0][1] + 1 < 28)
    //        {

    //            if (map[tasks[0][0], tasks[0][1] + 1] != 1)
    //            {
    //                int[] tmp_Pos = new int[2] { tasks[0][0], tasks[0][1] + 1 };
    //                if (tasks.Contains(tmp_Pos) | tmp_Pos == origin)
    //                {
    //                    Debug.Log("sasas");
    //                    continue;
    //                }
    //                tasks.Add(tmp_Pos);
    //                tmp_Map.Add(tmp_Pos, distance);
    //                Instantiate(tmp_Cube, new Vector3(tasks[0][1]+1, 0, tasks[0][0]), Quaternion.identity);
    //                if (tasks[0][0]==x&&tasks[0][1]+1==y)
    //                {
    //                    tasks.Clear();
    //                    yield return null;
    //                    break;

    //                }
    //                yield return 0;
    //            }
    //        }

    //        tasks.RemoveAt(0);

    //    }


    //    #region
    //    //int[] tmp_key = new int[2] { x, y };
    //    //

    //    //while (tmp_key != origin)
    //    //{
    //    //    int[] tmp_KeyUp = new int[2] { tmp_key[0] - 1, tmp_key[1] };
    //    //    int[] tmp_KeyDown = new int[2] { tmp_key[0] + 1, tmp_key[1] };
    //    //    int[] tmp_KeyLeft = new int[2] { tmp_key[0], tmp_key[1] - 1 };
    //    //    int[] tmp_KeyRight = new int[2] { tmp_key[0], tmp_key[1] + 1 };

    //    //    if (tmp_Map.ContainsKey(tmp_KeyUp))
    //    //    {
    //    //        if (tmp_Map[tmp_KeyUp] < tmp_Map[tmp_key])
    //    //        {
    //    //            Debug.Log("sasas");
    //    //            Instantiate(tmp_Cube, new Vector3(tmp_KeyUp[1], 0, tmp_KeyUp[0]), Quaternion.identity);
    //    //            tmp_key = tmp_KeyUp;
    //    //            continue;
    //    //        }

    //    //    }

    //    //    if (tmp_Map.ContainsKey(tmp_KeyDown))
    //    //    {
    //    //        if (tmp_Map[tmp_KeyDown] < tmp_Map[tmp_key])
    //    //        {
    //    //            Instantiate(tmp_Cube, new Vector3(tmp_KeyDown[1], 0, tmp_KeyDown[0]), Quaternion.identity);
    //    //            tmp_key = tmp_KeyDown;
    //    //            continue;
    //    //        }

    //    //    }

    //    //    if (tmp_Map.ContainsKey(tmp_KeyLeft))
    //    //    {
    //    //        if (tmp_Map[tmp_KeyLeft] < tmp_Map[tmp_key])
    //    //        {
    //    //            Instantiate(tmp_Cube, new Vector3(tmp_KeyLeft[1], 0, tmp_KeyLeft[0]), Quaternion.identity);
    //    //            tmp_key = tmp_KeyLeft;
    //    //            continue;
    //    //        }

    //    //    }

    //    //    if (tmp_Map.ContainsKey(tmp_KeyRight))
    //    //    {
    //    //        if (tmp_Map[tmp_KeyRight] < tmp_Map[tmp_key])
    //    //        {
    //    //            Instantiate(tmp_Cube, new Vector3(tmp_KeyRight[1], 0, tmp_KeyRight[0]), Quaternion.identity);
    //    //            tmp_key = tmp_KeyRight;
    //    //            continue;
    //    //        }

    //    //    }

    //    //}

    //    #endregion
    //}
    #endregion
    public IEnumerator ChaZhao3(int x,int y)//查找3
    {
        Pos origin = new Pos();//添加原点
        origin.x = 1;
        origin.y = 1;

        tasks.Add(origin);
        int tmpX = x;
        int tmpY = y;

        List<Pos> tmp_Tasks = new List<Pos>();//声明临时列表用来存储不同批次的任务
 
        SearchData originData = new SearchData();//生成原点的标志，就是原点在标志数组的代表
        originData.distance = 0;
        sign[1, 1] = originData;//原点附上标志

        int distance = 1;
        while (tasks.Count>0)//当任务列表为0结束
        {
            if (tasks[0].x-1>=0)
            {
                if (map[tasks[0].x - 1, tasks[0].y] != 1 && sign[tasks[0].x-1,tasks[0].y]==null)//如果下一个探索目标地图里面不是墙，且标志数组里面没有存储这个点的标志
                {
                    Pos tmp_Pos = new Pos();
                    tmp_Pos.x = tasks[0].x - 1;
                    tmp_Pos.y = tasks[0].y;
                    SearchData tmp_SearchData = new SearchData();//实例标志
                    tmp_SearchData.distance = distance;
                    sign[tasks[0].x - 1, tasks[0].y] = tmp_SearchData;//添加这个点的标志

                    tmp_Tasks.Add(tmp_Pos);//添加任务
                    var tmp_Map2 = Instantiate(tmp_Cube, new Vector3(tasks[0].y, 0, tasks[0].x - 1), Quaternion.identity) as GameObject;
                    map2.Add(tmp_Map2);//将实例的物体放入列表统一管理，方便删除
                    if (tasks[0].x - 1 == x && tasks[0].y == y)//当下一个探索目标是目标位置，结束循环
                    {
                        break;
                    }
                }

            }


            if (tasks[0].x + 1 < 28)
            {

                if (map[tasks[0].x + 1, tasks[0].y] != 1 && sign[tasks[0].x + 1, tasks[0].y] == null)
                {

                    Pos tmp_Pos = new Pos();
                    tmp_Pos.x = tasks[0].x + 1;
                    tmp_Pos.y = tasks[0].y;
                    SearchData tmp_SearchData = new SearchData();
                    tmp_SearchData.distance = distance;
                    sign[tasks[0].x + 1, tasks[0].y] = tmp_SearchData;
                    tmp_Tasks.Add(tmp_Pos);
                    var tmp_Map2 = Instantiate(tmp_Cube, new Vector3(tasks[0].y, 0, tasks[0].x+1), Quaternion.identity) as GameObject;
                    map2.Add(tmp_Map2);
                    if (tasks[0].x + 1 == x && tasks[0].y == y)
                    {
                       // tasks.Clear();
                        break;
                    }

                }
     

            }

            if (tasks[0].y-1>=0)
            {

                if (map[tasks[0].x, tasks[0].y - 1] != 1 && sign[tasks[0].x, tasks[0].y-1] == null)
                {
                    Pos tmp_Pos = new Pos();
                    tmp_Pos.x = tasks[0].x;
                    tmp_Pos.y = tasks[0].y - 1;

                    SearchData tmp_SearchData = new SearchData();
                    tmp_SearchData.distance = distance;
                    sign[tasks[0].x, tasks[0].y-1] = tmp_SearchData;
                    tmp_Tasks.Add(tmp_Pos);
                    var tmp_Map2 = Instantiate(tmp_Cube, new Vector3(tasks[0].y-1, 0, tasks[0].x), Quaternion.identity) as GameObject;
                    map2.Add(tmp_Map2);
                    if (tasks[0].x == x && tasks[0].y-1 == y)
                    {
                        // tasks.Clear();
                        break;
                    }

                }
           

            }

            if (tasks[0].y + 1 < 28)
            {

                if (map[tasks[0].x, tasks[0].y + 1] != 1 && sign[tasks[0].x, tasks[0].y + 1] == null)
                {
                    Pos tmp_Pos = new Pos();
                    tmp_Pos.x = tasks[0].x;
                    tmp_Pos.y = tasks[0].y + 1;


                    SearchData tmp_SearchData = new SearchData();
                    tmp_SearchData.distance = distance;
                    sign[tasks[0].x, tasks[0].y + 1] = tmp_SearchData;
                    tmp_Tasks.Add(tmp_Pos);
                    var tmp_Map2 = Instantiate(tmp_Cube, new Vector3(tasks[0].y+1, 0, tasks[0].x), Quaternion.identity) as GameObject;
                    map2.Add(tmp_Map2);
                    if (tasks[0].x == x && tasks[0].y+1 == y)
                    {
                        // tasks.Clear();
                        break;
                    }

                }
              

            }

            tasks.RemoveAt(0);//删除任务列表的首位
            yield return 0;

            if (tasks.Count<=0)//当任务列表的任务为0，从临时列表中获取下一步的任务
            {
                for (int i =0;i<tmp_Tasks.Count;i++)
                {
                    var tmp_task = tmp_Tasks[i];
                    tasks.Add(tmp_task);
                }

                tmp_Tasks.Clear();//清空临时任务列表
                distance++;//更改距离
            }

        }

        tasks.Clear();//清空任务列表的任务，防止下次进来造成错误
        Debug.Log(distance);
        //Debug.Log(sign[1,26].distance);
        StartCoroutine(HuaXian(tmpX, tmpY));//X,Z坐标颠倒  现在查找的目标坐标数（y，0，x）
        

        yield return null;

    }

    public IEnumerator HuaXian(int x, int y)//画线
    {
        while (sign[x,y].distance>0)
        {
            var tmp_Map2 = Instantiate(line_Cube, new Vector3(y, 0, x), Quaternion.identity) as GameObject;
            map2.Add(tmp_Map2);
            road.Add(new Vector3(y, 0, x));
            if (x-1>=0&& sign[x - 1, y]!=null)//如果x的坐标不超过数组界限，切sign数组中记录的这个坐标的步数不是空
            {
                if (sign[x - 1, y].distance < sign[x, y].distance)//探索自己身边的4个点，发现有个点的标志的步数小于自己，就实例化一个物体，并以这个点为目标进行下一次探索
                {
                    
                    x = x - 1;
                    
                    yield return 0;
                    continue;
                }
            }

            if (x+1<28 && sign[x + 1, y] != null)
            {
                if (sign[x+1,y].distance < sign[x,y].distance)
                {
                    x = x + 1;
                   
                    yield return 0;
                    continue;
                }

            }

            if (y-1>=0 && sign[x, y-1] != null)
            {
                if (sign[x,y-1].distance < sign[x,y].distance)
                {
                    y = y - 1;
                   
                    yield return 0;
                    continue;
                }

            }

            if (y+1<28 && sign[x, y + 1] != null)
            {
                if (sign[x, y + 1].distance < sign[x, y].distance)
                {
                    y = y + 1;
                   
                    yield return 0;
                    continue;
                }

            }



        }

        Debug.Log(sign[x, y].distance);
        if (sign[x - 1, y] != null)
        {
            Debug.Log(sign[x - 1, y].distance);
        }
        if (sign[x + 1, y] != null)
        {
            Debug.Log(sign[x + 1, y].distance);
        }
        if (sign[x, y - 1] != null)
        {
            Debug.Log(sign[x, y - 1].distance);
        }
        if (sign[x, y + 1] != null)
        {
            Debug.Log(sign[x, y + 1].distance);
        }

        isWangCheng = true;

        yield return null;


        

    }

    public void Delete()
    {
        for (int i=0;i<28;i++)
        {
            for (int j =0;j<28;j++)
            {
                sign[i, j] = null;
            }
        }


        if (map2.Count > 0)
        {
            for (int i = 0; i < map2.Count; i++)
            {
                Destroy(map2[i]);
            }
            map2.Clear();
        }

    }

    public IEnumerator ChaZhao4()//查找4  使用A*算法
    {
        List<AStart> start = new List<AStart>();
        List<AStart> endGame = new List<AStart>();
        bool isLookUp = false;//是否找到终点
        AStart endLattice = null;//结束的格子
        Pos zero = new Pos();//设置终点
        zero.x = 1;
        zero.y = 26;

        AStart orign = new AStart(0,0);//设置起点
        Pos point = new Pos();
        point.x = 1;
        point.y = 1;
        orign.pos = point;
        start.Add(orign);


        while (!isLookUp)//当没有找到终点，游戏继续
        {
#region //判断能否进行某个方向探测的bool变量
            bool isLeft = true;
            bool isRight = true;
            bool isUp = true;
            bool isDown = true;
#endregion

            var tmp_Astart = start[0];
            foreach (var pair in start)
            {
                
                if (pair.pos.x== zero.x && pair.pos.y== zero.y)
                {
                    isLookUp = true;
                    endLattice = pair;
                    Debug.Log("寻路完成");
                    break;
                }

                if (tmp_Astart.F>pair.F)
                {
                    tmp_Astart = pair;
                }

            }
#region//判断下一个点是否存在2个列表当中
            foreach (var endGrid in endGame)  //判断目标的下一个探测区域是否存在于探测完毕的列表
            {
                if (endGrid.pos.x == tmp_Astart.pos.x - 1 && endGrid.pos.y == tmp_Astart.pos.y)
                {
                    isLeft = false;
                }
                if (endGrid.pos.x == tmp_Astart.pos.x + 1 && endGrid.pos.y == tmp_Astart.pos.y)
                {
                    isRight = false;
                }
                if (endGrid.pos.x == tmp_Astart.pos.x && endGrid.pos.y == tmp_Astart.pos.y-1)
                {
                    isDown = false;
                }
                if (endGrid.pos.x == tmp_Astart.pos.x && endGrid.pos.y == tmp_Astart.pos.y + 1)
                {
                    isUp = false;
                }
            }

            foreach (var startGrid in start)//检测下一探测点是否出现在探测列表
            {
                if (startGrid.pos.x == tmp_Astart.pos.x - 1 && startGrid.pos.y == tmp_Astart.pos.y)
                {
                    if ((tmp_Astart.G + 4) < startGrid.G)
                    {
                        Debug.Log("出现异常");
                        startGrid.G = tmp_Astart.G + 10;
                        startGrid.Parent = tmp_Astart.pos;
                    }

                    isLeft = false;
                }
                if (startGrid.pos.x == tmp_Astart.pos.x + 1 && startGrid.pos.y == tmp_Astart.pos.y)
                {
                    if ((tmp_Astart.G +4 ) < startGrid.G)
                    {
                        Debug.Log("出现异常");
                        startGrid.G = tmp_Astart.G + 10;
                        startGrid.Parent = tmp_Astart.pos;
                    }
                    isRight = false;
                }
                if (startGrid.pos.x == tmp_Astart.pos.x && startGrid.pos.y == tmp_Astart.pos.y - 1)
                {
                    if ((tmp_Astart.G + 4) < startGrid.G)
                    {
                        Debug.Log("出现异常");
                        startGrid.G = tmp_Astart.G + 4;
                        startGrid.Parent = tmp_Astart.pos;
                    }

                    isDown = false;
                }
                if (startGrid.pos.x == tmp_Astart.pos.x && startGrid.pos.y == tmp_Astart.pos.y + 1)
                {
                    if ((tmp_Astart.G + 4) < startGrid.G)
                    {
                        Debug.Log("出现异常");
                        startGrid.G = tmp_Astart.G + 4;
                        startGrid.Parent = tmp_Astart.pos;
                    }
                    isUp = false;
                }
            }
            #endregion
#region//进行探测
            if (tmp_Astart.pos.x-1>=0 && map[tmp_Astart.pos.x - 1,tmp_Astart.pos.y]!=1&&isLeft)//判断下一个是否是墙，是否超出界限
            {
                
                AStart latticeLeft = new AStart();//表示右边的格子
                var tmp_Pos = new Pos();
                tmp_Pos.x = tmp_Astart.pos.x - 1;
                tmp_Pos.y = tmp_Astart.pos.y;
                latticeLeft.pos = tmp_Pos;//表示这个点的坐标
                latticeLeft.Parent = tmp_Astart.pos;//设置父级
                Instantiate(tmp_Cube, new Vector3(tmp_Pos.y, 0, tmp_Pos.x), Quaternion.identity);
                //latticeLeft.G = Mathf.Abs(latticeLeft.pos.x - orign.pos.x) * 10 + Mathf.Abs(latticeLeft.pos.y - orign.pos.y)  * 4;//计算这个格子的G值
                latticeLeft.G = tmp_Astart.G + 4;
                latticeLeft.H = Mathf.Abs(latticeLeft.pos.x-zero.x)*10+Mathf.Abs(latticeLeft.pos.y-zero.y)*10;//计算这个格子的H值
                start.Add(latticeLeft);
            }

            if (tmp_Astart.pos.x + 1 < 28 && map[tmp_Astart.pos.x + 1, tmp_Astart.pos.y] != 1 && isRight)
            {

                AStart latticeRight = new AStart();//表示左边的格子
                var tmp_Pos = new Pos();
                tmp_Pos.x = tmp_Astart.pos.x + 1;
                tmp_Pos.y = tmp_Astart.pos.y;
                latticeRight.pos = tmp_Pos;//表示这个点的坐标
                latticeRight.Parent = tmp_Astart.pos;//设置父级
                Instantiate(tmp_Cube, new Vector3(tmp_Pos.y, 0, tmp_Pos.x), Quaternion.identity);
                //latticeRight.G = Mathf.Abs(latticeRight.pos.x - orign.pos.x) * 10 + Mathf.Abs(latticeRight.pos.y - orign.pos.y) * 4;//计算这个格子的G值
                latticeRight.G = tmp_Astart.G + 4;
                latticeRight.H = Mathf.Abs(latticeRight.pos.x - zero.x) * 10 + Mathf.Abs(latticeRight.pos.y - zero.y) * 10;//计算这个格子的H值
                start.Add(latticeRight);

            }

            if (tmp_Astart.pos.y-1>=0 && map[tmp_Astart.pos.x, tmp_Astart.pos.y-1] != 1 && isDown)
            {

                AStart latticeDown = new AStart();//表示左边的格子
                var tmp_Pos = new Pos();
                tmp_Pos.x = tmp_Astart.pos.x;
                tmp_Pos.y = tmp_Astart.pos.y-1;
                latticeDown.pos = tmp_Pos;//表示这个点的坐标
                latticeDown.Parent = tmp_Astart.pos;//设置父级
                Instantiate(tmp_Cube, new Vector3(tmp_Pos.y, 0, tmp_Pos.x), Quaternion.identity);
                //latticeDown.G = Mathf.Abs(latticeDown.pos.x - orign.pos.x) * 10 + Mathf.Abs(latticeDown.pos.y - orign.pos.y) * 4;//计算这个格子的G值
                latticeDown.G = tmp_Astart.G + 4;
                latticeDown.H = Mathf.Abs(latticeDown.pos.x - zero.x) * 10 + Mathf.Abs(latticeDown.pos.y - zero.y) * 10;//计算这个格子的H值
                start.Add(latticeDown);

            }

            if (tmp_Astart.pos.y + 1 <28 && map[tmp_Astart.pos.x, tmp_Astart.pos.y + 1] != 1 && isUp)
            {
                AStart latticeUp = new AStart();//表示左边的格子
                var tmp_Pos = new Pos();
                tmp_Pos.x = tmp_Astart.pos.x;
                tmp_Pos.y = tmp_Astart.pos.y + 1;
                latticeUp.pos = tmp_Pos;//表示这个点的坐标
                latticeUp.Parent = tmp_Astart.pos;//设置父级
                Instantiate(tmp_Cube, new Vector3(tmp_Pos.y, 0, tmp_Pos.x), Quaternion.identity);
                //latticeUp.G = Mathf.Abs(latticeUp.pos.x - orign.pos.x) * 10 + Mathf.Abs(latticeUp.pos.y - orign.pos.y) * 4;//计算这个格子的G值
                latticeUp.G = tmp_Astart.G + 4;
                latticeUp.H = Mathf.Abs(latticeUp.pos.x - zero.x) * 10 + Mathf.Abs(latticeUp.pos.y - zero.y) * 10;//计算这个格子的H值
                start.Add(latticeUp);
            }
#endregion

            endGame.Add(tmp_Astart);
            start.Remove(tmp_Astart);
            Debug.Log("完成扩散一次");
            Debug.Log(tmp_Astart.F);
            yield return 0;
        }

        if (endLattice!=null)
        {
            var tmp_Parent = endLattice.Parent;
           
            while (tmp_Parent!=null)
            {

                Instantiate(line_Cube,new Vector3(tmp_Parent.y,0,tmp_Parent.x),Quaternion.identity);
                foreach (var lattice in endGame)
                {
                    if (lattice.pos!=null&&lattice.pos.x == tmp_Parent.x && lattice.pos.y == tmp_Parent.y)
                    {
                        tmp_Parent = lattice.Parent;
                        if (tmp_Parent==null)
                        {
                            break;
                        }
                    }
                }
                yield return 0;
            }
            
        }



        yield return null;


    }
}


