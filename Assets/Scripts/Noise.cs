﻿using UnityEngine;
using System.Collections;

public static class Noise  {


    public static float[,] GenrateNoiseMap(int mapWidth, int mapHeight, int seed,float scale,int octaves ,float persistance,float lacunarity,Vector2 offSet)
    {

        float[,] noiseMap = new float[mapWidth,mapHeight];

        System.Random random=new System.Random(seed);
        Vector2[] octavesOffSets=new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {
            float offsetX = random.Next(-100000, 100000)+offSet.x;
            float offsetY = random.Next(-100000, 100000)+offSet.y;
            
            octavesOffSets[i]=new Vector2(offsetX,offsetY);
        }



        if (scale <= 0)
            scale = 0.0001f;

        float maxNoise = float.MinValue;
        float minNoise = float.MaxValue;
        float halfWidth = mapWidth/2;
        float halfHeight = mapHeight/2;

        for (int y = 0; y < mapHeight; y++){
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

         

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x-halfWidth) / scale *frequency+octavesOffSets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octavesOffSets[i].y;
                    // the range can be from -1 to 1 we will normalize that later
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)*2 -1;
                    noiseHeight += perlinValue*amplitude;
                   // noiseMap[x, y] = perlinValue;
                    amplitude *= persistance;
                    frequency *= lacunarity;
                }
         

                if (noiseHeight > maxNoise)
                    maxNoise = noiseHeight;
                else if (noiseHeight < minNoise)
                    minNoise = noiseHeight;

                noiseMap[x, y] = noiseHeight;
            }

        }
        for (int y = 0; y < mapHeight; y++){
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoise, maxNoise, noiseMap[x, y]);
            }

        }

                return noiseMap;
    }



}
