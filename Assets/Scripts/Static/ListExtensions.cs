using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    //public static T DrawCard<T>(this List<T> list)
    //{
    //    if (list.Count == 0)
    //    {
    //        Debug.LogError("ÅÆ¶ÑÒÑ¿Õ£¡");  /////
    //        return default;
    //    }

    //    T card = list[0];
    //    list.RemoveAt(0);
    //    return card;
    //}

    //public static List<T> DrawCards<T>(this List<T> list, int count)
    //{
    //    List<T> drawnCards = new List<T>();

    //    for (int i = 0; i < count; i++)
    //    {
    //        if (list.Count > 0)
    //        {
    //            drawnCards.Add(list.DrawCard());
    //        }
    //    }

    //    return drawnCards;
    //}

}