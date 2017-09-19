using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ForwardDirection
{
    None,
    Up,
    Down,
    Left,
    Right,

}

public class Pos 
{
    public int x;
    public int y;
    public List<ForwardDirection> dirt = new List<ForwardDirection>();

    public Pos(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

}

public class RoomPos
{
    public List<Pos> roomPos = new List<Pos>();//装入房间的点，装入房间边缘的点
    public List<Pos> roomEdgePos = new List<Pos>();

    public void AddRoomPos(Pos roomSpot)
    {
        roomPos.Add(roomSpot);
    }

    public void AddRoomEdgePos(Pos roomEdgeSpot)
    {
        roomEdgePos.Add(roomEdgeSpot);
    }
}

public  class RoomCreate {


    public static RoomCreate instance;
    public int widthMax;
    public int lengthMax;
    public char[,] maps;//= new char[49, 49];//地图
    public  List<Pos> emptyPosList = new List<Pos>();//装入空余的点的坐标
    public  List<Pos> roadPos = new List<Pos>();//装入所有道路的点
    public  List<RoomPos> roomS = new List<RoomPos>();//装入所有房间的点，以及其边缘的点
    public  List<Pos> deletPos = new List<Pos>();//装入所有死路的点
    public  int keyNumber = 0;
    public  ForwardDirection nowDirct;

