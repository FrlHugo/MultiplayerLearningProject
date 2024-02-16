 using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[ExecuteInEditMode]
public class LevelGenerator : MonoBehaviour
{
    //public Texture2D texture;

    public int width = 15;
    public int height = 15;

    public float offSetX = 1;
    public float offSetY = 1;

    public float mapSeed = 1;

    public float percentChangeWoodBlock = 90f;

    public string imageSaveName;

    // Start is called before the first frame update
    void Start()
    {
        mapSeed = Random.Range(0f, 100f);
    }

    // Update is called once per frame
    void Update()
    {
        GenerateTexture();
    }


    public void GenerateTexture()
    {
        Debug.Log("Generatig");
        Texture2D texture2d = new Texture2D(width, height, TextureFormat.RGB24, false);

        for(int i = 0; i < width; ++i)
        {
            for(int j = 0; j < height; ++j)
            {
                Color color = SetColor(i, j);
                texture2d.SetPixel(i,j,color);
            }
        }


        byte[] bytes = texture2d.EncodeToJPG();
        var dirPath = Application.dataPath + "/Assets/Textures/";
        if(!Directory.Exists(dirPath))
        {
            Directory.CreateDirectory(dirPath);

        }
        File.WriteAllBytes(dirPath + imageSaveName + ".png", bytes); ;
    }

    public Color SetColor(int x, int y)
    {


        if (x == 0 || y == 0 || x == 14 || y == 14)
        {
            return Color.black; // walls

        }

        if (x > 0 && x < 14 && y > 0 && y < 14 && (x % 2 == 0) && (x % 2 == 0))
        {
            return Color.grey;//pilars 
        }

        if ((x == 1 && y == 1) || (x == 1 && y == 2) || (x == 2 && y == 1) ||
            (x == 13 && y == 1) || (x == 13 && y == 2) || (x == 12 && y == 1) ||
            (x == 1 && y == 13) || (x == 2 && y == 12) || (x == 1 && y == 12) ||
            (x == 13 && y == 13) || (x == 13 && y == 12) || (x == 12 && y == 13))
        {
            return Color.white; // spawns
        }

        int prob = Random.Range(0, 100);
        if (prob < percentChangeWoodBlock)
        {
            return new Color(88, 57, 39);// brown wood block
        }
        else
        {
            return Color.white;
        }

    }
}
