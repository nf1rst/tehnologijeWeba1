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
        public ThemeDTO CreateTheme(ManipulateThemeDTO createDTO)
        {
            Theme theme = mapper.Map<Theme>(createDTO);
            uow.Themes.Insert(theme);
            uow.SaveChanges();
            return mapper.Map<ThemeDTO>(theme);
        }

        public void DeleteTheme(int id)
        {
            var theme = uow.Themes.GetById(id);
            if (theme is null) throw new EntityNotExistant("Theme wasn't found");

            uow.Themes.Delete(theme);
            uow.SaveChanges();
        }

        public ICollection<ThemeDTO> GetAllThemes()
        {
            var themes = uow.Themes.GetThemesWithDetails();
            return mapper.Map<ICollection<ThemeDTO>>(themes);
        }

        public ThemeDTO? GetThemeById(int id)
        {
            var theme = uow.Themes.GetThemeWithDetailsById(id);
            return mapper.Map<ThemeDTO>(theme);
        }

        public ThemeDTO? GetThemeByName(string name)
        {
            var theme = uow.Themes.GetThemeWithDetailsByName(name);
            return mapper.Map<ThemeDTO>(theme);
        }

        public ICollection<ThemeDTO> GetThemesForBand(string bandName)
        {
            Band band = uow.Bands.GetBandWithDetailsByName(bandName);
            if (band is null) return null;
            var themes = uow.Themes.GetThemesWithDetailsForBand(band.Id);
            return mapper.Map<ICollection<ThemeDTO>>(themes);
        }

        public ICollection<ThemeDTO> GetThemesForLabel(string labelName)
        {
            Label label = uow.Labels.GetLabelWithDetailsByName(labelName);
            if (label is null) return null;
            var themes = uow.Themes.GetThemesWithDetailsForLabel(label.Id);
            return mapper.Map<ICollection<ThemeDTO>>(themes);
        }

        public ThemeDTO UpdateTheme(int id, ManipulateThemeDTO updateDTO)
        {
            Theme? theme = uow.Themes.GetThemeWithDetailsById(id);
            if (theme is null) throw new EntityNotExistant("Theme wasn't found");

            mapper.Map(updateDTO, theme);
            //ne ukljucuje bendove i labelove
            uow.SaveChanges();
            return mapper.Map<ThemeDTO>(theme);
        }
    }
}
