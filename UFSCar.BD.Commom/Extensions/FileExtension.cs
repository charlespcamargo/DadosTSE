using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UFSCar.BD.Commom.Extensions
{
    public static class FileExtension
    {
        /// <summary>
        /// Obtem os bytes de um HttpPostedFile
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Byte[] GetBytes(this HttpPostedFile file)
        {
            byte[] data = new Byte[file.ContentLength];
            file.InputStream.Read(data, 0, file.ContentLength);
            return data;
        }
    }
}
