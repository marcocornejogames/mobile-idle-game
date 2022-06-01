using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathTools
{
    public static float Remap(float from, float fromMin, float fromMax, float toMin, float toMax) //From Forum.Unity.Com, RazaTech
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }
}
