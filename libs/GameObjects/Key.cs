namespace libs;

public class Key : GameObject {
    public Key () : base() {
        this.Type = GameObjectType.Obstacle;
        this.CharRepresentation = '!';
        this.Color = ConsoleColor.Cyan;
    }
}