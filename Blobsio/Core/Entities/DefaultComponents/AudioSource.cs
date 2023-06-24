using SFML.Audio;

namespace Blobsio.Core.Entities;

public class AudioSource : Component
{
    private AudioClip _clip;
    public AudioClip clip
    {
        get => _clip;
        set
        {
            _clip = value;
            sound.SoundBuffer = _clip.clip;
        }
    }
    private Sound sound = new Sound();

    private bool _loop;
    public bool loop
    {
        get => _loop;
        set
        {
            _loop = value;
            sound.Loop = _loop;
        }
    }

    public AudioSource() : base() { }
    public AudioSource(AudioClip clip)
    {
        this.clip = clip;
    }
    public AudioSource(string file)
    {
        clip = new AudioClip(file);
    }

    public void Play()
    {
        sound.Play();
    }

    public void Stop()
    {
        sound.Stop();
    }
}
