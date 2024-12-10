namespace AdventOfCode.Helpers;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public class InvertedMap<T>(Map<T> map) : IImmutableDictionary<T, (int X, int Y)[]>
    where T : notnull
{
    private readonly Map<T> originalMap = map;
    private readonly ImmutableDictionary<T, (int X, int Y)[]> invertedMap = map.GroupBy(x => x.Value).ToImmutableDictionary(x => x.Key, x => x.Select(y => y.Key).ToArray());

    public int MinY => this.originalMap.MinY;

    public int MinX => this.originalMap.MinX;

    public int MaxX => this.originalMap.MaxX;

    public int MaxY => this.originalMap.MaxY;

    public IEnumerable<T> Keys => this.invertedMap.Keys;

    public IEnumerable<(int X, int Y)[]> Values => this.invertedMap.Values;

    public int Count => this.invertedMap.Count;

    public (int X, int Y)[] this[T key] => this.invertedMap[key];

    public bool IsLocationInBounds((int X, int Y) location) => this.originalMap.IsLocationInBounds(location);

    public IImmutableDictionary<T, (int X, int Y)[]> Add(T key, (int X, int Y)[] value) =>
        throw new NotImplementedException();

    public IImmutableDictionary<T, (int X, int Y)[]> AddRange(IEnumerable<KeyValuePair<T, (int X, int Y)[]>> pairs) =>
        throw new NotImplementedException();

    public IImmutableDictionary<T, (int X, int Y)[]> Clear() => throw new NotImplementedException();

    public bool Contains(KeyValuePair<T, (int X, int Y)[]> pair) => this.invertedMap.Contains(pair);

    public bool ContainsKey(T key) => this.invertedMap.ContainsKey(key);

    public IEnumerator<KeyValuePair<T, (int X, int Y)[]>> GetEnumerator() => this.invertedMap.GetEnumerator();

    public IImmutableDictionary<T, (int X, int Y)[]> Remove(T key) => throw new NotImplementedException();

    public IImmutableDictionary<T, (int X, int Y)[]> RemoveRange(IEnumerable<T> keys) => throw new NotImplementedException();

    public IImmutableDictionary<T, (int X, int Y)[]> SetItem(T key, (int X, int Y)[] value) => throw new NotImplementedException();

    public IImmutableDictionary<T, (int X, int Y)[]> SetItems(IEnumerable<KeyValuePair<T, (int X, int Y)[]>> items) =>
        throw new NotImplementedException();

    public bool TryGetKey(T equalKey, out T actualKey) => this.invertedMap.TryGetKey(equalKey, out actualKey);

    public bool TryGetValue(T key, [MaybeNullWhen(false)] out (int X, int Y)[] value) => this.invertedMap.TryGetValue(key, out value);

    IEnumerator IEnumerable.GetEnumerator() => this.invertedMap.GetEnumerator();
}
