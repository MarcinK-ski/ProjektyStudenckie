using System;
using System.Collections.Generic;
using System.Text;

namespace StabilneMalzenstwa_core
{
    class People
    {
        public ushort N { get; set; }  //Liczba kobiet/mezczyzn/malzenstw
        public List<Person> Women { get; private set; }
        public List<Person> Men { get; private set; }
        public SortedSet<Person> Relationships { get; private set; }

        public People(ushort n)
        {
            N = n;
            Women = new List<Person>(n);
            Men = new List<Person>(n);
            Relationships = new SortedSet<Person>();
            
        }

        public override string ToString()
        {
            if(Relationships.Count > 0)
            {
                string toReturn = "";
                foreach (var relationship in Relationships)
                {
                    toReturn += $"{relationship} {relationship.RelationshipWith} \n";
                }

                return toReturn;
            }
            else
            {
                return "Brak związków";
            }
        }

        public void Matching()
        {
            while(Relationships.Count < N)
            {
                //Console.WriteLine("---------\n");
                for (ushort m = 0; m < Men.Count; m++)
                {
                    Person man = Men[m];

                    if (man.IsSingle)
                    {
                        for(ushort p = 0; p < man.Preferences.Count; p++)
                        {
                            Person manPreference = Women.Find(w => w.Name == man.Preferences[p].Name);

                            if (!man.RefusedBy.Contains(manPreference))
                            {
                                //Console.WriteLine($"{man} z {manPreference}: ");

                                if (manPreference.IsSingle)
                                {
                                    man.CreateRelationShip(ref manPreference);
                                    Relationships.Add(man);

                                    //Console.WriteLine("Związali się");

                                    break;
                                }
                                else if(man.IsBetterThanRival(manPreference.RelationshipWith, manPreference))
                                {
                                    Person rival = Men.Find(r => r.Name == manPreference.RelationshipWith.Name);
                                    rival.Deserted(manPreference);
                                    Relationships.Remove(rival);

                                    man.CreateRelationShip(ref manPreference);
                                    Relationships.Add(man);

                                    //Console.WriteLine($"Związali się i zniszczyli życie {rival}");

                                    break;
                                }
                                else
                                {
                                    man.AddRefusedProposal(manPreference);
                                    //Console.WriteLine("Nie zostaną nigdy małżeństwem");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
