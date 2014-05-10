using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Altman
{
    public interface ILoadOrSaveSetting
    {
        void LoadSetting();
        object SaveSetting();
    }
}
