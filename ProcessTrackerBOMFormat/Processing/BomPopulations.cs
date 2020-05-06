using Formatter.Configuration;
using Formatter.Utility;
using System.Collections.ObjectModel;
using System.Data;

namespace Formatter.Processing
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomPopulations'
    public class BomPopulations
    {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomPopulations'

        private Collection<ConfigurationElementColumn> _columnsWithPopulations = new Collection<ConfigurationElementColumn>();
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomPopulations.PopulatedDataTable'
        public DataTable PopulatedDataTable { get { return _populatedTable; } }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomPopulations.PopulatedDataTable'

        private DataTable _populatedTable = null;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'BomPopulations.BomPopulations(BomOutput, ConfigurationCollectionColumns)'
        public BomPopulations(BomOutput loadedBom, ConfigurationCollectionColumns bomColumns)
        {
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'BomPopulations.BomPopulations(BomOutput, ConfigurationCollectionColumns)'
            _populatedTable = loadedBom.BomDataTable.Data;

            foreach (ConfigurationElementColumn column in bomColumns)
            {
                if (column.PopulationCollection.Count > 0)
                {
                    _columnsWithPopulations.Add(column);
                }
            }

            PerformPopulations();
        }

        private void PerformPopulations()
        {
            foreach (DataRow row in _populatedTable.Rows)
            {
                foreach (ConfigurationElementColumn column in _columnsWithPopulations)
                {
                    foreach (ConfigurationElementPopulation population in column.PopulationCollection)
                    {
                        if (population.Active && StringEvaluation.eval(population.Condition, row[column.Name].ToString(), population.FindValue))
                        {
                            row[population.ToColumn] = population.SetValue;
                        }
                    }
                }
            }
        }
    }
}
