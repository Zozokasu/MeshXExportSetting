using HarmonyLib;
using ResoniteModLoader;
using Elements.Assets;
using FrooxEngine.Store;
using System;
using SharpDX.DXGI;

namespace meshxExportSetting
{
    public class meshxMod : ResoniteMod
    {
        public override string Name => "MeshX Export Setting";
        public override string Author => "Zozokasu";
        public override string Version => "1.0.0";

        [AutoRegisterConfigKey]
        private static readonly ModConfigurationKey<Elements.Assets.MeshX.Encoding> MESHX_ENCODING =
            new ModConfigurationKey<MeshX.Encoding>("MeshEncoding", "Setting meshx encoding in baking meshes",
                () => MeshX.Encoding.LZ4);

        private static ModConfiguration Config;

        public override void OnEngineInit()
        {
            Config = GetConfiguration();
            Harmony harmony = new Harmony("me.zozokasu.MeshXConfig");
            harmony.PatchAll();
            
            Msg("MeshX mod running(maybe)");
        }

        [HarmonyPatch(typeof(MeshX), nameof(MeshX.SaveToFile))]
        public class MeshX_SaveToFile
        {
            static bool Prefix(string file, ref MeshX.Encoding encoding)
            {
                encoding = Config.GetValue(MESHX_ENCODING);
                return true;
            }
        }
    }
}