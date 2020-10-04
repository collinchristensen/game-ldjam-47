using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEditorInternal;
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

    public List<GameObject> Tiles { get; set; }
}

public class GameLevelGen : MonoBehaviour
{
    public const string PATH_LEVELS = "levels/";

    public AssetHolder assetHolder;

    private Dictionary<string, LevelObject> levelObjectDictionary;

    List<Chunk> chunkList;

    public int hubCount = 6;


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

    // path must end in  .json, file itself must end in .json.txt

    private void Awake()
    {
        chunkList = new List<Chunk>();

        UpdateLevelData();

        DebugPrintLevelData();

        //GenerateStartHub();

        GenerateLevel("hub-1-0-start", 0, 0);
        //GenerateLevel("megadungeon-1-0", 0, 0);

        for (int i = 1; i < 25; i++)
        {
            GenerateLevel("hub-1-2", 7 * i, 0);
        }
        for (int i = 1; i < 25; i++)
        {
            GenerateLevel("hub-1-2", -7 * i, 0);
        }

        // after generation complete, rotate level transform

        assetHolder.RotateLevel();
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

    //public void GenerateLevel(string code)
    //{
    //    GenerateLevel(code, 0, 0);
    //}

    public void GenerateLevel(string code, int xOffset, int yOffset)
    {
        LevelObject levelObject = levelObjectDictionary[code];

        //if (levelObject.Type == "hub")
        //{
        //    GenerateHub(levelObject, xOffset, yOffset);
        //}

        GenerateHub(levelObject, xOffset, yOffset);
    }

    //public void GenerateHub(LevelObject levelObject)
    //{
    //    GenerateHub(levelObject, 0, 0);
    //}

    public void GenerateHub(LevelObject levelObject, int xOffset, int yOffset)
    {
        ParseLevelData(levelObject.Data, xOffset, yOffset);
    }

    //private void ParseLevelData(List<string> data)
    //{
    //    ParseLevelData(data, 0, 0);
    //}

    private void ParseLevelData(List<string> data, int xOffset, int yOffset)
    {
        Debug.Log("started parsing level data");

        int x = xOffset;
        int y = data.Count + yOffset;

        foreach (string row in data)
        {
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

                    permanentTile = SpawnWallWithProbability(x, y, 1, 3);
                }
                else if (column == GameAssetCodes.optionalPath)
                {
                    permanentTile = SpawnWallWithProbability(x, y, 1, 4);
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
                        temporaryItem = SpawnRandomEntityWithProbability(x, y, 1, 4);
                    }
                }

                x++;
            }

            y--;
        }
    }

    // spawn enemy or item
    private GameObject SpawnRandomEntityWithProbability(int x, int y, int numerator, int maxRange)
    {
        int choice = Random.Range(1, maxRange);

        if (choice <= numerator)
        {
            int entityChoice = Random.Range(1, 2);
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
        return assetHolder.SpawnObject(assetHolder.player, x, y);
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
            return null;
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

    //private void SpawnPortal(int x, int y)
    //{
    //    SpawnEmptyFloor(x, y);
    //    assetHolder.SpawnObject(assetHolder.portal, x, y);
    //}

    //private void SpawnItem(int x, int y, string itemName)
    //{
    //    SpawnEmptyFloor(x, y);
    //    SpawnRandomItem(x, y);
    //}

    //// Spawn floor with small chance of good item
    //private void SpawnRandomFloor(int x, int y)
    //{
    //    int selected = Random.Range(0, assetHolder.floors.Count);

    //    SpawnSelectedFloor(selected, x, y);

    //    // small chance of spawning item
    //    int chance = Random.Range(1, 10);

    //    if (chance == 1)
    //    {
    //        SpawnRandomItem(x, y);
    //    }

    //}

    //// Spawn floor with small chance of good item
    //private void SpawnEmptyFloor(int x, int y)
    //{
    //    int selected = Random.Range(0, assetHolder.floors.Count);

    //    SpawnSelectedFloor(selected, x, y);

    //}

    //private void SpawnSelectedFloor(int selected, int x, int y)
    //{
    //    assetHolder.SpawnObject(assetHolder.floors[selected], x, y);
    //}

    //private void SpawnRandomWall(int x, int y)
    //{
    //    int wallsCount = assetHolder.walls.Count;

    //    int selected = Random.Range(0, wallsCount);

    //    assetHolder.SpawnObject(assetHolder.walls[selected], x, y);
    //}

    //private void SpawnSpawnPoint(int x, int y)
    //{
    //    // TODO: add persistent spawnpoint

    //    assetHolder.SpawnObject(assetHolder.player, x, y);

    //}

    //// Spawn entity, either item or enemy
    //private void SpawnRandomEntity(int x, int y)
    //{
    //    // TODO: add enemies and items

    //    // spawn item or enemy with 75% chance enemy
    //    int chance = Random.Range(1, 4);

    //    if (chance == 1)
    //    {
    //        SpawnRandomItem(x, y);
    //    }
    //    else
    //    {
    //        SpawnRandomEnemy(x, y);
    //    }

    //    assetHolder.SpawnObject(assetHolder.floors[2], x, y);
    //}

    //private void SpawnDoor(int x, int y)
    //{
    //    assetHolder.SpawnObject(assetHolder.door, x, y);
    //}

    //private void SpawnRandomEnemy(int x, int y)
    //{
    //    assetHolder.SpawnObject(assetHolder.floors[2], x, y);
    //}

    //// Spawn good item
    //private void SpawnRandomItem(int x, int y, float v, int v1)
    //{

    //    // spawn item, better items have lower chance

    //    int chance = Random.Range(1, 10);

    //    if (chance == 1)
    //    {
    //        SpawnItem(x, y, ItemCodes.goldblock);
    //    }
    //    else if (chance < 3)
    //    {
    //        SpawnItem(x, y, ItemCodes.treasure);
    //    }
    //    else
    //    {
    //        SpawnItem(x, y, ItemCodes.coin);
    //    }

    //}

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
