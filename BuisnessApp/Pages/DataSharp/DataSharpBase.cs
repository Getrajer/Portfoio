using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessApp.Pages.DataSharp
{
    public class DataSharpBase : ComponentBase
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        /// <summary>
        /// Single column in the column chart
        /// </summary>
        public class Column
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double DataPoint { get; set; }

            public Column() { }
        }

        /// <summary>
        /// This class will work as a blueprint for coulumn graphs
        /// </summary>
        public class ColumnGraph
        {
            public int Id { get; set; }

            /// <summary>
            /// Name of this chart
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Name of dataset from which this column chart data is comming from
            /// </summary>
            public string DataSetName { get; set; }

            /// <summary>
            /// Name of data label which is connected to the data set it is comming from
            /// </summary>
            public string DataVariableName { get; set; }

            public List<Column> Columns = new List<Column>();
            public ColumnGraph() { }
        }

        /// <summary>
        /// This class will be used for toggleing between charts
        /// </summary>
        public class ColumnChartFinder
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ColumnChartFinder() { }
        }

        /// <summary>
        /// This class will store parameters for dataset
        /// Parameter Id = Dataset Id
        /// </summary>
        public class Parameter
        {
            public int Id { get; set; }
            public List<string> Parameters = new List<string>();
            public Parameter()
            {

            }
        }

        /// <summary>
        /// Single label for the data point
        /// </summary>
        public class LabelDataset
        {
            public int Id { get; set; }
            public string Label { get; set; }

            public bool IsDouble { get; set; }
            public bool IsString { get; set; }

            public LabelDataset() { }
        }

        /// <summary>
        /// Labels collection for dataset
        /// </summary>
        public class Labels
        {
            public int Id { get; set; }
            public List<LabelDataset> LableCollection = new List<LabelDataset>();

            public Labels()
            {
                LableCollection = new List<LabelDataset>();
            }
        }

        /// <summary>
        /// This class will be used when editing variables in dataset
        /// </summary>
        public class EditingVariable
        {
            public int Id { get; set; }
            public string Label { get; set; }
            public string StringValue { get; set; }
            public double DoubleValue { get; set; }
            public bool IsDouble { get; set; }
            public bool IsString { get; set; }
            public EditingVariable() { }
        }

        /// <summary>
        /// This class represends data object in dataset
        /// </summary>
        public class DatasetRowObject
        {
            public int Id { get; set; }
            public string StringData { get; set; }
            public double DoubleData { get; set; }
            public bool IsDouble { get; set; }
            public bool IsString { get; set; }
            public DatasetRowObject() { }
        }

        /// <summary>
        /// This class represents row in dataset
        /// </summary>
        public class DatasetRow
        {
            public int Id { get; set; }
            public List<DatasetRowObject> Row = new List<DatasetRowObject>();
            public DatasetRow() { }
        }


        /// <summary>
        /// This class represends stored dataset
        /// Dataset Id = Parameter Id
        /// </summary>
        public class DataSet
        {
            public int Id { get; set; }
            public string Name { get; set; }

            public int ArrayColumnLength { get; set; }

            public int ArrayRowLenght { get; set; }

            public List<DatasetRow> Data = new List<DatasetRow>();

            public DataSet()
            {

            }

            public DataSet(string name, int array_row_lenght, int array_column_lenght)
            {
                this.Name = name;
                this.ArrayColumnLength = array_column_lenght;
                this.ArrayRowLenght = array_row_lenght;

            }
        }

        /// <summary>
        /// This class is blueprrint for output console
        /// </summary>
        public class ConsoleOutObj
        {
            public string Out { get; set; }
            public bool IfError { get; set; }
            public bool IfInfo { get; set; }
            public bool Normal { get; set; }
            public ConsoleOutObj() { }
        }

        /// <summary>
        /// This class stores output object
        /// </summary>
        public class ConsoleOutput
        {
            public int Id { get; set; }

            public List<ConsoleOutObj> OutputList = new List<ConsoleOutObj>();

            public bool If_Error { get; set; }

            public ConsoleOutput() { }
        }

        /// <summary>
        /// This object is used when calculating sums of data
        /// </summary>
        public class DataCollective
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public double Data { get; set; }
            public int Occurence { get; set; }

            public DataCollective() { }
        }

        /// <summary>
        /// This class will check if dataset was sorted by certain label and if it did sorting it again will sort it other way around
        /// </summary>
        public class DataSetSortChecker
        {
            public int Id { get; set; }
            public string LabelName { get; set; }

            public DataSetSortChecker() { }
        }

        /// <summary>
        /// This dataset shows dataset in window
        /// </summary>
        protected DataSet showcase_dataset = new DataSet();

        /// <summary>
        /// This list will store datasets
        /// </summary>
        protected List<DataSet> Data_Sets_List = new List<DataSet>();

        /// <summary>
        /// This list will store output console history 
        /// </summary>
        protected List<ConsoleOutput> console_output_history = new List<ConsoleOutput>();

        /// <summary>
        /// This list will store parameters for dataset
        /// </summary>
        protected List<Parameter> parameters_list = new List<Parameter>();

        /// <summary>
        /// This list will store column graphs
        /// </summary>
        protected List<ColumnGraph> column_graphs = new List<ColumnGraph>();

        /// <summary>
        /// This list will store labels for datasets
        /// </summary>
        protected List<Labels> labels_datasets = new List<Labels>();

        /// <summary>
        /// This variable works as file path to dataset on your device
        /// </summary>
        protected string FilePath;

        #region IJSRuntime functions

        protected async Task CreateGraphJS(ColumnGraph g)
        {
            var columns = g.Columns;
            await JSRuntime.InvokeVoidAsync("GenerateColumnChart", columns);
        }
        #endregion

        #region Console Interpretator

        /// <summary>
        /// This variable takes input from the user it is binded to textarea in component
        /// </summary>
        protected string consoleInput = "";

        /// <summary>
        /// It displays error next to console
        /// </summary>
        protected string error_console = "";

        /// <summary>
        /// This function will interpert console input.
        /// Thanks to interpretation it will execute some functions
        /// </summary>
        public void InterpretConsole()
        {
            error_console = "";

            if (consoleInput == "")
            {
                error_console = "There in nothing in console!";
            }
            else
            {
                if (consoleInput.Length >= 4)
                {
                    //Get code snipet
                    StringBuilder code_c = new StringBuilder();
                    for (int i = 0; i < 4; i++)
                    {
                        code_c.Append(consoleInput[i]);
                    }
                    string code_controller = code_c.ToString();

                    switch (code_controller)
                    {
                        //Creation of dataset from csv file
                        case "gets":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                if (console_in_breakdown.Length == 3)
                                {
                                    string name = "";

                                    DataSet new_dataset = new DataSet();
                                    if (console_in_breakdown[1].Length != 0)
                                    {
                                        name = console_in_breakdown[1];
                                    }
                                    else
                                    {
                                        ConsoleInHistory(consoleInput);

                                        ConsoleError("Wrong syntax for name");

                                        consoleInput = "";
                                        break;

                                    }

                                    if (console_in_breakdown[2].Length > 1 && console_in_breakdown[2] != "")
                                    {
                                        FilePath = console_in_breakdown[2];

                                        if (File.Exists(FilePath))
                                        {
                                            ConsoleInHistory(consoleInput);
                                            PopulateDataset(console_in_breakdown[1]);
                                            consoleInput = "";
                                        }
                                        else
                                        {
                                            ConsoleInHistory(consoleInput);

                                            ConsoleError("File does not exists");

                                            consoleInput = "";
                                            break;

                                        }
                                    }
                                    else
                                    {
                                        ConsoleInHistory(consoleInput);

                                        ConsoleError("Wrong syntax for path");

                                        consoleInput = "";
                                        break;
                                    }
                                }
                                else
                                {
                                    ConsoleError("Wrong syntax for creation");
                                    consoleInput = "";
                                    break;
                                }

                                break;
                            }

                        //Creation of column chart from dataset
                        case "colu":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                int dataset_id = 0;

                                string dataset_name = "";
                                string parameter_name = "";
                                string parameter_value = "";
                                string column_chart_name = "";

                                int parameters_name_id = 0;
                                int parameter_id = 0;

                                bool parameter_name_found = false;
                                bool dataset_found = false;
                                bool parameter_value_found = false;

                                if (console_in_breakdown.Length == 6)
                                {
                                    //Check for name of dataset
                                    if (console_in_breakdown[2] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong name");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {
                                        column_chart_name = console_in_breakdown[2];
                                    }

                                    //Check for the dataset name
                                    if (console_in_breakdown[3] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong dataset name");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {
                                        for (int i = 0; i < Data_Sets_List.Count; i++)
                                        {
                                            if (Data_Sets_List[i].Name == console_in_breakdown[3])
                                            {
                                                dataset_name = console_in_breakdown[3];
                                                dataset_id = i;
                                                dataset_found = true;
                                                dataset_name = console_in_breakdown[3];
                                            }
                                        }
                                    }

                                    if (dataset_found == false)
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Not found dataset");
                                        consoleInput = "";
                                        break;
                                    }

                                    //Check for the name of description for the columns
                                    if (console_in_breakdown[4] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong name for data description for columns");
                                        ConsoleError("If you do not have valid name just write(#numeric): colu create |name| |dataset name| #numeric |vlaue parameter| /and it will name columns from 0 to n");
                                        consoleInput = "";
                                        break;
                                    }

                                    //If "#numeric" count columns from 0 - n
                                    if (console_in_breakdown[4] == "#numeric")
                                    {
                                        parameter_name = console_in_breakdown[4];
                                        parameter_name_found = true;
                                    }//Or look for the list of names
                                    else
                                    {
                                        for (int i = 0; i < parameters_list[dataset_id].Parameters.Count; i++)
                                        {
                                            if (parameters_list[dataset_id].Parameters[i] == console_in_breakdown[4])
                                            {
                                                parameter_name = console_in_breakdown[4];
                                                parameters_name_id = i;
                                                parameter_name_found = true;
                                            }
                                        }
                                    }

                                    if (parameter_name_found == false)
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Not found the name for data description for columns");
                                        ConsoleError("If you do not have valid name just write(#numeric): colu create |name| |dataset name| #numeric |vlaue parameter| /and it will name columns from 0 to n");
                                        consoleInput = "";
                                        break;
                                    }

                                    //Check for the values of the columns
                                    if (console_in_breakdown[5] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong name for data values");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {
                                        for (int i = 0; i < parameters_list[dataset_id].Parameters.Count; i++)
                                        {
                                            if (parameters_list[dataset_id].Parameters[i] == console_in_breakdown[5])
                                            {
                                                parameter_value = console_in_breakdown[5];
                                                parameter_value_found = true;
                                            }
                                        }
                                    }

                                    if (parameter_value_found == false)
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Could not find value of the parameter for the column");
                                        break;
                                    }

                                    //If evrything is correct then:

                                    ConsoleInHistory(consoleInput);
                                    //Create datapoint of new columnchart and ad it do List of column charts
                                    CreateColumnGraph(parameter_value, parameter_name, parameters_name_id, dataset_name, column_chart_name);

                                    //Create column chart
                                    CreateGraphJS(column_graphs[column_graphs.Count - 1]);

                                    //Change current column chart
                                    ColumnChartFinder c = new ColumnChartFinder();
                                    c.Id = column_graphs.Count - 1;
                                    c.Name = column_chart_name;
                                    current_column_chart = c;

                                    consoleInput = "";
                                    break;
                                }
                                else
                                {
                                    ConsoleInHistory(consoleInput);
                                    ConsoleError("Wrong Syntax");
                                    consoleInput = "";
                                    break;
                                }
                            }

                        //Sorting charts
                        case "sort":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                bool chart_name_found = false;
                                string chart_name = "";

                                if (console_in_breakdown.Length == 4)
                                {
                                    //Check for name of dataset
                                    if (console_in_breakdown[1] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong name");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {
                                        for (int i = 0; i < column_graphs.Count; i++)
                                        {
                                            if (column_graphs[i].Name == console_in_breakdown[1])
                                            {
                                                chart_name = console_in_breakdown[1];
                                                chart_name_found = true;
                                            }
                                        }

                                        if (chart_name_found == false)
                                        {
                                            ConsoleInHistory(consoleInput);
                                            ConsoleError($"Could not find the chart by this name: {console_in_breakdown[1]}");
                                            consoleInput = "";
                                            break;
                                        }
                                    }

                                    if (console_in_breakdown[2] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong syntax");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {
                                        if (console_in_breakdown[2] == "min")
                                        {
                                            if (console_in_breakdown[3] == "max")
                                            {
                                                SortColumnMinMax(chart_name);

                                                ConsoleInHistory(consoleInput);
                                                ConsoleInfo($"Sorting {chart_name} from min to max");
                                                consoleInput = "";
                                                break;
                                            }
                                            else
                                            {
                                                ConsoleInHistory(consoleInput);
                                                ConsoleError("Wrong syntax");
                                                consoleInput = "";
                                                break;
                                            }
                                        }
                                        else if (console_in_breakdown[2] == "max")
                                        {
                                            if (console_in_breakdown[3] == "min")
                                            {
                                                SortColumnMaxMin(chart_name);

                                                ConsoleInHistory(consoleInput);
                                                ConsoleInfo($"Sorting {chart_name} from max to min");
                                                consoleInput = "";
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            ConsoleInHistory(consoleInput);
                                            ConsoleError("Wrong syntax");
                                            consoleInput = "";
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    ConsoleInHistory(consoleInput);
                                    ConsoleError("Wrong syntax");
                                    consoleInput = "";
                                    break;
                                }

                                break;
                            }

                        //Find median for chart
                        case "medi":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                if (console_in_breakdown.Length == 2)
                                {
                                    if (console_in_breakdown[1] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong syntax");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {
                                        if (column_graphs.Count > 0)
                                        {
                                            for (int i = 0; i < column_graphs.Count; i++)
                                            {
                                                if (column_graphs[i].Name == console_in_breakdown[1])
                                                {
                                                    ConsoleInHistory(consoleInput);

                                                    //Find median
                                                    CalcMedianColumnChart(i);

                                                    consoleInput = "";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ConsoleInHistory(consoleInput);
                                    ConsoleError("Wrong syntax");
                                    consoleInput = "";
                                    break;
                                }

                                break;
                            }

                        //Find mean for chart
                        case "mean":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                if (console_in_breakdown.Length == 2)
                                {
                                    if (console_in_breakdown[1] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong syntax");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {
                                        if (column_graphs.Count > 0)
                                        {
                                            for (int i = 0; i < column_graphs.Count; i++)
                                            {
                                                if (column_graphs[i].Name == console_in_breakdown[1])
                                                {
                                                    ConsoleInHistory(consoleInput);
                                                    CalcMeanColumnChart(i);
                                                    consoleInput = "";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ConsoleInHistory(consoleInput);
                                    ConsoleError("Wrong syntax");
                                    consoleInput = "";
                                    break;
                                }

                                break;
                            }

                        case "mode":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                if (console_in_breakdown.Length == 3)
                                {
                                    if (console_in_breakdown[1] == "number")
                                    {
                                        if (console_in_breakdown[2] == "")
                                        {
                                            ConsoleInHistory(consoleInput);
                                            ConsoleError("Wrong syntax");
                                            consoleInput = "";
                                            break;
                                        }
                                        else
                                        {
                                            if (column_graphs.Count > 0)
                                            {
                                                bool been_found = false;

                                                for (int i = 0; i < column_graphs.Count; i++)
                                                {
                                                    if (console_in_breakdown[2] == column_graphs[i].Name)
                                                    {
                                                        been_found = true;
                                                        ConsoleInHistory(consoleInput);
                                                        CalcModeColumnChartNumber(i);
                                                        consoleInput = "";
                                                        break;
                                                    }
                                                }

                                                if (been_found == false)
                                                {
                                                    ConsoleInHistory(consoleInput);
                                                    ConsoleError("Did not found the chart");
                                                    consoleInput = "";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else if (console_in_breakdown[1] == "name")
                                    {
                                        if (console_in_breakdown[2] == "")
                                        {
                                            ConsoleInHistory(consoleInput);
                                            ConsoleError("Wrong syntax");
                                            consoleInput = "";
                                            break;
                                        }
                                        else
                                        {
                                            if (column_graphs.Count > 0)
                                            {
                                                bool been_found = false;

                                                for (int i = 0; i < column_graphs.Count; i++)
                                                {
                                                    if (console_in_breakdown[2] == column_graphs[i].Name)
                                                    {
                                                        been_found = true;
                                                        ConsoleInHistory(consoleInput);
                                                        CalcModeColumnChartName(i);
                                                        consoleInput = "";
                                                        break;
                                                    }
                                                }

                                                if (been_found == false)
                                                {
                                                    ConsoleInHistory(consoleInput);
                                                    ConsoleError("Did not found the chart");
                                                    consoleInput = "";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong syntax");
                                        consoleInput = "";
                                        break;
                                    }
                                }
                                else
                                {
                                    ConsoleInHistory(consoleInput);
                                    ConsoleError("Unknown command");
                                    consoleInput = "";
                                    break;
                                }

                                break;
                            }

                        //This will delete various things depending on rest of the commands
                        case "dele":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                #region This section is for dataset delete
                                if (console_in_breakdown.Length == 3)
                                {
                                    if (console_in_breakdown[1] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong syntax");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {   //If user wants to delete dataset 
                                        if (console_in_breakdown[1] == "dataset")
                                        {
                                            if (console_in_breakdown[2] == "")
                                            {
                                                ConsoleInHistory(consoleInput);
                                                ConsoleError("Wrong syntax");
                                                consoleInput = "";
                                                break;
                                            }
                                            else
                                            {
                                                if (Data_Sets_List.Count > 0)
                                                {
                                                    for (int i = 0; i < Data_Sets_List.Count; i++)
                                                    {
                                                        if (Data_Sets_List[i].Name == console_in_breakdown[2])
                                                        {
                                                            ConsoleInHistory(consoleInput);
                                                            DeleteDataset(i);
                                                            consoleInput = "";
                                                            break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    ConsoleInHistory(consoleInput);
                                                    ConsoleError("There is no datasets to delete!");
                                                    consoleInput = "";
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region This section is for deleting entire chart
                                if (console_in_breakdown.Length == 4)
                                {
                                    if (console_in_breakdown[1] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong syntax");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {
                                        if (console_in_breakdown[1] == "column")
                                        {
                                            if (console_in_breakdown[2] == "")
                                            {
                                                ConsoleInHistory(consoleInput);
                                                ConsoleError("Wrong syntax");
                                                consoleInput = "";
                                                break;
                                            }
                                            else
                                            {
                                                if (console_in_breakdown[2] == "chart")
                                                {
                                                    if (console_in_breakdown[3] == "")
                                                    {
                                                        ConsoleInHistory(consoleInput);
                                                        ConsoleError("Wrong syntax");
                                                        consoleInput = "";
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        if (column_graphs.Count > 0)
                                                        {
                                                            for (int i = 0; i < column_graphs.Count; i++)
                                                            {
                                                                if (column_graphs[i].Name == console_in_breakdown[3])
                                                                {
                                                                    ConsoleInHistory(consoleInput);
                                                                    DeleteColumnChart(i);
                                                                    consoleInput = "";
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            ConsoleInHistory(consoleInput);
                                                            ConsoleError("There is no columns to delete");
                                                            consoleInput = "";
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                break;
                            }

                        //This will remove things from data Lists / Objects ECT.. depending on rest of the commands
                        case "remo":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                if (console_in_breakdown[1] == "column" && console_in_breakdown[2] == "chart")
                                {
                                    if (column_graphs.Count > 0)
                                    {
                                        if (console_in_breakdown[3] == "")
                                        {
                                            ConsoleInHistory(consoleInput);
                                            ConsoleError("Wrong syntax");
                                            consoleInput = "";
                                            break;
                                        }
                                        else
                                        {
                                            for (int i = 0; i < column_graphs.Count; i++)
                                            {
                                                if (column_graphs[i].Name == console_in_breakdown[3])
                                                {
                                                    if (console_in_breakdown[4] == "")
                                                    {
                                                        ConsoleInHistory(consoleInput);
                                                        ConsoleError("Wrong syntax");
                                                        consoleInput = "";
                                                        break;
                                                    }
                                                    else
                                                    {

                                                        //Delete all object from index x to index y
                                                        if (console_in_breakdown[4] == "from" && console_in_breakdown[6] == "to")
                                                        {
                                                            #region Delete From index To index 
                                                            if (console_in_breakdown[5] == "")
                                                            {
                                                                ConsoleInHistory(consoleInput);
                                                                ConsoleError("Wrong syntax");
                                                                consoleInput = "";
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                //Check if index is an int
                                                                bool isIntBegin = IsInt(console_in_breakdown[5]);

                                                                if (isIntBegin == true)
                                                                {
                                                                    int start = Convert.ToInt32(console_in_breakdown[5]);

                                                                    if (console_in_breakdown[7] == "")
                                                                    {
                                                                        ConsoleInHistory(consoleInput);
                                                                        ConsoleError("Range needs to be as a number!");
                                                                        consoleInput = "";
                                                                        break;
                                                                    }
                                                                    else
                                                                    {
                                                                        bool isIntEnd = IsInt(console_in_breakdown[7]);

                                                                        if (isIntEnd == true)
                                                                        {
                                                                            int end = Convert.ToInt32(console_in_breakdown[7]);
                                                                            ConsoleInHistory(consoleInput);
                                                                            RemoveFromColumnChartByRange(start, end, i);
                                                                            consoleInput = "";
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ConsoleInHistory(consoleInput);
                                                                    ConsoleError("Range needs to be as a number!");
                                                                    consoleInput = "";
                                                                    break;
                                                                }
                                                            }
                                                            #endregion
                                                        }//Remove at index
                                                        else if (console_in_breakdown[4] == "at")
                                                        {
                                                            if (console_in_breakdown[5] == "")
                                                            {
                                                                ConsoleInHistory(consoleInput);
                                                                ConsoleError("Wrong syntax");
                                                                consoleInput = "";
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                bool isInt = IsInt(console_in_breakdown[5]);

                                                                if (isInt == true)
                                                                {
                                                                    int index = Convert.ToInt32(console_in_breakdown[5]);
                                                                    RemoveFromColumnChartByIndex(index, i);

                                                                    ConsoleInHistory(consoleInput);
                                                                    consoleInput = "";
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    ConsoleInHistory(consoleInput);
                                                                    ConsoleError("Index needs to be set as a number!");
                                                                    consoleInput = "";
                                                                    break;
                                                                }
                                                            }
                                                        }//Remove all variables == variable
                                                        else if (console_in_breakdown[4] == "where")
                                                        {
                                                            if (console_in_breakdown[5] == "")
                                                            {
                                                                ConsoleInHistory(consoleInput);
                                                                ConsoleError("Wrong syntax");
                                                                consoleInput = "";
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                string str_double = console_in_breakdown[5].Replace('.', ',');

                                                                bool isDouble = IsDouble(str_double);

                                                                if (isDouble == true)
                                                                {
                                                                    double variable = Convert.ToDouble(str_double);
                                                                    ConsoleInHistory(consoleInput);
                                                                    RemoveFromColumnChartWhere(variable, i);
                                                                    consoleInput = "";
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    ConsoleInHistory(consoleInput);
                                                                    ConsoleError("Index needs to be set as a number!");
                                                                    consoleInput = "";
                                                                    break;
                                                                }
                                                            }
                                                        }//If remove All Greater than or all lower than
                                                        else if (console_in_breakdown[4] == "all")
                                                        {
                                                            if (console_in_breakdown[5] == "")
                                                            {
                                                                ConsoleInHistory(consoleInput);
                                                                ConsoleError("Wrong syntax");
                                                                consoleInput = "";
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                //If user wants to delete all greater than
                                                                if (console_in_breakdown[5] == "gt")
                                                                {
                                                                    if (console_in_breakdown[6] == "")
                                                                    {
                                                                        ConsoleInHistory(consoleInput);
                                                                        ConsoleError("Wrong syntax");
                                                                        consoleInput = "";
                                                                        break;
                                                                    }
                                                                    else
                                                                    {
                                                                        string str_double = console_in_breakdown[6].Replace('.', ',');

                                                                        bool isDouble = IsDouble(str_double);

                                                                        if (isDouble == true)
                                                                        {
                                                                            double variable = Convert.ToDouble(str_double);

                                                                            ConsoleInHistory(consoleInput);
                                                                            RemoveFromColumnChartGT(variable, i);
                                                                            consoleInput = "";
                                                                            break;
                                                                        }
                                                                    }
                                                                }
                                                                else if (console_in_breakdown[5] == "lt")
                                                                {
                                                                    string str_double = console_in_breakdown[6].Replace('.', ',');

                                                                    bool isDouble = IsDouble(str_double);

                                                                    if (isDouble == true)
                                                                    {
                                                                        double variable = Convert.ToDouble(str_double);

                                                                        ConsoleInHistory(consoleInput);
                                                                        RemoveFromColumnChartLT(variable, i);
                                                                        consoleInput = "";
                                                                        break;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    ConsoleInHistory(consoleInput);
                                                                    ConsoleError("Wrong syntax");
                                                                    consoleInput = "";
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //SOMETHING NEXT BUT FOR NOW THATS ALL

                                                            ConsoleInHistory(consoleInput);
                                                            ConsoleError("Wrong syntax");
                                                            consoleInput = "";
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("There is no column graphs");
                                        consoleInput = "";
                                        break;
                                    }
                                }
                                else
                                {
                                    ConsoleInHistory(consoleInput);
                                    ConsoleError("Unknown command");
                                    consoleInput = "";
                                    break;
                                }

                                break;
                            }

                        //This will peek data to the console
                        case "show":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                if (console_in_breakdown[1] == "")
                                {
                                    ConsoleInHistory(consoleInput);
                                    ConsoleError("Wrong syntax");
                                    consoleInput = "";
                                    break;
                                }
                                else
                                {
                                    if (console_in_breakdown[1] == "dataset")
                                    {
                                        if (console_in_breakdown[2] == "")
                                        {
                                            ConsoleInHistory(consoleInput);
                                            ConsoleError("Wrong syntax");
                                            consoleInput = "";
                                            break;
                                        }
                                        else if (console_in_breakdown[2] == "top")
                                        {
                                            if (console_in_breakdown[3] == "")
                                            {
                                                ConsoleInHistory(consoleInput);
                                                ConsoleError("Wrong syntax");
                                                consoleInput = "";
                                                break;
                                            }
                                            else
                                            {
                                                if (Data_Sets_List.Count > 0)
                                                {
                                                    for (int i = 0; i < Data_Sets_List.Count; i++)
                                                    {
                                                        if (Data_Sets_List[i].Name == console_in_breakdown[3])
                                                        {
                                                            ConsoleInHistory(consoleInput);
                                                            ShowTopDataset(i);

                                                            consoleInput = "";
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (console_in_breakdown[1] == "chart")
                                    {
                                        if (console_in_breakdown[2] == "")
                                        {
                                            ConsoleInHistory(consoleInput);
                                            ConsoleError("Wrong syntax");
                                            consoleInput = "";
                                            break;
                                        }
                                        else if (console_in_breakdown[2] == "top")
                                        {
                                            if (console_in_breakdown[3] == "")
                                            {
                                                ConsoleInHistory(consoleInput);
                                                ConsoleError("Wrong syntax");
                                                consoleInput = "";
                                                break;
                                            }
                                            else
                                            {
                                                if (column_graphs.Count > 0)
                                                {
                                                    for (int i = 0; i < column_graphs.Count; i++)
                                                    {
                                                        if (column_graphs[i].Name == console_in_breakdown[3])
                                                        {
                                                            ConsoleInHistory(consoleInput);
                                                            ShowTopChart(i);

                                                            consoleInput = "";
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong syntax");
                                        consoleInput = "";
                                        break;
                                    }
                                }

                                break;
                            }

                        //This function will sum all data points from dataset of the same type
                        case "summ":
                            {
                                string[] console_in_breakdown = consoleInput.Split(" ");

                                if(console_in_breakdown.Length == 3)
                                {
                                    if (console_in_breakdown[1] == "")
                                    {
                                        ConsoleInHistory(consoleInput);
                                        ConsoleError("Wrong syntax");
                                        consoleInput = "";
                                        break;
                                    }
                                    else
                                    {
                                        if (Data_Sets_List.Count == 0)
                                        {
                                            ConsoleInHistory(consoleInput);
                                            ConsoleError("There is no datasets");
                                            consoleInput = "";
                                            break;
                                        }
                                        else
                                        {
                                            bool foundDataset = false;

                                            for (int i = 0; i < Data_Sets_List.Count; i++)
                                            {
                                                if (Data_Sets_List[i].Name == console_in_breakdown[1])
                                                {
                                                    foundDataset = true;

                                                    bool foundLabel = false;
                                                    for(int j = 0; j < labels_datasets[i].LableCollection.Count; j++)
                                                    {
                                                        if(labels_datasets[i].LableCollection[j].Label == console_in_breakdown[2])
                                                        {
                                                            foundLabel = true;
                                                            ConsoleInHistory(consoleInput);
                                                            SumAllDatasetOfType(i, labels_datasets[i].LableCollection[j].Label);
                                                            consoleInput = "";
                                                            break;
                                                        }
                                                    }

                                                    if(foundLabel == false)
                                                    {
                                                        ConsoleInHistory(consoleInput);
                                                        ConsoleError("Could not found this label");
                                                        consoleInput = "";
                                                        break;
                                                    }
                                                }
                                            }

                                            if(foundDataset == false)
                                            {
                                                ConsoleInHistory(consoleInput);
                                                ConsoleError($"Could not find this dataset: {console_in_breakdown[1]}");
                                                consoleInput = "";
                                                break;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    ConsoleInHistory(consoleInput);
                                    ConsoleError("Wrong syntax");
                                    consoleInput = "";
                                    break;
                                }

                                break;
                            }

                        //This will load Example
                        case "exmp":
                            {
                                ConsoleInHistory(consoleInput);
                                CreateExample();
                                consoleInput = "";
                                break;
                            }


                        default:
                            {
                                ConsoleInHistory(consoleInput);
                                ConsoleError("Unknown command");
                                consoleInput = "";
                                break;
                            }
                    }

                }//If comand is too short
                else
                {
                    ConsoleInHistory(consoleInput);
                    ConsoleError("Unknown command");
                    consoleInput = "";
                }
            }


        }
        #endregion

        protected override async Task OnInitializedAsync()
        {
            ConsoleInHistory("To load example enter: 'exmp' ");
        }

        #region Example generator
        /// <summary>
        /// This function will create example
        /// </summary>
        public void CreateExample()
        {
            consoleInput = "gets games wwwroot\\vgsales.csv";
            InterpretConsole();
            consoleInput = "colu create Na_Sales games Name NA_Sales";
            InterpretConsole();
            consoleInput = "colu create Eu_Sales games Name EU_Sales";
            InterpretConsole();
            consoleInput = "colu create Jp_Sales games Name JP_Sales";
            InterpretConsole();
            consoleInput = "colu create Other_Sales games Name Other_Sales";
            InterpretConsole();
            consoleInput = "colu create Global_Sales games Name Global_Sales";
            InterpretConsole();
            consoleInput = "mean Na_Sales";
            InterpretConsole();
            consoleInput = "mean Eu_Sales";
            InterpretConsole();
            consoleInput = "mean Jp_Sales";
            InterpretConsole();
            consoleInput = "mean Other_Sales";
            InterpretConsole();
        }
        #endregion

        #region Console Output Functions

        /// <summary>
        /// This function will store and show in console output what user entered
        /// </summary>
        /// <param name="s"></param>
        public void ConsoleInHistory(string s)
        {
            ConsoleOutObj o = new ConsoleOutObj();
            o.Out = s;
            o.Normal = true;
            ConsoleOutput nco = new ConsoleOutput();

            nco.Id = console_output_history.Count;
            nco.OutputList.Insert(0, o);
            console_output_history.Add(nco);
        }

        /// <summary>
        /// This function will show in consol error
        /// </summary>
        /// <param name="s"></param>
        public void ConsoleError(string s)
        {
            ConsoleOutObj o = new ConsoleOutObj();
            o.Out = s;
            o.IfError = true;
            ConsoleOutput nco = new ConsoleOutput();

            nco.Id = console_output_history.Count;
            nco.OutputList.Insert(0, o);
            console_output_history.Add(nco);
        }

        /// <summary>
        /// This function will show in consol information about success
        /// </summary>
        /// <param name="s"></param>
        public void ConsoleInfo(string s)
        {
            ConsoleOutObj o = new ConsoleOutObj();
            o.Out = s;
            o.IfInfo = true;
            ConsoleOutput nco = new ConsoleOutput();

            nco.Id = console_output_history.Count;
            nco.OutputList.Insert(0, o);
            console_output_history.Add(nco);
        }

        #endregion

        #region Create and Populate New Dataset
        /// <summary>
        /// This function will create and populate dataset from csv file
        /// It will record it as table of strings
        /// </summary>
        /// <returns></returns>
        public void PopulateDataset(string name)
        {

            FilePath = Path.GetFullPath(FilePath);

            List<string> lines = File.ReadAllLines(FilePath).ToList();

            DataSet new_dataset = new DataSet();

            List<string> temp_list = new List<string>();

            Parameter new_parameter = new Parameter();

            for (int i = 0; i < lines.Count; i++)
            {
                //One line form list
                string line = lines[i];

                //line as char List
                List<char> line_fix = new List<char>();
                int quote_counter = 0;

                for (int j = 0; j < line.Length; j++)
                {
                    char c = line[j];

                    if (c != ',' && c != '\"')
                    {
                        line_fix.Add(c);
                    }
                    if (c == ',' && quote_counter == 0)
                    {
                        line_fix.Add(c);
                    }
                    if (c == '\"')
                    {
                        quote_counter++;
                    }
                    if (quote_counter == 1 && c == ',')
                    {
                        line_fix.Add('.');
                    }
                    if (quote_counter == 2)
                    {
                        quote_counter = 0;
                    }
                }

                StringBuilder fixed_line = new StringBuilder();

                for (int j = 0; j < line_fix.Count; j++)
                {
                    fixed_line.Append(line_fix[j]);
                }

                temp_list.Add(fixed_line.ToString());
            }

            string[] labels = temp_list[0].Split(',');

            for (int j = 0; j < labels.Length; j++)
            {
                new_parameter.Parameters.Add(labels[j]);
            }

            List<LabelDataset> labels_data = new List<LabelDataset>();
            Labels data_label = new Labels();

            int max_iteration = 0;

            if(temp_list.Count >= 100)
            {
                max_iteration = 100;
            }
            else
            {
                max_iteration = temp_list.Count;
            }

            //This variable checks if row 1 is set to be as counter in file / if not then create counting from 1 - n
            bool is_rank = true;

            for (int i = 0; i < max_iteration; i++)
            {

                string line = temp_list[i];
                string[] entries = line.Split(',');
                DatasetRow row = new DatasetRow();
                int index_counter = 0;


                for (int j = 0; j < new_parameter.Parameters.Count; j++)
                {

                    if (i != 0)
                    {//Add data points
                        index_counter++;
                        string double_check = entries[j].Replace('.', ',');
                        bool isDouble = IsDouble(double_check);
                        if (isDouble == true)
                        {
                            double number = Convert.ToDouble(double_check);

                            DatasetRowObject r_obj = new DatasetRowObject();
                            r_obj.Id = index_counter;
                            r_obj.DoubleData = number;
                            r_obj.IsDouble = true;
                            row.Row.Add(r_obj);

                            if(number != i && j == 0)
                            {
                                ConsoleInfo(i + "=" + number);
                                is_rank = false;
                            }
                        }
                        else
                        {
                            DatasetRowObject r_obj = new DatasetRowObject();
                            r_obj.Id = index_counter;
                            r_obj.IsString = true;
                            r_obj.StringData = entries[j];
                            row.Row.Add(r_obj);

                            if(j == 0)
                            {
                                is_rank = false;
                            }
                        }


                    }
                    else
                    {//Create labels
                        if (temp_list.Count > 1)
                        {
                            string line2 = temp_list[1];
                            string[] entry2 = line2.Split(',');

                            string double_check = entry2[j].Replace('.', ',');
                            bool isDouble = IsDouble(double_check);

                            if (isDouble == true)
                            {
                                LabelDataset label = new LabelDataset();
                                label.Id = j;
                                label.IsDouble = true;
                                label.Label = entries[j];
                                labels_data.Add(label);
                            }
                            else
                            {
                                LabelDataset label = new LabelDataset();
                                label.Id = j;
                                label.IsString = true;
                                label.Label = entries[j];
                                labels_data.Add(label);
                            }
                        }
                    }
                    new_dataset.ArrayRowLenght = j; 
                }

                if (i != 0)
                {
                    row.Id = i - 1;
                    new_dataset.Data.Add(row);
                }

                new_dataset.ArrayColumnLength = i;
            }

            //Create counting from 1 to n if data has no counting
            if(is_rank == false)
            {
                LabelDataset label = new LabelDataset();
                label.Id = 0;
                label.IsDouble = true;
                label.Label = "Rank";
                labels_data.Insert(0, label);

                for(int i = 0; i < new_dataset.Data.Count; i++)
                {
                    DatasetRowObject r_obj = new DatasetRowObject();
                    r_obj.DoubleData = i + 1;
                    r_obj.IsDouble = true;
                    r_obj.Id = i;
                    new_dataset.Data[i].Row.Insert(0, r_obj);

                    for (int j = 0; j < new_dataset.Data[i].Row.Count; j++)
                    {
                        //Change ID's for the labels
                        if(i == 0)
                        {
                            labels_data[j].Id = j;
                        }

                        //Create ranks of data
                        new_dataset.Data[i].Row[j].Id = j;
                    }
                }
            }

            data_label.LableCollection = labels_data;

            if (labels_datasets.Count > 0)
            {
                data_label.Id = labels_datasets.Count;
            }
            else
            {
                data_label.Id = 0;
            }

            new_dataset.Id = Data_Sets_List.Count();
            new_dataset.Name = name;

            showcase_dataset = new_dataset;
            Data_Sets_List.Add(new_dataset);
            AddToFourDataSets(new_dataset);

            labels_datasets.Add(data_label);
            parameters_list.Add(new_parameter);
        }
        #endregion

        #region Edit Dataset Row
        /// <summary>
        /// Row wil be edited by use of this variable
        /// </summary>
        protected List<EditingVariable> data_set_row_edit = new List<EditingVariable>();

        protected bool edit_row_toggler = false;
        protected string edit_row_class_string = "edit_row_box_none";
        protected int edit_ID_Dataset = 0;
        protected int edit_ID_Dataset_Row = 0;


        /// <summary>
        /// This function will toggle window for editing dataset row
        /// </summary>
        /// <param name="row_id"></param>
        public void EditDatasetRowToggler(int row_id)
        {
            if (edit_row_toggler == false)
            {
                edit_row_toggler = true;
                edit_row_class_string = "edit_row_box";

                for (int i = 1; i < showcase_dataset.Data[row_id].Row.Count; i++)
                {
                    EditingVariable es = new EditingVariable();
                    es.Id = i;

                    if (showcase_dataset.Data[row_id].Row[i].IsString)
                    {
                        es.IsString = true;
                        es.StringValue = showcase_dataset.Data[row_id].Row[i].StringData;
                        es.Label = labels_datasets[showcase_dataset.Id].LableCollection[i].Label;
                    }

                    if (showcase_dataset.Data[row_id].Row[i].IsDouble)
                    {
                        es.IsDouble = true;
                        es.DoubleValue = showcase_dataset.Data[row_id].Row[i].DoubleData;
                        es.Label = labels_datasets[showcase_dataset.Id].LableCollection[i].Label;
                    }

                    edit_ID_Dataset = showcase_dataset.Id;
                    edit_ID_Dataset_Row = row_id;

                    data_set_row_edit.Add(es);
                }
            }
            else
            {
                edit_row_toggler = false;
                edit_row_class_string = "edit_row_box_none";

                data_set_row_edit.Clear();
                edit_ID_Dataset = 0;
                edit_ID_Dataset_Row = 0;
            }
        }

        /// <summary>
        /// This function will edit row of dataset
        /// </summary>
        public void EditDatasetRow()
        {

            ConsoleInfo($"Row have beem edited");

            for (int i = 1; i < data_set_row_edit.Count; i++)
            {
                if (Data_Sets_List[edit_ID_Dataset].Data[edit_ID_Dataset_Row].Row[i].IsString)
                {
                    Data_Sets_List[edit_ID_Dataset].Data[edit_ID_Dataset_Row].Row[i].StringData = data_set_row_edit[i].StringValue;
                }

                if (Data_Sets_List[edit_ID_Dataset].Data[edit_ID_Dataset_Row].Row[i].IsDouble)
                {
                    Data_Sets_List[edit_ID_Dataset].Data[edit_ID_Dataset_Row].Row[i].DoubleData = data_set_row_edit[i].DoubleValue;
                }
            }

            EditDatasetRowToggler(edit_ID_Dataset_Row);
        }

        #endregion

        #region Delete Dataset Row

        /// <summary>
        /// This function will delete row from dataset
        /// </summary>
        public void DeleteRowDataset(int row_Id)
        {
            ConsoleInfo($"Row has been removed");
            showcase_dataset.Data.RemoveAt(row_Id);

            //Reset ID's and labels
            for (int i = 0; i < showcase_dataset.Data.Count; i++)
            {
                showcase_dataset.Data[i].Row[0].DoubleData = i + 1;
                showcase_dataset.Data[i].Id = i;
            }
        }
        #endregion

        #region Delete Dataset
        /// <summary>
        /// This function will delete Dataset by its id
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteDataset(int Id)
        {
            if (Data_Sets_List.Count > 0)
            {
                ConsoleInfo($"{Data_Sets_List[Id].Name} has been removed.");
                Data_Sets_List.RemoveAt(Id);

                //If showcase dataset is beeing deleted either add first other dataset or reset
                if (showcase_dataset.Id == Id)
                {
                    if (Data_Sets_List.Count > 0)
                    {
                        //If there is any more datasets
                        showcase_dataset = Data_Sets_List[0];

                        //Reset Id's
                        for (int i = 0; i < Data_Sets_List.Count; i++)
                        {
                            Data_Sets_List[i].Id = i;
                        }
                    }
                    else
                    {
                        //If no more just reset showcase
                        showcase_dataset = new DataSet();
                    }
                }


            }
        }
        #endregion

        #region ToggleDataset
        protected List<DataSet> FourDataSets = new List<DataSet>();


        /// <summary>
        /// This function will add Id to the list of 4 data sets
        /// </summary>
        /// <param name="c"></param>
        public void AddToFourDataSets(DataSet c)
        {
            if (FourDataSets.Count < 4)
            {
                FourDataSets.Add(c);
            }
        }


        /// <summary>
        ///This function will toggle between datasets
        /// </summary>
        public void ChangeDataset(DataSet c)
        {
            if(Data_Sets_List.Count != 0)
            {
                showcase_dataset = Data_Sets_List[c.Id];
            }
        }


        /// <summary>
        /// This function will load next 4 datasets
        /// </summary>
        public void LoadNextFourDataset()
        {
            if (Data_Sets_List.Count > FourDataSets.Count)
            {
                if (Data_Sets_List.Count > FourDataSets[FourDataSets.Count - 1].Id)
                {
                    if ((Data_Sets_List.Count - FourDataSets.Count) >= 4)
                    {

                        if ((Data_Sets_List.Count - FourDataSets[3].Id) >= 4)
                        {
                            int x = FourDataSets[3].Id;
                            FourDataSets.Clear();

                            for (int i = x + 1; i < (x + 4); i++)
                            {
                                DataSet c = new DataSet();
                                c.Name = Data_Sets_List[i].Name;
                                c.Id = Data_Sets_List[i].Id;
                                FourDataSets.Add(c);
                            }
                        }
                        else
                        {
                            int x = FourDataSets[3].Id;
                            FourDataSets.Clear();

                            for (int i = x + 1; i < Data_Sets_List.Count; i++)
                            {
                                DataSet c = new DataSet();
                                c.Name = Data_Sets_List[i].Name;
                                c.Id = Data_Sets_List[i].Id;
                                FourDataSets.Add(c);
                            }
                        }

                    }
                    else
                    {
                        int x = FourDataSets.Count;
                        FourDataSets.Clear();
                        for (int i = x; i < Data_Sets_List.Count; i++)
                        {
                            DataSet c = new DataSet();
                            c.Name = Data_Sets_List[i].Name;
                            c.Id = Data_Sets_List[i].Id;
                            FourDataSets.Add(c);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// This function will load previous 4 data sets
        /// </summary>
        public void LoadPreviousFourDatasets()
        {
            int x = FourDataSets[0].Id;
            FourDataSets.Clear();
            for (int i = x - 4; i < x; i++)
            {
                DataSet c = new DataSet();
                c.Name = Data_Sets_List[i].Name;
                c.Id = Data_Sets_List[i].Id;
                FourDataSets.Add(c);
            }
        }

        #endregion

        #region Sort Dataset By Its Label

        protected DataSetSortChecker sortChecker = new DataSetSortChecker();

        /// <summary>
        /// This function will sort dataset on display by column label value
        /// </summary>
        /// <param name="dataset_Id">Id of dataset to sort</param>
        /// <param name="column_label_Id"></param>
        public void SortShowcaseDataset(int column_label_Id, int dataset_connection_id)
        {
            if (labels_datasets[dataset_connection_id].LableCollection[column_label_Id].IsDouble)
            {
                if (sortChecker.LabelName != labels_datasets[dataset_connection_id].LableCollection[column_label_Id].Label)
                {
                    for (int i = 0; i < showcase_dataset.Data.Count - 1; i++)
                    {
                        for (int j = i + 1; j > 0; j--)
                        {
                            if (showcase_dataset.Data[j - 1].Row[column_label_Id].DoubleData > showcase_dataset.Data[j].Row[column_label_Id].DoubleData)
                            {
                                DatasetRow temp = showcase_dataset.Data[j - 1];
                                showcase_dataset.Data[j - 1] = showcase_dataset.Data[j];
                                showcase_dataset.Data[j] = temp;
                            }
                        }
                    }

                    for (int i = 0; i < showcase_dataset.Data.Count; i++)
                    {
                        showcase_dataset.Data[i].Row[0].DoubleData = i + 1;
                        showcase_dataset.Data[i].Id = i;
                    }

                    sortChecker.LabelName = labels_datasets[dataset_connection_id].LableCollection[column_label_Id].Label;
                }
                else
                {
                    for (int i = 0; i < showcase_dataset.Data.Count - 1; i++)
                    {
                        for (int j = i + 1; j > 0; j--)
                        {
                            if (showcase_dataset.Data[j - 1].Row[column_label_Id].DoubleData > showcase_dataset.Data[j].Row[column_label_Id].DoubleData)
                            {
                                DatasetRow temp = showcase_dataset.Data[j - 1];
                                showcase_dataset.Data[j - 1] = showcase_dataset.Data[j];
                                showcase_dataset.Data[j] = temp;
                            }
                        }
                    }

                    showcase_dataset.Data.Reverse();

                    for (int i = 0; i < showcase_dataset.Data.Count; i++)
                    {
                        showcase_dataset.Data[i].Row[0].DoubleData = i + 1;
                        showcase_dataset.Data[i].Id = i;
                    }

                    sortChecker.LabelName = "";
                }

            }

            if (labels_datasets[dataset_connection_id].LableCollection[column_label_Id].IsString)
            {
                ConsoleInfo($"Is string. Column Id: {column_label_Id}  Dataset Id: {dataset_connection_id} {showcase_dataset.Data.Count}");
            }
        }
        #endregion

        #region  Show 10 from top of the Dataset
        /// <summary>
        /// This function will write 10 top objects from dataset to the console
        /// </summary>
        /// <param name="dataset_Id">Id of dataset from which to peek</param>
        public void ShowTopDataset(int dataset_Id)
        {
            if (Data_Sets_List.Count > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int indx = i + 1;
                    string output = $"|{indx}|";
                    for (int j = 1; j < Data_Sets_List[dataset_Id].Data[i].Row.Count; j++)
                    {
                        output = output + "|" + Data_Sets_List[dataset_Id].Data[i].Row[j] + "|";
                    }
                    ConsoleInfo(output);
                }
            }
        }
        #endregion

        #region Sum all data points of one type in data set

        /// <summary>
        /// This function will sum all data points from dataset of the same type
        /// </summary>
        public void SumAllDatasetOfType(int datasetId, string label)
        {
            if(Data_Sets_List.Count < datasetId)
            {
                ConsoleError("Dataset does not exist");
            }
            else if(Data_Sets_List[datasetId] == null)
            {
                ConsoleError("Dataset does not exist");
            }
            else
            {
                bool found_label = false;

                for(int i = 0; i < labels_datasets[datasetId].LableCollection.Count; i++)
                {
                    if(labels_datasets[datasetId].LableCollection[i].Label == label)
                    {
                        found_label = true;
                        double sum = 0;

                        if(labels_datasets[datasetId].LableCollection[i].IsDouble == true)
                        {
                            for (int j = 0; j < Data_Sets_List[datasetId].Data.Count; j++)
                            {
                                sum += Data_Sets_List[datasetId].Data[i].Row[labels_datasets[datasetId].LableCollection[i].Id].DoubleData;
                            }

                            ConsoleInfo($"The sum of {labels_datasets[datasetId].LableCollection[i].Label} in dataset {Data_Sets_List[datasetId].Name} is {sum}");
                        }
                        else
                        {
                            ConsoleError("This label does not represends numeric value in dataset!");
                        }

                    }
                }

                if(found_label == false)
                {
                    ConsoleError("Could not find label");
                }
            }

            
        }
        #endregion

        #region Create Column Chart

        /// <summary>
        /// This function will create column graph on value
        /// </summary>
        /// <param name="parameter">Parameter on which user want to create column graph</param>
        /// <param name="dataset_name">Name of dataset from which user wants to create coulumn graph</param>
        public void CreateColumnGraph(string parameter, string parameter_names, int parameter_name_id, string dataset_name, string column_chart_name)
        {
            int param_Id = 0;
            int dataset_Id = 0;


            bool param_found = false;
            bool dataset_found = false;

            //Check for dataset
            for (int i = 0; i < Data_Sets_List.Count; i++)
            {
                if (Data_Sets_List[i].Name == dataset_name)
                {
                    dataset_Id = i;
                    //If dataset found
                    dataset_found = true;
                }
            }


            //Check for the right parameter
            if (dataset_found == true)
            {
                for (int i = 0; i < parameters_list[dataset_Id].Parameters.Count; i++)
                {
                    if (parameters_list[dataset_Id].Parameters[i] == parameter)
                    {
                        param_Id = i;
                        //If param found
                        param_found = true;
                    }
                }
            }



            //If it found the dataset and parameter
            if (dataset_found == true && param_found == true)
            {
                bool error = false;
                ColumnGraph new_coulumn = new ColumnGraph();

                //If user wants to use other rows to name columns in the graph
                if (parameter_names != "#numeric")
                {
                    for (int i = 0; i < Data_Sets_List[dataset_Id].Data.Count; i++)
                    {
                        Column c = new Column();
                        c.DataPoint = Data_Sets_List[dataset_Id].Data[i].Row[param_Id].DoubleData;
                        c.Id = i;
                        c.Name = Data_Sets_List[dataset_Id].Data[i].Row[parameter_name_id].StringData;
                        new_coulumn.Columns.Add(c);

                    }

                }//If user wants to name columns by numbers from 0 to n where n is ammount of columns
                else if (parameter_names == "#numeric")
                {
                    for (int i = 0; i < Data_Sets_List[dataset_Id].Data.Count; i++)
                    {
                        Column c = new Column();
                        c.Id = i;
                        c.DataPoint = Data_Sets_List[dataset_Id].Data[i].Row[param_Id].DoubleData;
                        c.Name = i.ToString();
                        new_coulumn.Columns.Add(c);
                    }
                }


                //If there is no errors create column chart
                if (error == false)
                {
                    if (column_graphs.Count == 0)
                    {
                        new_coulumn.Id = 0;
                    }
                    else
                    {
                        new_coulumn.Id = column_graphs.Count;
                    }

                    new_coulumn.Name = column_chart_name;
                    new_coulumn.DataVariableName = parameter;
                    new_coulumn.DataSetName = dataset_name;
                    column_graphs.Add(new_coulumn);
                    ConsoleInfo("Coulumn chart has been created");


                    //Create item for toggleing
                    ColumnChartFinder cf = new ColumnChartFinder();
                    cf.Id = new_coulumn.Id;
                    cf.Name = new_coulumn.Name;
                    AddToFourColumnCharts(cf);
                }
            }
        }

        #endregion

        #region Show 10 from top of the Chart
        /// <summary>
        /// This function will write 10 top objects from dataset to the console
        /// </summary>
        /// <param name="dataset_Id">Id of dataset from which to peek</param>
        public void ShowTopChart(int chart_Id)
        {
            if (Data_Sets_List.Count > 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    int indx = i + 1;
                    string output = $"|{indx}|";
                    output = output + "|" + column_graphs[chart_Id].Columns[i].Name + "|" + column_graphs[chart_Id].Columns[i].DataPoint + "|";
                    ConsoleInfo(output);
                }
            }
        }
        #endregion

        #region Show Data From The Chart
        protected bool data_dable_chart_toggler = false;
        protected string toggle_class_chart_data = "chart_data_box_none";

        public ColumnGraph display_data_object = new ColumnGraph();

        /// <summary>
        /// This function will toggle div which shows data of the chart
        /// </summary>
        public void ToggleDataChart()
        {
            if (data_dable_chart_toggler == false)
            {
                data_dable_chart_toggler = true;
                toggle_class_chart_data = "chart_data_box";

                if (column_graphs.Count > 0)
                {
                    display_data_object = column_graphs[current_column_chart.Id];
                }
                else
                {
                    ConsoleError("There is not data for display!");
                }
            }
            else
            {
                data_dable_chart_toggler = false;
                toggle_class_chart_data = "chart_data_box_none";
                display_data_object = new ColumnGraph();
            }
        }

        #endregion

        #region Delete Column Chart
        /// <summary>
        /// This function will delete Column Chart by its id
        /// </summary>
        /// <param name="Id"></param>
        public void DeleteColumnChart(int Id)
        {
            if (column_graphs.Count > 0)
            {
                ConsoleInfo($"{column_graphs[Id].Name} has been removed.");
                column_graphs.RemoveAt(Id);

                bool is_done = false;

                //If open column chart is beeing deleted change it for 1st one in list or reset view variable
                if (column_graphs.Count > 0)
                {
                    //Reset Id's
                    for (int i = 0; i < column_graphs.Count; i++)
                    {
                        column_graphs[i].Id = i;
                    }

                    is_done = true;

                    //Reset list for showing column charts
                    ResetFourColumnChart();
                }

                if (is_done == true)
                {
                    //Reset view for the first one from the list
                    CreateGraphJS(column_graphs[0]);
                }
            }
        }
        #endregion

        #region Delete objects form column chart (BY ID) (BY Range) (By Condition)

        /// <summary>
        /// This function will remove object from column chart by index of variable user wants to remove
        /// </summary>
        /// <param name="idx">Index of column object to remove</param>
        /// <param name="chart_Id">Id of chart from which to remove</param>
        public void RemoveFromColumnChartByIndex(int idx, int chart_Id)
        {
            if (column_graphs[chart_Id] != null)
            {
                column_graphs[chart_Id].Columns.RemoveAt(idx);

                //If this chart is open reset view
                if (current_column_chart.Id == chart_Id)
                {
                    string chart_name = column_graphs[chart_Id].Name;

                    ConsoleInfo($"Variable on index {idx} has been removed from chart {chart_name}");

                    //Reset ID's
                    for (int i = 0; i < column_graphs[chart_Id].Columns.Count; i++)
                    {
                        column_graphs[chart_Id].Columns[i].Id = i;
                    }

                    CreateGraphJS(column_graphs[chart_Id]);
                }
            }
        }

        /// <summary>
        /// This function will delete column items from (begin range) to (end_range)
        /// </summary>
        /// <param name="begin_range">Begin index</param>
        /// <param name="end_range">End index</param>
        /// <param name="chart_Id">Id of chart from which to remove</param>
        public void RemoveFromColumnChartByRange(int begin_range, int end_range, int chart_Id)
        {
            if (column_graphs[chart_Id] != null)
            {
                if (column_graphs[chart_Id].Columns.Count < begin_range)
                {
                    ConsoleInHistory(consoleInput);
                    ConsoleError($"Begin index ({begin_range}) is out of range!");
                    consoleInput = "";
                }
                else
                {
                    if (column_graphs[chart_Id].Columns.Count < end_range)
                    {
                        ConsoleInHistory(consoleInput);
                        ConsoleError($"End index ({end_range}) is out of range!");
                        consoleInput = "";
                    }
                    else
                    {
                        if (begin_range == end_range)
                        {
                            ConsoleInHistory(consoleInput);
                            ConsoleError($"Begin index ({begin_range}) is equal to end range ({end_range})!");
                            consoleInput = "";
                        }
                        else
                        {
                            if (begin_range > end_range)
                            {
                                ConsoleInHistory(consoleInput);
                                ConsoleError($"Begin index ({begin_range}) cannot be bigger than end range ({end_range})!");
                                consoleInput = "";
                            }
                            else
                            {
                                int ammount = end_range - begin_range;
                                string chart_name = column_graphs[chart_Id].Name;

                                column_graphs[chart_Id].Columns.RemoveRange(begin_range, ammount);
                                ConsoleInfo($"{ammount} has been removed from {chart_name}");

                                //Reset ID's
                                for (int i = 0; i < column_graphs[chart_Id].Columns.Count; i++)
                                {
                                    column_graphs[chart_Id].Columns[i].Id = i;
                                }


                                //If column chart is open reset the view
                                if (current_column_chart.Id == chart_Id)
                                {
                                    CreateGraphJS(column_graphs[chart_Id]);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This function will remove all items from chart where (variable) is
        /// </summary>
        /// <param name="variable">which items to remove</param>
        /// <param name="chart_Id">Id of chart from which to remove</param>
        public void RemoveFromColumnChartWhere(double variable, int chart_Id)
        {
            if (column_graphs[chart_Id] != null)
            {
                int counter = 0;

                for (int i = 0; i < column_graphs[chart_Id].Columns.Count; i++)
                {
                    if (column_graphs[chart_Id].Columns[i].DataPoint == variable)
                    {
                        column_graphs[chart_Id].Columns.RemoveAt(i);
                        counter++;
                    }
                }

                //Reset ID's
                for (int i = 0; i < column_graphs[chart_Id].Columns.Count; i++)
                {
                    column_graphs[chart_Id].Columns[i].Id = i;
                }

                string chart_name = column_graphs[chart_Id].Name;

                ConsoleInfo($"{counter} has been removed from {chart_name}");

                //If this column chart is open
                if (current_column_chart.Id == chart_Id)
                {
                    CreateGraphJS(column_graphs[chart_Id]);
                }
            }
        }

        /// <summary>
        /// This function will remove all items where variables are greater than (variable)
        /// </summary>
        /// <param name="variable">Variable from up which all objects are beeing removed</param>
        /// <param name="chart_Id">Id of chart from which to remove</param>
        public void RemoveFromColumnChartGT(double variable, int chart_Id)
        {
            if (column_graphs[chart_Id] != null)
            {
                for (int i = 0; i < column_graphs[chart_Id].Columns.Count; i++)
                {
                    if (column_graphs[chart_Id].Columns[i].DataPoint > variable)
                    {
                        column_graphs[chart_Id].Columns.RemoveAt(i);
                        i--;
                    }
                }

                //Reset ID's
                for (int i = 0; i < column_graphs[chart_Id].Columns.Count; i++)
                {
                    column_graphs[chart_Id].Columns[i].Id = i;
                }

                //If this column chart is open
                if (current_column_chart.Id == chart_Id)
                {
                    CreateGraphJS(column_graphs[chart_Id]);
                }
            }
        }

        /// <summary>
        /// This function will remove all items where variables are lower than (variable)
        /// </summary>
        /// <param name="variable">Variable from down which all objects are beeing removed</param>
        /// <param name="chart_Id">Id of chart from which to remove</param>
        public void RemoveFromColumnChartLT(double variable, int chart_Id)
        {
            if (column_graphs[chart_Id] != null)
            {
                for (int i = 0; i < column_graphs[chart_Id].Columns.Count; i++)
                {
                    if (column_graphs[chart_Id].Columns[i].DataPoint < variable)
                    {
                        column_graphs[chart_Id].Columns.RemoveAt(i);
                        i--;
                    }
                }

                //Reset ID's
                for (int i = 0; i < column_graphs[chart_Id].Columns.Count; i++)
                {
                    column_graphs[chart_Id].Columns[i].Id = i;
                }

                //If this column chart is open
                if (current_column_chart.Id == chart_Id)
                {
                    CreateGraphJS(column_graphs[chart_Id]);
                }
            }
        }


        #endregion

        #region Edit column chart object
        protected Column edit_column = new Column();

        protected bool edit_coulumn_toggler = false;
        protected string edit_column_class_string = "edit_column_box_none";
        protected int edit_ID_column = 0;
        protected int chart_edit_id;

        protected string error_data_point_edit = "";
        protected string error_name_edit = "";

        /// <summary>
        /// This function will toggle edit column object window
        /// </summary>
        public void ToggleEditColumnObject(int column_Id, int chart_Id)
        {
            if (edit_coulumn_toggler == false)
            {
                edit_coulumn_toggler = true;
                edit_column_class_string = "edit_column_box";
                edit_column = column_graphs[chart_Id].Columns[column_Id];
                chart_edit_id = chart_Id;
            }
            else
            {
                edit_coulumn_toggler = false;
                edit_column_class_string = "edit_column_box_none";
                edit_column = new Column();
                chart_edit_id = 0;
            }
        }

        /// <summary>
        /// This function will edit variables in column chart
        /// </summary>
        public void EditColumnObject()
        {
            bool error = false;
            if (edit_column.Name == "" || edit_column.Name == null)
            {
                error_name_edit = "You need to enter anything for the name";
                error = true;
            }

            string dbl = edit_column.DataPoint.ToString().Replace('.', ',');

            bool isDouble = IsDouble(dbl);

            if (isDouble == false)
            {
                error_data_point_edit = "Data point is not written as correct double!";
                error = true;
            }

            if (error == false)
            {
                column_graphs[chart_edit_id].Columns[edit_column.Id] = edit_column;
                CreateGraphJS(column_graphs[chart_edit_id]);
                ToggleEditColumnObject(0, 0);
            }
        }

        #endregion

        #region Functions for Toggleing Column Charts

        /// <summary>
        /// This list stores names and ID's for existing columns charts 
        /// </summary>
        protected List<ColumnChartFinder> FourColumnCharts = new List<ColumnChartFinder>();

        /// <summary>
        /// This function will add Id to the list of 4 column charts for toggleing them
        /// </summary>
        /// <param name="c"></param>
        public void AddToFourColumnCharts(ColumnChartFinder c)
        {
            if (FourColumnCharts.Count < 4)
            {
                FourColumnCharts.Add(c);
            }
        }

        /// <summary>
        ///This function will toggle between column charts
        /// </summary>
        public void ChangeCoulumnChart(ColumnChartFinder c)
        {
            CreateGraphJS(column_graphs[c.Id]);
            current_column_chart = c;
        }

        /// <summary>
        /// This function will load next 4 column charts into 
        /// </summary>
        public void LoadNextFourColumn()
        {
            if (column_graphs.Count > FourColumnCharts.Count)
            {
                if (column_graphs.Count > FourColumnCharts[FourColumnCharts.Count - 1].Id)
                {
                    if ((column_graphs.Count - FourColumnCharts.Count) >= 4)
                    {

                        if ((column_graphs.Count - FourColumnCharts[3].Id) >= 4)
                        {
                            int x = FourColumnCharts[3].Id;
                            FourColumnCharts.Clear();

                            for (int i = x + 1; i < (x + 4); i++)
                            {
                                ColumnChartFinder c = new ColumnChartFinder();
                                c.Name = column_graphs[i].Name;
                                c.Id = column_graphs[i].Id;
                                FourColumnCharts.Add(c);
                            }
                        }
                        else
                        {
                            int x = FourColumnCharts[3].Id;
                            FourColumnCharts.Clear();

                            for (int i = x + 1; i < column_graphs.Count; i++)
                            {
                                ColumnChartFinder c = new ColumnChartFinder();
                                c.Name = column_graphs[i].Name;
                                c.Id = column_graphs[i].Id;
                                FourColumnCharts.Add(c);
                            }
                        }

                    }
                    else
                    {
                        int x = FourColumnCharts.Count;
                        FourColumnCharts.Clear();
                        for (int i = x; i < column_graphs.Count; i++)
                        {
                            ColumnChartFinder c = new ColumnChartFinder();
                            c.Name = column_graphs[i].Name;
                            c.Id = column_graphs[i].Id;
                            FourColumnCharts.Add(c);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This function will reset four column chart list
        /// </summary>
        public void ResetFourColumnChart()
        {
            if (column_graphs.Count > 0)
            {
                if (column_graphs.Count > 4)
                {
                    FourColumnCharts.Clear();
                    for (int i = 0; i < 4; i++)
                    {
                        ColumnChartFinder c = new ColumnChartFinder();
                        c.Id = i;
                        c.Name = column_graphs[i].Name;

                        FourColumnCharts.Add(c);
                    }
                }
                else
                {
                    FourColumnCharts.Clear();
                    for (int i = 0; i < column_graphs.Count; i++)
                    {
                        ColumnChartFinder c = new ColumnChartFinder();
                        c.Id = i;
                        c.Name = column_graphs[i].Name;

                        FourColumnCharts.Add(c);
                    }
                }
            }
        }

        /// <summary>
        /// This function will load previous 4 column charts into 
        /// </summary>
        public void LoadPreviousFourColumn()
        {
            int x = FourColumnCharts[0].Id;
            FourColumnCharts.Clear();
            for (int i = x - 4; i < x; i++)
            {
                ColumnChartFinder c = new ColumnChartFinder();
                c.Name = column_graphs[i].Name;
                c.Id = column_graphs[i].Id;
                FourColumnCharts.Add(c);
            }
        }

        #endregion

        #region Large Column Chart Toggle functions
        /// <summary>
        /// This object will store information about current open column chart
        /// </summary>
        protected ColumnChartFinder current_column_chart = new ColumnChartFinder();

        protected bool large_column_chart_toggler = false;
        protected string lg_toggle_class = "chart_lg_box_none";


        protected async Task GenerateColumnChartLargeJS(ColumnChartFinder c)
        {
            ColumnGraph g = column_graphs[current_column_chart.Id];
            var columns = g.Columns;
            await JSRuntime.InvokeVoidAsync("GenerateColumnChartLarge", columns);
        }

        public async void ToggleLgColumnChart()
        {
            if (large_column_chart_toggler == false)
            {
                large_column_chart_toggler = true;
                lg_toggle_class = "chart_lg_box";
            }
            else
            {
                large_column_chart_toggler = false;
                lg_toggle_class = "chart_lg_box_none";
            }

            if (large_column_chart_toggler == true)
            {
                await GenerateColumnChartLargeJS(current_column_chart);
            }
        }
        #endregion

        #region Column Chart Sorting Functions

        /// <summary>
        /// This function will sort column chart from smallest variable to the largest variable
        /// </summary>
        /// <param name="name">Name of chart to sort</param>
        public void SortColumnMinMax(string name)
        {
            if (column_graphs.Count > 0)
            {
                for (int i = 0; i < column_graphs.Count; i++)
                {
                    if (name == column_graphs[i].Name)
                    {
                        column_graphs[i].Columns.Sort((x, y) => x.DataPoint.CompareTo(y.DataPoint));
                        //If this column chart is alredy open then change it as well in browser
                        if (current_column_chart.Name == column_graphs[i].Name)
                        {
                            CreateGraphJS(column_graphs[i]);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// This function will sort column chart from largest variable to the smallest
        /// </summary>
        /// <param name="name">Name of chart to sort</param>
        public void SortColumnMaxMin(string name)
        {
            if (column_graphs.Count > 0)
            {
                for (int i = 0; i < column_graphs.Count; i++)
                {
                    if (name == column_graphs[i].Name)
                    {
                        column_graphs[i].Columns.Sort((x, y) => y.DataPoint.CompareTo(x.DataPoint));
                        //If this column chart is alredy open then change it as well in browser
                        if (current_column_chart.Name == column_graphs[i].Name)
                        {
                            CreateGraphJS(column_graphs[i]);
                        }
                    }
                }
            }
        }
        #endregion

        #region Column Chart Median Calcualtion

        /// <summary>
        /// This function will find median for given data
        /// </summary>
        /// <param name="chart_ID">ID of chart</param>
        public void CalcMedianColumnChart(int chart_ID)
        {
            if (column_graphs.Count > 0)
            {
                List<double> variables = new List<double>();
                for (int i = 0; i < column_graphs[chart_ID].Columns.Count; i++)
                {
                    variables.Add(column_graphs[chart_ID].Columns[i].DataPoint);
                }

                string chart_name = column_graphs[chart_ID].Name;

                if (variables.Count > 0)
                {
                    //To find median data needs to be sorted
                    variables = variables.OrderBy(d => d).ToList();

                    //After sorting find median
                    bool isOdd = IsOdd(variables.Count);

                    //If number of variables is odd
                    if (isOdd == true)
                    {
                        int index = variables.Count / 2;
                        double median = variables[index];

                        ConsoleInfo($"Median for column chart: {chart_name} is: {median}");
                    }
                    else//If number of variables is even then median is mean of 2 middle items
                    {
                        int index = variables.Count / 2;
                        double v1 = variables[index];
                        double v2 = variables[index + 1];

                        double median = (v1 + v2) / 2;
                        ConsoleInfo($"Median for column chart: {chart_name} is: {median}");
                    }
                }
            }
        }
        #endregion

        #region Column Chart Mean Calculation
        /// <summary>
        /// This function will calculate mean for column chart
        /// </summary>
        /// <param name="chart_ID">ID of chart</param>
        public void CalcMeanColumnChart(int chart_ID)
        {
            if (column_graphs.Count > 0)
            {
                double sum = 0;
                for (int i = 0; i < column_graphs[chart_ID].Columns.Count; i++)
                {
                    sum += column_graphs[chart_ID].Columns[i].DataPoint;
                }

                double mean = sum / column_graphs[chart_ID].Columns.Count;
                string chart_name = column_graphs[chart_ID].Name;

                ConsoleInfo($"Mean for column chart: {chart_name} is: {mean}");

            }
        }
        #endregion

        #region Column Chart Mode Calculation

        /// <summary>
        /// This function will calculate mode for chart by appearing numbers
        /// </summary>
        /// <param name="chart_ID">chart Id</param>
        public void CalcModeColumnChartNumber(int chart_ID)
        {
            if (column_graphs.Count > 0)
            {
                ColumnGraph calculating_chart = new ColumnGraph();

                calculating_chart = column_graphs[chart_ID];

                List<DataCollective> dataCollectives = new List<DataCollective>();

                for (int i = 0; i < calculating_chart.Columns.Count; i++)
                {
                    DataCollective dc = new DataCollective();

                    dc.Id = i;
                    dc.Data = calculating_chart.Columns[i].DataPoint;
                    dc.Name = calculating_chart.Columns[i].Name;
                    dc.Occurence = 0;

                    for (int j = i + 1; j < calculating_chart.Columns.Count; j++)
                    {
                        if (dc.Data == calculating_chart.Columns[j].DataPoint)
                        {
                            calculating_chart.Columns.RemoveAt(j);
                            dc.Occurence++;
                            j--;
                        }
                    }

                    dataCollectives.Add(dc);
                }

                DataCollective equal_dc = new DataCollective();
                DataCollective max_dc = new DataCollective();
                max_dc.Occurence = 0;

                for (int i = 0; i < dataCollectives.Count; i++)
                {
                    if (max_dc.Occurence < dataCollectives[i].Occurence)
                    {
                        max_dc = dataCollectives[i];
                    }
                    else if (max_dc.Occurence == dataCollectives[i].Occurence)
                    {
                        equal_dc = dataCollectives[i];
                    }
                }

                if (max_dc.Occurence == equal_dc.Occurence)
                {
                    ConsoleInfo("There is no mode for this chart");
                }

                if (max_dc.Occurence > equal_dc.Occurence)
                {
                    ConsoleInfo($"Mode is: {max_dc.Occurence} for data point {max_dc.Data}");
                }
            }
        }

        /// <summary>
        /// This function will calculate mode for chart by name label
        /// </summary>
        /// <param name="chart_ID">chart Id</param>
        public void CalcModeColumnChartName(int chart_ID)
        {
            if (column_graphs.Count > 0)
            {
                if (column_graphs.Count > 0)
                {
                    ColumnGraph calculating_chart = new ColumnGraph();

                    calculating_chart = column_graphs[chart_ID];

                    List<DataCollective> dataCollectives = new List<DataCollective>();

                    for (int i = 0; i < calculating_chart.Columns.Count; i++)
                    {
                        DataCollective dc = new DataCollective();

                        dc.Id = i;
                        dc.Data = calculating_chart.Columns[i].DataPoint;
                        dc.Name = calculating_chart.Columns[i].Name;
                        dc.Occurence = 0;

                        for (int j = i + 1; j < calculating_chart.Columns.Count; j++)
                        {
                            if (dc.Name == calculating_chart.Columns[j].Name)
                            {
                                calculating_chart.Columns.RemoveAt(j);
                                dc.Occurence++;
                                j--;
                            }
                        }

                        dataCollectives.Add(dc);

                    }

                    DataCollective equal_dc = new DataCollective();
                    DataCollective max_dc = new DataCollective();
                    max_dc.Occurence = 0;


                    for (int i = 0; i < dataCollectives.Count; i++)
                    {
                        if (max_dc.Occurence == dataCollectives[i].Occurence)
                        {
                            equal_dc = dataCollectives[i];
                        }
                        else if (max_dc.Occurence < dataCollectives[i].Occurence)
                        {
                            max_dc = dataCollectives[i];
                        }
                    }


                    if (max_dc.Occurence == equal_dc.Occurence)
                    {
                        ConsoleInfo("There is no mode for this chart");
                    }

                    if (max_dc.Occurence > equal_dc.Occurence)
                    {
                        ConsoleInfo($"Mode is: {max_dc.Occurence} for name {max_dc.Name}");
                    }
                }
            }
        }
        #endregion

        #region Menu Toggler
        protected string menu_toggle_class = "menu_box_none";
        protected bool menu_toggler = false;

        /// <summary>
        /// This function will toggle menu screen
        /// </summary>
        public void ToggleMenu()
        {
            if(menu_toggler == false)
            {
                menu_toggler = true;
                menu_toggle_class = "menu_box";
            }
            else
            {
                menu_toggler = false;
                menu_toggle_class = "menu_box_none";
            }
        }

        #endregion

        #region All Commands Toggler
        protected string commands_toggle_class = "commands_box_none";
        protected bool commands_toggler = false;


        public void ToggleCommandsList()
        {
            if(commands_toggler == false)
            {
                commands_toggle_class = "commands_box";
                commands_toggler = true;
            }
            else
            {
                commands_toggle_class = "commands_box_none";
                commands_toggler = false;
            }
        }
        #endregion

        #region Help Functions
        /// <summary>
        /// This function will check if string == double
        /// </summary>
        /// <param name="text"></param>
        /// <returns>Is double / Is not double</returns>
        public bool IsDouble(string text)
        {
            Double num = 0;
            bool isDouble = false;

            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            isDouble = Double.TryParse(text, out num);

            return isDouble;
        }


        /// <summary>
        /// This function will check if string is an int
        /// </summary>
        /// <param name="sVal"></param>
        /// <returns></returns>
        private bool IsInt(string sVal)
        {
            foreach (char c in sVal)
            {
                int iN = (int)c;
                if ((iN > 57) || (iN < 48))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// This function will check if number is odd
        /// </summary>
        /// <param name="value">number to be checked</param>
        /// <returns></returns>
        public static bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
        #endregion


    }
}
