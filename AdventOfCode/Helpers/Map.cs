namespace AdventOfCode.Helpers;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

public class Map<T> : IImmutableDictionary<(int X, int Y), T>
{
    private readonly ImmutableDictionary<(int X, int Y), T> locations;

    public Map(IDictionary<(int X, int Y), T> locations, int minX, int minY, int maxX, int maxY)
    {
        this.locations = locations.ToImmutableDictionary();
        this.MinX = minX;
        this.MinY = minY;
        this.MaxX = maxX;
        this.MaxY = maxY;
    }

    public delegate bool MapItemMapper(char characterAtLocation, (int X, int Y) location, [MaybeNullWhen(false)] out T? item);

    public int MinY { get; }

    public int MinX { get; }

    public int MaxX { get; }

    public int MaxY { get; }

    public IEnumerable<(int X, int Y)> Keys
    {
        get => this.locations.Keys;
    }

    public IEnumerable<T> Values
    {
        get => this.locations.Values;
    }

    public int Count
    {
        get => this.locations.Count;
    }

    public T this[(int X, int Y) location]
    {
        get => this.locations[location];
    }

    public static Map<int> CreateDigitMap(string[] input)
    {
        return Map<int>.Create(input, c => c - '0');
    }

    public static Map<char> CreateCharMap(string[] input)
    {
        return Map<char>.Create(input, c => c);
    }

    public static Map<char> CreateCharMap(string[] input, char[] charactersToIgnore)
    {
        return Map<char>.Create(
            input,
            (char input, (int X, int Y) _, out char result) =>
            {
                if (charactersToIgnore.Contains(input))
                {
                    result = default;
                    return false;
                }

                result = input;
                return true;
            });
    }

    public static Map<T> Create(string[] input, Func<char, T> positionMapper)
    {
        return Create(
            input,
            (char input, (int X, int Y) _, out T? result) =>
            {
                result = positionMapper(input);
                return true;
            });
    }

    public static Map<T> Create(string[] input, MapItemMapper positionMapper)
    {
        Dictionary<(int X, int Y), T> map = [];

        for (int y = 0; y < input.Length; ++y)
        {
            for (int x = 0; x < input[y].Length; ++x)
            {
                if (positionMapper(input[y][x], (x, y), out T? item))
                {
                    map.Add((x, y), item!);
                }
            }
        }

        return new Map<T>(map, 0, 0, input[0].Length - 1, input.Length - 1);
    }

    public bool IsLocationInBounds((int X, int Y) location)
    {
        return location.X >= this.MinX && location.X <= this.MaxX && location.Y >= this.MinY && location.Y <= this.MaxY;
    }

    public IImmutableDictionary<(int X, int Y), T> Add((int X, int Y) key, T value)
    {
        throw new System.NotImplementedException();
    }

    public IImmutableDictionary<(int X, int Y), T> AddRange(IEnumerable<KeyValuePair<(int X, int Y), T>> pairs)
    {
        throw new System.NotImplementedException();
    }

    public IImmutableDictionary<(int X, int Y), T> Clear()
    {
        throw new System.NotImplementedException();
    }

    public bool Contains(KeyValuePair<(int X, int Y), T> pair)
    {
        throw new System.NotImplementedException();
    }

    public IImmutableDictionary<(int X, int Y), T> Remove((int X, int Y) key)
    {
        throw new System.NotImplementedException();
    }

    public IImmutableDictionary<(int X, int Y), T> RemoveRange(IEnumerable<(int X, int Y)> keys)
    {
        throw new System.NotImplementedException();
    }

    public IImmutableDictionary<(int X, int Y), T> SetItem((int X, int Y) key, T value)
    {
        throw new System.NotImplementedException();
    }

    public IImmutableDictionary<(int X, int Y), T> SetItems(IEnumerable<KeyValuePair<(int X, int Y), T>> items)
    {
        throw new System.NotImplementedException();
    }

    public bool TryGetKey((int X, int Y) equalKey, out (int X, int Y) actualKey)
    {
        return this.locations.TryGetKey(equalKey, out actualKey);
    }

    public bool ContainsKey((int X, int Y) key)
    {
        return this.locations.ContainsKey(key);
    }

    public bool TryGetValue((int X, int Y) key, [MaybeNullWhen(false)] out T value)
    {
        return this.locations.TryGetValue(key, out value);
    }

    public IEnumerator<KeyValuePair<(int X, int Y), T>> GetEnumerator()
    {
        return this.locations.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.locations.GetEnumerator();
    }
}
