//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;


//public class GameLevel
//{
//    public string code = "";
//    public string type = "";
//    public List<string> data = new List<string>();
//}

//public class GameAssetTile
//{
//    public Vector2 pos = new Vector2(0, 0);
//    public string name = GameStyleAssets.tiled;
//    public string val = GameAssetKeys.none;
//}

//public enum GameTileStatus
//{
//	undetermined,
//	obstacle,
//	free
//}

////public class GameAssetKeys
////{
////    // blankRandom can be either floor or wall
////    public static string blankRandom = "blankRandom";

////    public static string blankPath = "blankPath";
////    public static string none = blankPath;

////    public static string wall = "wall";
////    public static string door = "door";

////    public static string player = "player";

////    //public static string coin = "coin";
////    //public static string enemy = "enemy";
////    //public static string treasure = "treasure";
////    //public static string ladder = "ladder";
////    //public static string portal = "portal";

////}

//public class GameAssetCodes
//{
//    public static string blankRandom = " ";

//    public static string blankPath = "-";
//    public static string wall = "W";
//    public static string door = "D";

//    public static string spawnPoint = "S";

//    public static string optionalPath = ".";
//    public static string optionalWall = "O";

//    //public static string coin = "c";
//    //public static string enemy = "e";
//    //public static string treasure = "T";
//    //public static string ladder = "L";
//    //public static string portal = "p";

//}

//public class GameLevels : MonoBehaviour
//{
//	public List<List<GameAssetTile>> tileMapData = new List<List<GameAssetTile>>();
//	public int gridWidth = 0;
//	public int gridHeight = 0; // set on level load.

//	public float tileScale = 1f;
//	public float tileSize = 1f;

//	// singleton pattern
//	private static GameLevels _instance = null;

//	public static GameLevels instance
//	{
//		get
//		{
//			if (!_instance)
//			{
//				_instance = FindObjectOfType(typeof(GameLevels)) as GameLevels;

//				if (!_instance)
//				{
//					var obj = new GameObject("_GameLevels");
//					_instance = obj.AddComponent<GameLevels>();
//				}
//			}

//			return _instance;
//		}
//	}

//	private void OnApplicationQuit()
//	{
//		// release reference on exit
//		_instance = null;
//	}

//	void Awake()
//	{
//		_instance = this;
//	}

//	public string LoadDataFromResources(string path)
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

//	public Point GetGridTile(Vector3 pos)
//	{ //get_grid_tile

//		int tileX = 0;
//		int tileY = 0;

//		//Vector3 positionLevelContainer = GameController.GetLevelContainerPosition();

//		int gridWidth = GameLevels.instance.gridWidth;
//		int gridHeight = GameLevels.instance.gridHeight;
//		float tileSize = GameLevels.instance.tileSize;
//		float tileScale = GameLevels.instance.tileScale;

//		Vector3 posScale = ((pos));

//		posScale = ((posScale * tileSize) / tileScale);

//		if (GameLevels.instance != null)
//		{
//			tileX = (int)Math.Ceiling(
//				(((gridWidth) / 2) + (posScale.x))
//			);
//			tileY = (int)Math.Ceiling(
//				(((gridHeight) / 2) - (posScale.y)) + 1
//			);
//		}

//		return new Point(tileX, tileY);
//	}

//	public Vector3 GetGridPosScaled(Vector3 pos)
//	{

//		pos /= tileSize;

//		pos = pos * tileScale;

//		return pos;
//	}

//	public Vector3 GetGridTilePos(int tileX, int tileY)
//	{

//		Vector3 pos = Vector3.zero;

//		pos.x = tileX;
//		pos.y = tileY;

//		pos = GetGridPosScaled(pos);

//		return pos;
//	}

//	public void RenderTile(GameAssetTile tile)
//	{

//		if (tile == null)
//		{
//			return;
//		}

//		GameObject parentObject = GameController.instance.containerGameLevel;

//		// Load prefab from Resources/Assets/

//		GameObject prefabTile = PrefabsPool.PoolPrefab("Assets/" + tile.name);

//		if (prefabTile != null)
//		{

//			// Create GameObject from prefab using pooled

//			GameObject go = GameObjectHelper.CreateGameObject(
//				prefabTile,
//				Vector3.zero,
//				Quaternion.identity,
//				true);

//			// Set position in the grid

//			if (go != null)
//			{
//				go.transform.parent = parentObject.transform;
//				go.transform.position = Vector3.zero;
//				go.transform.localPosition = Vector3.zero;

