using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MapGenerator : MonoBehaviour
{

    public enum DrawMode { NoiseMap,ColorMap}

    public DrawMode drawMode;

    public int mapWidth;
    public int mapHeight;
    public float noiseScale;
    public bool autoUpdate;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public int seed;
    public Vector2 offset;

    public TerrainType[] terrain;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenrateNoiseMap(mapWidth, mapHeight,seed,noiseScale,octaves,persistance,lacunarity,offset);
        Color[] colorMap = new Color[mapWidth * mapHeight];
        for (int y = 0; y < mapHeight; y++){
            for (int x = 0; x < mapWidth; x++)
            {float currentHeight = noiseMap[x, y];
                for (int i = 0; i < terrain.Length; i++)
                {
                    if (currentHeight <= terrain[i].height)
                    {
                        colorMap[y*mapWidth + x] = terrain[i].color;
                        break;
                    }
                }

            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if(drawMode==DrawMode.NoiseMap)
        display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        else if (drawMode==DrawMode.ColorMap)
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap,mapWidth,mapHeight));


    }

    void OnValidate()
    {
        if (mapWidth < 1)
            mapWidth = 1;

        if (mapHeight < 1)
            mapHeight = 1;
        if (lacunarity < 1)
            lacunarity = 1;
        if (octaves < 0)
            octaves = 0;

    }
    [System.Serializable]
    public struct TerrainType
    {
        public String name;
        public float height;
        public Color color;
    }

}
