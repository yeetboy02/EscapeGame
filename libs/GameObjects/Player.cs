namespace libs;

public sealed class Player : GameObject {
    private static Player _instance = null;

    public static Player Instance {
        get{
            if(_instance == null)
            {
                _instance = new Player();
            }
            return _instance;
        }
    }

    private Dictionary<string, char> directions = new Dictionary<string, string>() {
        {"up", '▲'},
        {"down", '▼'},
        {"right", '▶'},
        {"left", '◀'}
    }; 

    private Player () : base(){
        Type = GameObjectType.Player;
        CharRepresentation = directions["up"];
        Color = ConsoleColor.DarkYellow;
    }

    public override void onCollision(GameObject gameObject, GameObject?[,] map) {
        if (gameObject.Type == GameObjectType.Obstacle) {
            this.PosX = this.GetPrevPosX();
            this.PosY = this.GetPrevPosY();
        }
    }
}