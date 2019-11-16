using Library.String;
using System;
using Xunit;

namespace Library.Tests.String
{
    public class TrieTest
    {
        [Fact]
        public void ConstructorTest()
        {
            Trie<int> trie = new Trie<int>();

            Assert.NotNull(trie.Root);

            var root = trie.Root;
            Assert.Equal(default, root.Current);
            Assert.Null(root.Weight);
            Assert.NotNull(root.Next);
            Assert.Empty(root.Next);
        }

        [Fact]
        public void InsertTest()
        {
            Trie<int> trie = new Trie<int>();

            Assert.Throws<ArgumentException>("value", () => trie.Insert("", 0));
            Assert.Throws<ArgumentException>("value", () => trie.Insert(null, 1));

            trie.Insert("a", 0);
            trie.Insert("ab", 1);
            trie.Insert("bc", 2);

            {
                var cur = trie.Root;
                Assert.True(cur.Next.TryGetValue('a', out var res));
                Assert.Equal(0, res.Weight);
            }

            {
                var cur = trie.Root;
                Assert.True(cur.Next.TryGetValue('a', out var res1));
                Assert.True(res1.Next.TryGetValue('b', out var res2));
                Assert.Equal(1, res2.Weight);
            }

            {
                var cur = trie.Root;
                Assert.True(cur.Next.TryGetValue('b', out var res1));
                Assert.Null(res1.Weight);
            }

            {
                var cur = trie.Root;
                Assert.False(cur.Next.TryGetValue('c', out var _));
            }
        }

        [Fact]
        public void TryFindTest()
        {
            Trie<int> trie = new Trie<int>();
            trie.Insert("a", 0);
            trie.Insert("ab", 1);
            trie.Insert("bc", 2);

            Assert.True(trie.TryFind("a", out var res1));
            Assert.Equal(0, res1);

            Assert.True(trie.TryFind("ab", out var res2));
            Assert.Equal(1, res2);

            Assert.False(trie.TryFind("c", out var _));
            Assert.False(trie.TryFind("ac", out var _));
        }
    }
}
