﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StarOJ.Core;
using StarOJ.Core.Problems;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using StarOJ.Server.API.Models;

namespace StarOJ.Server.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProblemsController : ControllerBase
    {
        private readonly IWorkspace _workspace;

        public ProblemsController(IWorkspace workspace)
        {
            _workspace = workspace;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProblemMetadata>>> GetAll()
        {
            var all = await _workspace.Problems.GetAll();
            List<ProblemMetadata> ans = new List<ProblemMetadata>();
            foreach (var v in all)
            {
                ans.Add(await v.GetMetadata());
            }
            return Ok(ans);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ProblemMetadata>> Get(string id)
        {
            var res = await (await _workspace.Problems.Get(id))?.GetMetadata();
            if (res != null)
                return Ok(res);
            else
                return NotFound();
        }

        [HttpGet("{id}/description")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ProblemDescription>> GetDescription(string id)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
                return Ok(await res.GetDescription());
            else
                return NotFound();
        }

        [HttpGet("{id}/samples")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<TestCaseMetadata>>> GetSamples(string id)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null){
                var all = await res.Samples.GetAll();
                List<TestCaseMetadata> ans = new List<TestCaseMetadata>();
                foreach(var v in all){
                    ans.Add(await v.GetMetadata());
                }
                return Ok(ans);
            }
            else
                return NotFound();
        }

        [HttpGet("{id}/samples/{tid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TestCaseMetadata>> GetSample(string id, string tid)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
                return Ok(await (await res.Samples.Get(tid)).GetMetadata());
            else
                return NotFound();
        }

        [HttpGet("{id}/samples/{tid}/input/{num}/preview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DataPreview>> GetSampleInputPreview(string id, string tid, int num)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
                return Ok(await (await res.Samples.Get(tid)).GetInputPreview(num));
            else
                return NotFound();
        }

        [HttpGet("{id}/samples/{tid}/input")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> GetSampleInput(string id, string tid)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
            {
                return Ok(await (await res.Samples.Get(tid)).GetInput());
            }
            else
                return NotFound();
        }

        [HttpGet("{id}/samples/{tid}/output/{num}/preview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DataPreview>> GetSampleOutputPreview(string id, string tid, int num)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
                return Ok(await (await res.Samples.Get(tid)).GetOutputPreview(num));
            else
                return NotFound();
        }

        [HttpGet("{id}/samples/{tid}/output")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> GetSampleOutput(string id, string tid)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
            {
                return Ok(await (await res.Samples.Get(tid)).GetOutput());
            }
            else
                return NotFound();
        }

        [HttpGet("{id}/tests")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<TestCaseMetadata>>> GetTests(string id)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null){
                var all = await res.Tests.GetAll();
                List<TestCaseMetadata> ans = new List<TestCaseMetadata>();
                foreach(var v in all){
                    ans.Add(await v.GetMetadata());
                }
                return Ok(ans);
            }
            else
                return NotFound();
        }

        [HttpGet("{id}/tests/{tid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TestCaseMetadata>> GetTest(string id, string tid)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
                return Ok(await (await res.Tests.Get(tid)).GetMetadata());
            else
                return NotFound();
        }

        [HttpGet("{id}/tests/{tid}/input/{num}/preview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DataPreview>> GetTestInputPreview(string id, string tid, int num)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
                return Ok(await (await res.Tests.Get(tid)).GetInputPreview(num));
            else
                return NotFound();
        }

        [HttpGet("{id}/tests/{tid}/input")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> GetTestInput(string id, string tid)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
            {
                return Ok(await (await res.Tests.Get(tid)).GetInput());
            }
            else
                return NotFound();
        }

        [HttpGet("{id}/tests/{tid}/output/{num}/preview")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<DataPreview>> GetTestOutputPreview(string id, string tid, int num)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
                return Ok(await (await res.Tests.Get(tid)).GetOutputPreview(num));
            else
                return NotFound();
        }

        [HttpGet("{id}/tests/{tid}/output")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<string>> GetTestOutput(string id, string tid)
        {
            var res = await _workspace.Problems.Get(id);
            if (res != null)
            {
                return Ok(await (await res.Tests.Get(tid)).GetOutput());
            }
            else
                return NotFound();
        }

        [HttpDelete("{id}")]
        public Task Delete(string id)
        {
            return _workspace.Problems.Delete(id);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<ProblemMetadata>> Create([FromBody] ProblemData data)
        {
            var res = await _workspace.Problems.Create(data.Metadata);
            if (res == null)
                return Conflict();

            await res.SetDescription(data.Description);
            foreach(var v in data.Samples)
            {
                var item = await res.Samples.Create(v.Metadata);
                await item.SetInput(v.Input);
                await item.SetOutput(v.Output);
            }

            foreach (var v in data.Tests)
            {
                var item = await res.Tests.Create(v.Metadata);
                await item.SetInput(v.Input);
                await item.SetOutput(v.Output);
            }

            return Created($"problems/{res.Id}", await res.GetMetadata());
        }
    }
}