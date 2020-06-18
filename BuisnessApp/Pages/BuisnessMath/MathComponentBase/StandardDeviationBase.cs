using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessApp.Pages.BuisnessMath.MathComponentBase
{
    public class StandardDeviationBase:ComponentBase
    {

        //Declaring Variables for Razor Page

        //Simple StandardDeviation number for input
        protected double s_sd_number;
        //Declaring List for output string in razor page
        protected List<string> output_string = new List<string>();
        //Declaring List for mode objects
        protected List<double> simple_SD_data = new List<double>();

        //Variables for calculation
        //Mean
        protected double mean;
        //Declaring List for output string for sum of the squares of deviation of items from the mean
        protected List<string> output_string_sum_of_squars = new List<string>();
        //Sum of the sum of the squares of deviation of items from the mean
        protected double sum_of_squares = 0;
        //Simple Standard Deviation Result
        protected double simple_SD_result = 0;
        //Bool check if SD is beeing calculated and it shows calculation result
        protected bool is_simple_SD_calc = false;


        /// <param name="Add_Variable_Simple_Deviation">This function will add variable to simple_SD_data List and to output_string List</param>
        public async void Add_Variable_Simple_Deviation()
        {
            if (s_sd_number != null)
            {
                string output_partial = "";

                if (simple_SD_data.Count > 0)
                {
                    output_partial = output_partial + " , " + s_sd_number.ToString();
                    output_string.Add(output_partial);
                }
                else
                {
                    output_partial = output_partial + s_sd_number.ToString();
                    output_string.Add(output_partial);
                }

                simple_SD_data.Add(s_sd_number);
            }

            simple_SD_result = 0;
            s_sd_number = 0;
            output_string_sum_of_squars.Clear();
            is_simple_SD_calc = false;
            sum_of_squares = 0;
        }

        /// <param name="Reset_Simple_SD">This function will reset Simple Standard Deviation program</param>
        public async void Reset_Simple_SD()
        {
            output_string.Clear();
            output_string_sum_of_squars.Clear();
            simple_SD_data.Clear();
            simple_SD_result = 0;
            s_sd_number = 0;
            sum_of_squares = 0;
            is_simple_SD_calc = false;
        }

        /// <param name="Delete_Variable_Simple_SD">This function will delete last variable from simple_SD_data and output_string</param>
        public async void Delete_Variable_Simple_SD()
        {
            if (simple_SD_data.Count != 0)
            {
                simple_SD_data.RemoveAt(simple_SD_data.Count - 1);
                output_string.RemoveAt(output_string.Count - 1);
                simple_SD_result = 0;
                s_sd_number = 0;
                output_string_sum_of_squars.Clear();
                is_simple_SD_calc = false;
                sum_of_squares = 0;
            }
        }

        /// <param name="Calculate_Simple_SD">This function will Calculate simple standard deviation</param>
        public async void Calculate_Simple_SD()
        {
            if (simple_SD_data.Count != 0)
            {
                is_simple_SD_calc = true;

                //Calculating mean for SD
                double simple_SD_sum = 0;

                for (int i = 0; i < simple_SD_data.Count; i++)
                {
                    simple_SD_sum += simple_SD_data[i];
                }

                mean = simple_SD_sum / (simple_SD_data.Count);

                //Calculating sum of the squares of deviation of items from the mean
                for (int i = 0; i < simple_SD_data.Count; i++)
                {
                    sum_of_squares += Math.Pow(simple_SD_data[i] - mean, 2);
                }
                //Creating output string for sum of the squares of deviation of items from the mean
                for (int i = 0; i < simple_SD_data.Count; i++)
                {
                    if (i == 0)
                    {
                        string partial = "(" + simple_SD_data[i].ToString() + " - " + mean + ")^2 + ";
                        output_string_sum_of_squars.Add(partial);
                    }
                    else if (i == simple_SD_data.Count - 1)
                    {
                        string partial = "(" + simple_SD_data[i].ToString() + " - " + mean + ")^2";
                        output_string_sum_of_squars.Add(partial);
                    }
                    else
                    {
                        string partial = "(" + simple_SD_data[i].ToString() + " - " + mean + ")^2 + ";
                        output_string_sum_of_squars.Add(partial);
                    }
                }

                //Calculating Standard Deviation
                simple_SD_result = Math.Sqrt(sum_of_squares / simple_SD_data.Count);
            }
        }

        /////////////////////////////////////////////////////////////////////////
        ///

        //Standard Deviation for Simple Frequency Distibution Program

        /// <param name="SFDDC_Object">Simple Frequency Distribution Data Class declaration</param>
        public class SFDDC_Object
        {
            public int Id { get; set; }
            public double X { get; set; }

            // X - Mean 
            public double XmMean { get; set; }
            // (X - Mean)^2
            public double XmMeanSquare { get; set; }

            public SFDDC_Object()
            {
            }

            public SFDDC_Object(double x)
            {
                X = x;
                XmMean = 0;
                XmMeanSquare = 0;
            }
        }

        //This function will generate random number
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        //Text for example
        protected string SFD_Text_Example = "This will estimate Standard Deviation for ammount of pairs of shoes sold daily by diffrent shops. Number of sold shoes (x)";

        //Declaring List for SFDDC_Object's
        protected List<SFDDC_Object> simple_frequency_data_SD = new List<SFDDC_Object>();
        //Checking if random generated data is used
        protected bool isRandom_SFD = false;
        //Checking if custom data is used
        protected bool isCustom_SFD = false;
        //Checking if Standard Deviation is calculated
        protected bool isCalculated_SFD = false;

        //Text for button which generates mock data for simple_frequency_data_median
        protected string btn_s_f_text = "See example";
        //Variables for custom input
        protected double x_SFD;

        //Counter for total x
        protected double total_X_SFD = 0;
        //Counter for total (x - mean)^2
        protected double total_XmMeanSquareSFF = 0;
        //(x-mean)^2 Total sum
        protected double SFD_xmMean_total = 0;

        //If there is no median found, or if is found this infromtation will be shown
        protected string is_Mode_SFD = "";
        protected bool is_Mode_B_SFD = true;

        //Mean for SFD
        protected double mean_SFD = 0;



        //Standard Deviation Result
        protected double SFD_SD_Result;

        //This bool will check if Standard Deviation is beeing calculated
        protected bool SFD_SD_isCalc = false;

        protected SFDDC_Object result_object = new SFDDC_Object();

        /// <param name="Add_C_SFDO">This function will add Simple Frequency Distribution Object to simple_frequency_data_SD List</param>
        protected async void Add_C_SFDO()
        {
            isCustom_SFD = true;
            total_X_SFD = 0;
            mean_SFD = 0;
            SFD_SD_isCalc = false;


            if (simple_frequency_data_SD.Count == 0)
            {
                SFDDC_Object obj = new SFDDC_Object(x_SFD);
                obj.Id = simple_frequency_data_SD.Count;
                obj.XmMean = obj.X;
                obj.XmMeanSquare = Math.Pow(obj.X, 2);
                simple_frequency_data_SD.Add(obj);
            }
            else
            {
                SFDDC_Object obj = new SFDDC_Object(x_SFD);
                obj.Id = simple_frequency_data_SD.Count;
                simple_frequency_data_SD.Add(obj);
                //Calculating mean
                //Calculating x - mean
                //Calculating (x-mean)^2
                for (int i = 0; i < simple_frequency_data_SD.Count; i++)
                {
                    total_X_SFD += simple_frequency_data_SD[i].X;
                }
                mean_SFD = total_X_SFD / (simple_frequency_data_SD.Count);

                for (int i = 0; i < simple_frequency_data_SD.Count; i++)
                {
                    simple_frequency_data_SD[i].XmMean = Math.Round(simple_frequency_data_SD[i].X - mean_SFD, 3);
                    simple_frequency_data_SD[i].XmMeanSquare = Math.Round(Math.Pow(simple_frequency_data_SD[i].XmMean, 2), 3);
                    SFD_xmMean_total += Math.Round(simple_frequency_data_SD[i].X - mean_SFD, 3);
                    SFD_xmMean_total += Math.Round(Math.Pow(simple_frequency_data_SD[i].XmMean, 2), 3);
                }
            }
            isCalculated_SFD = false;
            x_SFD = 0;
        }

        /// <param name="Delete_C_SFDO">This function will delete last Simple Frequency Distribution Object from simple_frequency_data_SD List</param>
        protected async void Delete_C_SFDO()
        {
            if (simple_frequency_data_SD.Count != 0)
            {
                int lenght = simple_frequency_data_SD.Count;
                simple_frequency_data_SD.RemoveAt(lenght - 1);

                //Reset Id numbers
                for (int i = 0; i < simple_frequency_data_SD.Count; i++)
                {
                    simple_frequency_data_SD[i].Id = i;
                }
            }
            if (simple_frequency_data_SD.Count == 0)
            {
                isCustom_SFD = false;
            }
            is_Mode_B_SFD = true;
            total_X_SFD = 0;
            result_object.X = 0;
            SFD_SD_isCalc = false;
            isCalculated_SFD = false;

        }

        /// <param name="Delete_SFDO_ByID">This function will delete Simple Frequency Distribution Object by its Id from simple_frequency_data_SD List</param>
        protected async void Delete_SFDO_ByID(int id)
        {
            if (simple_frequency_data_SD.Count != 0)
            {
                simple_frequency_data_SD.RemoveAt(id);

                //Reset Id numbers
                for (int i = 0; i < simple_frequency_data_SD.Count; i++)
                {
                    simple_frequency_data_SD[i].Id = i;
                }

                if (simple_frequency_data_SD.Count == 0)
                {
                    isRandom_SFD = false;
                    isCustom_SFD = false;

                    btn_s_f_text = "See example";
                }
                total_X_SFD = 0;
                result_object.X = 0;
                SFD_SD_isCalc = false;
                isCalculated_SFD = false;
            }
        }

        /// <param name="Reset_SFD">This function will reset Simple Frequency Distribution Standard Deviation Program</param>
        protected async void Reset_SFD()
        {
            simple_frequency_data_SD.Clear();
            isCustom_SFD = false;
            isRandom_SFD = false;
            isCalculated_SFD = false;
            SFD_SD_isCalc = false;

            total_XmMeanSquareSFF = 0;
            SFD_xmMean_total = 0;
            btn_s_f_text = "See example";
            total_X_SFD = 0;
            x_SFD = 0;

        }


        /// <param name="Generate_Simple_Freq_D">This function will generate mock data for simple_frequency_data_SD List</param>
        protected async void Generate_Simple_Freq_D()
        {
            simple_frequency_data_SD.Clear();
            double total = 0;
            for (int i = 1; i <= 10; i++)
            {
                SFDDC_Object obj = new SFDDC_Object(RandomNumber(1 * 2, 1 * 100));
                obj.Id = simple_frequency_data_SD.Count;
                total += obj.X;
                simple_frequency_data_SD.Add(obj);
            }

            mean_SFD = total / (simple_frequency_data_SD.Count - 1);

            for (int i = 0; i < simple_frequency_data_SD.Count; i++)
            {
                simple_frequency_data_SD[i].XmMean = Math.Round(simple_frequency_data_SD[i].X - mean_SFD, 2);
                simple_frequency_data_SD[i].XmMeanSquare = Math.Round(Math.Pow(simple_frequency_data_SD[i].XmMean, 2), 2);
                total_X_SFD += simple_frequency_data_SD[i].X;
            }


            isRandom_SFD = true;
            btn_s_f_text = "Reset Data";
            result_object.X = 0;
            SFD_SD_isCalc = false;
            isCalculated_SFD = false;


        }

        /// <param name="Calc_SFD_StandardDeviation">This function will calculate Standard Deviation for Simple Frequency Distribution</param>
        protected async void Calc_SFD_StandardDeviation()
        {
            if (simple_frequency_data_SD.Count != 0)
            {
                for (int i = 0; i < simple_frequency_data_SD.Count; i++)
                {
                    SFD_xmMean_total += simple_frequency_data_SD[i].XmMean;
                    total_XmMeanSquareSFF += simple_frequency_data_SD[i].XmMeanSquare;
                }

                SFD_xmMean_total = Math.Round(SFD_xmMean_total, 3);
                total_XmMeanSquareSFF = Math.Round(total_XmMeanSquareSFF, 3);
                mean_SFD = Math.Round(mean_SFD, 3);

                SFD_SD_Result = Math.Sqrt(total_XmMeanSquareSFF / (simple_frequency_data_SD.Count - 1));
                SFD_SD_Result = Math.Round(SFD_SD_Result, 3);
                SFD_SD_isCalc = true;
                isCalculated_SFD = true;

            }
        }


        ///////////////////////////////
        ///

        //Standard Deviation for Grouped Distribution Program
        /// <param name="GFDDC_Object">Grouped Frequency Distribution Data Class declaration</param>
        public class GFDDC_Object
        {
            public int Id { get; set; }
            public double Range1 { get; set; }
            public double Range2 { get; set; }
            public double F { get; set; }
            //Mid Point
            public double X { get; set; }
            public double Fx { get; set; }
            public double FxSquare { get; set; }

            public GFDDC_Object()
            {

            }

            public GFDDC_Object(double r1, double r2, double f)
            {
                Range1 = r1;
                Range2 = r2;
                F = f;
            }

        }


        //Declaring Mode variables for a Grouped Frequency Distribution Razor Page

        //Text for example
        protected string GFD_Text_Example = "This example shows number of producs bought by people in a grocery store. Number of items(Range). Ammount of people(f). ";

        //Declaring List for GFDDC_Object's
        protected List<GFDDC_Object> grouped_frequency_data_SD = new List<GFDDC_Object>();
        //Checking if random generated data is used
        protected bool isRandom_GFD = false;
        //Checking if custom data is used
        protected bool isCustom_GFD = false;
        //If to show formula
        protected bool isGDF_Calculated = false;
        //Text for button which generates mock data for grouped_frequency_data_median
        protected string btn_g_f_text = "See example";
        //Variables for custom input
        protected double range1;
        protected double range2;
        protected double f_GFD;

        //Function Variables//////////////////////////////
        //Total f
        protected double totalF_GFD;
        //Total x (mid points)
        protected double totalX_GFD;
        //Total Fx
        protected double totalfX_GFD;
        //Total FxSquare
        protected double totalfXSquare_GFD;

        //Variable for Standard Deviation Result
        protected double StandardDeviation_GFD_Result;


        public async void Add_C_GFDO()
        {
            if (grouped_frequency_data_SD.Count == 0)
            {
                GFDDC_Object obj = new GFDDC_Object(range1, range2, f_GFD);
                obj.Id = grouped_frequency_data_SD.Count;
                obj.X = obj.Range2 - ((obj.Range2 - obj.Range1) / 2);
                obj.Fx = obj.X * obj.F;
                obj.FxSquare = obj.X * obj.Fx;

                //Adding to totals
                totalF_GFD += obj.F;
                totalfX_GFD += obj.Fx;
                totalfXSquare_GFD += obj.FxSquare;
                totalX_GFD += obj.X;

                grouped_frequency_data_SD.Add(obj);
                range1 = 0;
                range2 = 0;
                f_GFD = 0;
                isCustom_GFD = true;
            }
            else
            {
                int lenght = grouped_frequency_data_SD.Count;
                GFDDC_Object temp = grouped_frequency_data_SD[0];
                GFDDC_Object temp2 = grouped_frequency_data_SD[lenght - 1];

                double r1 = temp2.Range2 + 1;
                double r2 = r1 + (temp.Range2 - temp.Range1) - 1;

                GFDDC_Object obj = new GFDDC_Object(r1, r2, f_GFD);
                obj.Id = grouped_frequency_data_SD.Count;
                obj.X = obj.Range2 - ((obj.Range2 - obj.Range1) / 2);
                obj.Fx = obj.X * obj.F;
                obj.FxSquare = obj.X * obj.Fx;

                //Adding to totals
                totalF_GFD += obj.F;
                totalfX_GFD += obj.Fx;
                totalfXSquare_GFD += obj.FxSquare;
                totalX_GFD += obj.X;

                grouped_frequency_data_SD.Add(obj);
                range1 = 0;
                range2 = 0;
                f_GFD = 0;
            }
        }


        public async void Delete_C_GFDO()
        {
            if (grouped_frequency_data_SD.Count != 0)
            {
                int lenght = grouped_frequency_data_SD.Count;

                //Substracting from totals
                totalF_GFD -= grouped_frequency_data_SD[lenght - 1].F;
                totalfX_GFD -= grouped_frequency_data_SD[lenght - 1].Fx;
                totalfXSquare_GFD -= grouped_frequency_data_SD[lenght - 1].FxSquare;
                totalX_GFD -= grouped_frequency_data_SD[lenght - 1].X;

                grouped_frequency_data_SD.RemoveAt(lenght - 1);

                //Reset Id numbers
                for (int i = 0; i < grouped_frequency_data_SD.Count; i++)
                {
                    grouped_frequency_data_SD[i].Id = i;
                }
            }
            if (grouped_frequency_data_SD.Count == 0)
            {
                isCustom_GFD = false;
            }

            range1 = 0;
            range2 = 0;
            f_GFD = 0;
            isGDF_Calculated = false;
        }

        public async void Delete_GDFDO_ByID(int id)
        {
            if (grouped_frequency_data_SD.Count != 0)
            {
                //Substracting from totals
                totalF_GFD -= grouped_frequency_data_SD[id].F;
                totalfX_GFD -= grouped_frequency_data_SD[id].Fx;
                totalfXSquare_GFD -= grouped_frequency_data_SD[id].FxSquare;
                totalX_GFD -= grouped_frequency_data_SD[id].X;

                grouped_frequency_data_SD.RemoveAt(id);

                //Reset Id numbers
                for (int i = 0; i < grouped_frequency_data_SD.Count; i++)
                {
                    grouped_frequency_data_SD[i].Id = i;
                }

                if (grouped_frequency_data_SD.Count == 0)
                {
                    isRandom_GFD = false;
                    isCustom_GFD = false;

                    btn_g_f_text = "See example";
                }
                range1 = 0;
                range2 = 0;
                f_GFD = 0;
                isGDF_Calculated = false;

            }
        }

        public async void RESET_GFD()
        {
            grouped_frequency_data_SD.Clear();
            isCustom_GFD = false;
            isRandom_GFD = false;

            totalF_GFD = 0;
            totalfX_GFD = 0;
            totalfXSquare_GFD = 0;
            totalX_GFD = 0;

            range1 = 0;
            range2 = 0;
            f_GFD = 0;
            isGDF_Calculated = false;
            btn_g_f_text = "See example";
        }

        public async void Generate_Grouped_Freq_D()
        {
            grouped_frequency_data_SD.Clear();
            isRandom_GFD = true;
            int r1 = 5;
            int r2 = 10;

            for (int i = 0; i <= 10; i++)
            {
                if (grouped_frequency_data_SD.Count == 0)
                {
                    double f = RandomNumber(100, 1000);
                    GFDDC_Object obj = new GFDDC_Object();
                    obj.Range1 = r1;
                    obj.Range2 = r2;
                    obj.F = f;
                    obj.Id = grouped_frequency_data_SD.Count;
                    obj.X = (obj.Range1 + obj.Range2) / 2;
                    obj.Fx = obj.X * obj.F;
                    obj.FxSquare = obj.X * obj.Fx;


                    //Adding to totals
                    totalF_GFD += obj.F;
                    totalfX_GFD += obj.Fx;
                    totalfXSquare_GFD += obj.FxSquare;
                    totalX_GFD += obj.X;

                    grouped_frequency_data_SD.Add(obj);

                }
                else
                {
                    int lenght = grouped_frequency_data_SD.Count;
                    GFDDC_Object temp = grouped_frequency_data_SD[lenght - 1];
                    double f = RandomNumber(100, 1000);
                    GFDDC_Object obj = new GFDDC_Object();
                    obj.Range1 = temp.Range2 + 1;
                    obj.Range2 += temp.Range2 + 5;

                    obj.F = f;
                    obj.Id = grouped_frequency_data_SD.Count;
                    obj.X = (obj.Range1 + obj.Range2) / 2;
                    obj.Fx = obj.X * obj.F;
                    obj.FxSquare = obj.X * obj.Fx;

                    //Adding to totals
                    totalF_GFD += obj.F;
                    totalfX_GFD += obj.Fx;
                    totalfXSquare_GFD += obj.FxSquare;
                    totalX_GFD += obj.X;


                    grouped_frequency_data_SD.Add(obj);
                }
            }
            btn_g_f_text = "Reset Data";
        }

        public async void Calculate_GFDSD()
        {
            if (grouped_frequency_data_SD.Count != 0)
            {
                isGDF_Calculated = true;
                StandardDeviation_GFD_Result = Math.Sqrt((totalfXSquare_GFD / totalF_GFD) - Math.Pow((totalfX_GFD / totalF_GFD), 2));
            }
        }
    }
}
