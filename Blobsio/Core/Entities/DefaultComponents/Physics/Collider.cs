namespace Blobsio.Core.Entities;

public class Collider : Component
{
    public float radius;
    public bool isTrigger;
    public bool processCollision = true;

    public override void Start()
    {

    }
}
