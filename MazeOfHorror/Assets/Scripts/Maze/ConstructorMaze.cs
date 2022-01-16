using System.Collections.Generic;
using UnityEngine;

public class ConstructorMaze : MonoBehaviour
{
    private enum TagName
    {
        Player,
        Exit,
        Gold,
        Treasure,
        Movet,
        NoMovet,
        Cell,
        Key,
        Monster,
        Map
    }

    [SerializeField] private LoadResurces _loadResurces;
    [SerializeField] private SaveProgress _saveProgress;

    public GameObject prefabPoint;         //0    - ����� ��������� � ������������ ���������

    private GameObject prefabWoll;          //1   

    public GameObject prefabExit;          //2
    public GameObject prefabPlayer;        //3
    public GameObject prefabKeys;          //4

    private GameObject prefabMonsterEar;    //5
    private GameObject prefabMonsterEye;    //6
    private GameObject prefabMonsterNose;   //7

    public GameObject prefabGold;          //8
    public GameObject prefabMap;           //9
    public GameObject prefabTreasure;      //10

    private GameObject prefabFloor;
    private GameObject prefabTop;

    // ������ ��� ���������
    private int sizeRows;
    private int sizeCols;
    private int amountKeys;
    private int amountMonster;
    private int amountMonster1;
    private int amountMonster2;

    //������ ��� ���������
    private int sizeCell = 2;
    private int amountGold;
    private int minDistance;
    private MazeDataGenerator dataGenerator;
    private Transform transformPlayer;
    private List<GameObject> transformsMonsters;

    private int[] listMonster;

    private List<GameObject> listDeletedObject;
    private int[] positionMap;
    private int levelPlayer;

    public int[,] Data
    {
        get; private set;
    }
    public int[,] CopyData
    {
        get; private set;
    }

    public int PositionX
    {
        get; private set;
    }
    public int PositionZ
    {
        get; private set;
    }

    private void Awake()
    {
        dataGenerator = new MazeDataGenerator();


        Data = new int[,]
        {
            {1, 1, 1},
            {1, 0, 1},
            {1, 1, 1}
        };
    }

    public void GenerateNewMaze(List<int> constrMaze)
    {
        listDeletedObject = new List<GameObject>();
        transformsMonsters = new List<GameObject>();
        //��������� ������
        sizeRows = constrMaze[0];
        sizeCols = constrMaze[1];
        amountKeys = constrMaze[2];
        amountMonster = constrMaze[3];
        amountMonster1 = constrMaze[4];
        amountMonster2 = constrMaze[5];

        GameObject[] arrayResurces = _loadResurces.GetResurces();
        
        prefabWoll = arrayResurces[0];
        prefabTop = arrayResurces[1];
        prefabFloor = arrayResurces[2];
        prefabMonsterEar = arrayResurces[3];
        prefabMonsterEye = arrayResurces[4];
        prefabMonsterNose = arrayResurces[5];

        levelPlayer = _saveProgress.LevelPlayer;
        if ((sizeCell + sizeCols) / 2 < 25 && levelPlayer > 15)
        {
            levelPlayer = 15;
        }
        if ((sizeCell + sizeCols) / 2 < 35 && levelPlayer > 25)
        {
            levelPlayer = 25;
        }
        if ((sizeCell + sizeCols) / 2 < 55 && levelPlayer > 35)
        {
            levelPlayer = 35;
        }


        Data = dataGenerator.FromDimensions(sizeRows, sizeCols);                                //��������� ���������
        CopyData = Data;

        amountGold = (sizeRows + sizeCols) / 2;                                                 // ���������� ���������� ������
        minDistance = amountGold / 2;

        GeneratorListMonster();                                                                 // ���������� ������ ��������

        int[] posExit = FindRandomPosition();
        CopyData[posExit[0], posExit[1]] = 2;                                                   //������ �����
        int[] posPlayer = FindRandomPositionDistanceTarget(posExit, minDistance);
        CopyData[posPlayer[0], posPlayer[1]] = 3;                                               //������ ������
        for (int i = 0; i < amountKeys; i++)
        {
            int[] posKey = FindRandomPositionDistanceTarget(posExit, minDistance - 2);
            CopyData[posKey[0], posKey[1]] = 4;                                                 //����������� �����
        }
        for (int i = 0; i < listMonster.Length; i++)
        {
            int[] posMonster = FindRandomPositionDistanceTarget(posPlayer, minDistance - 2);
            CopyData[posMonster[0], posMonster[1]] = listMonster[i];                            //������ ��������
        }
        int[] posMap = FindRandomPositionDistanceTarget(posPlayer, minDistance);                //������ �����
        CopyData[posMap[0], posMap[1]] = 9;
        positionMap = posMap;
        for (int i = 0; i < amountGold; i++)
        {
            int[] posGold = FindRandomPosition();
            CopyData[posGold[0], posGold[1]] = 8;                                               //����������� ������
        }

        DisplayMaze(CopyData);      // ���������� ��������

    }

