// Copyright (c) 2026 Frederick William Haslam born 1962 in the USA.
// Licensed under "The MIT License" https://opensource.org/license/mit/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

using FactoryModel.Models.Constants;

using static FactoryModel.Models.Constants.SiteGroup;

namespace FactoryModel.Models.Sites {


    /// <summary>
    /// Products are creating here with no input.
    /// </summary>
    public class MakeSite : Site {

        override public SiteGroup Group {  get {  return Make; } }

    }
}
