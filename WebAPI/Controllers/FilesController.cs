using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/files")]
    public class FilesController: ControllerBase
    {
        [HttpPost("formfile")]
        public ActionResult PostIFormFile([FromForm] InputIFormFileDTO inputIFormFileDTO)
        {
            if (inputIFormFileDTO == null) { return BadRequest("all null"); }
            if (inputIFormFileDTO.File == null) { return BadRequest("File null"); }
            return Ok();
        }

        [HttpPost("bytes")]
        public ActionResult PostBase64File(InputBase64DTO inputBase64DTO)
        {
            if (inputBase64DTO == null) { return BadRequest("all null"); }
            if (inputBase64DTO.FileBase64 == null) { return BadRequest("File null"); }
            return Ok();
        }
    }

    public class InputIFormFileDTO
    {
        public string TestProperty { get; set; }
        public IFormFile File { get; set; }
    }

    public class InputBase64DTO
    {
        public string TestProperty { get; set; }
        public string FileBase64 { get; set; }
    }
}