    public void Restart()
    {
        DeleteAll();
        DisplayMaze(CopyData);
    }

    public void DeleteAll()
    {
        foreach (var e in listDeletedObject) Destroy(e);
        listDeletedObject.Clear();
        transformsMonsters.Clear();
    }


    private void DisplayMaze(int[,] data)
    {
        for (int i = 0; i < sizeRows; i++)
        {
            for (int j = 0; j < sizeCols; j++)
            {
                switch (data[i, j])
                {
                    case (0):
                        CreateActivCellDesactivWoll(i, j);
                        break;
                    case (1):
                        if (i == 0 || j == 0 || i == sizeRows - 1 || j == sizeCols - 1)
                        {
                            CreateObject(prefabWoll, true, TagName.NoMovet, i, j);
                        }
                        else
                        {
                            GameObject wollMovet = CreateObject(prefabWoll, true, TagName.Movet, i, j);
                            GameObject cellMovet = CreateObject(prefabPoint, false, TagName.Cell, i, j);
                            cellMovet.GetComponent<VisibleOnn>().SetGo(wollMovet);
                            wollMovet.GetComponent<VisibleOnn>().SetGo(cellMovet);
                        }
                        break;
                    case (2):
                        CreateActivCellDesactivWoll(i, j);
                        CreateObject(prefabExit, true, TagName.Exit, i, j);
                        break;
                    case (3):
                        CreateActivCellDesactivWoll(i, j);
                        CreateObject(prefabPlayer, true, TagName.Player, i, j);

                        break;
                    case (4):
                        CreateActivCellDesactivWoll(i, j);
                        CreateObject(prefabKeys, true, TagName.Key, i, j);
                        break;
                    case (5):
                        CreateActivCellDesactivWoll(i, j);
                        CreateObject(prefabMonsterEar, true, TagName.Monster, i, j);
                        break;
                    case (6):
                        CreateActivCellDesactivWoll(i, j);
                        CreateObject(prefabMonsterEye, true, TagName.Monster, i, j);
                        break;
                    case (7):
                        CreateActivCellDesactivWoll(i, j);
                        CreateObject(prefabMonsterNose, true, TagName.Monster, i, j);
                        break;
                    case (8):
                        CreateActivCellDesactivWoll(i, j);
                        CreateObject(prefabGold, true, TagName.Gold, i, j);
                        break;
                    case (9):
                        CreateActivCellDesactivWoll(i, j);
                        CreateObject(prefabMap, true, TagName.Map, i, j);
                        break;
                }

            }
        }


        GameObject floor = Instantiate(prefabFloor);
        floor.transform.position = new Vector3(sizeRows * sizeCell / 2, -1, sizeCols * sizeCell / 2);
        listDeletedObject.Add(floor);

        GameObject top = Instantiate(prefabTop);
        top.transform.position = new Vector3(sizeRows * sizeCell / 2, 1, sizeCols * sizeCell / 2);
        listDeletedObject.Add(top);
    }

    private GameObject CreateObject(GameObject prefab, bool isActiv, TagName tag, int i, int j)
    {
        GameObject go = Instantiate(prefab);
        go.transform.position = new Vector3(i * sizeCell, 0, j * sizeCell);
        if (tag != TagName.Exit && tag != TagName.Map)
        {
            go.transform.Rotate(Vector3.up, ChangeAngle());
        }
        if (tag == TagName.Player)
        {
            go.GetComponent<MovetCube>().SetCubeInHundle(prefabWoll);
            go.GetComponent<FpsMovement>().SetSpeed(_saveProgress.SpeedPlayer);
        }
        if (tag == TagName.Monster)
        {
            go.GetComponent<MonsterController>().SetSpeedNormal(levelPlayer);
            transformsMonsters.Add(go);
        }
        go.tag = tag.ToString();
        go.SetActive(isActiv);
        
        listDeletedObject.Add(go);
        return go;
    }

