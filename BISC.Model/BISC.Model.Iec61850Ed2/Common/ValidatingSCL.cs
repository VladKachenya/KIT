using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using BISC.Model.Iec61850Ed2.Properties;

namespace BISC.Model.Iec61850Ed2.Common
{
    public static class SclSchemeValidator
    {
        public static List<string> ListErrors { get; private set; }

        /// <summary>
        /// Constructior: donde se realiza la inicialización de las variables.
        /// </summary>
        static SclSchemeValidator()
        {
            ListErrors = new List<string>();
        }

        /// <summary>
        /// Валидация XML-файла по XML-схеме
        /// </summary>
        /// <param name="sclFileName">
        /// Путь к XML-файлу, который надо провалидировать
        /// </param>
        /// <returns>
        /// Возвращает список ошибок во время валидации
        /// </returns>
        public static List<string> ValidateSclFile(string sclFileName)
        {
            ListErrors.Clear();
            XmlSchema schema = CreateSclScheme();
            if (schema != null)
            {					
                ValidateSclAgainstXsd(sclFileName, schema);
            }
            return ListErrors;
        }
        
        /// <summary>
        /// Создает XmlSchema. Если схема имеет ошибки, то они вносятся в список и возвращается null
        /// </summary>
        private static XmlSchema CreateSclScheme()
        {
            try
            {
                XmlSchema baseSimpleTypesSchema = XmlSchema.Read(XmlReader.Create(new StringReader(Resources.scl_basesimpletypes)), SchemaValidationHandler);
                XmlSchema enumsSchema = XmlSchema.Read(XmlReader.Create(new StringReader(Resources.scl_enums)), SchemaValidationHandler);
                enumsSchema.Includes.Add(new XmlSchemaInclude { Schema = baseSimpleTypesSchema });
                XmlSchema baseTypesSchema = XmlSchema.Read(XmlReader.Create(new StringReader(Resources.scl_basetypes)), SchemaValidationHandler);
                baseTypesSchema.Includes.Add(new XmlSchemaInclude { Schema = enumsSchema });

                XmlSchema substationSchema = XmlSchema.Read(XmlReader.Create(new StringReader(Resources.SCL_Substation)), SchemaValidationHandler);
                substationSchema.Includes.Add(new XmlSchemaInclude { Schema = baseTypesSchema });
                XmlSchema iedSchema = XmlSchema.Read(XmlReader.Create(new StringReader(Resources.SCL_IED)), SchemaValidationHandler);
                iedSchema.Includes.Add(new XmlSchemaInclude { Schema = baseTypesSchema });
                XmlSchema communicationSchema = XmlSchema.Read(XmlReader.Create(new StringReader(Resources.SCL_Communication)), SchemaValidationHandler);
                communicationSchema.Includes.Add(new XmlSchemaInclude { Schema = baseTypesSchema });
                XmlSchema dataTemplatesSchema = XmlSchema.Read(XmlReader.Create(new StringReader(Resources.SCL_DataTypeTemplates)), SchemaValidationHandler);
                dataTemplatesSchema.Includes.Add(new XmlSchemaInclude { Schema = baseTypesSchema });

                XmlSchema sclScheme = XmlSchema.Read(XmlReader.Create(new StringReader(Resources.scl)), SchemaValidationHandler);
                sclScheme.Includes.Add(new XmlSchemaInclude { Schema = substationSchema });
                sclScheme.Includes.Add(new XmlSchemaInclude { Schema = iedSchema });
                sclScheme.Includes.Add(new XmlSchemaInclude { Schema = communicationSchema });
                sclScheme.Includes.Add(new XmlSchemaInclude { Schema = dataTemplatesSchema });
                return sclScheme;
            }
            catch (Exception e)
            {
                ListErrors.Add(e.Message);
                return null;
            }
        }

        /// <summary>
        /// Валидация файла по схеме  		
        /// </summary>
        /// <param name="sclDoc">
        /// Путь к файлу XML
        /// </param>
        /// <param name="sclSchema">
        /// XSD-файл схемы
        /// </param>
        private static void ValidateSclAgainstXsd(string sclDoc, XmlSchema sclSchema)
        {
            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(sclSchema);
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Compile();
                settings.ValidationEventHandler += SettingsValidationHandler;
                XmlReader reader = XmlReader.Create(sclDoc, settings);
                while (reader.Read())
                {
                }
            }
            catch (Exception e)
            {
                ListErrors.Add(e.Message);
            }
        }

        /// <summary>
        /// This event handle the errors founded during the validation process.
        /// </summary>
        /// <param name="sender">
        /// Name of the object.
        /// </param>
        /// <param name="e">
        /// This class contains no event data; it is used by events that do not pass state information to an event 
        /// handler when an event is raised. If the event handler requires state information, the application must 
        /// derive a class from this class to hold the data.
        /// </param>
        private static void SettingsValidationHandler(object sender, ValidationEventArgs e)
        {
            int lineNumber = Convert.ToInt32(sender.GetType().GetProperty("LineNumber").GetValue(sender));
            int linePosition = Convert.ToInt32(sender.GetType().GetProperty("LinePosition").GetValue(sender));
            string pattern = e.Severity == XmlSeverityType.Warning
                ? "WARNING: {0}\nСтрока: {1}, Позиция: {2}"
                : "ERROR: {0}\nСтрока: {1}, Позиция: {2}";
            ListErrors.Add(string.Format(pattern, e.Message, lineNumber, linePosition));
        }

        /// <summary>
        /// This method allows to handle the errors that occurs during a validation of SCL file.
        /// </summary>
        /// <param name="sender">
        /// Name of the object.
        /// </param>
        /// <param name="e">
        /// This class contains no event data; it is used by events that do not pass state information to an event 
        /// handler when an event is raised. If the event handler requires state information, the application must 
        /// derive a class from this class to hold the data.
        /// </param>
        private static void SchemaValidationHandler(object sender, ValidationEventArgs e)
        {
            ListErrors.Add(e.Message);
        }
    }
}