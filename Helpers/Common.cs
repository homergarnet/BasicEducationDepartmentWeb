using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace BasicEducationDepartmentWeb.Helpers
{
    public static class Common
    {

        public static string GetCustomMessage(this HttpResponseCustom response)
        {
            string _message = "";
            dynamic result = JsonConvert.DeserializeObject<dynamic>(response.Result);

            if (response.StatusCode == 200)
                return result == null ? "" : result.ToString();

            _message = result == null ? "" : (string)result.Message;

            if (result != null && result.ExceptionMessage != null)
                _message = (string)result.ExceptionMessage;

            return _message;
        }

        /// <summary>
        /// Convert a list of T into a datatable.
        /// </summary>
        /// <typeparam name="T">A class.</typeparam>
        /// <param name="data">A list of T.</param>
        /// <returns>A datatable based on list of T.</returns>
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }
            return table;
        }

        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                        pro.SetValue(obj, dr.IsNull(column.ColumnName) ? null : dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        /// <summary>
        /// Convert a DataTable into a List of T.
        /// </summary>
        /// <typeparam name="T">A class.</typeparam>
        /// <param name="dt">The datatable to convert.</param>
        /// <returns>Returns a list from a datatable. Note that the datacolumn from the datatable must match the properties of the class T.</returns>
        public static List<T> ToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        /// <summary>
        /// Deserialize a JSON string into a list of T.
        /// </summary>
        /// <typeparam name="T">A class.</typeparam>
        /// <param name="json">A JSON string.</param>
        /// <returns>A list of objects of T based on the JSON string. Note that the JSON string properties must match the properties of class T.</returns>
        public static List<T> DeserializeToList<T>(this string json) => JsonConvert.DeserializeObject<List<T>>(json);

        /// <summary>
        /// Deserialize a JSON string into an object of T.
        /// </summary>
        /// <typeparam name="T">A class.</typeparam>
        /// <param name="json">A JSON string.</param>
        /// <returns>An object of T based on the JSON string. Note that the JSON string properties must match the properties of class T.</returns>
        public static T Deserialize<T>(this string json) => JsonConvert.DeserializeObject<T>(json);

        /// <summary>
        /// Serializes a list of T into JSON string.
        /// </summary>
        /// <typeparam name="T">A class.</typeparam>
        /// <param name="list">A list of T.</param>
        /// <returns>A JSON string based on list of T.</returns>
        public static string Serialize<T>(this IList<T> list) => JsonConvert.SerializeObject(list);

        /// <summary>
        /// Serializes an object of T into JSON string.
        /// </summary>
        /// <typeparam name="T">A class.</typeparam>
        /// <param name="data">A an object of T.</param>
        /// <returns>A JSON string based on object of T.</returns>
        public static string Serialize<T>(this T data) => JsonConvert.SerializeObject(data);

        /// <summary>
        /// List of months.
        /// </summary>
        /// <param name="abbrev">If true, abbreviation of the month name is used. Else, full month name.</param>
        /// <returns>A dictionary of months.</returns>
        public static Dictionary<int, string> MonthList(bool abbrev = false)
        {
            var dctMonths = new Dictionary<int, string>();

            for (int i = 1; i <= 12; i++)
            {
                string month = abbrev ? DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i) : DateTimeFormatInfo.CurrentInfo.GetMonthName(i); ;

                dctMonths.Add(i, month);
            }

            return dctMonths;
        }

        /// <summary>
        /// List of years based on given starting year.
        /// </summary>
        /// <param name="startYear">The year to start the count.</param>
        /// <returns>A dictionary of year from the starting year to the latest year.</returns>
        public static Dictionary<int, int> YearList(int startYear)
        {
            var dctYear = new Dictionary<int, int>();
            var dtmYearNow = DateTime.Now.Year;

            for (int i = startYear; i <= dtmYearNow; i++)
            {
                dctYear.Add(i, i);
            }
            return dctYear;
        }

        /// <summary>
        /// Returns DBNull.Value if object is null.
        /// </summary>
        /// <param name="obj">An object</param>
        /// <returns>Null of the object. This is basically a null check for ADO.NET parameters.</returns>
        public static object DBNullIfNull(this object obj) => obj ?? DBNull.Value;

        /// <summary>
        /// Encodes a string to base 64 depending on the declared encoding type.
        /// </summary>
        /// <param name="txt">String to encode.</param>
        /// <param name="encoding">Encoding type. If null, uses UTF-8 as default.</param>
        /// <returns></returns>
        public static string ToBase64Encode(string txt, Encoding encoding = null)
        {
            Encoding _encoding = encoding ?? Encoding.UTF8;

            byte[] txtInByte = new byte[txt.Length];
            txtInByte = _encoding.GetBytes(txt);

            return Convert.ToBase64String(txtInByte);
        }

    }
}