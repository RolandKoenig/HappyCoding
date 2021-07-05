using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.AvaloniaMarkdownHelpBrowser.Util
{
    public class PooledStringBuilders
    {
        private ConcurrentStack<StringBuilder> _stringBuilders;

        public int Count => _stringBuilders.Count;

        public static PooledStringBuilders Current
        {
            get;
            private set;
        }

        static PooledStringBuilders()
        {
            Current = new PooledStringBuilders();
        }

        public PooledStringBuilders()
        {
            _stringBuilders = new ConcurrentStack<StringBuilder>();
        }

        public StringBuilder TakeStringBuilder(int requiredCapacity = 128)
        {
            if (!_stringBuilders.TryPop(out var result))
            {
                result = new StringBuilder(requiredCapacity);
            }
            else
            {
                if (result.Capacity < requiredCapacity) { result.EnsureCapacity(requiredCapacity); }
            }
            return result;
        }

        public void ReRegisterStringBuilder(StringBuilder stringBuilder)
        {
            stringBuilder.Remove(0, stringBuilder.Length);
            _stringBuilders.Push(stringBuilder);
        }

        public void Clear()
        {
            _stringBuilders.Clear();
        }
    }
}
