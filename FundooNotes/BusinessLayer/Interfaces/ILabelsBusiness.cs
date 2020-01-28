//-----------------------------------------------------------------------
// <copyright file="ILabelsBusiness.cs" author=Vinita Thopte company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooBusinessLayer.Interfaces
{
    using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

    /// <summary>
    /// method declaration
    /// </summary>
    public interface ILabelsBusiness
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="label">The label.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the label</returns>
        Task<LabelResponseModel> AddLabel(LabelsRequestModel label, int userId);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns a boolean value</returns>
        Task<bool> DeleteLabel(int userId, int labelId);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="label">The label.</param>
        /// <returns>returns the updated label</returns>
        Task<LabelResponseModel> UpdateLabel(int userId, int labelId, LabelsRequestModel label);

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the list of labels</returns>
        List<LabelResponseModel> GetAllLabels(int userId);
    }
}
