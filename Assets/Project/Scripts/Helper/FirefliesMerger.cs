using UnityEngine;
using System.Collections;

// Singleton to handle merging of two fireflies objects
public sealed class FirefliesMerger
{
    static FirefliesMerger() {}

    private FirefliesMerger() {}

    public static FirefliesMerger Instance { get; } = new FirefliesMerger();

    private int requestCount;

    // Called by fireflies object, returns true for only one of the two objects
    public bool RequestMerging()
    {
        requestCount++;

        if (requestCount == 2)
        {
            requestCount = 0;
            return true;
        }

        return false;
    }
}
