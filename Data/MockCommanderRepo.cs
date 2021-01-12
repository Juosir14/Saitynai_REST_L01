using System.Collections.Generic;
using System;
using Saitynai_REST_L01.Models;

namespace Saitynai_REST_L01.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boil an egg", Line="Boiled water"},
                new Command{Id=1, HowTo="Cut", Line="knife"},
                new Command{Id=2, HowTo="Make cup", Line="tea bag"}
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id=0,HowTo="Boil an egg",Line="Boiled water"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }  

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}