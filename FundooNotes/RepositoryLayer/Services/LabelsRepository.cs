using FundooCommonLayer.Model;
using FundooCommonLayer.UserRequestModel;
using FundooRepositoryLayer.Interfaces;
using FundooRepositoryLayer.ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooRepositoryLayer.Services
{
    public class LabelsRepository : ILabelsRepository
    {
        private UserContext _userContext;
        public LabelsRepository(UserContext userContext)
        {
            this._userContext = userContext;
        }
        public async Task<LabelResponseModel> AddLabel(LabelsRequestModel label, int userId)
        {
            try
            {
                LabelModel labelModel = new LabelModel
                {
                    UserId = userId,
                    Label = label.Label,
                    IsCreated = DateTime.Now,
                    IsModified = DateTime.Now
                };
                LabelResponseModel labelResponse = new LabelResponseModel()
                {
                    Id = labelModel.Id,
                    Label = labelModel.Label,
                    IsCreated = labelModel.IsCreated,
                    IsModified = labelModel.IsModified
                };
                _userContext.Labels.Add(labelModel);
                await _userContext.SaveChangesAsync();
                return labelResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> DeleteLabel(int userId, int labelId)
        {
            try
            {
                LabelModel labelModel = _userContext.Labels.FirstOrDefault(linq => (linq.UserId == userId) && (linq.Id == labelId));
                if (labelModel != null)
                {
                    _userContext.Labels.Remove(labelModel);
                    await _userContext.SaveChangesAsync();
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
        public async Task<LabelResponseModel> UpdateLabel(int userId, int labelId, LabelsRequestModel labelsRequestModel)
        {
            try
            {
                var labelModel = _userContext.Labels.FirstOrDefault(linq => (linq.UserId == userId) && (linq.Id == labelId));
                LabelModel label = new LabelModel();
                if (labelModel != null)
                {
                    labelModel.Label = labelsRequestModel.Label;
                    var data = this._userContext.Labels.Attach(labelModel);
                    data.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await this._userContext.SaveChangesAsync();
                }
                LabelResponseModel labelResponse = new LabelResponseModel()
                {
                    Id = label.Id,
                    Label = label.Label,
                    IsCreated = label.IsCreated,
                    IsModified = label.IsModified
                };
                return labelResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public List<LabelResponseModel> GetAllLabels(int userId)
        {
            try
            {
                List<LabelResponseModel> label = _userContext.Labels.Where(linq => linq.UserId == userId).Select(linq => new LabelResponseModel
                {
                    Id = linq.Id,
                    Label = linq.Label,
                    IsCreated = linq.IsCreated,
                    IsModified = linq.IsModified
                }).ToList();
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
