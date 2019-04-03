﻿using StarOJ.Core;
using StarOJ.Core.Problems;
using StarOJ.Data.Provider.SqlServer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StarOJ.Data.Provider.SqlServer
{
    public class TestCaseProvider : ITestCaseProvider
    {
        private readonly OJContext _context;
        private readonly TestCase _testcase;

        public TestCaseProvider(OJContext context, TestCase testcase)
        {
            _context = context;
            _testcase = testcase;
        }

        public string Id => _testcase.Id;

        public Task<string> GetInput()
        {
            return Task.FromResult(_testcase.Input);
        }

        public Task<DataPreview> GetInputPreview(int maxbytes)
        {
            var bytes = Encoding.UTF8.GetBytes(_testcase.Input);
            int len = Math.Min(bytes.Length, maxbytes);
            return Task.FromResult(new DataPreview
            {
                Content = Encoding.UTF8.GetString(bytes, 0, len),
                RemainBytes = bytes.Length - len,
            });
        }

        public Task<TestCaseMetadata> GetMetadata()
        {
            return Task.FromResult(new TestCaseMetadata
            {
                Id = Id,
                MemoryLimit = _testcase.MemoryLimit,
                TimeLimit = _testcase.TimeLimit,
            });
        }

        public Task<string> GetOutput()
        {
            return Task.FromResult(_testcase.Output);
        }

        public Task<DataPreview> GetOutputPreview(int maxbytes)
        {
            var bytes = Encoding.UTF8.GetBytes(_testcase.Output);
            int len = Math.Min(bytes.Length, maxbytes);
            return Task.FromResult(new DataPreview
            {
                Content = Encoding.UTF8.GetString(bytes, 0, len),
                RemainBytes = bytes.Length - len,
            });
        }
    }
}
