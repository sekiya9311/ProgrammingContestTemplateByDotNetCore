using System;
using System.Collections.Generic;

namespace Library.String
{
    public class Trie<T> where T : struct
    {
        public interface IReadOnlyNode<WeightType> where WeightType : struct
        {
            char Current { get; }
            WeightType? Weight { get; }
            IReadOnlyDictionary<char, IReadOnlyNode<WeightType>> Next { get; }
        }

        private class Node<WeightType> : IReadOnlyNode<WeightType> where WeightType : struct
        {
            public char Current { get; }
            public WeightType? Weight { get; set; }

            public readonly Dictionary<char, IReadOnlyNode<WeightType>> _next;
            public IReadOnlyDictionary<char, IReadOnlyNode<WeightType>> Next => _next;

            public Node(char cur)
            {
                _next = new Dictionary<char, IReadOnlyNode<WeightType>>();
                Current = cur;
                Weight = null;
            }
        }

        private readonly Node<T> _root;
        public IReadOnlyNode<T> Root => _root;

        public Trie() { _root = new Node<T>(default); }

        public void Insert(string value, T weight)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("value must not empty.", nameof(value));

            var cur = _root;
            for (int i = 0; i < value.Length; i++)
            {
                var c = value[i];

                Node<T> nextNode;
                if (cur._next.TryGetValue(c, out var tmp))
                {
                    // しゃーないんです…
                    nextNode = (Node<T>)tmp;
                }
                else
                {
                    nextNode = new Node<T>(c);
                    cur._next.Add(c, nextNode);
                }

                if (i + 1 == value.Length)
                    nextNode.Weight = weight;

                cur = nextNode;
            }
        }

        public bool TryFind(string value, out T result)
        {
            result = default;

            if (string.IsNullOrEmpty(value))
                return false;

            var cur = _root;
            foreach (var c in value)
            {
                if (!cur._next.TryGetValue(c, out var tmp))
                    return false;

                // しゃーないんです…
                cur = (Node<T>)tmp;
            }

            if (!cur.Weight.HasValue)
                return false;

            result = cur.Weight.Value;
            return true;
        }
    }
}
