using Exceptions;
using HeavyArchives.Application.DTOs.Label;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Security.AccessControl;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {

        private readonly ILabelService labelService;
        public LabelController(ILabelService labelService)
        {
            this.labelService = labelService;
        }

      
        [HttpGet]
        public IActionResult GetAllLabels()
        {
            var labels = labelService.GetAllLabels();
            if (labels is null) return NotFound();
            return Ok(labels);
        }

        [HttpGet("{id}")]
        public IActionResult GetLabelByID(int id)
        {
            var label = labelService.GetLabelById(id);
            if (label is null) return NotFound();
            return Ok(label);
        }
        [HttpGet("Name")]
        public IActionResult GetLabelByName(string labelName)
        {
            if (labelName is null) throw new InvalidInputException("Label name cannot be null");
            var label = labelService.GetLabelByName(labelName);
            if (label is null) return NotFound();
            return Ok(label);
        }
        [HttpGet("ByTheme")]
        public IActionResult GetLabelsByTheme(string themeName)
        {
            if (themeName is null) throw new InvalidInputException("Theme name cannot be null");
            var labels = labelService.GetLabelsWithDetailsByTheme(themeName);
            if (labels is null) return NotFound();
            return Ok(labels);
        }
        [HttpGet("ForBand")]
        public IActionResult GetLabelsForBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var labels = labelService.GetLabelsForBand(bandName);
            if (labels is null) return NotFound();
            return Ok(labels);
        }

        [HttpGet("Active")]
        public IActionResult GetActiveLabels()
        {
            var labels = labelService.GetActiveLabels();
            return Ok(labels);
        }

        [HttpPost]
        public IActionResult CreateLabel(ManipulateLabelDTO labelDTO)
        {
            var created = labelService.CreateLabel(labelDTO);
            return Created();
        }


        [HttpDelete]
        public IActionResult DeleteLabel(int id)
        {
            labelService.DeleteLabel(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateLabel(int id, ManipulateLabelDTO updatedDTO)
        {
            var label = labelService.UpdateLabel(id, updatedDTO);
            return Ok(label);
        }

        [HttpPut("AddBand")]
        public IActionResult AddBand(int bandId, int labelId)
        {
            var band = labelService.AddBand(bandId, labelId);
            return Ok(band);
        }
        [HttpPut("RemoveBand")]
        public IActionResult RemoveBand(int bandId, int labelId)
        {
            var band = labelService.RemoveBand(bandId, labelId);
            return Ok(band);
        }
        [HttpPut("AddTheme")]
        public IActionResult AddTheme(int bandId, int labelId)
        {
            var band = labelService.AddTheme(bandId, labelId);
            return Ok(band);
        }
        [HttpPut("RemoveTheme")]
        public IActionResult RemoveTheme(int bandId, int labelId)
        {
            var band = labelService.RemoveBand(bandId, labelId);
            return Ok(band);
        }
      


    }
}
