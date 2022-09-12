using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


// How to use this sprite slicer:
//      1. Put the sprites inside the "Assets/Resources/SpriteSlicer" folder.
//      2. Configure parameters - Slice width and height
//      3. Run the script via Menu -> Tools -> Sprite Slicer. All sprites inside will be sliced with same width and height

public class SpriteSlicer : MonoBehaviour {
    private static int sliceWidth = 32;
    private static int sliceHeight = 32;

    

    [MenuItem("Tools/Slice Sprites")]
    static void SliceSprites() {

        Texture2D[] textures = Resources.LoadAll<Texture2D>("SpriteSlicer");

        foreach ( Texture2D texture in textures ) { 
            string path = AssetDatabase.GetAssetPath(texture);
            TextureImporter ti = AssetImporter.GetAtPath(path) as TextureImporter;
            ti.isReadable = true;
            ti.spriteImportMode = SpriteImportMode.Multiple;

            List<SpriteMetaData> newData = new List<SpriteMetaData>();
            for (int i = 0; i < texture.width; i += SpriteSlicer.sliceWidth) {
                for (int j = texture.height; j > 0; j -= SpriteSlicer.sliceHeight) {
                    SpriteMetaData smd = new SpriteMetaData();
                    smd.pivot = new Vector2(0.5f, 0.5f);
                    smd.alignment = 9;
                    smd.name = $"{texture.name}({(texture.height - j) / SpriteSlicer.sliceHeight},{i / SpriteSlicer.sliceWidth})";
                    smd.rect = new Rect(i, j - SpriteSlicer.sliceHeight, SpriteSlicer.sliceWidth, SpriteSlicer.sliceHeight);

                    newData.Add(smd);
                }
            }

            ti.spritesheet = newData.ToArray();
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }

        Debug.Log( "Sprite Slicer: " + textures.Length + " Sprites sliced");
    }
}