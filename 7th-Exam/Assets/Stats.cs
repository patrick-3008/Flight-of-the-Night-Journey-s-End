using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public static int redbullsCollected;
    public static int totalMeteors;
    public static int totalRedbulls;
    public static int destroyedMeteors;
    public static int redbullsDestroyed;
    public static int stars;
    public static int pickedStars = 0;
    // Start is called before the first frame update
    void Start()
    {
        redbullsCollected = 0;
        totalMeteors = 0;
        totalRedbulls = 0;
        destroyedMeteors = 0;
        redbullsDestroyed = 0;
        stars = 0;
        pickedStars = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
