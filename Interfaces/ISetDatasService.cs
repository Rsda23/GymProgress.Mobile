using GymProgress.Domain.Models;

namespace GymProgress.Mobile.Interfaces
{
    public interface ISetDatasService
    {
        public Task<SetData> GetSetDataByExericceId(string exerciceId);
        public Task<List<SetData>> GetSetDataByUserId(string userId);
        public Task<bool> PostSetData(SetData setData);
        public Task<bool> ReplaceSetData(SetData setData);
        public Task<bool> Delete(string setDataId);
    }
}
