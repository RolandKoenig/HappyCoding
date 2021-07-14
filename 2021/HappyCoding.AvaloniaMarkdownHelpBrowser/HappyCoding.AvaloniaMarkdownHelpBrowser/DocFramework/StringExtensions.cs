using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.DocFramework
{
    // Based on code from https://www.meziantou.net/split-a-string-into-lines-without-allocation.htm

    public static class StringExtensions
    {
        public static LineSplitEnumerator SplitZeroAlloc(this string str, char splitChar)
        {
            // LineSplitEnumerator is a struct so there is no allocation here
            return new(str.AsMemory(), splitChar);
        }

        // Must be a ref struct as it contains a ReadOnlySpan<char>
        public ref struct LineSplitEnumerator
        {
            private char _splitChar;
            private ReadOnlyMemory<char> _remainingBlock;
            private bool _reachedEnd;

            public ReadOnlyMemory<char> Current { get; private set; }

            public LineSplitEnumerator(ReadOnlyMemory<char> remainingBlock, char splitChar)
            {
                _remainingBlock = remainingBlock;
                _splitChar = splitChar;
                _reachedEnd = false;
                this.Current = default;
            }

            // Needed to be compatible with the foreach operator
            public LineSplitEnumerator GetEnumerator() => this;

            public bool MoveNext()
            {
                if (_reachedEnd) { return false; }

                var remainingBlock = _remainingBlock;
                if (remainingBlock.Length == 0)
                {
                    this.Current = ReadOnlyMemory<char>.Empty;
                    _reachedEnd = true;
                    return true;
                }
                
                var index = remainingBlock.Span.IndexOf(_splitChar);
                if (index == -1) // The string is composed of only one part
                {
                    _remainingBlock = ReadOnlyMemory<char>.Empty; // The remaining string is an empty string
                    _reachedEnd = true;
                    this.Current = remainingBlock;
                    return true;
                }

                this.Current = remainingBlock[..index];
                _remainingBlock = remainingBlock[(index + 1)..];
                return true;
            }
        }
    }
}