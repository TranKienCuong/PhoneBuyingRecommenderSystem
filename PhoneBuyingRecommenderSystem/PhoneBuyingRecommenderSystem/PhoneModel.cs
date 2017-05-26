using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using VDS.RDF.Query;

namespace PhoneBuyingRecommenderSystem
{
    /// <summary>
    /// Represents a phone model with its properties
    /// </summary>
    class PhoneModel
    {
        public string ModelKey { get { return Get("model"); } }
        public string Name { get { return Get("name"); } }
        public string Price { get { return string.Format(new CultureInfo("vi-VN"), "{0:N0}", int.Parse(Get("price"))) + " VND"; } }
        public string BatteryCapacity { get { return Get("battery") + " mAh"; } }
        public string ScreenSize { get { return Get("screensize") + "\""; } }
        public string Resolution { get { return Get("hres") + " x " + Get("wres"); } }
        public string Color { get { return Get("color"); } }
        public string OSName { get { return Get("osName"); } }
        public string OSVersion { get { return Get("osVersion"); } }
        public string OS { get { return GetOS(); } }
        public string CPUName { get { return Get("cpuName"); } }
        public string CPUBit { get { return Get("bit"); } }
        public string CPUCores { get { return Get("core"); } }
        public string CPU { get { return GetCPU(); } }
        public string RAMCapacity { get { return Get("ram"); } }
        public string StorageCapacity { get { return Get("storage"); } }
        public string FrontCamera { get { return Get("fcam"); } }
        public string RearCamera { get { return Get("rcam"); } }
        public string Link { get { return Get("link"); } }

        private SparqlResult info;

        /// <summary>
        /// Gets all phone models and their names
        /// </summary>
        /// <returns>Dictionary with key is model and value is name</returns>
        public static Dictionary<string, string> GetAllModels()
        {
            SparqlResultSet models = SPARQL.DoQuery(@"
                PREFIX ont: <http://www.co-ode.org/ontologies/ont.owl#>
                SELECT ?model ?name WHERE 
                { 
                    ?s a ont:PhoneModel. BIND (STRAFTER(STR(?s), STR(ont:)) AS ?model).
                    ?s ont:hasName ?name.                    
                }");

            Dictionary<string, string> D = new Dictionary<string, string>();
            foreach (SparqlResult model in models)
                D.Add(model.Value("model").ToString(), model.Value("name").ToString());
            return D;
        }

        public PhoneModel() { }

        /// <summary>
        /// Constructs a phone model with all properties gotten by querying the model key
        /// </summary>
        public PhoneModel(string modelKey)
        {
            SparqlResultSet results = SPARQL.DoQuery(@"
                PREFIX ont: <http://www.co-ode.org/ontologies/ont.owl#>
                SELECT * WHERE 
                { 
                    ?s a ont:PhoneModel. BIND (STRAFTER(STR(?s), STR(ont:)) AS ?model).
                    ?s ont:hasName ?name.
                    ?s ont:hasPrice ?t1. BIND (STR(?t1) AS ?price).
                    ?s ont:hasBatteryCapacity ?t2. BIND (STR(?t2) AS ?battery).
                    ?s ont:hasScreenSize ?t3. BIND (STR(?t3) AS ?screensize).
                    ?s ont:hasHeightOfRes ?t4. BIND (STR(?t4) AS ?hres).
                    ?s ont:hasWidthOfRes ?t5. BIND (STR(?t5) AS ?wres).
                    ?s ont:hasColor ?color.
                    ?s ont:hasLink ?link.
                    OPTIONAL { 
                        ?s ont:hasOS ?os.
                        ?os ont:hasName ?osName.
                        ?os ont:hasVersion ?t6. BIND (STR(?t6) AS ?osVersion). }
                    OPTIONAL { 
                        ?s ont:hasCPU ?cpu.
                        ?cpu ont:hasName ?cpuName.
                        ?cpu ont:hasBitArchitecture ?t7. BIND (STR(?t7) AS ?bit).
                        ?cpu ont:hasCores ?t8. BIND (STR(?t8) AS ?core). }
                    OPTIONAL { 
                        ?s ont:hasRAMCapacity ?t9. BIND (STR(?t9) AS ?ram). }
                    OPTIONAL {
                        ?s ont:hasInternalStorageCapacity ?t10. BIND (STR(?t10) AS ?storage). }
                    OPTIONAL {
                        ?s ont:hasFrontMegapixel ?t11. BIND (STR(?t11) AS ?fcam). }
                    OPTIONAL {
                        ?s ont:hasRearMegapixel ?t12. BIND (STR(?t12) AS ?rcam). }
                    FILTER (?model = '" + modelKey + @"').
                }
                LIMIT 1");

            if (results.Count != 0)
                info = results[0];
        }

        private string Get(string property)
        {
            if (info.Variables.Contains(property))
            {
                if (property == "ram" || property == "storage")
                    return info.Value(property).ToString() + " GB";
                if (property == "fcam" || property == "rcam")
                    return info.Value(property).ToString() + " MP";
                return info.Value(property).ToString();
            }
            return "Không";
        }

        private string GetCPU()
        {
            if (info.Variables.Contains("cpu"))
                return CPUName + ' ' + CPUCores + " nhân " + CPUBit + " bit";
            return "Không";
        }

        private string GetOS()
        {
            if (info.Variables.Contains("os"))
                return OSName + ' ' + OSVersion;
            return "Không";
        }
    }
}
