using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Singleton to handle merging of two fireflies objects
public sealed class FirefliesHelper
{
    static FirefliesHelper() {}

    private FirefliesHelper() {}

    public static FirefliesHelper Instance { get; } = new FirefliesHelper();

    private int mergeRequestCount;
    private Dictionary<int, List<int>> seperatingRequestIds = new Dictionary<int, List<int>>();

    // Called by fireflies object, returns true for only one of the two objects
    public bool RequestMerging()
    {
        mergeRequestCount++;

        if (mergeRequestCount == 2)
        {
            mergeRequestCount = 0;
            return true;
        }

        return false;
    }

    // Called by fireflies interaction object (of controllers or attractBall), returns true for only one of the two objects
    public bool RequestSeperating(int controllerId, int firefliesId)
    {
        if (!seperatingRequestIds.ContainsKey(firefliesId))
        {
            seperatingRequestIds[firefliesId] = new List<int> { controllerId };
        } else if (!seperatingRequestIds[firefliesId].Contains(controllerId))
        {
            seperatingRequestIds[firefliesId].Add(controllerId);

            if (seperatingRequestIds[firefliesId].Count == 2)
            {
                ResetSeperationCount(firefliesId);
                return true;
            }
        }

        return false;
    }

    public void ResetSeperationCount(int firefliesId)
    {
        seperatingRequestIds[firefliesId] = new List<int>();
    }
}
