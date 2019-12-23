using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class TestPrefabSnapshooter : MonoBehaviour
{
    public Transform prefabPosition;
    public Camera cameraToRender;
    public PermanentGonfigsInstaller configs;

    [SerializeField]
    private GameObject ItemPrefab;
    [SerializeField]
    private GameObject Content;

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {

            Debug.Log("TestPrefabSnapshooter");

            var (atlas, texRects) = ConvertPrefabsToAtlas(configs.chassiConfigs.chassisConfigs.Select(c => c.prefab).ToArray());

            var sprites = new Sprite[texRects.Length];
            for (int i = 0; i < sprites.Length; i++)
            {
                var rect = texRects[i];
                rect.x *= atlas.width;
                rect.width *= atlas.width;
                rect.y *= atlas.height;
                rect.height *= atlas.height;
                sprites[i] = Sprite.Create(atlas, rect, Vector2.one);
                var partItem = Instantiate(ItemPrefab, Content.transform);
                //partItem.transform.GetComponent<RobotPartItemView>().SetImage(sprites[i]);
            }

            var image = transform.GetComponent<RawImage>();
            image.texture = atlas;
            image.uvRect = texRects[1];
        }
    }

    private (Texture2D atlas, Rect[] rects) ConvertPrefabsToAtlas(GameObject[] prefabs)
    {
        var textures = new Texture2D[prefabs.Length];
        for (int i = 0; i < prefabs.Length; i++)
        {
            textures[i] = TakeSnapShot(prefabs[i]);
        }

        var atlas = new Texture2D(2048, 2048);
        var texRects = atlas.PackTextures(textures, 2);

        return (atlas, texRects);
    }

    Texture2D TakeSnapShot(GameObject prefab)
    {
        var instance = GameObject.Instantiate(prefab, prefabPosition.position, prefabPosition.rotation);

        var tempRenderTexture = RenderTexture.GetTemporary(128, 128);
        cameraToRender.targetTexture = tempRenderTexture;
        cameraToRender.aspect = (float)tempRenderTexture.width / (float)tempRenderTexture.height;
        cameraToRender.Render();

        var resultTexture = new Texture2D(tempRenderTexture.width, tempRenderTexture.height);
        RenderTexture.active = tempRenderTexture;
        resultTexture.ReadPixels(new Rect(0, 0, tempRenderTexture.width, tempRenderTexture.height), 0, 0);
        resultTexture.Apply();

        //transform.GetComponent<RawImage>().texture = resultTexture;
        cameraToRender.targetTexture = null;

        GameObject.DestroyImmediate(instance);

        return resultTexture;
    }
}
