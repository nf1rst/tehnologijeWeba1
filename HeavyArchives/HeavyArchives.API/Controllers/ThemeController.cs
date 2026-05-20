using Exceptions;
using HeavyArchives.Application.DTOs.Theme;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Application.Services;
using HeavyArchives.DAL.Repositories;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace HeavyArchives.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThemeController : ControllerBase
    {
        private readonly IThemeService themeService;
        public ThemeController(IThemeService themeService)
        {
            this.themeService = themeService;
        }

        [HttpGet]
        public IActionResult GetAllThemes()
        {
            var themes = themeService.GetAllThemes();
            return Ok(themes);
        }
        [HttpGet("{id}")]
        public IActionResult GetThemeById(int id)
        {
            var theme = themeService.GetThemeById(id);
            if (theme is null) return NotFound();
            return Ok(theme);
        }
        [HttpGet("Name")]
        public IActionResult GetThemeByName(string name)
        {
            if (name is null) throw new InvalidInputException("Theme name cannot be null");
            var theme = themeService.GetThemeByName(name);
            if (theme is null) return NotFound();
            return Ok(theme);
        }
        [HttpGet("forBand")]
        public IActionResult GetThemesForBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var themes = themeService.GetThemesForBand(bandName);
            if(themes is null)return NotFound();
            return Ok(themes);
        }
        [HttpGet("forLabel")]
        public IActionResult GetThemesForLabel(string labelName)
        {
            if (labelName is null) throw new InvalidInputException("Label name cannot be null");
            var labels = themeService.GetThemesForLabel(labelName);
            if (labels is null) return NotFound();
            return Ok(labels);
        }
        [HttpPost]
        public IActionResult CreateTheme(ManipulateThemeDTO createDTO)
        {
            var theme = themeService.CreateTheme(createDTO);
            return Created();
        }
        [HttpDelete]
        public IActionResult DelteTheme(int id)
        {
            themeService.DeleteTheme(id);
            return NoContent();
        }
        [HttpPut]
        public IActionResult UpdateTheme(int id, ManipulateThemeDTO updateDTO)
        {
            var theme = themeService.UpdateTheme(id, updateDTO);
            return Ok(theme);
        }
    }
}
