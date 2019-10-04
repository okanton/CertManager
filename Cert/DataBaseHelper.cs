using System;
using System.Linq;

namespace Сerts
{
    class DataBaseHelper
    {
        public static Byte ImageType()
        {
            using (var model = new CertsUsersEntities())
            {
                var list = model.CertsUsers.ToList();
                byte imgType = 0;
                foreach (var item in list)
                {
                    if (Environment.UserName == item.login.Trim())
                    {
                        imgType = item.imageType;
                    }
                }
                return imgType;
            }
        }
    }
}
