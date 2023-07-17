using Blobsio.Recources;
using SFML.Audio;

namespace Blobsio.Core.Entities;

public class AudioClip
{
    public SoundBuffer clip;

    public AudioClip(string file)
    {
        clip = RecourcesManager.GetSoundBuffer(file);
    }

    public void Load(string file)
    {
        clip = RecourcesManager.GetSoundBuffer(file);
    }
}
