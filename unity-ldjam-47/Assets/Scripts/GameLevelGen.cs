using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;
using Random = UnityEngine.Random;

public class GameAssetCodes
{
    public static char centerLane = '=';

    public static char blankRandom = ' ';

    public static char blankPath = '-';
    public static char wall = 'W';
    public static char door = 'D';

    public static char spawnPoint = 'S';

    public static char optionalPath = '.';
    public static char optionalWall = 'O';

    public static char coin = 'C';
    public static char treasure = 'T';

    public static char enemy = 'E';

    public static char portal = 'P';

    public static char goldBlock = 'G';

    //public static char ladder = 'L';

}

public class ItemCodes
{
    public static string coin = "coin";
    public static string treasure = "treasure";
    public static string goldblock = "goldblock";
}

public class LevelTypes
{
    public static string hub = "hub";
    public static string megadungeon = "megadungeon";
}


// JSON class

public class LevelObject
{
    public string Code { get; set; }
    public string Type { get; set; }

    public string Size { get; set; }
    public List<string> Data { get; set; }
}

public class Chunk
{
    public string Type { get; set; }

    public List<List<GameObject>> Tiles { get; set; }
}

public class GameLevelGen : MonoBehaviour
{
    public const string PATH_LEVELS = "levels/";

    public AssetHolder assetHolder;

    private Dictionary<string, LevelObject> levelObjectDictionary;

    List<Chunk> chunksLeft;
    List<Chunk> chunksRight;

    // number of chunks generated on either side of the current chunk
    public int chunkBuffer = 2;

    private int hubWidth = 9;
    private int hubHeight = 9;

    private int dungeonWidth = 21;



    // path must end in  .json, file itself must end in .json.txt
    //public string LoadDataFromResources(string path)
    //{
    //    string dataValue = "";

    //    TextAsset textData = Resources.Load<TextAsset>(path);
    //    if (textData != null)
    //    {
    //        dataValue = textData.text;
    //    }

    //    return dataValue;
    //}

    private void OnEnable()
    {
        Messenger.AddListener(GameActionKeys.gameResetState, OnGameResetState);

    }

    private void OnDisable()
    {
        Messenger.RemoveListener(GameActionKeys.gameResetState, OnGameResetState);

    }

    private void OnGameResetState()
    {
        ResetLevels();
    }

    private void Awake()
    {
        ResetLevels();

        DontDestroyOnLoad(this.gameObject);
    }

    private void ResetLevels()
    {
        chunksLeft = new List<Chunk>();
        chunksRight = new List<Chunk>();

        UpdateLevelData();

        DebugPrintLevelData();

        //GenerateStartHub();
        //GenerateRandomHub();




        //GenerateLevel("megadungeon-1-0", 0, 0);

        //for (int i = 1; i < chunkBuffer - 1; i++)
        //{
        //    Chunk temp = GenerateLevel("hub-1-1", hubWidth * i, 0);
        //    chunksRight.Add(temp);
        //}
        //for (int i = 1; i < chunkBuffer - 1; i++)
        //{
        //    Chunk temp = GenerateLevel("hub-1-1", -hubWidth * i, 0);
        //    chunksLeft.Add(temp);
        //}


        // temporary hardcode
        // TODO: check chunk distance and spawn and despawn by player radius

        Chunk spawnPointHub;
        spawnPointHub = GenerateLevel("hub-1-0-start", 0, 0);

        GenerateLevel("hub-1-1", 9, 0);
        GenerateLevel("hub-1-1", 18, 0);
        GenerateLevel("hub-1-1", 27, 0);

        GenerateLevel("corner-bottomright", 36, 0);

        GenerateLevel("edge-vertical", 36, 9);
        GenerateLevel("edge-vertical", 36, 18);

        GenerateLevel("corner-topright", 36, 27);

        GenerateLevel("edge-horizontal", 27, 27);
        GenerateLevel("edge-horizontal", 18, 27);
        GenerateLevel("edge-horizontal", 9, 27);

        GenerateLevel("hub-1-1", 0, 27);

        GenerateLevel("edge-horizontal", -9, 27);
        GenerateLevel("edge-horizontal", -18, 27);
        GenerateLevel("edge-horizontal", -27, 27);

        GenerateLevel("corner-topleft", -36, 27);

        GenerateLevel("edge-vertical", -36, 18);
        GenerateLevel("edge-vertical", -36, 9);

        GenerateLevel("corner-bottomleft", -36, 0);

        GenerateLevel("hub-1-1", -27, 0);
        GenerateLevel("hub-1-1", -18, 0);
        GenerateLevel("hub-1-1", -9, 0);

        Messenger.Broadcast(GameActionKeys.LevelLoaded);

        //CopyLevel(spawnPointHub, 10, 10);

        // after generation complete, rotate level transform

        //assetHolder.RotateLevel();
    }

