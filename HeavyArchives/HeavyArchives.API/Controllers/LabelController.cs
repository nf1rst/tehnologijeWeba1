using Exceptions;
using FluentValidation;
using HeavyArchives.Application.DTOs.Label;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {

        private readonly ILabelService labelService;
        private readonly ILogger<LabelController> logger;
        public LabelController(ILabelService labelService, ILogger<LabelController>logger)
        {
            this.labelService = labelService;
            this.logger = logger;
        }

      
        [HttpGet]
        public async Task<IActionResult> GetAllLabels()
        {
            var labels =await labelService.GetAllLabelsAsync();
            if (labels is null) return NotFound();
            return Ok(labels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLabelByID(int id)
        {
            var label =await labelService.GetLabelByIdAsync(id);
            if (label is null) return NotFound();
            return Ok(label);
        }
        [HttpGet("Name")]
        public async Task<IActionResult> GetLabelByName(string labelName)
        {
            if (labelName is null) throw new InvalidInputException("Name cannot be null");

            var label =await labelService.GetLabelByNameAsync(labelName);
            if (label is null) return NotFound();
            return Ok(label);
        }
        [HttpGet("ByTheme")]
        public async Task<IActionResult> GetLabelsByTheme(string themeName)
        {
            if (themeName is null) throw new InvalidInputException("Name cannot be null");

            var labels =await labelService.GetLabelsWithDetailsByThemeAsync(themeName);
            if (labels is null) return NotFound();
            return Ok(labels);
        }
        [HttpGet("ForBand")]
        public async Task<IActionResult> GetLabelsForBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Name cannot be null");

            var labels =await labelService.GetLabelsForBandAsync(bandName);
            if (labels is null) return NotFound();
            return Ok(labels);
        }

        [HttpGet("Active")]
        public async Task<IActionResult> GetActiveLabels()
        {
            var labels =await labelService.GetActiveLabelsAsync();
            return Ok(labels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLabel(ManipulateLabelDTO labelDTO)
        {
            var created =await labelService.CreateLabelAsync(labelDTO);
            logger.LogInformation("[{Controller}] Label created: {@Label}", nameof(LabelController), created);
            return Created();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteLabel(int id)
        {
            await labelService.DeleteLabelAsync(id);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLabel(int id, ManipulateLabelDTO updatedDTO)
        {
            var label =await labelService.UpdateLabelAsync(id, updatedDTO);
            return Ok(label);
        }

        [HttpPut("AddBand")]
        public async Task<IActionResult> AddBand(int bandId, int labelId)
        {
            var band =await labelService.AddBandAsync(bandId, labelId);
            return Ok(band);
        }
        [HttpPut("RemoveBand")]
        public async Task<IActionResult> RemoveBand(int bandId, int labelId)
        {
            var band =await labelService.RemoveBandAsync(bandId, labelId);
            return Ok(band);
        }
        [HttpPut("AddTheme")]
        public async Task<IActionResult> AddTheme(int labelId, int themeId)
        {
            var band =await labelService.AddThemeAsync(themeId, labelId);
            return Ok(band);
        }
        [HttpPut("RemoveTheme")]
        public async Task<IActionResult> RemoveTheme(int labelId, int themeid)
        {
            var band =await labelService.RemoveThemeAsync(themeid, labelId);
            return Ok(band);
        }
      


    }
}
