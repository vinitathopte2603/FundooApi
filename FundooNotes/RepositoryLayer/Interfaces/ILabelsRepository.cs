using FundooCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooRepositoryLayer.Interfaces
{
    public interface ILabelsRepository
    {
       LabelModel AddLabel(string label, int userId);
       bool DeleteLabel(int userId, int labelId);
       LabelModel UpdateLabel(int userId, int labelId, string label);
       List<LabelModel> GetAllLabels(int userId);
    }
}