    public void UpdateLevelData()
    {
        levelObjectDictionary = LoadLevelData(PATH_LEVELS);
    }

    public Dictionary<string, LevelObject> LoadLevelData(string path)
    {
        // load text assets as object array and cast to text asset array

        TextAsset[] jsonTextAssets = Resources.LoadAll<TextAsset>(path);

        Dictionary<string, LevelObject> jsonLevelDictionary = new Dictionary<string, LevelObject>();

        string levelJson = "";
        foreach (TextAsset jsonTextAsset in jsonTextAssets)
        {
            if (jsonTextAsset != null)
            {
                //Debug.Log(typeof(LevelObject) + jsonTextAsset.text);
                levelJson = jsonTextAsset.text;
            }

            LevelObject tempLevel = levelJson.FromJSON<LevelObject>();

            jsonLevelDictionary.Add(tempLevel.Code, tempLevel);
        }

        return jsonLevelDictionary;
    }

    public Chunk GenerateLevel(string code, int xOffset, int yOffset)
    {
        LevelObject levelObject = levelObjectDictionary[code];

        //if (levelObject.Type == "hub")
        //{
        //    GenerateHub(levelObject, xOffset, yOffset);
        //}

        //GenerateHub(levelObject, xOffset, yOffset);


        return ParseLevelData(levelObject, xOffset, yOffset);
    }

    //public void GenerateHub(LevelObject levelObject, int xOffset, int yOffset)
    //{
    //    ParseLevelData(levelObject.Data, xOffset, yOffset);
    //}

    private void CopyLevel(Chunk spawnPointHub, int xOffset, int yOffset)
    {
        List<List<GameObject>> chunkTiles = spawnPointHub.Tiles;

        int x = xOffset;
        int y = chunkTiles.Count + yOffset;

        // TODO: Spawn new temp items like enemies and coins

        foreach (List<GameObject> chunkRows in chunkTiles)
        {
            x = xOffset;
            foreach (GameObject chunkColumn in chunkRows)
            {
                assetHolder.SpawnObject(chunkColumn, x, y);

                x++;
            }
            y--;
        }
    }

    private Chunk ParseLevelData(LevelObject levelObject, int xOffset, int yOffset)
    {
        Debug.Log("started parsing level data");

        List<string> data = levelObject.Data;

        int x = xOffset;
        int y = data.Count + yOffset;

        Chunk chunk = new Chunk();
        chunk.Type = levelObject.Type;

        List<List<GameObject>> chunkColumns = new List<List<GameObject>>();

        foreach (string row in data)
        {
            List<GameObject> chunkRow = new List<GameObject>();

            char[] columns = row.ToCharArray();

            x = xOffset;

            foreach (char column in columns)
            {
                GameObject permanentTile;

                GameObject temporaryItem;

                bool isObstacle = true;

                if (column == GameAssetCodes.wall)
                {
                    permanentTile = SpawnRandomWall(x, y);
                }
                else if (column == GameAssetCodes.blankRandom)
                {
                    permanentTile = SpawnWallWithProbability(x, y, 1, 4);
                }
                else if (column == GameAssetCodes.optionalPath)
                {
                    permanentTile = SpawnWallWithProbability(x, y, 1, 5);
                }
                else if (column == GameAssetCodes.optionalWall)
                {
                    permanentTile = SpawnWallWithProbability(x, y, 3, 4);
                }
                else
                {
                    permanentTile = SpawnEmptyFloor(x, y);
                    isObstacle = false;
                }

                if (!isObstacle)
                {
                    // if space is free, spawn items

                    if (column == GameAssetCodes.centerLane)
                    {
                        temporaryItem = SpawnRandomItemWithProbability(x, y, 1, 4);
                    }
                    else if (column == GameAssetCodes.enemy)
                    {
                        temporaryItem = SpawnRandomEnemy(x, y);
                    }
                    else if (column == GameAssetCodes.spawnPoint)
                    {
                        temporaryItem = SpawnSpawnPoint(x, y);
                    }
                    else if (column == GameAssetCodes.door)
                    {
                        permanentTile = SpawnDoor(x, y);
                    }
                    else if (column == GameAssetCodes.portal)
                    {
                        permanentTile = SpawnPortal(x, y);
                    }
                    else if (column == GameAssetCodes.coin)
                    {
                        temporaryItem = SpawnCoin(x, y);
                    }
                    else if (column == GameAssetCodes.treasure)
                    {
                        temporaryItem = SpawnTreasure(x, y);
                    }
                    else if (column == GameAssetCodes.goldBlock)
                    {
                        temporaryItem = SpawnGoldBlock(x, y);
                    }
                    else
                    {
                        temporaryItem = SpawnRandomEntityWithProbability(x, y, 1, 5);
                    }
                }

                chunkRow.Add(permanentTile);

                x++;
            }
            chunkColumns.Add(chunkRow);

            y--;
        }

        chunk.Tiles = chunkColumns;

        return chunk;
    }

