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

    private List<List<AudioClip>> SoundsList;
    private int textureIndex;
    private DetermineGroundTexture det;
    private AudioSource audioSrc;
    private AudioClip soundToPlay;
    private System.Random randSoundIndex;
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

        audioSrc = this.gameObject.GetComponent<AudioSource>();
        det = this.gameObject.GetComponent<DetermineGroundTexture>();
        textureIndex = det.GetIndexOfCurrentTexture();
        randSoundIndex = new System.Random();
        randSoundIndex.Next();
        soundToPlay = SoundsList[textureIndex][randSoundIndex.Next(SoundsList[textureIndex].Count)];
        audioSrc.clip = soundToPlay;
        movingAndGrounded = this.gameObject.GetComponent<CharacterMotor>().isMovingAndGrounded();
    }

    // Update is called once per frame
    void Update()
    {
        //get index of the texture currently on
        textureIndex = det.GetIndexOfCurrentTexture();
        //get a random sound from collection of sounds representing that texture
        soundToPlay = SoundsList[textureIndex][randSoundIndex.Next(SoundsList[textureIndex].Count)];
        //make sure the player is on the ground and moving
        movingAndGrounded = this.gameObject.GetComponent<CharacterMotor>().isMovingAndGrounded();
        //finally make sure the sound isn't already playing
        if(movingAndGrounded && !audioSrc.isPlaying)
        {
            audioSrc.clip = soundToPlay;
            audioSrc.Play();
        }
        //Determine sound based on index standing on.  check index on and see what it has in its name play sound accordingly
    }
}
