using System;
using System.Collections.Generic;
using System.Text;

namespace FileMap.Test
{
    public abstract class BaseFileMapTest
    {
        protected const string _inputFile = "TestFixtures/input.txt";
        protected const string _locationsFile = "TestFixtures/locs.txt";

        protected string OutputFile() => $"TestFixtures/output_{Guid.NewGuid().ToString()}.out";
    }
}
