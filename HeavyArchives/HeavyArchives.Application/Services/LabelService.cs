using HeavyArchives.Application.DTOs.Label;
using HeavyArchives.Application.Interfaces;
using HeavyArchives.Domain.Entities;
using HeavyArchives.Domain.Interfaces;
using HeavyArchives.Domain.Entities.M2MTables;
using AutoMapper;
using Exceptions;

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
        public LabelWithMembersDTO AddBand(int bandId, int labelId)
        {
            Band? band = uow.Bands.GetBandWithDetails(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");
        
            Label? label = uow.Labels.GetLabelWithDetails(labelId);
            if (label is null) throw new EntityNotExistant("Label wans't found");

            if (label.BandLabel.Any(bl => bl.BandId == bandId)) throw new RelationExistsException("That band is already associated with the label");

            label.BandLabel.Add(new BandLabel() { BandId = bandId, LabelId = labelId });
            uow.SaveChanges();
            return mapper.Map<LabelWithMembersDTO>(label);
        }

        public LabelWithThemesDTO AddTheme(int themeId, int labelId)
        {
            Label? label= uow.Labels.GetLabelWithDetails(labelId);
            if (label is null) throw new EntityNotExistant("Label wasn't found");

            Theme? theme = uow.Themes.GetThemeWithDetailsById(themeId);
            if (theme is null) throw new EntityNotExistant("Theme wans't found");

            if (label.LabelTheme.Any(lt=>lt.ThemeId==themeId)) throw new RelationExistsException("That theme is already associated with the label");

            label.LabelTheme.Add(new LabelTheme() { ThemeId=themeId, LabelId = labelId });
            uow.SaveChanges();
            return mapper.Map<LabelWithThemesDTO>(label);
    }

        public LabelDTO CreateLabel(ManipulateLabelDTO createLabelDTO)
        {
            var label = mapper.Map<Label>(createLabelDTO);
            uow.Labels.Insert(label);
            uow.SaveChanges();
            return mapper.Map<LabelDTO>(label);
        }

        public void DeleteLabel(int id)
        { 
            var label = uow.Labels.GetById(id);
            if (label is null) throw new EntityNotExistant("Label wasn't found");

            uow.Labels.Delete(label);
            uow.SaveChanges();
        }

        public ICollection<JustLabelDTO> GetActiveLabels()
        {
            var labels = uow.Labels.GetActiveLabels();
            return mapper.Map <ICollection<JustLabelDTO>>(labels);
        }

        public ICollection<JustLabelDTO> GetAllLabels()
        {
            var labels = uow.Labels.GetAllLabels();
            return mapper.Map<ICollection<JustLabelDTO>>(labels);
        }

        public LabelDTO? GetLabelById(int id)
        {
            var label = uow.Labels.GetLabelWithDetails(id);
            if (label is null) throw new EntityNotExistant("Label wasn't found");
            return mapper.Map<LabelDTO>(label);
        }

        public LabelDTO? GetLabelByName(string labelName)
        {
            Label label = uow.Labels.GetLabelWithDetailsByName(labelName);
            if (label is null) throw new EntityNotExistant("Label wasn't found");
            return mapper.Map<LabelDTO>(label);
        }

        public ICollection<LabelWithThemesDTO> GetLabelsForBand(string bandName)
        {
            var labels = uow.Labels.GetLabelsWithDetailsByBand(bandName);
            return mapper.Map<ICollection<LabelWithThemesDTO>>(labels);
        }

        public ICollection<LabelWithThemesDTO> GetLabelsWithDetailsByTheme(string themeName)
        {
            var labels = uow.Labels.GetLabelsByTheme(themeName);
            return mapper.Map<ICollection<LabelWithThemesDTO>>(labels);
        }

        public LabelWithMembersDTO RemoveBand(int bandId, int labelId)
        {
            Band? band = uow.Bands.GetBandWithDetails(bandId);
            if (band is null) throw new EntityNotExistant("Band wasn't found");

            Label? label = uow.Labels.GetLabelWithDetails(labelId);
            if (label is null) throw new EntityNotExistant("Label wasn't found");

            BandLabel? entry = label.BandLabel.FirstOrDefault(bl => bl.BandId == bandId);
            if (entry is null) throw new NoRelationException("Band is not associated with that label");
            
            label.BandLabel.Remove(entry);
            uow.SaveChanges();
            return mapper.Map<LabelWithMembersDTO>(label);
        }

        public LabelWithThemesDTO RemoveTheme(int themeId, int labelId)
        {
            Theme? theme = uow.Themes.GetThemeWithDetailsById(themeId);
            if (theme is null) throw new EntityNotExistant("Theme wasn't found");

            Label? label = uow.Labels.GetLabelWithDetails(labelId);
            if (label is null) throw new EntityNotExistant("Label wasn't found");

            LabelTheme? entry = label.LabelTheme.FirstOrDefault(bl => bl.ThemeId== themeId);
            if (entry is null) throw new NoRelationException("Theme is not associated with that label");

            label.LabelTheme.Remove(entry);
            uow.SaveChanges();
            return mapper.Map<LabelWithThemesDTO>(label);
    }

        public LabelDTO UpdateLabel(int id, ManipulateLabelDTO updatedDTO)
        {
            var label = uow.Labels.GetLabelWithDetails(id);
            if (label is null) throw new EntityNotExistant("Label wans't found");

            mapper.Map(updatedDTO, label);

            uow.SaveChanges();
            return mapper.Map<LabelDTO>(label);

        }
    }
