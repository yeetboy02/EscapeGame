namespace libs;

public class GameObjectFactory : IGameObjectFactory
{
    public GameObject CreateGameObject(dynamic obj) {

        GameObject newObj = new GameObject();
        int type = obj.Type;

        switch (type)
        {
            case (int) GameObjectType.Player:
                newObj = Player.Instance;
                newObj.PosX = obj.PosX;
                newObj.PosY = obj.PosY;
                newObj.Color = obj.Color;
                break;
            case (int) GameObjectType.Obstacle:
                newObj = obj.ToObject<Obstacle>();
                break;
            case (int) GameObjectType.NPC:
                newObj = obj.ToObject<NPC>();
                break;
            case (int) GameObjectType.Key:
                newObj = Key.Instance;
                newObj.PosX = obj.PosX;
                newObj.PosY = obj.PosY;
                newObj.Color = obj.Color;
                break;
        }

        return newObj;
    }
}