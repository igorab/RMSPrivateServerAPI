using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

namespace RMSPrivateServerAPI.Repositories
{
    public class PPMRepository : IPPMRepository
    {
        Task<int> IPPMRepository.DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        Task<PPMTask?> IPPMRepository.Get(int id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<PPMTask>> IPPMRepository.GetAll(bool returnDeletedRecords)
        {
            throw new NotImplementedException();
        }

        Task<int> IPPMRepository.UpsertAsync(PPMTask ppmTask)
        {
            throw new NotImplementedException();
        }
    }
}
