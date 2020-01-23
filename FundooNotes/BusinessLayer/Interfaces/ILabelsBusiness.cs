using FundooCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundooBusinessLayer.Interfaces
{
    public interface ILabelsBusiness
    {
        LabelModel AddLabel(string label, int userId);
        bool DeleteLabel(int userId, int labelId);
        LabelModel UpdateLabel(int userId, int labelId, string label);
        List<LabelModel> GetAllLabels(int userId);
    }
}
