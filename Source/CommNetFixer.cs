﻿using CommNet;
using System;
using UnityEngine;

namespace AdvancedPQSTools
{
    [KSPAddon(KSPAddon.Startup.SpaceCentre, true)]
    public class CommNetFixer : MonoBehaviour
    {
        public void Start()
        {
            try
            {
                bool enableExtraGroundStations = true;
                bool overrideCommNetParams = true;

                float occlusionMultiplierInAtm = 1.0f;
                float occlusionMultiplierInVac = 1.0f;

                Debug.Log("[AdvancedPQSTools] Checking for custom CommNet settings...");

                foreach (ConfigNode Settings in GameDatabase.Instance.GetConfigNodes("AdvancedPQSTools"))
                {
                    Settings.TryGetValue("overrideCommNetParams", ref overrideCommNetParams);
                    Settings.TryGetValue("enableGroundStations", ref enableExtraGroundStations);
                    Settings.TryGetValue("occlusionMultiplierAtm", ref occlusionMultiplierInAtm);
                    Settings.TryGetValue("occlusionMultiplierVac", ref occlusionMultiplierInVac);
                }

                if (overrideCommNetParams)
                {
                    //  Set the default CommNet parameters for RealSolarSystem.

                    Debug.Log("[AdvancedPQSTools] Updating the CommNet settings...");

                    HighLogic.CurrentGame.Parameters.CustomParams<CommNetParams>().enableGroundStations = enableExtraGroundStations;
                    HighLogic.CurrentGame.Parameters.CustomParams<CommNetParams>().occlusionMultiplierAtm = occlusionMultiplierInAtm;
                    HighLogic.CurrentGame.Parameters.CustomParams<CommNetParams>().occlusionMultiplierVac = occlusionMultiplierInVac;
                }
            }
            catch (Exception exceptionStack)
            {
                Debug.Log("[AdvancedPQSTools] CommNetFixer.Start() caught an exception: " + exceptionStack);
            }
            finally
            {
                Destroy(this);
            }
        }
    }
}