    public static  RoomCreate Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new RoomCreate();
            }
            return instance;
        }
    }

    public  void Fill()//填充
    {
        for (int i = 0; i < widthMax; i++)
        {
            for (int j = 0; j < lengthMax; j++)
            {
                maps[i, j] = ' ';
            }
        }


    }

    public  void RandomRoom()//随机生成房间
    {
        int roomlength = Random.Range(3, 6);
        int roomWidth = Random.Range(3, 6);//随机房间的大小
        int randomStratX = Random.Range(1, widthMax-1);
        int randomStratY = Random.Range(1, lengthMax-1);//随机房间的开始生成位置
        if (randomStratX % 2 == 0 || randomStratY % 2 == 0)
        {
            return;
        }
        if (roomlength % 2 == 0 || roomWidth % 2 == 0)
        {
            return;
        }


        int roomEdgeMinX = randomStratX - 1;
        int roomEdgeMinY = randomStratY - 1;//计算房间边缘最小的点的坐标

        int roomMaxX = randomStratX + roomlength - 1;
        int roomMaxY = randomStratY + roomWidth - 1;//计算房间的最大XY
        int roomEdgeMaxX = roomEdgeMinX + roomlength + 1;
        int roomEdgeMaxY = roomEdgeMinY + roomWidth + 1;//计算房间边缘的最大的XY


        if (roomEdgeMaxX < widthMax && roomEdgeMaxY < lengthMax)
        {
            for (int i = roomEdgeMinX; i <= roomEdgeMaxX; i++)//判断房间是否重叠
            {
                for (int j = roomEdgeMinY; j <= roomEdgeMaxY; j++)
                {
                    if (maps[i, j] == '1')
                    {
                        return;
                    }
                }
            }

            RoomPos tmp_Room = new RoomPos();
            #region//房间边填充石块
            for (int i = roomEdgeMinX; i <= roomEdgeMaxX; i++)
            {
                if (i != roomEdgeMinX && i != roomEdgeMaxX)
                {
                    Pos tmp_EdgePosLeft = new Pos(i, roomEdgeMinY);
                    Pos tmp_EdgePosRight = new Pos(i, roomEdgeMaxY);
                    tmp_Room.AddRoomEdgePos(tmp_EdgePosLeft);
                    tmp_Room.AddRoomEdgePos(tmp_EdgePosRight);//将房间边缘的点添加进房间类的列表中进行保存
                }


                maps[i, roomEdgeMinY] = '#';
                maps[i, roomEdgeMaxY] = '#';
            }

            for (int j = roomEdgeMinY; j <= roomEdgeMaxY; j++)
            {
                if (j != roomEdgeMinY && j != roomEdgeMaxY)
                {
                    Pos tmp_EdgePosUp = new Pos(roomEdgeMinX, j);
                    Pos tmp_EdgePosDown = new Pos(roomEdgeMaxX, j);
                    tmp_Room.AddRoomEdgePos(tmp_EdgePosUp);
                    tmp_Room.AddRoomEdgePos(tmp_EdgePosDown);
                    maps[roomEdgeMinX, j] = '#';
                    maps[roomEdgeMaxX, j] = '#';
                }

            }
            #endregion
            for (int i = randomStratX; i <= roomMaxX; i++)//房间填充
            {
                for (int j = randomStratY; j <= roomMaxY; j++)
                {
                    Pos roomSpot = new Pos(i, j);
                    tmp_Room.AddRoomPos(roomSpot);
                    maps[i, j] = '1';
                }
            }
            roomS.Add(tmp_Room);


        }

    }

    public  void FillEmpty()//填充空的列表
    {
        for (int i = 1; i < widthMax-1; i++)
        {
            for (int j = 1; j < lengthMax-1; j++)
            {
                if (maps[i, j] == ' ')
                {
                    Pos emptyPos = new Pos(i, j);
                    emptyPosList.Add(emptyPos);
                }
            }
        }

        for (int i = 0; i < widthMax; i++)
        {
            for (int j = 0; j < lengthMax; j++)
            {
                if (maps[i, j] == '#')
                {
                    maps[i, j] = ' ';
                }
            }
        }
    }

    public  Pos Carving()//选择初始点
    {

        Pos tmp_emptyPos = emptyPosList[0];
        foreach (var pair in emptyPosList)//选择空节点中坐标最小的
        {
            if (pair.x < tmp_emptyPos.x || pair.y < tmp_emptyPos.y)
            {
                tmp_emptyPos = pair;
            }
        }
        nowDirct = ForwardDirection.None;//重置
        return tmp_emptyPos;
    }

    public  void Connect(List<Pos> roomEdgePos)//选出房间的连接点
    {
        for (int i = 0; i < roomEdgePos.Count; i++)
        {
            int stone = 0;//表示这个点周围的石头数量
            int tmp_X = roomEdgePos[i].x;
            int tmp_Y = roomEdgePos[i].y;


            if (tmp_X - 1 >= 0 && (maps[tmp_X - 1, tmp_Y] == '=' || maps[tmp_X - 1, tmp_Y] == '1'))
            {
                stone++;
            }
            if (tmp_X + 1 < widthMax && (maps[tmp_X + 1, tmp_Y] == '=' || maps[tmp_X + 1, tmp_Y] == '1'))
            {
                stone++;
            }
            if (tmp_Y - 1 >= 0 && (maps[tmp_X, tmp_Y - 1] == '=' || maps[tmp_X, tmp_Y - 1] == '1'))
            {
                stone++;
            }
            if (tmp_Y + 1 < lengthMax && (maps[tmp_X, tmp_Y + 1] == '=' || maps[tmp_X, tmp_Y + 1] == '1'))
            {
                stone++;
            }

            if (stone < 2)
            {
                roomEdgePos.RemoveAt(i);
                i--;
            }


        }


    }

    public  Pos Painting(Pos startPos)//画点
    {
        ChoiceDir(startPos);
        ForwardDirection dirt2 = ForwardDirection.None;
        if (startPos.dirt.Count > 0)
        {
            if (nowDirct == ForwardDirection.None)
            {
                nowDirct = startPos.dirt[0];
            }

           // bool isCanDir = IsDir(startPos);
            if (startPos.dirt.Count == 1)
            {
                dirt2 = startPos.dirt[0];
            }

            if (startPos.dirt.Count > 1)
            {

                bool isDir = Random.Range(1, 4) == 2 ? true : false;//是否转向

                if (isDir)
                {
                    dirt2 = startPos.dirt[Random.Range(0, startPos.dirt.Count)];
                }
                else
                {
                    if (startPos.dirt.Contains(nowDirct))
                    {
                        dirt2 = nowDirct;
                    }
                    else
                    {
                        dirt2 = startPos.dirt[0];
                    }

                }

            }
            FillRoad(startPos);//画点
            roadPos.Add(startPos);//添加到道路列表
            DeltEmptyPos(startPos);
            startPos.dirt.Clear();
        }
        else
        {
            FillRoad(startPos);//画点
            return null;
        }

        nowDirct = dirt2;
        Pos tmp_ForwardPos;//声明临时POS用于接取下一个点的引用
        switch (dirt2)
        {
            case ForwardDirection.Up:
                tmp_ForwardPos = new Pos(startPos.x - 1, startPos.y);//从空列表获取这个点表示的POS引用
                return tmp_ForwardPos;//传出这个点的引用
            case ForwardDirection.Down:
                tmp_ForwardPos = new Pos(startPos.x + 1, startPos.y);
                return tmp_ForwardPos;
            case ForwardDirection.Left:
                tmp_ForwardPos = new Pos(startPos.x, startPos.y - 1);
                return tmp_ForwardPos;
            case ForwardDirection.Right:
                tmp_ForwardPos = new Pos(startPos.x, startPos.y + 1);
                return tmp_ForwardPos;
        }
        return null;

    }

    public  void FillRoad()//填充道路
    {
        roadPos.Clear();

        for (int i = 0; i < widthMax; i++)
        {
            for (int j = 0; j < lengthMax; j++)
            {
                if (maps[i, j] == '=')
                {
                    Pos roadSpot = new Pos(i, j);
                    roadPos.Add(roadSpot);
                }
            }
        }

    }

    public  void DeletRoad()//删除死路
    {

        for (int i = 0; i < roadPos.Count; i++)
        {
            int stone = 0;//表示这个点周围的石头数量
            int tmp_X = roadPos[i].x;
            int tmp_Y = roadPos[i].y;
            if (tmp_X - 1 >= 0 && maps[tmp_X - 1, tmp_Y] == ' ')
            {
                stone++;
            }
            if (tmp_X - 1 < widthMax && maps[tmp_X + 1, tmp_Y] == ' ')
            {
                stone++;
            }
            if (tmp_Y - 1 >= 0 && maps[tmp_X, tmp_Y - 1] == ' ')
            {
                stone++;
            }
            if (tmp_Y + 1 < lengthMax && maps[tmp_X, tmp_Y + 1] == ' ')
            {
                stone++;
            }

            if (stone >= 3)
            {
                deletPos.Add(roadPos[i]);
            }

        }
    }

    public  void ChoiceDir(Pos tmp_emptyPos)//计算这个点能去的其余方向
    {
        ForwardDirection dir;
        if (tmp_emptyPos.x - 2 >= 0 && maps[tmp_emptyPos.x - 1, tmp_emptyPos.y] == ' ' && maps[tmp_emptyPos.x - 2, tmp_emptyPos.y] == ' ')
        {

            DeltEmptyPos(tmp_emptyPos.x - 1, tmp_emptyPos.y);
            // DeltEmptyPos(tmp_emptyPos.x - 2, tmp_emptyPos.y);
            int tmp_dir = ISJs(tmp_emptyPos);
            if (tmp_dir == 3 || tmp_dir == 2)
            {
                dir = ForwardDirection.Up;

                tmp_emptyPos.dirt.Add(dir);
            }


        }

        if (tmp_emptyPos.x + 2 < widthMax && maps[tmp_emptyPos.x + 1, tmp_emptyPos.y] == ' ' && maps[tmp_emptyPos.x + 2, tmp_emptyPos.y] == ' ')
        {

            DeltEmptyPos(tmp_emptyPos.x + 1, tmp_emptyPos.y);
            // DeltEmptyPos(tmp_emptyPos.x + 2, tmp_emptyPos.y);
            int tmp_dir = ISJs(tmp_emptyPos);
            if (tmp_dir == 3 || tmp_dir == 2)
            {
                dir = ForwardDirection.Down;
                tmp_emptyPos.dirt.Add(dir);
            }
        }

        if (tmp_emptyPos.y - 2 >= 0 && maps[tmp_emptyPos.x, tmp_emptyPos.y - 1] == ' ' && maps[tmp_emptyPos.x, tmp_emptyPos.y - 2] == ' ')
        {

            DeltEmptyPos(tmp_emptyPos.x, tmp_emptyPos.y - 1);
            //DeltEmptyPos(tmp_emptyPos.x, tmp_emptyPos.y-2);
            int tmp_dir = ISJs(tmp_emptyPos);
            if (tmp_dir == 3 || tmp_dir == 1)
            {
                dir = ForwardDirection.Left;
                tmp_emptyPos.dirt.Add(dir);
            }
        }

        if (tmp_emptyPos.y + 2 < lengthMax && maps[tmp_emptyPos.x, tmp_emptyPos.y + 1] == ' ' && maps[tmp_emptyPos.x, tmp_emptyPos.y + 2] == ' ')
        {
            DeltEmptyPos(tmp_emptyPos.x, tmp_emptyPos.y + 1);
            // DeltEmptyPos(tmp_emptyPos.x, tmp_emptyPos.y + 2);

            int tmp_dir = ISJs(tmp_emptyPos);
            if (tmp_dir == 3 || tmp_dir == 1)
            {
                dir = ForwardDirection.Right;
                tmp_emptyPos.dirt.Add(dir);
            }
        }
    }

    public  void DeltEmptyPos(Pos emptyPos)//从空列表中进行删除操作
    {


        for (int i = 0; i < emptyPosList.Count; i++)
        {
            if (emptyPosList[i].x == emptyPos.x && emptyPosList[i].y == emptyPos.y)
            {
                emptyPosList.RemoveAt(i);
                break;
            }
        }

    }

    public  int ISJs(Pos startPos)//判断这个点的坐标XY值是否存在有一个是奇数
    {
        int dir = 0;

        if (startPos.x % 2 == 1)
        {
            dir += 1;
        }

        if (startPos.y % 2 == 1)
        {
            dir += 2;
        }
        return dir;
    }

    public  void DeltEmptyPos(int x, int y)
    {
        for (int i = 0; i < emptyPosList.Count; i++)
        {
            if (emptyPosList[i].x == x && emptyPosList[i].y == y)
            {
                emptyPosList.RemoveAt(i);
                break;
            }
        }
    }

    public  void FillRoad(Pos emptyPos)
    {
        maps[emptyPos.x, emptyPos.y] = '=';
    }

    public  void RandomProp(List<Pos> roomSpot)//房间随机生成道具
    {
        if (roomSpot.Count >= 18)
        {
            int propNumber = Random.Range(2, 5);

            while (propNumber > 0)
            {
                Pos propSpot = roomSpot[Random.Range(0, roomSpot.Count)];
                int prop = Chance();

                if (prop == 1)
                {
                    maps[propSpot.x, propSpot.y] = 't';
                    roomSpot.Remove(propSpot);
                }
                else if (prop == 2)
                {
                    maps[propSpot.x, propSpot.y] = 'y';
                    roomSpot.Remove(propSpot);
                }
                else if (prop == 3)
                {
                    maps[propSpot.x, propSpot.y] = 'z';
                    roomSpot.Remove(propSpot);
                }
                else if (prop == 4)
                {
                    maps[propSpot.x, propSpot.y] = 'b';
                    roomSpot.Remove(propSpot);
                }
                else if (prop == 5)
                {
                    maps[propSpot.x, propSpot.y] = 'j';
                    roomSpot.Remove(propSpot);
                }
                propNumber--;
            }
        }
        else
        {
            int propNumber = Random.Range(1, 3);

            while (propNumber > 0)
            {
                Pos propSpot = roomSpot[Random.Range(0, roomSpot.Count)];
                int prop = Chance();

                if (prop == 1)
                {
                    maps[propSpot.x, propSpot.y] = 't';
                    roomSpot.Remove(propSpot);
                }
                else if (prop == 2)
                {
                    maps[propSpot.x, propSpot.y] = 'y';
                    roomSpot.Remove(propSpot);
                }
                else if (prop == 3)
                {
                    maps[propSpot.x, propSpot.y] = 'z';
                    roomSpot.Remove(propSpot);
                }
                else if (prop == 4)
                {
                    maps[propSpot.x, propSpot.y] = 'b';
                    roomSpot.Remove(propSpot);
                }
                else if (prop == 5)
                {
                    maps[propSpot.x, propSpot.y] = 'j';
                    roomSpot.Remove(propSpot);
                }
                propNumber--;
            }
        }
    }

    public  void RandomMonster(List<Pos> roomSpot)//房间随机生成怪物
    {
        int isCreateMonster = Chance();
        if (isCreateMonster == 2)
        {
            return;
        }
        else
        {
            if (roomSpot.Count >= 18)
            {
                int monsterNumber = Random.Range(2, 5);
                while (monsterNumber > 0)
                {
                    Pos propSpot = roomSpot[Random.Range(0, roomSpot.Count)];
                    maps[propSpot.x, propSpot.y] = 'm';
                    monsterNumber--;
                    roomSpot.Remove(propSpot);
                }
            }
            else
            {
                int monsterNumber = Random.Range(1, 3);
                while (monsterNumber > 0)
                {
                    Pos propSpot = roomSpot[Random.Range(0, roomSpot.Count)];
                    maps[propSpot.x, propSpot.y] = 'm';
                    monsterNumber--;
                    roomSpot.Remove(propSpot);
                }
            }
        }

    }

    public  int Chance()//概率计算
    {
        int timeSeed = Random.Range(0, 100);
        if (timeSeed == 0)//  1/100
        {
            return 1;
        }
        else if (timeSeed >= 1 && timeSeed <= 10)//  1/10
        {
            return 2;
        }
        else if (timeSeed >= 50 && timeSeed <= 51)//   1/50
        {
            return 3;
        }
        else if (timeSeed >= 30 && timeSeed <= 40)//  1/10
        {
            return 4;
        }
        else
        {
            return 5;
        }

    }

    public  void RandomStairs()//随机楼梯
    {
        RoomPos stairsRoomUp = roomS[Random.Range(0, roomS.Count)];
        int i = 0;
        while (!IsGreatStairs(stairsRoomUp.roomPos[i]))//判断这点周围是否有门
        {
            i++;
        }

        maps[stairsRoomUp.roomPos[i].x, stairsRoomUp.roomPos[i].y] = 'p';//没有就设置为门

        Pos tmp_PointUp = InitPoint(stairsRoomUp.roomPos[i], "Down");//选出附近的传送点
        DeletListPos(stairsRoomUp.roomPos, tmp_PointUp);
        stairsRoomUp.roomPos.RemoveAt(i);

        maps[stairsRoomUp.roomPos[0].x, stairsRoomUp.roomPos[0].y] = 'k';
        stairsRoomUp.roomPos.RemoveAt(0);
        roomS.Remove(stairsRoomUp);//删除掉有上楼梯的这个房间

        RoomPos stairsRoomDown = roomS[Random.Range(0, roomS.Count)];

        int j = 0;
        while (!IsGreatStairs(stairsRoomDown.roomPos[j]))
        {
            j++;
        }

        maps[stairsRoomDown.roomPos[j].x, stairsRoomDown.roomPos[j].y] = 'o';

        Pos tmp_PointDown = InitPoint(stairsRoomDown.roomPos[j], "Up");
        DeletListPos(stairsRoomDown.roomPos, tmp_PointDown);

        stairsRoomDown.roomPos.RemoveAt(j);

        maps[stairsRoomDown.roomPos[0].x, stairsRoomDown.roomPos[0].y] = 'k';
        stairsRoomDown.roomPos.RemoveAt(0);
        
    }

    public bool IsGreatStairs(Pos stairs)
    {
        bool isGreat = true;

        if (maps[stairs.x - 1, stairs.y] == 'g')
        {
            isGreat = false;
        }
        else if (maps[stairs.x + 1, stairs.y] == 'g')
        {
            isGreat = false;
        }
        else if (maps[stairs.x, stairs.y - 1] == 'g')
        {
            isGreat = false;
        }
        else if (maps[stairs.x, stairs.y + 1] == 'g')
        {
            isGreat = false;
        }
        return isGreat;
    }




    public  void DeletListPos(List<Pos> posList, Pos deletPos)//从pos列表中删除指定元素
    {

        for (int i = 0; i < posList.Count; i++)
        {
            if (posList[i].x == deletPos.x && posList[i].y == deletPos.y)
            {
                posList.RemoveAt(i);
                return;
            }
        }

    }

    public  Pos InitPoint(Pos stairs, string dir)//设置上下楼层的初始点
    {
        int sign = 0;
        if (dir == "Down")
        {
            if (maps[stairs.x - 1, stairs.y] == '1')
            {
                maps[stairs.x - 1, stairs.y] = '9';
                sign = 1;
            }
            else if (maps[stairs.x + 1, stairs.y] == '1')
            {
                maps[stairs.x + 1, stairs.y] = '9';
                sign = 2;
            }
            else if (maps[stairs.x, stairs.y - 1] == '1')
            {
                maps[stairs.x, stairs.y - 1] = '9';
                sign = 3;
            }
            else if (maps[stairs.x, stairs.y + 1] == '1')
            {
                maps[stairs.x, stairs.y + 1] = '9';
                sign = 4;
            }
        }
        else
        {
            if (maps[stairs.x - 1, stairs.y] == '1')
            {
                maps[stairs.x - 1, stairs.y] = '6';
                sign = 1;
            }
            else if (maps[stairs.x + 1, stairs.y] == '1')
            {
                maps[stairs.x + 1, stairs.y] = '6';
                sign = 2;
            }
            else if (maps[stairs.x, stairs.y - 1] == '1')
            {
                maps[stairs.x, stairs.y - 1] = '6';
                sign = 3;
            }
            else if (maps[stairs.x, stairs.y + 1] == '1')
            {
                maps[stairs.x, stairs.y + 1] = '6';
                sign = 4;
            }
        }

        if (sign == 1)
        {
            Pos tmp_Pos = new Pos(stairs.x - 1, stairs.y);
            return tmp_Pos;
        }
        else if (sign == 2)
        {
            Pos tmp_Pos = new Pos(stairs.x + 1, stairs.y);
            return tmp_Pos;
        }
        else if (sign == 3)
        {
            Pos tmp_Pos = new Pos(stairs.x, stairs.y - 1);
            return tmp_Pos;
        }
        else
        {
            Pos tmp_Pos = new Pos(stairs.x, stairs.y + 1);
            return tmp_Pos;
        }
    }

    public  void RandomRoadKey()//道路上随机钥匙
    {
        keyNumber = roomS.Count-1;
        int roadKey = Random.Range(3, 7);
        keyNumber = keyNumber - roadKey;
        while (roadKey > 0)
        {
            Pos keyPoint = roadPos[Random.Range(0, roadPos.Count)];
            maps[keyPoint.x, keyPoint.y] = 'k';
            roadKey--;
        }
    }

    public  void RandomRoomKey()//房间随机钥匙
    {
        int i = 0;
        while (keyNumber > 0)
        {
            int j = 0;
            int tmp_KeyNumber = Random.Range(1, 3);
            if (tmp_KeyNumber >= keyNumber)
            {
                tmp_KeyNumber = keyNumber;
            }
            keyNumber = keyNumber - tmp_KeyNumber;
            while (tmp_KeyNumber > 0)
            {
                maps[roomS[i].roomPos[j].x, roomS[i].roomPos[j].y] = 'k';
                j++;
                tmp_KeyNumber--;
            }

            i++;
        }
    }

    public  char[,] Maps(int tmp_Width,int tmp_Length)
    {
        widthMax = tmp_Width;
        lengthMax = tmp_Width;
        maps = new char[widthMax,lengthMax];

        Fill();
        int randomRoomFrequency = 0;
        while (randomRoomFrequency < 800)
        {
            RandomRoom();
            randomRoomFrequency++;
        }
        FillEmpty();
        Pos startPos = Carving();
        while (emptyPosList.Count > 5)
        {
            startPos = Painting(startPos);
            if (startPos == null)
            {
                if (roadPos.Count > 0)
                {
                    for (int i = roadPos.Count - 1; i >= 0; i--)
                    {
                        ChoiceDir(roadPos[i]);
                        if (roadPos[i].dirt.Count > 0)
                        {
                            startPos = roadPos[i];
                            break;
                        }
                    }
                }

                if (startPos == null)
                {
                    startPos = Carving();
                }
            }
        }

        for (int i = 0; i < roomS.Count; i++)//选出合适的连接点
        {
            Connect(roomS[i].roomEdgePos);
            int road = Random.Range(0, roomS[i].roomEdgePos.Count);
            maps[roomS[i].roomEdgePos[road].x, roomS[i].roomEdgePos[road].y] = 'g';//选出连接点
        }

        FillRoad();
        DeletRoad();
        while (deletPos.Count > 0)
        {
            foreach (var deletSpot in deletPos)
            {
                maps[deletSpot.x, deletSpot.y] = ' ';
            }
            deletPos.Clear();
            FillRoad();
            DeletRoad();

        }

        FillRoad();//重新填充道路列表
        RandomStairs();//最后生成楼梯
        for (int i = 0; i < roomS.Count; i++)
        {
            RandomMonster(roomS[i].roomPos);//房间随机生成怪物
            RandomProp(roomS[i].roomPos);//房间随机生成道具
        }

        RandomRoadKey();
        RandomRoomKey();

        emptyPosList.Clear();
        roadPos.Clear();
        roomS.Clear();
        deletPos.Clear();
        return maps;
    }
}
