using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.Tools
{
    [System.Serializable]
    public class Entity
    {
        public int x;
        public int y;
        public int entityType;
    }

    [System.Serializable]
    public class ArrayData
    {
        [SerializeField] public string[] data;
        public int size;
        public Entity[] entities;

        public int[,] GetData()
        {
            int[,] dataArray = new int[data.Length, data[0].Split(',').Length];

            for (int i = 0; i < data.Length; i++)
            {
                string[] elements = data[i].Split(',');
                for (int j = 0; j < elements.Length; j++)
                {
                    int value;
                    if (int.TryParse(elements[j], out value))
                    {
                        dataArray[i, j] = value;
                    }
                    else
                    {
                        Debug.LogError("Failed to parse element at position [" + i + "," + j + "]");
                    }
                }
            }

            return dataArray;
        }

        /**
         * In mathematics, the coordinate system consists of four quadrants, numbered from 1 to 4, which divide the plane into different regions.
         * The 2nd quadrant is located to the left of the origin, while the 1st quadrant is located to the right of the origin.
         */
        public int[,] CoordinateConversion(int[,] origin)
        {
            int[,] cache = origin;
            int width = cache.GetLength(0);
            int height = cache.GetLength(1);

            int[,] grid = MMGridGeneratorFull.Generate(width, height, false);
            int[,] temp = MMGridGeneratorFull.Generate(width, height, false);

            for (int i = width - 1; i >= 0; i--)
            {
                for (int j = 0; j < height; j++)
                {
                    temp[i, j] = cache[j, i];
                }
            }

            return SaveArray(temp);
        }

        public int[,] GetGrid()
        {
            return CoordinateConversion(GetData());
        }

        public int GetWidth()
        {
            int[,] cache = GetData();
            int width = cache.GetLength(0);
            return width;
        }

        public int GetHeight()
        {
            int[,] cache = GetData();
            int height = cache.GetLength(1);
            return height;
        }

        public List<Vector3Int> GetEntities()
        {
            List<Vector3Int> vec3Entities = new List<Vector3Int>();

            int width = GetWidth();
            int height = GetHeight();

            int[,] temp = MMGridGeneratorFull.Generate(width, height, false);

            for (var i = 0; i < entities.Length; i++)
            {
                Entity entity = entities[i];
                temp[entity.y, entity.x] = (entity.entityType + 1);
            }

            temp = CoordinateConversion(temp);
            for (var j = 0; j < height; j++)
            {
                for (int k = 0; k < width; k++)
                {
                    if (temp[j, k] != 0)
                    {
                        vec3Entities.Add(new Vector3Int(j, k, temp[j, k]));
                        Debug.LogError(k + ":" + j);
                    }
                }
            }

            return vec3Entities;
        }

        static int[,] SaveArray(int[,] sourceArray)
        {
            int rows = sourceArray.GetLength(0);
            int cols = sourceArray.GetLength(1);
            int[,] targetArray = new int[rows, cols];

            int targetRow = 0;
            int targetCol = 0;

            // for (int i = rows - 1; i >= 0; i--)
            for (int i = 0; i < rows; i++)
            {
                // for (int j = 0; j < cols; j++)
                for (int j = cols - 1; j >= 0; j--)
                {
                    targetArray[targetRow, targetCol] = sourceArray[i, j];
                    targetCol++;
                }

                targetRow++;
                targetCol = 0;
            }

            return targetArray;
        }
    }
}