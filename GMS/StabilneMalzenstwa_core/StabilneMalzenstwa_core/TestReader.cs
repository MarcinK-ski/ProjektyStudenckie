using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StabilneMalzenstwa_core
{
    class TestReader
    {
        ushort _testCasesCount;
        private List<People> _peopleList;
        const ushort PERSON_NAME_OFFSET = 1;

        public TestReader(string filePath)
        {
            ReadTestFile(filePath);
        }

        public void ReadTestFile(string file)
        {
            using (StreamReader reader = new StreamReader(file))
            {
                _testCasesCount = UInt16.Parse(reader.ReadLine());

                _peopleList = new List<People>(_testCasesCount);

                for (int i = 0; i < _testCasesCount; i++)
                {
                    ushort n = UInt16.Parse(reader.ReadLine());

                    _peopleList.Add(new People(n));

                    for (ushort j = 0; j < n; j++)
                    {
                        _peopleList[i].Men.Add(new Person(j));
                        _peopleList[i].Women.Add(new Person(j));
                    }

                    bool womenNow = true;
                    for (ushort j = 0; j < 2*n; j++)
                    {
                        if (j == n)
                        {
                            womenNow = false;
                        }

                        List<ushort> nameAndPreferences = _ReadWomenOrMenFromFileWithPreferences(reader.ReadLine());
                        ushort name = (ushort)(nameAndPreferences[0] - PERSON_NAME_OFFSET);

                        List<Person> preferences = new List<Person>();
                        for (ushort p = 1; p < nameAndPreferences.Count; p++)  //Starts with one, becouse 0 is name; adding preferences
                        {
                            ushort currentPreferencePersonName = (ushort)(nameAndPreferences[p] - PERSON_NAME_OFFSET);
                            if(womenNow)
                            {
                                preferences.Add(_peopleList[i].Women[currentPreferencePersonName]);
                            }
                            else
                            {
                                preferences.Add(_peopleList[i].Men[currentPreferencePersonName]);
                            }
                        }



                        if (womenNow)
                        {
                            _peopleList[i].Women[name].AddPreferences(preferences);
                        }
                        else
                        {
                            _peopleList[i].Men[name].AddPreferences(preferences);
                        }
                    }

                    _peopleList[i].Matching();
                    Console.Write(_peopleList[i]);
                }
            }
        }

        private List<ushort> _ReadWomenOrMenFromFileWithPreferences(string stringLine)
        {
            List<ushort> person = new List<ushort>();
            string[] splittedStringLine = stringLine.Trim().Split(' ');

            for (int i = 0; i < splittedStringLine.Length; i++)
            {
                person.Add(UInt16.Parse(splittedStringLine[i]));
            }

            return person;
        }
    }
}
