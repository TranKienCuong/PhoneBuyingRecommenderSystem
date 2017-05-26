using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF.Query;

namespace PhoneBuyingRecommenderSystem
{
    /// <summary>
    /// Contains methods for searching and filtering phones
    /// </summary>
    static class SearchEngine
    {
        /// <summary>
        /// Searchs phone models by name
        /// </summary>
        /// <returns>Dictionary with key is model and value is name</returns>
        public static Dictionary<string, string> SearchName(string name)
        {
            SparqlResultSet models = SPARQL.DoQuery(@"
                PREFIX ont: <http://www.co-ode.org/ontologies/ont.owl#>
                SELECT ?model ?name WHERE 
                { 
                    ?s a ont:PhoneModel. BIND (STRAFTER(STR(?s), STR(ont:)) AS ?model).
                    ?s ont:hasName ?name.
                    FILTER regex(?name, '" + name + @"', 'i').
                }");

            Dictionary<string, string> D = new Dictionary<string, string>();
            foreach (SparqlResult model in models)
                D.Add(model.Value("model").ToString(), model.Value("name").ToString());
            return D;
        }

        /// <summary>
        /// Searchs phone models by their properties
        /// </summary>
        /// <param name="filterOptions">phone properties to filter</param>
        /// <returns>Dictionary with key is model and value is name</returns>
        public static Dictionary<string, string> SearchProperties(FilterOptions filterOptions)
        {
            string pattern = filterOptions.GetQueryPattern();

            SparqlResultSet models = SPARQL.DoQuery(@"
                PREFIX ont: <http://www.co-ode.org/ontologies/ont.owl#>
                SELECT ?model ?name WHERE 
                { 
                    ?s a ont:PhoneModel. BIND (STRAFTER(STR(?s), STR(ont:)) AS ?model).
                    ?s ont:hasName ?name.
                    " + pattern + @"
                }");

            Dictionary<string, string> D = new Dictionary<string, string>();
            foreach (SparqlResult model in models)
                D.Add(model.Value("model").ToString(), model.Value("name").ToString());
            return D;
        }
    }
}
