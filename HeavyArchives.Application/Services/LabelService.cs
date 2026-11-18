using HeavyArchives.Application.DTOs.Label;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using HeavyArchives.Domain.Entities.M2MTables;
using AutoMapper;
using Exceptions;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Services;
    public class LabelService : ILabelService
    {
        private readonly IUnitOfWork uow;
        private readonly IMapper mapper;

        public LabelService(IUnitOfWork uow, IMapper mapper)
        {
            this.uow = uow;
            this.mapper = mapper;
        }
        public async Task<LabelWithMembersDTO> AddBandAsync(int bandId, int labelId)
          {
            Band? band = await uow.Bands.GetBandWithDetailsAsync(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");

            Label? label =await uow.Labels.GetLabelWithDetailsAsync(labelId);
            if (label is null) throw new EntityNotExistant("Label wans't found");

            if (label.BandLabel.Any(bl => bl.BandId == bandId)) throw new RelationExistsException("That band is already associated with the label");

            label.BandLabel.Add(new BandLabel() { Band = band, Label = label});
            await uow.SaveChangesAsync();
            return mapper.Map<LabelWithMembersDTO>(label);
        }

        public async Task<LabelWithThemesDTO> AddThemeAsync(int themeId, int labelId)
        {
            Label? label=await uow.Labels.GetLabelWithDetailsAsync(labelId);
            if (label is null) throw new EntityNotExistant("Label wasn't found");

            Theme? theme = await uow.Themes.GetThemeWithDetailsByIdAsync(themeId);
            if (theme is null) throw new EntityNotExistant("Theme wans't found");

            if (label.LabelTheme.Any(lt=>lt.ThemeId==themeId)) throw new RelationExistsException("That theme is already associated with the label");


            label.LabelTheme.Add(new LabelTheme() { Theme= theme, Label=label});

            await uow.SaveChangesAsync();
            return mapper.Map<LabelWithThemesDTO>(label);
    }

        public async Task<LabelDTO> CreateLabelAsync(ManipulateLabelDTO createLabelDTO)
        {
            var label = mapper.Map<Label>(createLabelDTO);
            await uow.Labels.InsertAsync(label);
            await uow.SaveChangesAsync();
            return mapper.Map<LabelDTO>(label);
        }

        public async Task DeleteLabelAsync(int id)
        { 
            var label = await uow.Labels.GetByIdAsync(id);
            if (label is null) throw new EntityNotExistant("Label wasn't found");

            await uow.Labels.DeleteAsync(label);
            await uow.SaveChangesAsync();
        }

        public async Task<ICollection<JustLabelDTO>> GetActiveLabelsAsync()
        {
            var labels =await uow.Labels.GetActiveLabelsAsync();
            return mapper.Map <ICollection<JustLabelDTO>>(labels);
        }

        public async Task<ICollection<JustLabelDTO>> GetAllLabelsAsync()
        {
            var labels =await uow.Labels.GetAllLabelsAsync();
            return mapper.Map<ICollection<JustLabelDTO>>(labels);
        }

        public async Task<LabelDTO?> GetLabelByIdAsync(int id)
        {
            var label =await uow.Labels.GetLabelWithDetailsAsync(id);
            if (label is null) throw new EntityNotExistant("Label wasn't found");
            return mapper.Map<LabelDTO>(label);
        }

        public async Task<LabelDTO?> GetLabelByNameAsync(string labelName)
        {
            Label? label =await uow.Labels.GetLabelWithDetailsByNameAsync(labelName);
            if (label is null) throw new EntityNotExistant("Label wasn't found");
            return mapper.Map<LabelDTO>(label);
        }

        public async Task<ICollection<LabelWithThemesDTO>> GetLabelsForBandAsync(string bandName)
        {
            var labels =await uow.Labels.GetLabelsWithDetailsByBandAsync(bandName);
            return mapper.Map<ICollection<LabelWithThemesDTO>>(labels);
        }

        public async Task<ICollection<LabelWithThemesDTO>> GetLabelsWithDetailsByThemeAsync(string themeName)
        {
            var labels =await uow.Labels.GetLabelsByThemeAsync(themeName);
            return mapper.Map<ICollection<LabelWithThemesDTO>>(labels);
        }

        public async Task<LabelWithMembersDTO> RemoveBandAsync(int bandId, int labelId)
        {
            Label? label =await uow.Labels.GetLabelWithDetailsAsync(labelId);
            if (label is null) throw new EntityNotExistant("Label wasn't found");

            BandLabel? entry = label.BandLabel.FirstOrDefault(bl => bl.BandId == bandId);
            if (entry is null) throw new NoRelationException("Band is not associated with that label");

            await uow.BandLabel.RemoveBandLabelAsync(entry);
            await uow.SaveChangesAsync();
            return mapper.Map<LabelWithMembersDTO>(label);
        }

        public async Task<LabelWithThemesDTO> RemoveThemeAsync(int themeId, int labelId)
        {
            Theme? theme = await uow.Themes.GetThemeWithDetailsByIdAsync(themeId);
            if (theme is null) throw new EntityNotExistant("Theme wasn't found");

            Label? label =await uow.Labels.GetLabelWithDetailsAsync(labelId);
            if (label is null) throw new EntityNotExistant("Label wasn't found");

            LabelTheme? entry = label.LabelTheme.FirstOrDefault(bl => bl.ThemeId== themeId);
            if (entry is null) throw new NoRelationException("Theme is not associated with that label");

            await uow.LabelThemes.RemoveLabelThemeAsync(entry);
            await uow.SaveChangesAsync();
            return mapper.Map<LabelWithThemesDTO>(label);
    }

        public async Task<LabelDTO> UpdateLabelAsync(int id, ManipulateLabelDTO updatedDTO)
        {
            var label =await uow.Labels.GetLabelWithDetailsAsync(id);
            if (label is null) throw new EntityNotExistant("Label wans't found");

            mapper.Map(updatedDTO, label);

            await uow.SaveChangesAsync();
            return mapper.Map<LabelDTO>(label);

        }
    }
