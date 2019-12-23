using System.Linq;
using UnityEngine;
using Zenject;

public class ItemIconsProvider : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefabToAtlasConverterPrefab;

    private Sprite[] _chassiIconSprites;
    private Sprite[] _headIconSprites;


    private ChassiConfigs _chassiConfigs;
    private HeadConfigs _headConfigs;


    [Inject]
    public void Construct(ChassiConfigs chassiConfigs, HeadConfigs headConfigs)
    {
        _chassiConfigs = chassiConfigs;
        _headConfigs = headConfigs;
    }

    public Sprite GetChassiIcon(GameObject prefab)
    {
        if (_chassiIconSprites == null)
        {
            _chassiIconSprites = GenerateAtlas(_chassiConfigs.chassisConfigs);
        }

        var rectIndex = 0;
        for (var i = 0; i < _chassiConfigs.chassisConfigs.Length; i++)
        {
            if (prefab == _chassiConfigs.chassisConfigs[i].prefab)
            {
                rectIndex = i;
                break;
            }
        }

        return _chassiIconSprites[rectIndex];
    }

    public Sprite GetHeadsIcon(GameObject prefab)
    {
        if (_headIconSprites == null)
        {
            _headIconSprites = GenerateAtlas(_headConfigs.headConfigs);
        }

        var rectIndex = 0;
        for (var i = 0; i < _headConfigs.headConfigs.Length; i++)
        {
            if (prefab == _headConfigs.headConfigs[i].prefab)
            {
                rectIndex = i;
                break;
            }
        }

        return _headIconSprites[rectIndex];
    }

    private Sprite[] GenerateAtlas(PartConfigBase[] configs)
    {
        var converterObject = Instantiate(_prefabToAtlasConverterPrefab);

        var converter = converterObject.GetComponent<PrefabToAtlasConverter>();
        var prefabs = configs.Select(c => c.prefab).ToArray();
        var sprites = converter.ConvertPrefabsToAtlas(prefabs);

        Destroy(converterObject);

        return sprites;
    }
}
