﻿namespace LocalJudge.Core.Judgers
{
    public enum JudgeState
    {
        Pending,
        Compiling,
        Judging,
        Accepted,
        WrongAnswer,
        TimeLimitExceeded,
        MemoryLimitExceeded,
        RuntimeError,
        CompileError,
        SystemError,
    }
}
