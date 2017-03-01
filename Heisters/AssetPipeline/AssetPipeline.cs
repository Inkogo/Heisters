using System.Collections.Generic;

namespace Heisters
{
    class AssetPipeline
    {
        public Dictionary<string, Asset> assets;

        public AssetPipeline()
        {
            InjectionContainer.Instance.RegisterObject(this);
            assets = new Dictionary<string, Asset>();
        }

        public T GetAsset<T>(string path) where T : Asset, new()
        {
            if (!assets.ContainsKey(path))
            {
                T asset = new T();
                asset.Load(path);
                assets.Add(path, asset);
            }
            return assets[path] as T;
        }
    }
}
