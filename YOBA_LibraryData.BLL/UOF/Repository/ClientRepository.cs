using System.Collections.Generic;
using System.Linq;
using YOBA_LibraryData.BLL;
using YOBA_LibraryData.BLL.Entities.User;
using YOBA_LibraryData.DAL.UOF.Interfaces;
using YOBA_Services.Exceptions;

namespace YOBA_LibraryData.DAL.UOF.Repository
{
    public class ClientRepository:IClientRepository
    {
        private readonly YOBAContext _context;
        public ClientRepository(YOBAContext context)
        {
            _context = context;
        }
        public void Add(Client item)
        {
            if (_context.Clients.Find(item.Login) == null)
            {
                _context.Add(item);
                _context.SaveChanges();
            }
            else
            {
                throw new AlreadyExistException(item.Login);
            }
        }

        public void Delete(Client item)
        {
            if (_context.Clients.First(client => client.Login == item.Login) != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.Login);
            }
        }

        public IEnumerable<Client> GetAll()
        {
            if (_context.Clients != null)
            {
                return _context.Clients;
            }
            else
            {
                throw new EmptyDataException(typeof(Client).ToString());
            }
        }

        public Client GetById(int id)
        {
            var result = _context.Clients.First(client => client.ClientId == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new EmptyDataException(typeof(Client).ToString());
            }
        }

        public void Change(Client item)
        {
            if (_context.Clients.First(x => x.ClientId == item.ClientId) != null)
            {
                _context.Clients.Update(item);
                _context.SaveChanges();
            }
            else
            {
                throw new NotFoundException(item.ClientId);
            }
        }
    }
}

