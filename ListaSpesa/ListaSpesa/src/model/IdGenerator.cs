using System.Collections.Generic;

namespace ListaSpesa.Model
{
    public class IdGenerator
    {
        private static IdGenerator _instance;

        public static IdGenerator GetInstance()
        {
            return _instance ?? (_instance = new IdGenerator());
        }

        private List<int> _generatedId;

        private IdGenerator()
        {
            _generatedId = new List<int>();
        }

        public void AddUsedIds(IEnumerable<int> usedIds)
        {
            _generatedId.AddRange(usedIds);
        }

        public int GetNextId()
        {
            int nextId = 1;
            while (_generatedId.Contains(nextId))
            {
                nextId++;
            }
            _generatedId.Add(nextId);
            return nextId;
        }
    }
}
