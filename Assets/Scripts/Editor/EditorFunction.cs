using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.YamlDotNet.Serialization.NodeTypeResolvers;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class EditorFunction : EditorWindow
{
    [MenuItem("Window/Edit Mode Functions")]

    public static void ShowWindow()
    {
        EditorWindow.GetWindow<EditorFunction>("Edit Mode Functions");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Run Function"))
        {
            LevelGenerator2();
        }
    }

    private void FunctionToRun()
    {

        Debug.Log("The function Start.");

        GameObject level = new GameObject("Level");
        level.transform.position = Vector3.zero;

        GameObject floor = new GameObject("Floor");
        GameObject field = new GameObject("Field");
        GameObject walls = new GameObject("walls");


        floor.transform.parent = level.transform;
        field.transform.parent = level.transform;
        walls.transform.parent = level.transform;

        floor.transform.position = new Vector3(0,-1,0);
        field.transform.position = Vector3.zero;
        walls.transform.position = Vector3.zero;

       //load materials

        var floorGreen = Resources.Load<GameObject>("GameObjects/FloorGreen");
        var floorGreenLight = Resources.Load<GameObject>("GameObjects/FloorGreenLight");
        var wall = Resources.Load<GameObject>("GameObjects/Wall");
        var pilar = Resources.Load<GameObject>("GameObjects/Pilar");
        //spawn floor
        bool flipflop = true;

        for (int i = 0; i < 15; i++)
        {
            
            for (int j = 0; j < 15; j++)
            {
                if(flipflop)
                {

                    GameObject go = Instantiate(floorGreenLight, new Vector3(i, -1, j), Quaternion.identity);
                    go.transform.SetParent(floor.transform);

                    go.name = "FloorLight_" + i + "/" + j;
                }
                else
                {
                    GameObject go = Instantiate(floorGreen, new Vector3(i, -1, j), Quaternion.identity);
                    go.transform.SetParent(floor.transform);

                    go.name = "Floor_" + i + "/" + j;
                }

                flipflop = !flipflop;
            }


        }
        
        //spawn walls
        for (int i = 0 ; i < 15; i++)
        {
            for(int j = 0; j< 15; j++)
            {
                if( i == 0 || j == 0 || i == 14 || j == 14 )
                {
                    GameObject go = Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity);
       
                    go.transform.SetParent(walls.transform);
                    go.name = "Wall_" + i + "/" + j;
                }
            }
        }

        
        for (int i = 1; i < 14; i++)
        {
            for (int j = 1; j < 14; j++)
            {
                if ((i % 2 == 0) && (j % 2 == 0))
                {
                    GameObject go = Instantiate(pilar, new Vector3(i, 0, j), Quaternion.identity);
                    go.transform.SetParent(field.transform);


                    go.name = "Pilar_" + i + "/" + j;

                }
            }
        }
        
        Debug.Log("The function Ended.");
    }
    /// <summary>
    /// Generate a perling noise map than will we used to place the wood blocks
    /// 0 = empty
    /// 1 = Walls
    /// 2 = pillars
    /// 3 = WoodBlock
    /// 
    /// </summary>

    public void LevelGenerator2()
    {
        int width = 15;
        int height = 15;

        GameObject level = new GameObject("Level");
        level.transform.position = Vector3.zero;

        GameObject floor = new GameObject("Floor");
        GameObject field = new GameObject("Field");
        GameObject walls = new GameObject("walls");


        floor.transform.parent = level.transform;
        field.transform.parent = level.transform;
        walls.transform.parent = level.transform;

        floor.transform.position = new Vector3(0, -1, 0);
        field.transform.position = Vector3.zero;
        walls.transform.position = Vector3.zero;

        //load materials

        var floorGreen = Resources.Load<GameObject>("GameObjects/FloorGreen");
        var floorGreenLight = Resources.Load<GameObject>("GameObjects/FloorGreenLight");
        var wall = Resources.Load<GameObject>("GameObjects/Wall");
        var pilar = Resources.Load<GameObject>("GameObjects/Pilar");
        var woodBlock = Resources.Load<GameObject>("GameObjects/WoodBlock");
        //spawn floor
        bool flipflop = true;

        for (int i = 0; i < 15; i++)
        {

            for (int j = 0; j < 15; j++)
            {
                if (flipflop)
                {

                    GameObject go = Instantiate(floorGreenLight, new Vector3(i, -1, j), Quaternion.identity);
                    go.transform.SetParent(floor.transform);

                    go.name = "FloorLight_" + i + "/" + j;
                }
                else
                {
                    GameObject go = Instantiate(floorGreen, new Vector3(i, -1, j), Quaternion.identity);
                    go.transform.SetParent(floor.transform);

                    go.name = "Floor_" + i + "/" + j;
                }

                flipflop = !flipflop;
            }


        }

        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                if (i == 0 || j == 0 || i == 14 || j == 14)
                {
                    GameObject go = Instantiate(wall, new Vector3(i, 0, j), Quaternion.identity);

                    go.transform.SetParent(walls.transform);
                    go.name = "Wall_" + i + "/" + j;

                }
                else if ((i > 0 && i < 14 && j > 0 && j < 14) && ((i % 2 == 0) && (j % 2 == 0)))
                {
                    GameObject go = Instantiate(pilar, new Vector3(i, 0, j), Quaternion.identity);
                    go.transform.SetParent(field.transform);
                    go.name = "Pilar_" + i + "/" + j;

                }
                else if (!((i == 1 && j == 1) || (i == 1 && j == 2) || (i == 2 && j == 1) ||
                    (i == 13 && j == 1) || (i == 13 && j == 2) || (i == 12 && j == 1) ||
                    (i == 1 && j == 13) || (i == 2 && j == 13) || (i == 1 && j == 12) ||
                    (i == 13 && j == 13) || (i == 13 && j == 12) || (i == 12 && j == 13)))
                {
                    GameObject go = Instantiate(woodBlock, new Vector3(i, 0, j), Quaternion.identity);
                    go.transform.SetParent(field.transform);
                    go.name = "WoodBlock_" + i + "/" + j;
                }

     
            }
        }
    }

}
