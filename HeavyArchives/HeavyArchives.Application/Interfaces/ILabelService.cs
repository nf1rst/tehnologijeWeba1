using HeavyArchives.Application.DTOs.Label;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyArchives.Application.Interfaces
{
    public interface ILabelService
    {
        LabelDTO? GetLabelById(int id);
        ICollection<JustLabelDTO> GetAllLabels();
        LabelDTO? GetLabelByName(string labelName);
        LabelDTO CreateLabel(ManipulateLabelDTO createLabelDTO);
        ICollection<LabelWithThemesDTO> GetLabelsWithDetailsByTheme(string themeName);
        ICollection<LabelWithThemesDTO> GetLabelsForBand(string bandName);
        ICollection<JustLabelDTO> GetActiveLabels();
        LabelDTO UpdateLabel(int id, ManipulateLabelDTO updatedDTO);
        void DeleteLabel(int id);
        LabelWithMembersDTO AddBand(int bandId, int labelId);
        LabelWithMembersDTO RemoveBand(int bandId, int labelId);
        LabelWithThemesDTO AddTheme(int themeId, int labelId);
        LabelWithThemesDTO RemoveTheme(int themeId, int labelId);

    }
}
