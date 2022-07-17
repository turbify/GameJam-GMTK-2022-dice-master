using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class board_create : MonoBehaviour
{
    [SerializeField]
    GameObject BlockPrefab;
    AudioSource audioData;
    [SerializeField]
    GameObject DicePrefab;
    GameObject player;
    dice_system playerSystem;

    public score scr;

    public GameObject EnemyPrefab;
    public GameObject[] enemies = new GameObject[9];

    public Transform[] map = new Transform[9];
    public string[] tileType = new string[9];

    public int movesSum = 0;
    public int movesToSpawn = 0;
    public int movesDoneAfterS = 0;

    private void Awake() 
    {
        CreateBoard();
    }

    void Start()
    {
        scr = GetComponent<score>();
        audioData = GetComponent<AudioSource>();

        for (int i = 0; i < tileType.Length; i++)
        {
            tileType[i] = "blank";
        }

        GenerateMapOnStart();
    }

    public void CreateBoard()
    {
        player = Instantiate(DicePrefab);
        player.transform.localPosition = Vector3.zero;
        playerSystem = player.GetComponent<dice_system>();
        playerSystem.board = this;

        var b1 = Instantiate(BlockPrefab);
        b1.transform.localPosition = new Vector3(-2, -0.2f ,2);
        map[0] = b1.transform;
        //b1.name = "";
        //GameObject go2 = new GameObject("go2");
        //go2.AddComponent<Rigidbody>();
        //GameObject go3 = new GameObject("go3", typeof(Rigidbody), typeof(BoxCollider));
        // b1.transform.parent = transform;
        var b2 = Instantiate(BlockPrefab);
        b2.transform.localPosition = new Vector3(0,-0.2f,2);
        map[1] = b2.transform;

        var b3 = Instantiate(BlockPrefab);
        b3.transform.localPosition = new Vector3(2,-0.2f,2);
        map[2] = b3.transform;

        var b4 = Instantiate(BlockPrefab);
        b4.transform.localPosition = new Vector3(-2,-0.2f,0);
        map[3] = b4.transform;

        var b5 = Instantiate(BlockPrefab);
        b5.transform.localPosition = new Vector3(0,-0.2f,0);
        map[4] = b5.transform;
        
        var b6 = Instantiate(BlockPrefab);
        b6.transform.localPosition = new Vector3(2,-0.2f,0);
        map[5] = b6.transform;

        var b7 = Instantiate(BlockPrefab);
        b7.transform.localPosition = new Vector3(-2,-0.2f,-2);
        map[6] = b7.transform;

        var b8 = Instantiate(BlockPrefab);
        b8.transform.localPosition = new Vector3(0,-0.2f,-2);  
        map[7] = b8.transform;

        var b9 = Instantiate(BlockPrefab);
        b9.transform.localPosition = new Vector3(2,-0.2f,-2);
        map[8] = b9.transform;
    }

    void GenerateMapOnStart()
    {
        int maxEnemies = 3;
        int enemiesCount = 0;

        movesToSpawn = Random.Range(4, 8);

        for (int i = 0; i < 9; i++)
        {
            if(i+1 != playerSystem.dicePos)
            {

                randomTileGenerate(i);


            }
        }

        int tileCounter = 0;
        foreach (string type in tileType)
        {
            if (type == "enemy") { tileCounter++; }
        }

        while (tileCounter < 2)
        {
            foreach (string type in tileType)
            {
                if (type == "enemy") { tileCounter++; }
            }

            spawnEnemy(5);
        }

        void randomTileGenerate(int tile_)
        {
            if(enemiesCount < maxEnemies)
            {
                if(Random.Range(0,2) == 1)
                {
                    enemiesCount++;

                    tileType[tile_] = "enemy";
                    enemies[tile_] = Instantiate(EnemyPrefab);
                    enemies[tile_].transform.position = map[tile_].transform.position + new Vector3(0,0.2f,0);
                }
            }
        }
    }

    public void countMove(int lastPos)
    {
        movesSum++;
        movesDoneAfterS++;
        movesWithoutKill++;

        if(movesDoneAfterS == movesToSpawn)
        {
            spawnEnemy(lastPos);
        }

        int tileCounter = 0;
        foreach (string type in tileType)
        {
            if (type == "enemy") { tileCounter++; }
        }
        if(tileCounter == 7 && movesWithoutKill >= 3) { SwitchScene(); }
    }

    string sceneName = "Lose";
    public void SwitchScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    void spawnEnemy(int lastPos)
    {
        movesWithoutKill = 0;
        movesDoneAfterS = 0;
        movesToSpawn = Random.Range(4, 8);

        int tileCounter = 0;
        foreach(string type in tileType)
        {
            if(type == "enemy") { tileCounter++; }
        }

        while (1 == 1)
        {
            bool canSpawn = true;
            int randomTile = Random.Range(0, 9);

            //Debug.Log(lastPos);
            //Debug.Log(playerSystem.dicePos - 1);

            if(playerSystem.dicePos - 1 == randomTile || lastPos == randomTile) { canSpawn = false; }

            if(tileType[randomTile] == "blank" && canSpawn)
            {
                tileType[randomTile] = "enemy";
                enemies[randomTile] = Instantiate(EnemyPrefab);
                if (randomTile == 4) { enemies[randomTile].GetComponent<enemy_system>().isOnMiddle = true; }
                enemies[randomTile].transform.position = map[randomTile].transform.position + new Vector3(0, 0.2f, 0);

                break;
            }

            if (tileCounter > 6) { break; }

        }
    }

    public int movesWithoutKill = 0;
    public void killEnemy(int pos)
    {
        movesWithoutKill = 0;
        audioData.Play();
        scr.addScore(enemies[pos].GetComponent<enemy_system>().health);

        tileType[pos] = "blank";
        enemies[pos].GetComponent<enemy_system>().death();
        enemies[pos] = null;

        int tileCounter = 0;
        foreach (string type in tileType)
        {
            if (type == "enemy") { tileCounter++; }
        }
        if(tileCounter == 0) { spawnEnemy(playerSystem.dicePos); }

    }

    void changeTileType(int tile,string type)
    {
        tileType[tile] = type;
    }

            void audioPlay()
    {
    audioData = GetComponent<AudioSource>();
    audioData.Play();
   
    }
}


