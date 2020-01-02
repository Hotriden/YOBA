using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.DAL.Entities.User;
using YOBA_LibraryData.DAL.UOF.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.DAL.UOF.Repository
{
    public class ClientLogRepository : IClientLogRepository
    {
        private readonly YOBAContext _context;
        public ClientLogRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(ClientLog item)
        {
            if (_context.ClientLogs.Find(item.Client) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.Client.Login);
            }
        }

        public void Change(ClientLog item)
        {
            if (_context.ClientLogs.First(x => x. == item.) != null)
            {
                _context.Clients.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.ClientId);
            }
        }

        public void Delete(ClientLog item)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ClientLog> GetAll()
        {
            throw new NotImplementedException();
        }

        public ClientLog GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
