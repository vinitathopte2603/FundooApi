//-----------------------------------------------------------------------
// <copyright file="CollaboratorRequestModel.cs" author="Vinita Thopte" company="Bridgelabz">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace FundooCommonLayer.UserRequestModel
{
    using System;
using System.Collections.Generic;

    /// <summary>
    /// property declaration
    /// </summary>
    public class CollaboratorRequestModel
    {
        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }
    }

    /// <summary>
    /// property declaration
    /// </summary>
    public class CollaborateMultiple
    {
        /// <summary>
        /// Gets or sets the collaborator request models.
        /// </summary>
        /// <value>
        /// The collaborator request models.
        /// </value>
        public List<CollaboratorRequestModel> CollaboratorRequestModels { get; set; }
    }
}
