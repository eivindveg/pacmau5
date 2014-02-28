using System.Diagnostics.CodeAnalysis;

using UnityEngine;

// ReSharper disable once CheckNamespace
public class SoundSystem : MonoBehaviour
{
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Reviewed. Suppression is OK here.")]
    public float Nr2 = 0.0f;
    private bool playStartSound;

    private int timeBeforeStart;
    private int nr;
    private int clipnr;
    private bool firstRun = true;
    private bool startCount;
    private bool playAgain;

    private bool playnext;
    private bool pingpong;
    private bool ping;
    private GameObject currentCamera;

    public AudioClip StartSound { get; set; }

    public AudioClip[] AudioClips { get; set; }

    public float[] DelayClips { get; set; }

    private AudioSource[] AllAudios { get; set; }

    public void AssignCamera(GameObject sceneCamera)
    {
        this.currentCamera = sceneCamera;
    }

    // Use this for initialization
    // ReSharper disable once UnusedMember.Local
    private void Start()
    {
        this.AllAudios = currentCamera.gameObject.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    // ReSharper disable once UnusedMember.Local
    private void Update()
    {
        if (firstRun)
        {
            timeBeforeStart++;

            if (timeBeforeStart > 20)
            {
                playStartSound = true;
                firstRun = false;
            }
        }

        if (playStartSound)
        {
            this.AllAudios[1].clip = StartSound;
            this.AllAudios[1].Play();
            firstRun = false;
            playStartSound = false;
            startCount = true;
        }

        if (startCount)
        {
            nr++;

            if (nr > this.DelayClips[clipnr])
            {
                playnext = true;
                startCount = false;
                nr = 0;
                clipnr = 1;
            }
        }

        if (playnext)
        {
            playnext = false;
            this.AllAudios[0].clip = this.AudioClips[clipnr];

            if (clipnr == 1)
            {
                pingpong = true;
            }

            this.AllAudios[0].Play();
        }

        if (pingpong)
        {
            if (ping)
            {
                this.Nr2--;
            }
            else
            {
                this.Nr2++;
            }

            if (this.Nr2 > this.DelayClips[2] && !ping)
            {
                this.AllAudios[1].clip = this.AudioClips[1];
                this.AllAudios[1].Play();

                ping = true;
            }

            if (this.Nr2 < 0.0f && ping)
            {
                this.AllAudios[0].clip = this.AudioClips[1];
                this.AllAudios[0].Play();

                ping = false;
            }
        }
    }
}
