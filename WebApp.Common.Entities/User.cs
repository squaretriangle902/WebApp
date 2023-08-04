using Denis.UserList.Common.Libraries;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Denis.UserList.Common.Entities
{
    public class User
    {
        private readonly HashSet<Award> awards;

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public int Age => DateTimeAdditional.CompleteYearDifference(BirthDate, DateTime.Now);

        public byte[] Image { get; set; }

        public User()
        {
            awards = new HashSet<Award>();
            Image = new byte[0];
        }

        public User(int id, string name, DateTime birthDate)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            awards = new HashSet<Award>();
            Image = new byte[0];
        }

        public bool AddAward(Award award) 
        {
            return awards.Add(award);
        }

        public bool RemoveAward(Award award)
        {
           return awards.Remove(award);
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