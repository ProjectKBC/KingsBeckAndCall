using UnityEditor;
using UnityEngine;

namespace UnityManiax
{
    /// <summary>
    /// MissilePrefab カタログ
    /// </summary>
    [CustomEditor(typeof(MissileCatalog))]
    public sealed class MissileCatalogEditor : AssetCatalogEditor<GameObject, MISSILE>{ }
}