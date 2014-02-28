using UnityEngine;
using System.Collections;


public class SoundSystem : MonoBehaviour
{
    public AudioClip StartSound;
    public AudioClip[] audioClips;
    public float[] delayClips;
    private bool playStartSound;

    private AudioSource[] allAudios { get; set; }

    private int timeBeforeStart = 0;
    private int nr = 0;
    private int clipnr = 0;
    private bool firstRun = true;
    private bool startCount;
    private bool playAgain;

    private bool playnext;
    private bool pingpong;
    private bool ping;

    private GameObject currentCamera;

    public float nr2 = 0.0f;

    // Use this for initialization
    void Start()
    {
        allAudios = currentCamera.gameObject.GetComponents<AudioSource>();
    }

    public void AssignCamera(GameObject currentCamera)
    {
        this.currentCamera = currentCamera;
    }

    // Update is called once per frame
    void Update()
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
            allAudios[1].clip = StartSound;
            allAudios[1].Play();
            firstRun = false;
            playStartSound = false;
            startCount = true;
        }

        if (startCount)
        {
            nr++;

            if (nr > delayClips[clipnr])
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
            allAudios[0].clip = audioClips[clipnr];

            if (clipnr == 1)
            {
                pingpong = true;
            }

            allAudios[0].Play();
        }
        if (pingpong)
        {

            if (ping)
            {
                nr2--;
            }
            else
            {
                nr2++;
            }

            if (nr2 > delayClips[2] && !ping)
            {

                allAudios[1].clip = audioClips[1];
                allAudios[1].Play();

                ping = true;
            }
            if (nr2 < 0.0f && ping)
            {

                allAudios[0].clip = audioClips[1];
                allAudios[0].Play();

                ping = false;
            }

        }

    }

}