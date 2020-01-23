using FundooBusinessLayer.Interfaces;
using FundooCommonLayer.Model;
using FundooRepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooBusinessLayer.Services
{
    public class LabelsBusiness : ILabelsBusiness
    {
        private ILabelsRepository _labelsRepository;
        public LabelsBusiness(ILabelsRepository labelsRepository)
        {
            this._labelsRepository = labelsRepository;
        }
        public LabelModel AddLabel(string label, int userId)
        {
            if (label != null && userId != 0)
            {
                return this._labelsRepository.AddLabel(label, userId);
            }
            else
            {
                return null;
            }
        }

        public bool DeleteLabel(int userId, int labelId)
        {
            if (userId != 0 && labelId != 0)
            {
                return this._labelsRepository.DeleteLabel(userId, labelId);
            }
            else 
            {
                return false;
            }
        }

        public List<LabelModel> GetAllLabels(int userId)
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

        public LabelModel UpdateLabel(int userId, int labelId, string label)
        {
            if (userId != 0 && labelId != 0 && label != null)
            {
                return this._labelsRepository.UpdateLabel(userId, labelId, label);
            }
            else
            {
                return null;
            }
        }
    }
}
