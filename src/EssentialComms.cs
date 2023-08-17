

using Vintagestory.API.Common;
using Vintagestory.API.MathTools;
using Vintagestory.API.Server;
using System.Collections.Generic;
using Vintagestory.API.Util;


namespace EssentialComms {
    public class EssentialComms : ModSystem {

        private ICoreServerAPI serverAPI;
        public Dictionary<string, Vec3d> homes;

        // Server Side Only
        public override bool ShouldLoad(EnumAppSide side) {
            return side == EnumAppSide.Server;
        }

        public override void StartServerSide(ICoreServerAPI api) {
            base.StartServerSide(api);
            serverAPI = api;

            api.Event.SaveGameLoaded += OnSaveGameLoading;
            api.Event.GameWorldSave += OnSaveGameSaving;

            Home home = new Home(this);

        }

        private void OnSaveGameLoading() {
            byte[] data = serverAPI.WorldManager.SaveGame.GetData("homes");

            homes = data == null ? new Dictionary<string, Vec3d>() : SerializerUtil.Deserialize<Dictionary<string, Vec3d>>(data);
        }

        private void OnSaveGameSaving() {
            serverAPI.WorldManager.SaveGame.StoreData("homes", SerializerUtil.Serialize(homes));
        }

    }
}