using GymProgress.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProgress.Mobile.Interfaces
{
    public interface ISetDatasService
    {
        public Task<SetData> GetSetDataByExericceId(string exerciceId);
        public Task<bool> PostSetData(SetData setData);
        public Task<bool> ReplaceSetData(SetData setData);
        public Task<bool> Delete(string setDataId);
    }
}
