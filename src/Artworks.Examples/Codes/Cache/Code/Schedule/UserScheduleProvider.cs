using Artworks.Cache.Core;
using Artworks.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Artworks.Examples.Codes.Cache.Code.Schedule
{
    public class UserScheduleProvider : ScheduleProvider
    {
        public UserScheduleProvider(string name)
            : base(name)
        {

        }

        protected override void Execute()
        {
            var dt = UserDAL.GetList();
            string file = CacheFile.UserList;
            CacheFileWriter.Write(file, dt);
        }
    }
}
