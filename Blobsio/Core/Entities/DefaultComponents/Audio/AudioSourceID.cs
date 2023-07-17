namespace Blobsio.Core.Entities;

public class AudioSourceID : Component
{
    public string Name;

    public AudioSourceID() : base() { }
    public AudioSourceID(string name)
    {
        Name = name;
    }

    public override void Start()
    {
        base.Start();

        AudioSystem.instance.AddAudioSource(this, GetComponent<AudioSource>());
    }
}
