using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListTools<T>
{

    public static List<T> ValidateList(List<T> list)
    {
        List<T> toBeDeleted = new List<T>();

        foreach (T item in list)
        {
            if (item == null) toBeDeleted.Add(item);
        }

        foreach (T item in toBeDeleted)
        {
            list.Remove(item);
        }

        return list;
    }
}
