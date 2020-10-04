using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class GameAssetCodes
{
    public static string blankRandom = " ";

    public static string blankPath = "-";
    public static string wall = "W";
    public static string door = "D";

    public static string spawnPoint = "S";

    public static string optionalPath = ".";
    public static string optionalWall = "O";

    //public static string coin = "c";
    //public static string enemy = "e";
    //public static string treasure = "T";
    //public static string ladder = "L";
    //public static string portal = "p";

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


    List<LevelObject> levelsList;

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
    }

    public void UpdateLevelData()
    {
        levelsList = LoadLevelData(PATH_LEVELS);
    }

    public List<LevelObject> LoadLevelData(string path)
    {
        // load text assets as object array and cast to text asset array

        TextAsset[] jsonTextAssets = Resources.LoadAll<TextAsset>(path);

        List<LevelObject> jsonObjectList = new List<LevelObject>();

        string levelJson = "";
        foreach (TextAsset jsonTextAsset in jsonTextAssets)
        {
            if (jsonTextAsset != null)
            {
                Debug.Log(typeof(LevelObject) + jsonTextAsset.text);
                levelJson = jsonTextAsset.text;
            }

            jsonObjectList.Add(levelJson.FromJSON<LevelObject>());
        }

        return jsonObjectList;
    }

    public void DebugPrintLevelData()
    {
        foreach (LevelObject level in levelsList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(level.Code);
            stringBuilder.Append(level.Type);
            stringBuilder.Append(level.Size);
            foreach (string dataRow in level.Data)
            {
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
