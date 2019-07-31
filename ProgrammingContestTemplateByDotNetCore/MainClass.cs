using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Numerics;

namespace ProgrammingContestTemplateByDotNetCore
{

#region "Input and Output Classes"

    class Scanner : IDisposable
    {
        private readonly Queue<string> _buffer;
        private readonly char[] _sep;
        private readonly TextReader _reader;

        public Scanner(TextReader reader = null, char[] sep = null)
        {
            _buffer = new Queue<string>();
            _sep = sep ?? new[] { ' ' };
            _reader = reader ?? Console.In;
        }

        public Scanner(string path, char[] sep = null)
            : this(new StreamReader(path), sep) { }

        private void CheckBuffer()
        {
            if (_buffer.Any() || _reader.Peek() == -1)
                return;

            string str = string.Empty;
            for (;
                string.IsNullOrWhiteSpace(str);
                str = _reader.ReadLine()) { }

            var values = str.Split(_sep).Where(s => !string.IsNullOrWhiteSpace(s));
            foreach (var item in values)
            {
                _buffer.Enqueue(item);
            }
        }

        public string Next()
        {
            CheckBuffer();
            return _buffer.Dequeue();
        }
        public string[] GetStringArray(int count)
            => Enumerable.Range(0, count)
                .Select(e => Next())
                .ToArray();

        public int NextInt() => int.Parse(Next());
        public int[] GetIntArray(int count)
            => Enumerable.Range(0, count)
                .Select(e => NextInt())
                .ToArray();

        public long NextLong() => long.Parse(Next());
        public long[] GetLongArray(int count)
            => Enumerable.Range(0, count)
                .Select(e => NextLong())
                .ToArray();

        public double NextDouble() => double.Parse(Next());
        public double[] GetDoubleArray(int count)
            => Enumerable.Range(0, count)
                .Select(e => NextDouble())
                .ToArray();

        public decimal NextDecimal() => decimal.Parse(Next());
        public decimal[] GetDecimalArray(int count)
            => Enumerable.Range(0, count)
                .Select(e => NextDecimal())
                .ToArray();

        public BigInteger NextBigInt() => BigInteger.Parse(Next());
        public BigInteger[] GetBigIntArray(int count)
            => Enumerable.Range(0, count)
                .Select(e => NextBigInt())
                .ToArray();

        public bool IsEnd
        {
            get
            {
                CheckBuffer();
                return !_buffer.Any();
            }
        }

        public void Dispose()
        {
            if (!_reader.Equals(Console.In))
                _reader.Dispose();
        }
    }

    class Writer : IDisposable
    {
        private readonly TextWriter _writer;
        private readonly StringBuilder _cache;
        private readonly bool _isReactive;

        public Writer(TextWriter writer = null, bool isReactive = false)
        {
            _writer = writer ?? Console.Out;
            _isReactive = isReactive;
            if (!_isReactive)
                _cache = new StringBuilder();
        }

        public Writer(string path)
            : this(new StreamWriter(path)) { }

        public Writer(bool isReactive)
            : this(null, isReactive) { }

        public void Write(object value)
        {
            if (_isReactive)
            {
                _writer.Write(value);
                _writer.Flush();
            }
            else
            {
                _cache.Append(value);
            }
        }

        public void WriteFormat(string format, params object[] values)
        {
            var value = string.Format(format, values);
            Write(value);
        }

        public void WriteLine(object value = null)
        {
            Write($"{value}{Environment.NewLine}");
        }

        public void WriteLine(string format, params object[] values)
        {
            WriteFormat($"{format}{Environment.NewLine}", values);
        }

        public void Dispose()
        {
            if (!_isReactive)
            {
                _writer.Write(_cache);
                _writer.Flush();
            }
            if (!_writer.Equals(Console.Out))
            {
                _writer.Dispose();
            }
        }
    }

#endregion

    class MainClass : IDisposable
    {
#region "Template"

        /// <summary> 入力を受け取る </summary>
        private readonly Scanner sc;
        /// <summary> 出力を行う </summary>
        private readonly Writer wr;
        private const string _inputFile = "input.txt";
        private const string _outFile = "output.txt";

        static void Main(string[] args)
        {
            using (new MainClass()) { }
        }

        public MainClass()
        {
            wr = new Writer(_isReactive);
            // TODO: ファイルに出力したい場合はこっち → wr = new Writer(_outFile);

#if DEBUG
            // 手元では、ファイルから入力を受け取る
            sc = new Scanner(_inputFile);
#else

            // 提出時、標準入力から受け取る
            sc = new Scanner();
#endif

            Solve();
        }

        public void Dispose()
        {
            sc?.Dispose();
            wr?.Dispose();
        }

#endregion

        void Solve()
        {
            // TODO: ここに書く
        }

        /// <summary>
        /// TODO: リアクティブ問題か否かを指定してね♪
        /// </summary>
        private const bool _isReactive = false;
    }
}
