using Formatter.Configuration;
using Formatter.Utility;
using System.Collections.ObjectModel;
using System.Data;

namespace Formatter.Processing {
    public class BomPopulations {

        private Collection<ConfigurationElementColumn> _columnsWithPopulations = new Collection<ConfigurationElementColumn>();
        public DataTable PopulatedDataTable { get { return _populatedTable; } }

        private DataTable _populatedTable = null;

        public BomPopulations(BomOutput loadedBom, ConfigurationElementBom bomConfiguration) {
            _populatedTable = loadedBom.BomDataTable.Data;

            foreach (ConfigurationElementColumn column in bomConfiguration.ColumnCollection) {
                if (column.PopulationCollection.Count > 0) {
                    _columnsWithPopulations.Add(column);
                }
            }

            PerformPopulations();
        }

        private void PerformPopulations() {
            foreach (DataRow row in _populatedTable.Rows) {
                foreach (ConfigurationElementColumn column in _columnsWithPopulations) {
                    foreach (ConfigurationElementPopulation population in column.PopulationCollection) {
                        if (population.Active && StringEvaluation.eval(population.Condition, row[column.Name].ToString(), population.FindValue)) {
                            row[population.ToColumn] = population.SetValue;
                        }
                    }
                }
            }
        }
    }
}