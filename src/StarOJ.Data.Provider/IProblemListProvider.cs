﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarOJ.Core.Problems
{
    public interface IProblemListProvider : IItemListProvider<IProblemProvider,ProblemMetadata>
    {
        Task<IEnumerable<IProblemProvider>> Query(string id, string userId, string name, string source);
    }
}
