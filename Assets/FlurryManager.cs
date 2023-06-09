using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FlurrySDK;

public class FlurryManager : Singleton<FlurryManager>
{
#if UNITY_ANDROID
    private string FLURRY_API_KEY = "PQ2PVXNH52GV924W9QZ6";
#elif UNITY_IPHONE
    //private string FLURRY_API_KEY = "BT8R8TDWK69NBB9X4WND";
#else
    private string FLURRY_API_KEY = null;
#endif

    public void Init()
    {
        // Initialize Flurry.
        new Flurry.Builder()
                  .WithCrashReporting(true)
                  .WithLogEnabled(true)
                  .WithLogLevel(Flurry.LogLevel.VERBOSE)
                  .WithMessaging(true)
                  .Build(FLURRY_API_KEY);
    }
}
