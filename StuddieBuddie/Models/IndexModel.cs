using StuddieBuddie.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StuddieBuddie.Models
{
    public class IndexModel
    {
        public Dictionary<string, Test> Tests = new Dictionary<string, Test>();

        public IndexModel()
        {
            foreach(var guid in DataStore.ListTests())
            {
                Tests.Add(guid, DataStore.GetTest(guid));
            }
        }
    }
}