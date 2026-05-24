using AutoMapper;
using Exceptions;
using HeavyArchives.Application.DTOs.Theme;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Services
{
    public class ThemeService : IThemeService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;
        public ThemeService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<ThemeDTO> CreateThemeAsync(ManipulateThemeDTO createDTO)
        {
            Theme? theme = mapper.Map<Theme>(createDTO);
            await uow.Themes.InsertAsync(theme);
            await uow.SaveChangesAsync();
            return mapper.Map<ThemeDTO>(theme);
        }

        public async Task DeleteThemeAsync(int id)
        {
            var theme =await uow.Themes.GetByIdAsync(id);
            if (theme is null) throw new EntityNotExistant("Theme wasn't found");

            await uow.Themes.DeleteAsync(theme);
            await uow.SaveChangesAsync();
        }

        public async Task<ICollection<ThemeDTO>> GetAllThemesAsync()
        {
            var themes =await uow.Themes.GetThemesWithDetailsAsync();
            return mapper.Map<ICollection<ThemeDTO>>(themes);
        }

        public async Task<ThemeDTO?> GetThemeByIdAsync(int id)
        {
            var theme =await uow.Themes.GetThemeWithDetailsByIdAsync(id);
            return mapper.Map<ThemeDTO>(theme);
        }

        public async Task<ThemeDTO?> GetThemeByNameAsync(string name)
        {
            var theme =await uow.Themes.GetThemeWithDetailsByNameAsync(name);
            return mapper.Map<ThemeDTO>(theme);
        }

        public async Task<ICollection<ThemeDTO>> GetThemesForBandAsync(string bandName)
        {
            Band? band =await uow.Bands.GetBandWithDetailsByNameAsync(bandName);
            if (band is null) return null;
            var themes =await uow.Themes.GetThemesWithDetailsForBandAsync(band.Id);
            return mapper.Map<ICollection<ThemeDTO>>(themes);
        }

        public async Task<ICollection<ThemeDTO>> GetThemesForLabelAsync(string labelName)
        {
            Label? label =await uow.Labels.GetLabelWithDetailsByNameAsync(labelName);
            if (label is null) return null;
            var themes = uow.Themes.GetThemesWithDetailsForLabelAsync(label.Id);
            return mapper.Map<ICollection<ThemeDTO>>(themes);
        }

        public async Task<ThemeDTO> UpdateThemeAsync(int id, ManipulateThemeDTO updateDTO)
        {
            Theme? theme =await uow.Themes.GetThemeWithDetailsByIdAsync(id);
            if (theme is null) throw new EntityNotExistant("Theme wasn't found");

            mapper.Map(updateDTO, theme);
            //ne ukljucuje bendove i labelove
            await uow.SaveChangesAsync();
            return mapper.Map<ThemeDTO>(theme);
        }
    }
}
