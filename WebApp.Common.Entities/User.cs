using Denis.UserList.Common.Libraries;
using System;
using System.Collections.Generic;

namespace Denis.UserList.Common.Entities
{
    public class User
    {
        private readonly HashSet<Award> awards;

        public int Id { get; set; }

        public string Name { get; private set; }

        public DateTime BirthDate { get; private set; }

        public int Age => DateTimeAdditional.CompleteYearDifference(BirthDate, DateTime.Now);

        public User(int id, string name, DateTime birthDate)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            awards = new HashSet<Award>();
        }

        public bool AddAward(Award award) 
        {
            return awards.Add(award);
        }

        public void AddAwards(IEnumerable<Award> awards)
        {
            foreach (var award in awards)
            {
                this.awards.Add(award);
            }
        }

        public IEnumerable<Award> GetAwards()
        {
            foreach (var award in awards)
            {
                yield return award;
            }
        }

    }
}