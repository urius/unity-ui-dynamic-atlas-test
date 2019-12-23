using System.Linq;
using UnityEngine;

public class PrefabToAtlasConverter : MonoBehaviour
{
    [SerializeField]
    private Camera _cameraToRender;

    [SerializeField]
    private Transform _prefabPosition;

    public Sprite[] ConvertPrefabsToAtlas(GameObject[] prefabs)
    {
        var textures = new Texture2D[prefabs.Length];

        for (int i = 0; i < prefabs.Length; i++)
        {
            textures[i] = TakeSnapShot(prefabs[i]);
        }

        var atlas = new Texture2D(2048, 2048);
        var texRects = atlas.PackTextures(textures, 2);

        return texRects.Select(r => ToSprite(atlas, r)).ToArray();
    }

    private Sprite ToSprite(Texture2D atlas, Rect uvRect) {
        var rect = new Rect(uvRect);
        rect.x *= atlas.width;
        rect.width *= atlas.width;
        rect.y *= atlas.height;
        rect.height *= atlas.height;
        
        return Sprite.Create(atlas, rect, Vector2.one);
    }

    private Texture2D TakeSnapShot(GameObject prefab)
    {
        var instance = GameObject.Instantiate(prefab, _prefabPosition.position, _prefabPosition.rotation);

        var tempRenderTexture = RenderTexture.GetTemporary(128, 128);
        _cameraToRender.targetTexture = tempRenderTexture;
        _cameraToRender.aspect = (float)tempRenderTexture.width / (float)tempRenderTexture.height;
        _cameraToRender.Render();

        var resultTexture = new Texture2D(tempRenderTexture.width, tempRenderTexture.height);
        RenderTexture.active = tempRenderTexture;
        resultTexture.ReadPixels(new Rect(0, 0, tempRenderTexture.width, tempRenderTexture.height), 0, 0);
        resultTexture.Apply();

        //transform.GetComponent<RawImage>().texture = resultTexture;
        _cameraToRender.targetTexture = null;

        GameObject.DestroyImmediate(instance);

        return resultTexture;
    }
}
