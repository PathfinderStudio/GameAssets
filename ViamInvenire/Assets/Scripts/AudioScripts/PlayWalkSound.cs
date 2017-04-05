using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayWalkSound : MonoBehaviour
{
    [Header("Grass Sound List")]
    public List<AudioClip> grassSounds;
    [Header("Rock Sound List")]
    public List<AudioClip> rockSounds;
    [Header("Sand Sound List")]
    public List<AudioClip> sandSounds;

    private List<List<AudioClip>> SoundsList;
    private int textureIndex;
    private DetermineGroundTexture det;
    private AudioClip soundToPlay;
    private System.Random randSoundIndex;
    private bool movingAndGrounded;

    // Use this for initialization
    void Start()
    {
        SoundsList = new List<List<AudioClip>>();
        SoundsList.Add(grassSounds);
        SoundsList.Add(rockSounds);
        SoundsList.Add(sandSounds);

        det = this.gameObject.GetComponent<DetermineGroundTexture>();
        textureIndex = det.GetIndexOfCurrentTexture();
        randSoundIndex = new System.Random();
        randSoundIndex.Next();
        //soundToPlay = SoundsList[textureIndex][randSoundIndex.Next(SoundsList[textureIndex].Count)];
        movingAndGrounded = this.gameObject.GetComponent<CharacterMotor>().isMovingAndGrounded();
    }

    // Update is called once per frame
    void Update()
    {
        textureIndex = det.GetIndexOfCurrentTexture();
        //soundToPlay = SoundsList[textureIndex][randSoundIndex.Next(SoundsList[textureIndex].Count)];
        movingAndGrounded = this.gameObject.GetComponent<CharacterMotor>().isMovingAndGrounded();
        if(movingAndGrounded)
        {
            this.gameObject.GetComponent<AudioSource>().PlayOneShot(soundToPlay);
            Debug.Log("This is working.");
        }
        else
        {
            Debug.Log("See?");
        }
        //Determine sound based on index standing on.  check index on and see what it has in its name play sound accordingly
    }
}
