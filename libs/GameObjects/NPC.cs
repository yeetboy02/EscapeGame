namespace libs;

public class NPC : GameObject {
    public NPC () : base() {
        this.Type = GameObjectType.Obstacle;
        this.CharRepresentation = '☺';
        this.Color = ConsoleColor.Cyan;
    }
}