//				go.transform.localPosition = GetGridPosScaled(tile.pos);//, (int)tile.pos.x, (int)tile.pos.y);

//				go.transform.localScale = Vector3.one * tileScale;

//				GameTile gameTile = go.AddComponent<GameTile>();
//				gameTile.assetTile = tile;

//				if (tile.name == GameAssetKeys.player
//					|| tile.name.Contains(GameAssetKeys.enemy))
//				{
//					// allow movement
//					gameTile.tileType = GameTileStatus.free;
//				}
//				else
//				{
//					// lock it in place in the 2d physics system
//					gameTile.tileType = GameTileStatus.locked;
//				}

//				// attach the components and set random / magix

//				if (tile.name == GameAssetKeys.player)
//				{

//					// If this is a player then add the GamePlayer Component
//					// for health, speed, score and input controls
//					GamePlayer gamePlayer = go.Set<GamePlayer>();
//					GameController.instance.currentGamePlayer = gamePlayer;
//				}
//				else if (tile.name.StartsWith(GameAssetKeys.enemy))
//				{

//					// If this is an enemy randomize 
//					// Enemies can use pathfinding or not with GameEnemy usePathfinding A*

//					GameEnemy gameEnemy = go.Set<GameEnemy>();
//					gameEnemy.usePathfinding = GameController.usePathfinding;
//					gameEnemy.speed = UnityEngine.Random.Range(.3f, 1.7f);
//					gameEnemy.health = gameEnemy.health * UnityEngine.Random.Range(.8f, 1.2f);
//				}
//			}
//		}
//	}

//	public void RenderLevel()
//	{
//		// Render current assetLayout items to screen


//		GameObject parentObject = GameController.instance.containerGameLevel;

//		parentObject.DestroyChildren();

//		foreach (List<GameAssetTile> listRow in tileMapData)
//		{
//			foreach (GameAssetTile tile in listRow)
//			{

//				if (!string.IsNullOrEmpty(tile.name))
//				{

//					// random tile the path set					
//					// randomize available slots

//					// Add coins and blocks first for pathfinding

//					int randomItem = UnityEngine.Random.Range(1, 10);

//					if (tile.name == GameAssetKeys.none)
//					{

//						// randomize tile backers
//						tile.name = GetRandomBackgroundTile();

//						// if it is a background tile randomly place coins

//						if (randomItem < 3)
//						{
//							GameAssetTile tileCoin = new GameAssetTile();

//							tileCoin.pos = tile.pos;
//							tileCoin.val = GameAssetCodes.coin;
//							tileCoin.name = GameAssetKeys.coin;

//							RenderTile(tileCoin);
//						}
//						else if (randomItem >= 3 && randomItem < 5)
//						{
//							// block added above
//						}
//						else
//						{
//							randomItem = UnityEngine.Random.Range(1, 30);

//							// If not in debug, spawn random enemies
//							if (!GameController.debugMode)
//							{
//								if (randomItem == 1)
//								{

//									GameAssetTile tileEnemy = new GameAssetTile();
//									tileEnemy.pos = tile.pos;
//									tileEnemy.val = GameAssetCodes.enemyeye;
//									tileEnemy.name = GameAssetKeys.enemyeye;

//									RenderTile(tileEnemy);
//								}
//								else if (randomItem == 2)
//								{

//									GameAssetTile tileEnemy = new GameAssetTile();
//									tileEnemy.pos = tile.pos;
//									tileEnemy.val = GameAssetCodes.enemyskull;
//									tileEnemy.name = GameAssetKeys.enemyskull;

//									RenderTile(tileEnemy);
//								}
//							}
//						}
//					}

//					if (tile.name == GameAssetKeys.blank)
//					{

//						// randomize tile backers

//						tile.name = GetRandomBackgroundTile();

//						int randomCoin = UnityEngine.Random.Range(1, 10);

//						if (randomCoin < 2)
//						{
//							GameAssetTile tileCoin = new GameAssetTile();

//							tileCoin.pos = tile.pos;
//							tileCoin.val = GameAssetCodes.coin;
//							tileCoin.name = GameAssetKeys.coin;

//							RenderTile(tileCoin);
//						}
//					}

//					RenderTile(tile);
//				}
//			}
//		}
//	}

//	public List<string> GetLevelData(string levelCode)
//	{

//		string levelData = LoadDataFromResources("Levels/" + levelCode + ".json");

//		GameLevel data = Engine.Data.Json.JsonMapper.ToObject<GameLevel>(levelData);

//		return data.data;
//	}
//}
