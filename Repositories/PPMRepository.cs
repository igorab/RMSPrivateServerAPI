using RMSPrivateServerAPI.Data;
using RMSPrivateServerAPI.Entities;
using RMSPrivateServerAPI.Interfaces;

namespace RMSPrivateServerAPI.Repositories;

public class PPMRepository : IPPMRepository
{
    private readonly DatabaseConnectionFactory _databaseConnectionFactory;

    public PPMRepository(DatabaseConnectionFactory databaseConnectionFactory)
    {
        _databaseConnectionFactory = databaseConnectionFactory;
    }

    Task<PPMTask?> IPPMRepository.Get(int id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<PPMTask>> IPPMRepository.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<int> IPPMRepository.UpsertAsync(PPMTask ppmTask)
    {
        throw new NotImplementedException();
    }

    Task<int> IPPMRepository.DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
