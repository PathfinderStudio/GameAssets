using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class used to determine which surface is being stood on in the terrain.  
/// The value returned is the texture index which currently holds the greatest
/// influence over the location the object is located in x,z space. y axis does
/// not influence this index.  
/// Thank you to Jay Kay (alucardj) on
/// http://answers.unity3d.com/questions/456973/getting-the-texture-of-a-certain-point-on-terrain.html
/// for the code for getting this up and running.  Translated from JavaScript to
/// C# by myself.
/// </summary>
public class DetermineGroundTexture : MonoBehaviour
{
    private int surfaceIndex = 0;
    private Terrain CurrentActiveTerrain;
    private TerrainData theTerrainData;
    private Vector3 terrainPosition;

    // Use this for initialization
    void Start()
    {
        CurrentActiveTerrain = Terrain.activeTerrain; //s[0]
        theTerrainData = CurrentActiveTerrain.terrainData;
        terrainPosition = CurrentActiveTerrain.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        surfaceIndex = GetTextureOn(transform.position);
    }

    private float[] GetTextureMix(Vector3 worldPosition)
    {
        //getting the splat map cell for our current position
        int mapX = (int)(((worldPosition.x - terrainPosition.x) / theTerrainData.size.x) * theTerrainData.alphamapWidth);
        int mapZ = (int)(((worldPosition.z - terrainPosition.z) / theTerrainData.size.z) * theTerrainData.alphamapHeight);

        //get the splat data for this cell for a 1x1xN 3d array where n is number of textures
        float[,,] splatData = theTerrainData.GetAlphamaps(mapX, mapZ, 1, 1);
        //get out the 1 directional information
        float[] cellDataMix = new float[splatData.GetUpperBound(2) + 1];

        for (int i = 0; i < cellDataMix.Length; i++)
        {
            cellDataMix[i] = splatData[0, 0, i];
        }

        return cellDataMix;
    }

    private int GetTextureOn(Vector3 worldPosition)
    {
        //Returns index of most dominant texture currently standing on
        //hopefully the top terrain
        float[] mixData = GetTextureMix(worldPosition);
        float maxMix = 0;
        int maxIndex = 0;

        //loop through mix values and find the maximum
        for (int i = 0; i < mixData.Length; i++)
        {
            if(mixData[i] > maxMix)
            {
                maxIndex = i;
                maxMix = mixData[i];
            }
        }

        return maxIndex;
    }

    /// <summary>
    /// Determines the index of the texture that the player is standing on.
    /// </summary>
    /// <returns>Zero based index for texture.</returns>
    public int GetIndexOfCurrentTexture()
    {
        return surfaceIndex;
    }
}