using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using FundooRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FundooBusinessLayer.Services
{
    public class LabelsBusiness : ILabelsBusiness
    {
        private ILabelsRepository _labelsRepository;
        public LabelsBusiness(ILabelsRepository labelsRepository)
        {
            this._labelsRepository = labelsRepository;
        }

        public async Task<LabelResponseModel> AddLabel(LabelsRequestModel label, int userId)
        {
            if (label != null && userId != 0)
            {
                return await this._labelsRepository.AddLabel(label, userId);
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> DeleteLabel(int userId, int labelId)
        {
            if (userId != 0 && labelId != 0)
            {
                return await this._labelsRepository.DeleteLabel(userId, labelId);
            }
            else 
            {
                return false;
            }
        }

        public List<LabelResponseModel> GetAllLabels(int userId)
        {
            if (userId != 0)
            {
                return this._labelsRepository.GetAllLabels(userId);
            }
            else
            {
                return null;
            }
        }

        public async Task<LabelResponseModel> UpdateLabel(int userId, int labelId, LabelsRequestModel label)
        {
            if (userId != 0 && labelId != 0 && label != null)
            {
                return await this._labelsRepository.UpdateLabel(userId, labelId, label);
            }
            else
            {
                return null;
            }
        }
    }
}
