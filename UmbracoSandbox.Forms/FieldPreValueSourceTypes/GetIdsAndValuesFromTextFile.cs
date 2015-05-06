namespace UmbracoSandbox.Forms.FieldPreValueSourceTypes
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
            Id = new Guid("ddb4b99f-8874-4eea-9ba1-305a35cd6993");
            Name = "Get Ids and values from textfile";
            Description = "Upload textfile that contains the prevalues (Id and value separated by tab, prevalues seperated by linebreak)";
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
                var sort = 0;
                var tr = new StreamReader(HttpContext.Current.Server.MapPath(TextFile));
                var values = tr.ReadToEnd().Split('\n');
                foreach (var value in values)
                {
                    if (string.IsNullOrEmpty(value.Trim()))
                    {
                        continue;
                    }

                    var parts = value.Split('\t');
                    var pv = new PreValue
                    {
                        Id = parts[0],
                        Value = parts[1],
                        SortOrder = sort
                    };
                    result.Add(pv);
                    sort++;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error<GetIdsAndValuesFromTextFile>("Get Ids and values from textfile provider: " + ex, ex);
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
