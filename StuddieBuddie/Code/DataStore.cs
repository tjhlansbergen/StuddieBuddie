using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace StuddieBuddie.Code
{
    public static class DataStore
    {
        private static readonly string _store = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/");

        public static Test StoreTest(HttpPostedFileBase file)
        {
            //filename
            Guid id = Guid.NewGuid();

            //Test
            Test test;

            //check file by trying to deserialize
            try
            {
                test = JsonConvert.DeserializeObject<Test>(new StreamReader(file.InputStream).ReadToEnd());
            }
            catch (Exception)
            {
                return null;
            }

            //save file
            try
            {
                file.SaveAs(Path.Combine(_store, id.ToString()));
            }
            catch (Exception)
            {
                return null;
            }

            //and return
            return test;
        }

        public static bool DeleteTest(string GUID)
        {
            //delete file
            try
            {
                File.Delete(Path.Combine(_store, GUID));
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public static bool TestExists(string GUID)
        {
            return File.Exists(Path.Combine(_store, GUID));
        }

        //returns tests as list of guids
        public static List<string> ListTests()
        {
            List<string> result = new List<string>();

            foreach(var filename in Directory.EnumerateFiles(_store))
            {
                result.Add(Path.GetFileName(filename));
            }

            return result;
        }

        public static Test GetTest(string guid)
        {
            //object to be created
            Test test;

            //build path
            string path = Path.Combine(_store, guid);

            //deserialize existing datastore if we have one, otherwise create empty object
            try
            {
                test = JsonConvert.DeserializeObject<Test>(File.ReadAllText(path));
            }
            catch (Exception)
            {
                test = null;
            }

            return test;
        }
    }
}