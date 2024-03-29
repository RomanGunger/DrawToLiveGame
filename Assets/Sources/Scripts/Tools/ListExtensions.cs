using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static List<List<T>> ChunkBy<T>(this List<T> source, int chunkSize)
    {
        return source
            .Select((x, i) => new { Index = i, Value = x })
            .GroupBy(x => x.Index / chunkSize)
            .Select(x => x.Select(v => v.Value).ToList())
            .ToList();
    }

    public static List<List<T>> Split<T>(this List<T> items, int sliceSize = 30)
    {
        List<List<T>> list = new List<List<T>>();
        for (int i = 0; i < items.Count; i += sliceSize)
            list.Add(items.GetRange(i, Math.Min(sliceSize, items.Count - i)));
        return list;
    }
}
