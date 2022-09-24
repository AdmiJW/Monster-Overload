using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;


// How to use this animation creator:
//      1. Place your sprite groups (Texture2D, sliced sprites) inside the "Assets/Resources/AnimationCreator" folder.
//      2. Configure parameters - framerate, and the getKey() function to be able to group sprites together.
//      3. Run the script via Menu -> Tools -> Animation Creator

public class AnimationCreator : MonoBehaviour {

    private class KeyExtractors {

        // Each sprite will have its own animation clip, given their names are different
        public static Func<Sprite, string> ByWholeString = (Sprite sprite) => {
            return sprite.name;
        };

        // Extract part of sprite name up until '('
        // Like 'Run_Up' in 'Run_Up(0,0)'
        public static Func<Sprite, string> ByNameUntilBracket = (Sprite sprite) => {
            return sprite.name.Substring(0, sprite.name.IndexOf(' '));   
        };

        // By name and direction. The NAME(X,Y) format, X represents the direction, Y represents sequence.
        public static Func<Sprite, string> ByNameAndDirection = (Sprite sprite) => {
            string[] parts = sprite.name.Split('(', ',', ')');
            string name = parts[0];
            int sequence = int.Parse(parts[2]);
            
            return $"{name} {sequence}";
        };
    }



    private static float frameRate = 1;
    private static Func<Sprite, string> getKey = KeyExtractors.ByNameAndDirection;





    //================================================================================================

    [MenuItem("Tools/Create Bulk Animation Clip")]
    static void CreateAnimationClip() {
        Sprite[] sprites = Resources.LoadAll<Sprite>("AnimationCreator");
        Dictionary<string, List<Sprite>> spriteGroups = new Dictionary<string, List<Sprite>>();
        
        // Group sprite into dictionary
        foreach( Sprite s in sprites) {
            string key = getKey(s);
            if ( !spriteGroups.ContainsKey(key) ) spriteGroups.Add(key, new List<Sprite>());
            spriteGroups[key].Add(s);
        }

        // Create animation clips for each sprite group
        foreach( var grp in spriteGroups ) {
            string name = grp.Key;
            List<Sprite> grpSprite = grp.Value;

            // Creating animation clip
            AnimationClip animClip = new AnimationClip();
            animClip.frameRate = AnimationCreator.frameRate; 
            EditorCurveBinding spriteBinding = new EditorCurveBinding();
            spriteBinding.type = typeof(SpriteRenderer);
            spriteBinding.path = "";
            spriteBinding.propertyName = "m_Sprite";

            // Keyframes
            ObjectReferenceKeyframe[] spriteKeyFrames = new ObjectReferenceKeyframe[grpSprite.Count];
            for (int i = 0; i < grpSprite.Count; i++) {
                spriteKeyFrames[i] = new ObjectReferenceKeyframe();
                spriteKeyFrames[i].time = i;
                spriteKeyFrames[i].value = grpSprite[i];
            }
            AnimationUtility.SetObjectReferenceCurve(animClip, spriteBinding, spriteKeyFrames);

            AssetDatabase.CreateAsset(animClip, $"Assets/Resources/AnimationCreator/{name}.anim");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        Debug.Log($"AnimationCreator: {spriteGroups.Count} Animation Clips Created");
    }
}