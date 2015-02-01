namespace ProstateCancerUK.Contour.Workflows
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web;
    using Umbraco.Forms.Core;
    using Umbraco.Forms.Core.Attributes;
    using Umbraco.Forms.Data;

    public class GetIdsAndValuesFromTextFile : FieldPreValueSourceType
    {
        public GetIdsAndValuesFromTextFile()
        {
            this.Id = new Guid("ddb4b99f-8874-4eea-9ba1-305a35cd6993");
            this.Name = "Get Ids and values from textfile";
            this.Description = "Upload textfile that contains the prevalues (Id and value separated by tab, prevalues seperated by linebreak)";
        }

        [Setting("TextFile",
            description = "File containing the prevalues (Id and value separated by tab, prevalues seperated by linebreak)",
            view = "file")]
        public string TextFile { get; set; }

        public override List<PreValue> GetPreValues(Field field, Form form)
        {
            var result = new List<PreValue>();
            try
            {
                int sort = 0;
                var tr = new StreamReader(HttpContext.Current.Server.MapPath(TextFile));
                var values = tr.ReadToEnd().Split('\n');
                foreach (string value in values)
                {
                    if (!string.IsNullOrEmpty(value.Trim()))
                    {
                        var parts = value.Split('\t');
                        var pv = new PreValue();
                        pv.Id = parts[0];
                        pv.Value = parts[1];
                        pv.SortOrder = sort;
                        result.Add(pv);
                        sort++;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<GetIdsAndValuesFromTextFile>("Get Ids and values from textfile provider: " + ex.ToString(), ex);
            }

            return result;
        }

        public override List<Exception> ValidateSettings()
        {
            var ex = new List<Exception>();
            if (string.IsNullOrEmpty(TextFile))
            {
                ex.Add(new Exception("'TextFile' setting not filled out"));
            }

            return ex;
        }
    }
}
