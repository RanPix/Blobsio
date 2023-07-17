namespace Blobsio.Core.Entities;

public class AudioClipID
{
    public AudioClip Clip;
    public string ClipName;

    public AudioClipID(AudioClip clip, string clipName)
    {
        Clip = clip;
        ClipName = clipName;
    }
}
