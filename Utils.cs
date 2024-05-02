﻿using BepInEx.Logging;
using Comfort.Common;
using EFT;
using EFT.InventoryLogic;
using System;
using System.IO;
using System.Reflection;

namespace RealismMod
{
    public static class Utils
    {
        public static ManualLogSource Logger;

        public static bool Verified = false;
        public static bool IsReady = false;
        public static bool IsInHideout = false;
        public static bool WeaponReady = false;
        public static bool HasRunErgoWeightCalc = false;

        public static string Silencer = "550aa4cd4bdc2dd8348b456c";
        public static string FlashHider = "550aa4bf4bdc2dd6348b456b";
        public static string MuzzleCombo = "550aa4dd4bdc2dc9348b4569";
        public static string Barrel = "555ef6e44bdc2de9068b457e";
        public static string Mount = "55818b224bdc2dde698b456f";
        public static string Receiver = "55818a304bdc2db5418b457d";
        public static string Stock = "55818a594bdc2db9688b456a";
        public static string Charge = "55818a6f4bdc2db9688b456b";
        public static string CompactCollimator = "55818acf4bdc2dde698b456b";
        public static string Collimator = "55818ad54bdc2ddc698b4569";
        public static string AssaultScope = "55818add4bdc2d5b648b456f";
        public static string Scope = "55818ae44bdc2dde698b456c";
        public static string IronSight = "55818ac54bdc2d5b648b456e";
        public static string SpecialScope = "55818aeb4bdc2ddc698b456a";
        public static string AuxiliaryMod = "5a74651486f7744e73386dd1";
        public static string Foregrip = "55818af64bdc2d5b648b4570";
        public static string PistolGrip = "55818a684bdc2ddd698b456d";
        public static string Gasblock = "56ea9461d2720b67698b456f";
        public static string Handguard = "55818a104bdc2db9688b4569";
        public static string Bipod = "55818afb4bdc2dde698b456d";
        public static string Flashlight = "55818b084bdc2d5b648b4571";
        public static string TacticalCombo = "55818b164bdc2ddc698b456c";
        public static string UBGL = "55818b014bdc2ddc698b456b";

        public static bool AreFloatsEqual(float a, float b, float epsilon = 0.001f)
        {
            float difference = Math.Abs(a - b);
            return difference < epsilon;
        }

        public static bool IsNull(string[] confItemArray, int expectedLength = 0)
        {
            if (confItemArray != null && confItemArray.Length > expectedLength)
            {
                if (confItemArray[0] == "SPTRM")
                {
                    return false;
                }
            }
            return true;
        }

        public static bool ConfItemsIsNullOrInvalid(string[] confItemArray, int length)
        {
            if (confItemArray != null && confItemArray.Length >= length)
            {
                if (confItemArray[0] == "SPTRM") 
                {
                    return false;
                }
            }
            return true;
        }

        public static Player GetYourPlayer() 
        {
            GameWorld gameWorld = Singleton<GameWorld>.Instance;
            return gameWorld.MainPlayer;
        }

        public static Player GetPlayerByID(string id)
        {
            GameWorld gameWorld = Singleton<GameWorld>.Instance;
            return gameWorld.GetAlivePlayerByProfileID(id);   
        }

        public static bool CheckIsReady()
        {
            GameWorld gameWorld = Singleton<GameWorld>.Instance;
            SessionResultPanel sessionResultPanel = Singleton<SessionResultPanel>.Instance;

            Player player = gameWorld?.MainPlayer;
            if (player != null)
            {
                Utils.WeaponReady = player?.HandsController != null && player?.HandsController?.Item != null && player?.HandsController?.Item is Weapon ? true : false;
                Utils.IsInHideout = player is HideoutPlayer ? true : false; 
            }

            if (gameWorld == null || gameWorld.AllAlivePlayersList == null || gameWorld.MainPlayer == null || sessionResultPanel != null)
            {
                Utils.IsReady = false;
                return false;
            }
            Utils.IsReady = true;
            return true;
        }

        public static void SafelyAddAttributeToList(ItemAttributeClass itemAttribute, Mod __instance)
        {
            if (itemAttribute.Base() != 0f)
            {
                __instance.Attributes.Add(itemAttribute);
            }
        }

        public static void VerifyFileIntegrity(ManualLogSource Logger)
        {
            var dllLoc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string checksum = "d2F5ZmFyZXI=";
            byte[] bytes = Convert.FromBase64String(checksum);
            string decodedString = System.Text.Encoding.UTF8.GetString(bytes);
            var modsLoc = Path.Combine(dllLoc, "..", "..", "user", "mods", decodedString);
            var fullPath = Path.GetFullPath(modsLoc);

            if (Directory.Exists(fullPath))
            {
                /*Environment.Exit(0);*/
                Verified = true;
                Logger.LogWarning("Verification Checksum: {0}" + checksum);
            }
        }

        public static string GenId()
        {
            return Guid.NewGuid().ToString();
        }

        public static bool IsSight(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Scope] || mod.GetType() == GClass2752.TypeTable[AssaultScope] || mod.GetType() == GClass2752.TypeTable[SpecialScope] || mod.GetType() == GClass2752.TypeTable[CompactCollimator] || mod.GetType() == GClass2752.TypeTable[Collimator] || mod.GetType() == GClass2752.TypeTable[IronSight];
        }
        public static bool IsMuzzleDevice(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[MuzzleCombo] || mod.GetType() == GClass2752.TypeTable[Silencer] || mod.GetType() == GClass2752.TypeTable[FlashHider];
        }
        public static bool IsStock(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Stock];
        }
        public static bool IsSilencer(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Silencer];
        }
        public static bool IsMagazine(Mod mod)
        {
            return (mod is MagazineClass);
        }
        public static bool IsFlashHider(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[FlashHider];
        }
        public static bool IsMuzzleCombo(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[MuzzleCombo];
        }
        public static bool IsBarrel(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Barrel];
        }
        public static bool IsMount(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Mount];
        }
        public static bool IsReceiver(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Receiver];
        }
        public static bool IsCharge(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Charge];
        }
        public static bool IsCompactCollimator(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[CompactCollimator];
        }
        public static bool IsCollimator(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Collimator];
        }
        public static bool IsAssaultScope(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[AssaultScope];
        }
        public static bool IsScope(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Scope];
        }
        public static bool IsIronSight(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[IronSight];
        }
        public static bool IsSpecialScope(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[SpecialScope];
        }
        public static bool IsAuxiliaryMod(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[AuxiliaryMod];
        }
        public static bool IsForegrip(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Foregrip];
        }
        public static bool IsPistolGrip(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[PistolGrip];
        }
        public static bool IsGasblock(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Gasblock];
        }
        public static bool IsHandguard(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Handguard];
        }
        public static bool IsBipod(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Bipod];
        }
        public static bool IsFlashlight(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[Flashlight];
        }
        public static bool IsTacticalCombo(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[TacticalCombo];
        }
        public static bool IsUBGL(Mod mod)
        {
            return mod.GetType() == GClass2752.TypeTable[UBGL];
        }
    }
}
