using UnityEngine;
using UnityEditor;

public class SetTexture
{

    [MenuItem("Assets/MySpriteSet")]
    static void MySpriteSet()
    {

        Object[] _textures = Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);

        foreach (Texture2D texture in _textures)
        {
            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            textureImporter.spritePixelsPerUnit = 10;
            textureImporter.filterMode = FilterMode.Point;

            AssetDatabase.ImportAsset(path);

        }
    }
}