    private float ChangeAngle()
    {
        float angle = Random.value < 0.5f ? Random.value < 0.5f ? 0 : 90 : Random.value < 0.5f ? 180 : -90;
        return angle;
    }

    private void CreateActivCellDesactivWoll(int i, int j)
    {
        GameObject cell = CreateObject(prefabPoint, true, TagName.Cell, i, j);
        GameObject woll = CreateObject(prefabWoll, false, TagName.Movet, i, j);
        cell.GetComponent<VisibleOnn>().SetGo(woll);
        woll.GetComponent<VisibleOnn>().SetGo(cell);
    }

    private int[] FindRandomPosition()
    {
        int randomX, randomZ;
        int[] result = new int[2];

        randomX = Random.Range(1, Data.GetLength(0) - 2);
        randomZ = Random.Range(1, Data.GetLength(1) - 2);

        if (CopyData[randomX, randomZ] != 0)

        {
            for (int i = randomX - 1; i <= randomX + 1; i++)
            {
                for (int j = randomZ - 1; j <= randomZ + 1; j++)
                {
                    if (CopyData[i, j] == 0)
                    {
                        randomX = i;
                        randomZ = j;
                    }
                }
            }
        }
        result[0] = randomX;
        result[1] = randomZ;
        return result;
    }

    private int[] FindRandomPositionDistanceTarget(int[] target, float distance)
    {
        Vector3 positionTarget = new Vector3(target[0], 0, target[1]);
        float distReal;
        int[] position;
        int counter = 0;
        do
        {
            position = FindRandomPosition();
            Vector3 positionNew = new Vector3(position[0], 0, position[1]);
            distReal = (positionNew - positionTarget).magnitude;
            counter++;
            if (counter > 20)
            {
                position = new int[] { -5, -5 };
                break;
            }
        }
        while (distReal < distance);
        return position;
    }

    private void GeneratorListMonster()
    {
        listMonster = new int[amountMonster + amountMonster1 + amountMonster2];
        for (int i = 0; i < amountMonster + amountMonster1 + amountMonster2; i++)
        {
            if (i < amountMonster) listMonster[i] = 5;
            else if (i >= amountMonster + amountMonster1) listMonster[i] = 7;
            else listMonster[i] = 6;
        }

    }

    public void TransformingMonstersAffterMinusLifePlayer(float powerPlayer)
    {
        transformPlayer = GameObject.FindGameObjectWithTag("Player").transform;
        int x = (int)transformPlayer.position.x;
        int z = (int)transformPlayer.position.z;
        int[] posPlayer = { x, z };
        foreach (var go in transformsMonsters)
        {
            if ((go.transform.position - transformPlayer.position).magnitude < 7 + powerPlayer * 2)
            {
                go.GetComponentInChildren<TriggerBusyMonsters>().NoBusy();
                bool isWollNot = true;
                Vector3 positionGO = go.transform.position;
                do
                {
                    int[] newPosition = FindRandomPositionDistanceTarget(posPlayer, 10f + powerPlayer * 2);
                    positionGO.x = newPosition[0];
                    positionGO.z = newPosition[1];
                    positionGO.y = 5f;
                    if (Physics.Raycast(positionGO, transform.TransformDirection(Vector3.down), out RaycastHit hit, 10f))
                    {
                        if (hit.collider.CompareTag("Movet"))
                        {
                            isWollNot = false;
                        }
                        else
                        {
                            isWollNot = true;
                        }
                    }
                }
                while (isWollNot == false);
                positionGO.y = 0f;
                go.transform.position = positionGO;
                go.GetComponent<MonsterController>().SetState(MonsterController.State.Idle);

            }
        }
    }

    public void CreateTreasure()
    {
        int[] posTreasure = FindRandomPositionDistanceTarget(positionMap, minDistance);          //������ ���������
        CreateObject(prefabTreasure, true, TagName.Treasure, posTreasure[0], posTreasure[1]);
    }

}
