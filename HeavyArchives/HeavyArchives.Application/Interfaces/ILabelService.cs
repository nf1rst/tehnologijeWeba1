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
        Task<LabelDTO?> GetLabelByIdAsync(int id);
        Task<ICollection<JustLabelDTO>> GetAllLabelsAsync();
       Task<LabelDTO?> GetLabelByNameAsync(string labelName);
        Task<LabelDTO> CreateLabelAsync(ManipulateLabelDTO createLabelDTO);
        Task<ICollection<LabelWithThemesDTO>> GetLabelsWithDetailsByThemeAsync(string themeName);
        Task<ICollection<LabelWithThemesDTO>> GetLabelsForBandAsync(string bandName);
        Task<ICollection<JustLabelDTO>> GetActiveLabelsAsync();
        Task<LabelDTO> UpdateLabelAsync(int id, ManipulateLabelDTO updatedDTO);
        Task DeleteLabelAsync(int id);
        Task<LabelWithMembersDTO >AddBandAsync(int bandId, int labelId);
        Task<LabelWithMembersDTO > RemoveBandAsync(int bandId, int labelId);
        Task<LabelWithThemesDTO >AddThemeAsync(int themeId, int labelId);
        Task<LabelWithThemesDTO >RemoveThemeAsync(int themeId, int labelId);

    }
}
