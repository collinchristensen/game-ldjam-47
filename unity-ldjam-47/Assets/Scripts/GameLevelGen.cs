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
    public static char blankRandom = ' ';

    public static char blankPath = '-';
    public static char wall = 'W';
    public static char door = 'D';

    public static char spawnPoint = 'S';

    public static char optionalPath = '.';
    public static char optionalWall = 'O';

    //public static char coin = 'c';
    //public static char enemy = 'e';
    //public static char treasure = 'T';
    //public static char ladder = 'L';
    //public static char portal = 'p';

}


// JSON classes

public class LevelObject
{
    public string Code { get; set; }
    public string Type { get; set; }

    public string Size { get; set; }
    public List<string> Data { get; set; }
}

public class GameLevelGen : MonoBehaviour
{
    public const string PATH_LEVELS = "levels/";

    public AssetHolder assetHolder;

    private Dictionary<string, LevelObject> levelObjectDictionary;


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

        UpdateLevelData();

        DebugPrintLevelData();

        GenerateLevel("hub-1-0-start");
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

    public void GenerateLevel(string code)
    {
        LevelObject levelObject = levelObjectDictionary[code];

        if (levelObject.Type == "hub")
        {
            GenerateHub(levelObject);
        }
    }

    public void GenerateHub(LevelObject levelObject)
    {
        ParseLevelData(levelObject.Data);
    }

    private void ParseLevelData(List<string> data)
    {
        Debug.Log("started parsing level data");

        int x = 0;
        int y = data.Count;

        foreach (string row in data)
        {
            char[] columns = row.ToCharArray();

            x = 0;

            foreach (char column in columns)
            {
                GameObject tile;

                if (column == GameAssetCodes.blankRandom)
                {
                    // spawn floor or wall
                    int chance = Random.Range(1, 10);

                    if (chance < 5)
                    {
                        SpawnRandomWall(x,y);
                    }
                    else if (chance > 8)
                    {
                        SpawnRandomFloor(x,y);
                    }
                    else
                    {
                        SpawnRandomEntity(x,y);
                    }

                }
                else if (column == GameAssetCodes.blankPath)
                {
                    // spawn floor 
                    SpawnRandomFloor(x,y);

                }
                else if (column == GameAssetCodes.wall)
                {
                    // spawn wall with 100% chance
                    SpawnRandomWall(x,y);

                }
                else if (column == GameAssetCodes.optionalPath)
                {
                    // spawn floor or wall with 75% chance path
                    int chance = Random.Range(1, 4);

                    if (chance == 1)
                    {
                        SpawnRandomWall(x,y);
                    }
                    else
                    {
                        SpawnRandomFloor(x,y);
                    }

                }
                else if (column == GameAssetCodes.optionalWall)
                {
                    // spawn floor or wall with 75% chance wall
                    int chance = Random.Range(1, 4);

                    if (chance == 1)
                    {
                        SpawnRandomFloor(x,y);
                    }
                    else
                    {
                        SpawnRandomWall(x,y);
                    }

                }
                else if (column == GameAssetCodes.door)
                {
                    SpawnDoor(x,y);
                }
                else if (column == GameAssetCodes.spawnPoint)
                {
                    // spawn floor
                    SpawnRandomFloor(x,y);

                    // spawn player on top of floor
                    SpawnSpawnPoint(x, y);

                }

                x++;
            }

            y--;
        }
    }

    // Spawn floor with small chance of good item
    private void SpawnRandomFloor(int x, int y)
    {
        int selected = Random.Range(0, assetHolder.floors.Count);

        SpawnSelectedFloor(selected, x, y);

        // small chance of spawning item
        int chance = Random.Range(1, 10);

        if (chance == 1)
        {
            //SpawnRandomItem(x, y);
        }

    }

    private void SpawnSelectedFloor(int selected, int x, int y)
    {
        assetHolder.SpawnObject(assetHolder.floors[selected], x, y);
    }

    private void SpawnRandomWall(int x, int y)
    {
        int wallsCount = assetHolder.walls.Count;

        int selected = Random.Range(0, wallsCount);

        assetHolder.SpawnObject(assetHolder.walls[selected], x, y);
    }

    private void SpawnSpawnPoint(int x, int y)
    {
        // TODO: add persistent spawnpoint

        assetHolder.SpawnObject(assetHolder.player, x, y);

    }

    // Spawn entity, either item or enemy
    private void SpawnRandomEntity(int x, int y)
    {
        // TODO: add enemies and items

        // spawn item or enemy with 75% chance enemy
        int chance = Random.Range(1, 4);

        if (chance == 1)
        {
            //SpawnRandomItem(x, y);
        }
        else
        {
            //SpawnRandomEnemy(x, y);
        }

        assetHolder.SpawnObject(assetHolder.floors[2], x, y);
    }

    private void SpawnDoor(int x, int y)
    {
        assetHolder.SpawnObject(assetHolder.door, x, y);
    }

    private void SpawnRandomEnemy(int x, int y)
    {
        assetHolder.SpawnObject(assetHolder.floors[2], x, y);
    }

    // Spawn good item
    private void SpawnRandomItem(int x, int y)
    {
        // TODO: add enemies and items

        assetHolder.SpawnObject(assetHolder.floors[2], x, y);

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
