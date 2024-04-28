using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;
using System.Threading;

namespace libs;

using System.Security.Cryptography;
using Newtonsoft.Json;

public sealed class GameEngine
{
    private static GameEngine? _instance;
    private IGameObjectFactory gameObjectFactory;

    public static GameEngine Instance {
        get{
            if(_instance == null)
            {
                _instance = new GameEngine();
            }
            return _instance;
        }
    }


    private GameEngine() {
        //INIT PROPS HERE IF NEEDED
        gameObjectFactory = new GameObjectFactory();
    }

    private GameObject? _focusedObject;

    private Map map = new Map();

    private List<GameObject> gameObjects = new List<GameObject>();


    public Map GetMap() {
        return map;
    }

    public GameObject GetFocusedObject(){
        return _focusedObject;
    }

    public void Setup(){
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        dynamic gameData = FileHandler.ReadJson();
        
        map.MapWidth = gameData.map.width;
        map.MapHeight = gameData.map.height;

        gameObjects = new List<GameObject>();

        foreach (var gameObject in gameData.gameObjects)
        {
            AddGameObject(CreateGameObject(gameObject));
        }
        
        _focusedObject = gameObjects.OfType<Player>().First();

    }

    private int numberofSeconds = 0;
    public int GetNumberOfSeconds() { 
        return numberofSeconds; 
    }
 
    public void SetNumberOfSeconds(int value) {
 
        numberofSeconds = value;
 
    }

    public void Render() {
        
        //Clean the map
        Console.Clear();

        map.Initialize();

        PlaceGameObjects();

        // Render the map
        for (int i = 0; i < map.MapHeight; i++)
        {
            for (int j = 0; j < map.MapWidth; j++)
            {
                DrawObject(map.Get(i, j));
            }
            Console.WriteLine();
        };

        Console.Write($"\rTime remaining: {this.GetNumberOfSeconds()} seconds ");
    }
    
    // Method to create GameObject using the factory from clients
    public GameObject CreateGameObject(dynamic obj)
    {
        return gameObjectFactory.CreateGameObject(obj);
    }

    public void AddGameObject(GameObject gameObject){
        gameObjects.Add(gameObject);
    }

    public void RemoveGameObject(GameObject gameObject){
        gameObjects.Remove(gameObject);
    }

    private void PlaceGameObjects(){

        // RENDER THE WALLS
        gameObjects.ForEach(delegate(GameObject obj)
        {
            if (obj.Type == GameObjectType.Obstacle)
            {
                map.Set(ref obj);
            }
        });

        // RENDER THE NPCs
        gameObjects.ForEach(delegate(GameObject obj)
        {
            if (obj.Type == GameObjectType.NPC)
            {
                map.Set(ref obj);
                return;
            }
        });

        // RENDER THE KEY
        gameObjects.ForEach(delegate(GameObject obj)
        {
            if (obj.Type == GameObjectType.Key)
            {
                map.Set(ref obj);
                return;
            }
        });

        // RENDER THE PLAYER
        gameObjects.ForEach(delegate(GameObject obj)
        {
            if (obj.Type == GameObjectType.Player)
            {
                map.Set(ref obj);
                return;
            }
        });
    }

    private void DrawObject(GameObject gameObject){
        
        Console.ResetColor();

        if(gameObject != null && !gameObject.isHidden)
        {
            Console.ForegroundColor = gameObject.Color;
            Console.Write(gameObject.CharRepresentation);
        }
        else if(gameObject.isHidden){
            Console.Write('•');
        }
        else{
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(' ');
        }
    }

    public void Update() {
        // CHECK COLLISIONS
        gameObjects.ForEach(delegate(GameObject obj)
        {
            if (obj.Type != GameObjectType.Floor) {
                if (map.Get(obj.PosY, obj.PosX) is GameObject gameObject && map.Get(obj.PosY, obj.PosX) != obj && map.Get(obj.PosY, obj.PosX).Type != GameObjectType.Floor) {
                    obj.onCollision(gameObject, map.GetMap());
                    map.Get(obj.PosY, obj.PosX).onCollision(obj, map.GetMap());
                }
            }
        });
    }
}