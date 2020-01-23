using FundooCommonLayer.Model;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundooRepositoryLayer.Services
{
    public class LabelsRepository : ILabelsRepository
    {
        private UserContext _userContext;
        public LabelsRepository(UserContext userContext)
        {
            this._userContext = userContext;
        }
        public LabelModel AddLabel(string label, int userId)
        {
            try
            {
                LabelModel labelModel = new LabelModel
                {
                    UserId = userId,
                    Label = label,
                    IsCreated = DateTime.Now,
                    IsModified = DateTime.Now
                };
                _userContext.Labels.Add(labelModel);
                _userContext.SaveChanges();
                return labelModel;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public bool DeleteLabel(int userId, int labelId)
        {
            try
            {
                LabelModel labelModel = _userContext.Labels.FirstOrDefault(linq => (linq.UserId == userId) && (linq.Id == labelId));
                if (labelModel != null)
                {
                    _userContext.Labels.Remove(labelModel);
                    _userContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public LabelModel UpdateLabel(int userId, int labelId, string label)
        {
            try
            {
                LabelModel labelModel = _userContext.Labels.FirstOrDefault(linq => (linq.UserId == userId) && (linq.Id == labelId));
                if (labelModel != null)
                {
                    var labelUpdate = new LabelModel();
                    labelModel.Label = label;
                    labelModel.IsModified = DateTime.Now;
                    var labelstate = this._userContext.Labels.Attach(labelModel);
                    labelstate.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _userContext.SaveChanges();
                    return labelUpdate;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<LabelModel> GetAllLabels(int userId)
        {
            try
            {
                List<LabelModel> label = _userContext.Labels.Where(linq => linq.UserId == userId).ToList();
                if (label.Count != 0)
                {
                    return label;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
