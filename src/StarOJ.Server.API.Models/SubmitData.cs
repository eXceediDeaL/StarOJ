﻿using Microsoft.AspNetCore.Http;
using StarOJ.Core.Judgers;
using System.ComponentModel.DataAnnotations;

namespace StarOJ.Server.API.Models
{
    public class SubmitData
    {
        [Required]
        public string ProblemId { get; set; }

        [Required]
        public string UserId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Code { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile CodeFile { get; set; }

        [Required]
        public ProgrammingLanguage Language { get; set; }
    }
}
