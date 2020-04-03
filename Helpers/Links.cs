using System;
using System.IO;
using Covid19Texas.Models;
using Newtonsoft.Json;

namespace Covid19Texas.Helpers
{
    public class Links
    {

        public static LinkListModel ReadLinks()
        {
            var JSON = System.IO.File.ReadAllText(Path.Combine(Constants.RootPath + Path.DirectorySeparatorChar + Constants.LinksFile));
            return JsonConvert.DeserializeObject<LinkListModel>(JSON);
        }

    }
}