    // spawn enemy or item
    private GameObject SpawnRandomEntityWithProbability(int x, int y, int numerator, int maxRange)
    {
        int choice = Random.Range(1, maxRange);

        if (choice <= numerator)
        {
            int entityChoice = Random.Range(1, 3);
            {
                if (entityChoice == 1)
                {
                    return SpawnRandomEnemy(x,y);
                }
                else
                {
                    return SpawnRandomItem(x, y);
                }
            }
        }
        else
        {
            // spawn nothing
            return null;
        }

    }

    private GameObject SpawnRandomItem(int x, int y)
    {
        GameObject temp;

        int choice = Random.Range(1, 10);

        if (choice == 1)
        {
            temp = SpawnGoldBlock(x, y);
        }
        else if (choice < 3)
        {
            temp = SpawnTreasure(x, y);
        }
        else
        {
            temp = SpawnCoin(x, y);

        }
        return temp;
    }

    private GameObject SpawnGoldBlock(int x, int y)
    {
        return assetHolder.SpawnObject(assetHolder.goldBlock, x, y);
    }

    private GameObject SpawnTreasure(int x, int y)
    {
        return assetHolder.SpawnObject(assetHolder.treasure, x, y);
    }

    private GameObject SpawnCoin(int x, int y)
    {
        return assetHolder.SpawnObject(assetHolder.coin, x, y);
    }

    private GameObject SpawnPortal(int x, int y)
    {
        return assetHolder.SpawnObject(assetHolder.portal, x, y);
    }

    private GameObject SpawnDoor(int x, int y)
    {
        return assetHolder.SpawnObject(assetHolder.door, x, y);
    }

    private GameObject SpawnSpawnPoint(int x, int y)
    {
        if (Player.Instance == null)
        {
            return assetHolder.SpawnObject(assetHolder.player, x, y);
        }
        else
        {
            Messenger.Broadcast<int>(GameActionKeys.playerHealthChanged, 20);
            Player.Instance.SetPlayerPosition(3f, 5f);
            return Player.Instance.gameObject;
        }

    }

    private GameObject SpawnWallWithProbability(int x, int y, int numerator, int maxRange)
    {
        int choice = Random.Range(1, maxRange);

        if (choice <= numerator)
        {
            return SpawnRandomWall(x, y);
        }
        else
        {
            // spawn nothing
            return SpawnEmptyFloor(x, y);
        }
    }

    private GameObject SpawnRandomItemWithProbability(int x, int y, int numerator, int maxRange)
    {
        int choice = Random.Range(1, maxRange);

        if (choice <= numerator)
        {
            return SpawnRandomItem(x, y);
        }
        else
        {
            // spawn nothing
            return null;
        }
    }

    private GameObject SpawnEmptyFloor(int x, int y)
    {
        return SpawnRandomFloor(x, y);
    }

    private GameObject SpawnRandomFloor(int x, int y)
    {
        int choice = Random.Range(0, assetHolder.floors.Count - 1);

        return assetHolder.SpawnObject(assetHolder.floors[choice], x, y);
    }

    private GameObject SpawnRandomEnemy(int x, int y)
    {
        int choice = Random.Range(0, assetHolder.enemies.Count - 1);

        return assetHolder.SpawnObject(assetHolder.enemies[choice], x, y);
    }

    private GameObject SpawnRandomWall(int x, int y)
    {
        int choice = Random.Range(0, assetHolder.walls.Count - 1);

        return assetHolder.SpawnObject(assetHolder.walls[choice], x, y);
    }

    public void DebugPrintLevelData()
    {
        foreach (KeyValuePair<string, LevelObject> kvp in levelObjectDictionary)
        {
            LevelObject level = kvp.Value;

            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("Key: ");
            stringBuilder.Append(kvp.Key);

            stringBuilder.Append("\nCode: ");
            stringBuilder.Append(level.Code);

            stringBuilder.Append("\nType: ");
            stringBuilder.Append(level.Type);

            stringBuilder.Append("\nSize: ");
            stringBuilder.Append(level.Size);

            foreach (string dataRow in level.Data)
            {
                stringBuilder.Append("\n");
                stringBuilder.Append(dataRow);
            }

            Debug.Log(stringBuilder.ToString());
        }
    }
}
