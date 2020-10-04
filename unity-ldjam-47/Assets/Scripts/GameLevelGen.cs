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


    Dictionary<string, LevelObject> levelObjectDictionary;


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
        LevelObject levelObject = levelObjectDictionary["code"];

        if (levelObject.Code == "hub")
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
                        SpawnRandomEntity(x,y);
                    }
                    else
                    {
                        SpawnRandomEnemy(x,y);
                    }

                }
                else if (column == GameAssetCodes.blankPath)
                {
                    // spawn floor 
                    SpawnRandomEnemy(x,y);

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
                        SpawnRandomEnemy(x,y);
                    }

                }
                else if (column == GameAssetCodes.optionalWall)
                {
                    // spawn floor or wall with 75% chance wall
                    int chance = Random.Range(1, 4);

                    if (chance == 1)
                    {
                        SpawnRandomEnemy(x,y);
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
                    SpawnRandomEnemy(x,y);

                    // spawn player on top of floor
                    SpawnSpawnPoint(x, y);

                }

                x++;
            }

            y--;
        }
    }

    private void SpawnSpawnPoint(int x, int y)
    {
        throw new NotImplementedException();
    }

    // Spawn enemy or item
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

        SpawnRandomEntity(x,y);
    }

    private void SpawnDoor(int x, int y)
    {
        throw new NotImplementedException();
    }

    // Spawn floor with small chance of good item
    private void SpawnRandomEnemy(int x, int y)
    {
        throw new NotImplementedException();
    }

    // Spawn good item
    private void SpawnRandomItem(int x, int y)
    {
        // TODO: add enemies and items


    }

    private void SpawnRandomWall(int x, int y)
    {
        throw new NotImplementedException();
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




    //public string LoadDataFromResources(string path)
    //	{

    //		string dataValue = "";

    //		TextAsset textData = Resources.Load<TextAsset>(path);
    //		if (textData != null)
    //		{
    //			dataValue = textData.text;
    //		}

    //		return dataValue;
    //	}

    //	public void LoadLevel(string levelCode)
    //	{
    //		UpdateAssetLayout(levelCode);
    //		RenderLevel();
    //	}


    //  public void LoadLevel(string levelCode)
    //	{
    //		UpdateAssetLayout(levelCode);
    //		RenderLevel();
    //	}

    //	public void UpdateAssetLayout(string levelCode)
    //	{

    //		// Creates the assetLayout grid to later be rendered

    //		tileMapData = new List<List<GameAssetTile>>();

    //		List<string> levelData = GetLevelData(levelCode);

    //		int x = 0;
    //		int y = levelData.Count;

    //		gridHeight = levelData.Count;

    //		foreach (string row in levelData)
    //		{

    //			List<string> arrCol = row.ToStringArray();

    //			List<GameAssetTile> listRow = new List<GameAssetTile>();

    //			gridWidth = arrCol.Count;

    //			x = 0;

    //			foreach (string col in arrCol)
    //			{

    //				GameAssetTile tile = new GameAssetTile();

    //				// laod asset and render at x/y

    //				string val = col;
    //				string name = GameAssetKeys.none;

    //				int nodeX = (int)x;
    //				int nodeY = (levelData.Count) - ((int)y);

    //				string colVal = col;

    //				if (colVal == GameAssetCodes.none)
    //				{
    //					// Fill random blocks here at random for pathfinding	
    //					int randomItem = UnityEngine.Random.Range(1, 10);
    //					if (randomItem >= 3 && randomItem < 5)
    //					{
    //						colVal = GameAssetCodes.block;
    //					}
    //				}

    //				if (colVal == GameAssetCodes.block)
    //				{
    //					name = GameAssetKeys.block;
    //					nodes.Add(new Point(nodeX, nodeY));
    //				}
    //				else if (colVal == GameAssetCodes.none)
    //				{
    //					name = GameAssetKeys.none;
    //				}
    //				else if (colVal == GameAssetCodes.blank)
    //				{
    //					name = GameAssetKeys.blank;
    //				}
    //				else if (colVal == GameAssetCodes.coin)
    //				{
    //					name = GameAssetKeys.coin;
    //				}
    //				else if (colVal == GameAssetCodes.exit)
    //				{
    //					name = GameAssetKeys.exit;
    //				}
    //				else if (colVal == GameAssetCodes.player)
    //				{
    //					name = GameAssetKeys.player;
    //				}
    //				else if (colVal == GameAssetCodes.enemyeye)
    //				{
    //					name = GameAssetKeys.enemyeye;
    //				}
    //				else if (colVal == GameAssetCodes.enemyskull)
    //				{
    //					name = GameAssetKeys.enemyskull;
    //				}

    //				tile.pos.x = (x - ((gridWidth) / 2));
    //				tile.pos.y = (y - ((gridHeight) / 2));
    //				tile.val = val;
    //				tile.name = name;

    //				listRow.Add(tile);

    //				x++;

    //			}

    //			tileMapData.Add(listRow);

    //			y--;
    //		}
    //	}


    //	public string GetRandomBackgroundTile()
    //	{
    //		// randomize tile backers

    //		string tileName = "";

    //		int randomTile = UnityEngine.Random.Range(1, 3);

    //		if (randomTile == 1)
    //		{
    //			tileName = GameAssetKeys.tiled;
    //		}
    //		else if (randomTile == 2)
    //		{
    //			tileName = GameAssetKeys.tilebroken;
    //		}
    //		else if (randomTile == 3)
    //		{
    //			tileName = GameAssetKeys.tileruined;
    //		}
    //		else
    //		{
    //			tileName = GameAssetKeys.tiled;
    //		}
    //		return tileName;
    //	}
}
