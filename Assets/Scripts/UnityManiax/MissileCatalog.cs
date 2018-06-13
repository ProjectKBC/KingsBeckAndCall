using UnityEngine;

namespace UnityManiax
{
    /// <summary>
    /// ミサイル種類
    /// </summary>
    public enum MISSILE
    {
        UP,
        RIGHT,
        DOWN,
        LEFT,
    }

    /// <summary>
    /// ミサイルカタログ
    /// </summary>
    [CreateAssetMenu(menuName = "Catalog/Missile")]
    public sealed class MissileCatalog : AssetCatalog { }
}