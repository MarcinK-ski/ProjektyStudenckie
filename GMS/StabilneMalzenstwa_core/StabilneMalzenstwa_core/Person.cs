using System;
using System.Collections.Generic;
using System.Text;

namespace StabilneMalzenstwa_core
{
    class Person : IComparable<Person>
    {
        public ushort Name { get; private set; }
        public List<Person> Preferences { get; private set; }
        public bool IsSingle { get; set; } = true;
        public Person RelationshipWith { get; set; }
        public HashSet<Person> RefusedBy { get; private set; } = new HashSet<Person>();


        public Person(ushort name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return (Name + 1).ToString();
        }

        public int CompareTo(Person other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public void AddPreferences(List<Person> preferences)
        {
            Preferences = preferences;
        }

        public void AddRefusedProposal(Person bitch)
        {
            RefusedBy.Add(bitch);
        }

        public void Deserted(Person bitch)
        {
            AddRefusedProposal(bitch);
            IsSingle = true;
            RelationshipWith = null;
        }

        public void CreateRelationShip(ref Person withWhom)
        {
            IsSingle = false;
            RelationshipWith = withWhom;

            withWhom.IsSingle = false;
            withWhom.RelationshipWith = this;
        }

        public bool IsBetterThanRival(Person rival, Person beloved)
        {
            for (int i = 0; i < beloved.Preferences.Count; i++)
            {
                if(beloved.Preferences[i].Name == rival.Name)
                {
                    return false;
                }
                else if(beloved.Preferences[i].Name == this.Name)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
