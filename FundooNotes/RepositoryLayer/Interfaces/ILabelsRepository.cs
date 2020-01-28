//-----------------------------------------------------------------------
// <copyright file="ILabelsRepository.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooRepositoryLayer.Interfaces
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
    public interface ILabelsRepository
    {
        /// <summary>
        /// Adds the label.
        /// </summary>
        /// <param name="labelsRequestModel">The labels request model.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the added label</returns>
        Task<LabelResponseModel> AddLabel(LabelsRequestModel labelsRequestModel, int userId);

        /// <summary>
        /// Deletes the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <returns>returns the boolean result</returns>
        Task<bool> DeleteLabel(int userId, int labelId);

        /// <summary>
        /// Updates the label.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="labelId">The label identifier.</param>
        /// <param name="labelsRequestModel">The labels request model.</param>
        /// <returns>returns updated result</returns>
        Task<LabelResponseModel> UpdateLabel(int userId, int labelId, LabelsRequestModel labelsRequestModel);

        /// <summary>
        /// Gets all labels.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>returns the list of labels</returns>
        List<LabelResponseModel> GetAllLabels(int userId);
     
    }
}
