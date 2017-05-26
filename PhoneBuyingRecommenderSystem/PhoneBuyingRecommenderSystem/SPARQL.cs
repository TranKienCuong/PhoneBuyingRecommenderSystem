using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;

namespace PhoneBuyingRecommenderSystem
{
    /// <summary>
    /// Static class for querying ontology with SPARQL language
    /// </summary>
    static class SPARQL
    {
        static TripleStore store;
        static ISparqlQueryProcessor processor;
        static SparqlQueryParser sparqlparser;

        /// <summary>
        /// Loads data from ontology file and initialize some intances for querying
        /// </summary>
        public static void Start()
        {
            store = new TripleStore();
            store.LoadFromFile("PhoneOntology.owl");

            InMemoryDataset ds = new InMemoryDataset(store);
            processor = new LeviathanQueryProcessor(ds);
            sparqlparser = new SparqlQueryParser();
        }

        /// <summary>
        /// Does the SPARQL query then returns the result set
        /// </summary>
        public static SparqlResultSet DoQuery(string str)
        {
            SparqlQuery query = sparqlparser.ParseFromString(str);
            return (SparqlResultSet)processor.ProcessQuery(query);
        }
    }
}
