using Blobsio.Core.Entities;

namespace Blobsio.Core.Entities;

public class AudioSystem : Entity
{
    public static AudioSystem instance;

    private List<AudioClipID> AudioClipsList;

    public AudioClip ErrorSound;

    private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();
    private Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    public AudioSystem() : base() { }
    public AudioSystem(List<AudioClipID> AudioClipsList) : base() 
    { 
        this.AudioClipsList = AudioClipsList;
    }
    public AudioSystem(List<Component> components) : base(components) { }
    public AudioSystem(List<Component> components, List<AudioClipID> AudioClipsList) : base(components) 
    {
        this.AudioClipsList = AudioClipsList;
    }

    public override void Start()
    {
        base.Start();

        if (instance == null)
        {
            instance = this;
        }

        CreateAudioClipLibrary();
    }

    private void CreateAudioClipLibrary()
    {
        foreach (var clip in AudioClipsList)
        {
            string name = clip.ClipName;

            AudioClip Aclip = clip.Clip;

            audioClips.Add(name, Aclip);
        }
    }

    public void AddAudioSource(AudioSourceID ID, AudioSource source)
    {
        if (source != null)
        {
            if (ID == null)
                return;
            if (audioSources.Keys.Count != 0)
            {
                if (audioSources.ContainsKey(ID.Name))
                    return;
            }

            audioSources.Add(ID.Name, source);
        }
    }

    #region ControlMethods
    public static void PlaySoundOnce(string name, AudioClip sound, float radius = 0f)
    {
        AudioSource source = instance.audioSources[name];

        source.loop = false;

        source.clip = sound;

        source.Play();
    }
    public static void PlaySoundLooped(string name, AudioClip sound, float radius = 0f)
    {
        AudioSource source = instance.audioSources[name];

        source.loop = true;

        source.clip = sound;

        source.Play();
    }
    public static void StopSound(string name, AudioClip sound)
    {
        AudioSource source = instance.audioSources[name];

        source.Stop();
    }
    public static void PlaySetSoundAt(string name, float radius = 0f)
    {
        AudioSource source = instance.audioSources[name];

        source.Play();
    }
    public static AudioClip GetSound(string clipName)
    {
        if (!instance.audioClips.ContainsKey(clipName))
        {
            return instance.ErrorSound;
        }

        AudioClip clip = instance.audioClips[clipName];

        return clip;
    }
    #endregion
}
