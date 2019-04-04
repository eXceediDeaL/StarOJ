﻿using StarOJ.Core.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarOJ.Core.Problems
{
    public interface IProblemProvider : IHasId<string>, IHasMetadata<ProblemMetadata>
    {
        Task<ProblemDescription> GetDescription();

        Task SetDescription(ProblemDescription value);

        ITestCaseListProvider Samples { get; }

        ITestCaseListProvider Tests { get; }
    }
}
