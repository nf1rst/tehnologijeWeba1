using Exceptions;
using HeavyArchives.Application.DTOs.Theme;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Application.Services;
using HeavyArchives.DAL.Repositories;
using HeavyArchives.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetAllThemes()
        {
            var themes =await themeService.GetAllThemesAsync();
            return Ok(themes);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetThemeById(int id)
        {
            var theme =await themeService.GetThemeByIdAsync(id);
            if (theme is null) return NotFound();
            return Ok(theme);
        }
        [HttpGet("Name")]
        public async Task<IActionResult> GetThemeByName(string name)
        {
            if (name is null) throw new InvalidInputException("Theme name cannot be null");
            var theme =await themeService.GetThemeByNameAsync(name);
            if (theme is null) return NotFound();
            return Ok(theme);
        }
        [HttpGet("forBand")]
        public async Task<IActionResult> GetThemesForBand(string bandName)
        {
            if (bandName is null) throw new InvalidInputException("Band name cannot be null");
            var themes =await themeService.GetThemesForBandAsync(bandName);
            if(themes is null)return NotFound();
            return Ok(themes);
        }
        [HttpGet("forLabel")]
        public async Task<IActionResult> GetThemesForLabel(string labelName)
        {
            if (labelName is null) throw new InvalidInputException("Label name cannot be null");
            var labels =await themeService.GetThemesForLabelAsync(labelName);
            if (labels is null) return NotFound();
            return Ok(labels);
        }
        [HttpPost]
        public async Task<IActionResult> CreateTheme(ManipulateThemeDTO createDTO)
        {
            var theme =await themeService.CreateThemeAsync(createDTO);
            return Created();
        }
        [HttpDelete]
        public async Task<IActionResult> DelteTheme(int id)
        {
            await themeService.DeleteThemeAsync(id);
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTheme(int id, ManipulateThemeDTO updateDTO)
        {
            var theme = await themeService.UpdateThemeAsync(id, updateDTO);
            return Ok(theme);
        }
    }
}
