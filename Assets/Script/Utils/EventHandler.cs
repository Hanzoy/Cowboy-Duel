using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventHandler
{
    public static event Action CardLoadingAnimation;

    public static void CallCardLoadingAnimation()
    {
        CardLoadingAnimation?.Invoke();
    }

    public static event Action<bool> CardSettlement;

    public static void CallCardSettlement(bool identical)
    {
        CardSettlement?.Invoke(identical);
    }
}
