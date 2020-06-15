using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFcoreBL.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using EFmodels.Resource;

namespace EFcoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleImageDataController : ControllerBase
    {
        private readonly IMultipleImageData imageData;
        private readonly IMapper map;
        public MultipleImageDataController(IMultipleImageData _imageData, IMapper _map)
        {
            imageData = _imageData;
            map = _map;
        }

        [HttpDelete("DeleteMultipleImage/{id}")]
        [Authorize(Roles = "Admin,Author")]
        public IActionResult DeleteMultipleImage(int id)
        {
            imageData.DeleteFromMultipleImageDataTable(id);
            imageData.Save();
            return Ok(HttpStatusCode.OK);
        }

        [HttpGet("ShowImage")]
        public IActionResult ShowImage()
        {
            var data = imageData.GetDataFromImageTable().GetAwaiter().GetResult();
            var res = map.Map<List<MultipleImageDataResource>>(data);
            //  var res = imageData.GetAllFromTable();
            return Ok(res);
        }
    }
}