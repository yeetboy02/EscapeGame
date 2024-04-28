namespace libs;

public sealed class Key : GameObject {
    private static Key _instance = null;

    public static Key Instance {
        get{
            if(_instance == null)
            {
                _instance = new Key();
            }
            return _instance;
        }
    }
    private Key () : base() {
        this.Type = GameObjectType.Obstacle;
        this.CharRepresentation = '!';
        this.Color = ConsoleColor.Cyan;
    }
}