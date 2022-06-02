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


    public static string ReadableNumber(float number)
    {
        if(number / 1000000000000 >= 1) //Trillion
        {
            return (number / 1000000000000).ToString("F2") + " Tril";
        }

        if (number / 1000000000 >= 1) //Billion
        {
            return (number / 1000000000).ToString("F2") + " Bil";
        }

        if (number / 1000000 >= 1) //Million
        {
            return (number / 1000000).ToString("F1") + " Mil";
        }

        if (number / 1000 >= 1) //Thousand
        {
            return (number / 1000).ToString("F1") + " K";
        }

        return number.ToString();

    }

}
