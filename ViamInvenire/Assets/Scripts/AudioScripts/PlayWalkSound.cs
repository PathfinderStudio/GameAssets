using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWalkSound : MonoBehaviour
{
    [Header("Green Grass Sound List")]
    public List<AudioClip> greenGrassSounds;
    [Header("Brown Grass Sound List")]
    public List<AudioClip> brownGrassSounds;
    [Header("Solid Stone Sound List")]
    public List<AudioClip> solidRockSounds;
    [Header("Gravel Stone Sound List")]
    public List<AudioClip> gravelRockSounds;
    [Header("Soft Dirt Sound List")]
    public List<AudioClip> softDirtSounds;
    [Header("Hard Dirt Sound List")]
    public List<AudioClip> hardDirtSounds;
    [Header("Sand Sound List")]
    public List<AudioClip> sandSounds;
    [Header("Audio Settings")]
    [Range(0.0f, 10.0f)]
    public float clipSpeed = 1f;
    [Range(0.0f, 10.0f)]
    public float clipVolume = 1f;
    public float runningClipSpeedModifier = 2f;
    public float runningClipVolumeModifier = 2f;

    private List<List<AudioClip>> SoundsList;
    private int textureIndex;
    private DetermineGroundTexture det;
    private AudioSource audioSrc;
    private AudioClip soundToPlay;
    private System.Random randSoundIndex;
    private CharacterMotor motor;
    private bool movingAndGrounded;

    // Use this for initialization
    void Start()
    {
        SoundsList = new List<List<AudioClip>>();
        SoundsList.Add(greenGrassSounds);
        SoundsList.Add(brownGrassSounds);
        SoundsList.Add(solidRockSounds);
        SoundsList.Add(gravelRockSounds);
        SoundsList.Add(softDirtSounds);  
        SoundsList.Add(hardDirtSounds);
        SoundsList.Add(sandSounds);

        audioSrc = this.GetComponent<AudioSource>();
        det = this.GetComponent<DetermineGroundTexture>();
        textureIndex = det.GetIndexOfCurrentTexture();
        randSoundIndex = new System.Random();
        randSoundIndex.Next();
        soundToPlay = SoundsList[textureIndex][randSoundIndex.Next(SoundsList[textureIndex].Count)];
        audioSrc.clip = soundToPlay;
        motor = this.GetComponent<CharacterMotor>();
        movingAndGrounded = motor.isMovingAndGrounded();
    }

    // Update is called once per frame
    void Update()
    {
        //get index of the texture currently on
        textureIndex = det.GetIndexOfCurrentTexture();
        //get a random sound from collection of sounds representing that texture
        soundToPlay = SoundsList[textureIndex][randSoundIndex.Next(SoundsList[textureIndex].Count)];
        //make sure the player is on the ground and moving
        movingAndGrounded = motor.isMovingAndGrounded();
        //finally make sure the sound isn't already playing
        if(movingAndGrounded && !audioSrc.isPlaying)
        {
            //player is running, increase tempo of footsteps and volume slightly
            if(this.GetComponent<CharacterStamina>().GetIsRunning())
            {
                audioSrc.pitch = clipSpeed * runningClipSpeedModifier;
                audioSrc.volume = clipVolume * runningClipVolumeModifier;
            }
            else
            {
                audioSrc.pitch = clipSpeed;
                audioSrc.volume = clipVolume;
            }
            audioSrc.clip = soundToPlay;
            audioSrc.Play();
        }
    }
}
