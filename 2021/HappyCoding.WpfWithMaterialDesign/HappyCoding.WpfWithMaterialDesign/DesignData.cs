using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HappyCoding.WpfWithMaterialDesign
{
    internal static class DesignData
    {
        public static MainWindowViewModel MainWindowVM => new(10);
    }
}
