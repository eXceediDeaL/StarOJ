﻿using LocalJudge.Core.Helpers;
using System;
using System.IO;

namespace LocalJudge.Core.Problems
{
    public class TestCasePath : IHasRoot, IHasId<string>
    {
        public const string PF_Profile = "profile.json", PF_Input = "input.data", PF_Output = "output.data";

        public string Root { get; private set; }

        public string Id { get; private set; }

        public string Profile { get; private set; }

        public TestCaseMetadata GetMetadata()
        {
            var res = Newtonsoft.Json.JsonConvert.DeserializeObject<TestCaseMetadata>(TextIO.ReadAllInUTF8(Profile));
            res.Id = Path.GetFileName(Root);
            return res;
        }

        public string Input { get; private set; }

        public DataPreview GetInput(int maxbytes) => TextIO.GetPreviewInUTF8(Input, maxbytes);

        public string Output { get; set; }

        public DataPreview GetOutput(int maxbytes) => TextIO.GetPreviewInUTF8(Output, maxbytes);

        public TestCasePath(string root)
        {
            Root = root;
            Id = Path.GetFileName(Root);
            Profile = Path.Combine(Root, PF_Profile);
            Input = Path.Combine(Root, PF_Input);
            Output = Path.Combine(Root, PF_Output);
        }

        public static TestCasePath Initialize(string root, TestCaseMetadata metadata = null, string input = "", string output = "")
        {
            var res = new TestCasePath(root);
            if (metadata == null) metadata = new TestCaseMetadata { TimeLimit = TimeSpan.FromSeconds(1), MemoryLimit = 128 * 1024 * 1024 };
            metadata.Id = res.Id;
            TextIO.WriteAllInUTF8(res.Profile, Newtonsoft.Json.JsonConvert.SerializeObject(metadata, Newtonsoft.Json.Formatting.Indented));
            TextIO.WriteAllInUTF8(res.Input, input);
            TextIO.WriteAllInUTF8(res.Output, output);
            return res;
        }
    }
}
