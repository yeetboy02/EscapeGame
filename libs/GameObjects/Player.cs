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

    private string currDir = "up";

    private Dictionary<string, char> directions = new Dictionary<string, char>() {
        {"up", '▲'},
        {"down", '▼'},
        {"right", '▶'},
        {"left", '◀'}
    }; 

    private Player () : base(){
        Type = GameObjectType.Player;
        CharRepresentation = directions[currDir];
        Color = ConsoleColor.DarkYellow;
    }

    public override void onCollision(GameObject gameObject, GameObject?[,] map) {
        if (gameObject.Type == GameObjectType.Obstacle) {
            this.PosX = this.GetPrevPosX();
            this.PosY = this.GetPrevPosY();
        }
    }

    public override void Move(int dx, int dy) {
        base.Move(dx, dy);
        currDir = dx == 0 ? (dy == 1 ? "down" : "up") : (dx == 1 ? "right" : "left");
        CharRepresentation = directions[currDir];
    }
}