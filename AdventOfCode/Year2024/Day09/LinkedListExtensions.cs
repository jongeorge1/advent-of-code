namespace AdventOfCode.Year2024.Day09;

using System;
using System.Collections.Generic;

public static class LinkedListExtensions
{
    public static LinkedListNode<T>? FindBefore<T>(this LinkedList<T> self, LinkedListNode<T> beforeItem, Func<T, bool> predicate)
    {
        LinkedListNode<T>? current = self.First;

        while (current is not null && current != beforeItem)
        {
            if (predicate(current.Value))
            {
                return current;
            }

            current = current.Next;
        }

        return null;
    }

    public static LinkedListNode<T>[] Replace<T>(this LinkedListNode<T> self, params T[] newItems)
    {
        LinkedList<T> list = self.List ?? throw new InvalidOperationException("Node is not attached to a list.");
        List<LinkedListNode<T>> newNodes = new();

        LinkedListNode<T>? currentNode = self;
        foreach (T newItem in newItems)
        {
            currentNode = list.AddAfter(currentNode, newItem);
            newNodes.Add(currentNode);
        }

        list.Remove(self);

        return [.. newNodes];
    }

    public static void SwapNodes<T>(this LinkedList<T> self, LinkedListNode<T> first, LinkedListNode<T> second)
    {
        if (first.List != self || second.List != self)
        {
            throw new InvalidOperationException("Nodes are not attached to the list.");
        }

        if (first == second)
        {
            return;
        }

        LinkedListNode<T>? firstPredecessor = first.Previous;
        LinkedListNode<T>? secondPredecessor = second.Previous;

        if (firstPredecessor == second)
        {
            self.Remove(first);
            self.AddBefore(second, first);
        }
        else if (secondPredecessor == first)
        {
            self.Remove(second);
            self.AddBefore(first, second);
        }
        else
        {
            self.Remove(first);
            self.Remove(second);

            if (firstPredecessor is null)
            {
                self.AddFirst(second);
            }
            else
            {
                self.AddAfter(firstPredecessor, second);
            }

            if (secondPredecessor is null)
            {
                self.AddFirst(first);
            }
            else
            {
                self.AddAfter(secondPredecessor, first);
            }
        }
    }
}