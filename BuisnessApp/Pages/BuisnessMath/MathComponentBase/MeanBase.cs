using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessApp.Pages.BuisnessMath.MathComponentBase
{
    public class MeanBase:ComponentBase
    {
        //Basic aritmhmetic mean

        //This class provides output string obj for the first mean
        public class Output_String_Mean1
        {
            public string Variable { get; set; }

            public Output_String_Mean1(string variable)
            {
                Variable = variable;
            }
        }

        //Variables for 1st calculation
        protected List<double> numbers_calc_1 = new List<double>();
        //Variables output string
        protected List<Output_String_Mean1> output_string = new List<Output_String_Mean1>();

        //This bool checks if mean is beeing calculated
        protected bool isCalcMean = false;

        protected double number_1;
        protected double ammount_1;
        protected double result_1;
        public void Add_Number_1()
        {
            if (number_1 != null)
            {
                numbers_calc_1.Add(number_1);
                string output_partial = "";

                if (ammount_1 != 0)
                {
                    output_partial = output_partial + " + " + number_1.ToString();
                    Output_String_Mean1 obj = new Output_String_Mean1(output_partial);
                    output_string.Add(obj);
                }
                else
                {
                    output_partial = output_partial + number_1.ToString();
                    Output_String_Mean1 obj = new Output_String_Mean1(output_partial);
                    output_string.Add(obj);
                }

                result_1 = 0;
                number_1 = 0;
                ammount_1 = numbers_calc_1.Count();
                isCalcMean = false;
            }
        }

        /// <summary>
        /// This function will be called to calculate mean anywhere in the program
        /// </summary>
        /// <param name="CalculateMean">Calculates mean</param>
        /// <returns>Mean</returns>
        public double CalculateMean(List<double> numbers)
        {
            double result = 0;

            for (int i = 0; i < numbers.Count; i++)
            {
                result = result + numbers[i];
            }

            result = result / numbers.Count;

            return result;
        }
        ///................................................................................///


        /// <summary>
        /// This function will calculate mean for the 1st example
        /// </summary>
        /// <param name="Calculate_First_Mean">Calculates mean for the 1st example</param>
        /// <returns>Mean</returns>
        public void Calculate_First_Mean()
        {
            result_1 = CalculateMean(numbers_calc_1);
            isCalcMean = true;
        }
        //..................................................................//

        //This function will delete last object
        public void Delete_First_Mean()
        {
            if (numbers_calc_1.Count != 0)
            {
                output_string.RemoveAt(output_string.Count - 1);
                numbers_calc_1.RemoveAt(numbers_calc_1.Count - 1);
                ammount_1--;
                isCalcMean = false;
            }
        }

        //This function will clear List<double> for the 1st example (numbers_calc_1)
        public void Reset_1()
        {
            output_string.Clear();
            ammount_1 = 0;
            result_1 = 0;
            numbers_calc_1.Clear();
            isCalcMean = false;
        }
        //.........................................................

        //////
        ///This section is for calculation of mean for simple frequency distribution mean
        ///
        /// <param name="Simple_Frequency_Data_Object">This object will hold data of 1 row in table</param>
        public class Simple_Frequency_Data_Object
        {
            public int Id { get; set; }
            public double X { get; set; }
            public double F { get; set; }
            public double Fx { get; set; }

            public Simple_Frequency_Data_Object()
            {
                X = 0;
                F = 0;
                Fx = X * F;
            }

            public Simple_Frequency_Data_Object(double x, double f)
            {
                X = x;
                F = f;
                Fx = x * f;
            }
        }

        //Text for example
        protected string SFD_Text_Example = "Lets say there is number of computers servicable(x) going trough number of days (f)";

        //Declaring variables for razor
        protected List<Simple_Frequency_Data_Object> simple_Frequency_Data_Objects = new List<Simple_Frequency_Data_Object>();
        protected string btn_s_f_text = "See example";
        //Simple Frequency X
        protected double SFX;
        //Simple Frequency f
        protected double SFF;
        //This Bool will chceck if user ads custom input and if so then it will hide random data generator button
        protected bool isCustom_SFD = false;
        //This Bool will chceck if user uses random generator and if so then it will hide add custom data button
        protected bool isRandom_SFD = false;
        //This bool will check if mean is beeing calculated
        protected bool isCalculated_SFD = false;

        //Simple Frequency X for editing
        protected double SFX_E;
        //Simple Frequency f for editing
        protected double SFF_E;

        protected double f_total = 0;
        protected double fx_total = 0;
        /// <param name="CSFDM_Result">Result for Calc_Simple_Frequency_Distribution_Mean</param>
        protected double CSFDM_Result = 0;

        /// <param name="Add_C_SFDO">This function will add Custom Simple_Frequency_Data_Object to the custom list simple_Frequency_Data_Objects</param>
        public void Add_C_SFDO()
        {
            Simple_Frequency_Data_Object obj = new Simple_Frequency_Data_Object(SFX, SFF);
            obj.Id = simple_Frequency_Data_Objects.Count;
            simple_Frequency_Data_Objects.Add(obj);
            SFX = 0;
            SFF = 0;
            isCustom_SFD = true;
            f_total = 0;
            fx_total = 0;
            CSFDM_Result = 0;
            isCalculated_SFD = false;
        }

        /// <param name="Delete_C_SFDO">This function will delete last Custom Simple_Frequency_Data_Object from custom list simple_Frequency_Data_Objects</param>
        public void Delete_C_SFDO()
        {
            if (simple_Frequency_Data_Objects.Count != 0)
            {
                simple_Frequency_Data_Objects.RemoveAt(simple_Frequency_Data_Objects.Count - 1);

                //Reset Id numbers
                for (int i = 0; i < simple_Frequency_Data_Objects.Count; i++)
                {
                    simple_Frequency_Data_Objects[i].Id = i;
                }

                if (simple_Frequency_Data_Objects.Count == 0)
                {
                    isCustom_SFD = false;
                }
            }
            f_total = 0;
            fx_total = 0;
            isCalculated_SFD = false;
            CSFDM_Result = 0;
        }

        /// <param name="Delete_SFDO_ByID">This function will delete Simple_Frequency_Data_Object from custom list simple_Frequency_Data_Objects by its Id number</param>
        public void Delete_SFDO_ByID(int id)
        {
            if (simple_Frequency_Data_Objects.Count != 0)
            {
                simple_Frequency_Data_Objects.RemoveAt(id);

                //Reset Id numbers
                for (int i = 0; i < simple_Frequency_Data_Objects.Count; i++)
                {
                    simple_Frequency_Data_Objects[i].Id = i;
                }

                if (simple_Frequency_Data_Objects.Count == 0)
                {
                    isCustom_SFD = false;
                    btn_s_f_text = "See example";

                }
            }
            f_total = 0;
            fx_total = 0;
            CSFDM_Result = 0;
            isCalculated_SFD = false;

        }


        /// <param name="RandomNumber">This function will generate random number between min-max</param>
        /// <returns>Random number</returns>
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        /// <param name="Generate_Simple_Freq_D">This function will crate mock data for the table</param>
        /// <returns>Mock Data</returns>
        public void Generate_Simple_Freq_D()
        {
            simple_Frequency_Data_Objects.Clear();
            btn_s_f_text = "Reset Data";
            for (int i = 0; i <= 10; i++)
            {
                Simple_Frequency_Data_Object obj =
                    new Simple_Frequency_Data_Object(i, RandomNumber(20, 200));
                obj.Id = i;
                simple_Frequency_Data_Objects.Add(obj);
            }
            f_total = 0;
            f_total = 0;
            CSFDM_Result = 0;
            isRandom_SFD = true;
            isCalculated_SFD = false;

        }



        public void Calc_Simple_Frequency_Distribution_Mean()
        {
            if (simple_Frequency_Data_Objects != null && simple_Frequency_Data_Objects.Count != 0)
            {
                for (int i = 0; i < simple_Frequency_Data_Objects.Count; i++)
                {
                    f_total += simple_Frequency_Data_Objects[i].F;
                    fx_total += simple_Frequency_Data_Objects[i].Fx;
                }

                CSFDM_Result = fx_total / f_total;
                isCalculated_SFD = true;

            }
        }
        /// <param name="RESET_SFD">This function resets Simple Frequency Distribution program</param>
        public async void RESET_SFD()
        {
            simple_Frequency_Data_Objects.Clear();
            isRandom_SFD = false;
            isCustom_SFD = false;
            isCalculated_SFD = false;
            f_total = 0;
            fx_total = 0;
            CSFDM_Result = 0;
        }

        //.................................................................................

        //////
        ///This section is for calculation of mean for grouped discrete frequency distribution mean
        ///
        /// <param name="Simple_Frequency_Data_Object">This object will hold data of 1 row in table</param>
        public class Grouped_Discrete_Frequency_Data_Object
        {
            public int Id { get; set; }
            public double Range_1 { get; set; }
            public double Range_2 { get; set; }
            public double X { get; set; }
            public double F { get; set; }
            public double Fx { get; }

            public Grouped_Discrete_Frequency_Data_Object()
            {
                X = 0;
                F = 0;
                Fx = X * F;
            }

            public Grouped_Discrete_Frequency_Data_Object(double f, double range1, double range2)
            {
                Range_1 = range1;
                Range_2 = range2;
                X = (Range_1 + Range_2) / 2;
                F = f;
                Fx = X * f;
            }
        }

        //Text for example
        protected string GDFD_Text_Example = "Fallowing data shows number of sales made by mobile strores in  particular quater. Number of sales(Range). Number of shops (f).";

        //Declaring variables for razor
        protected List<Grouped_Discrete_Frequency_Data_Object> grouped_Discrete_Frequency_Data_Objects = new List<Grouped_Discrete_Frequency_Data_Object>();
        protected string btn_d_f_text = "See example";
        //Grouped Discrete Range 1 
        protected double GDFR1;
        //Grouped Discrete Range 2 
        protected double GDFR2;
        //Grouped Discrete f
        protected double GDFF;
        //This Bool will chceck if user ads custom input and if so then it will hide random data generator button
        protected bool isCustom_GDFD = false;
        //This Bool will chceck if user uses random generator and if so then it will hide add custom data button
        protected bool isRandom_GDFD = false;
        //This Bool will check if mean is beeing calculated
        protected bool isCalculated_GFD = false;

        //This error_GD will show error message for incorrect user input
        protected string error_GD = "";

        protected double f_d_total = 0;
        protected double fx_d_total = 0;
        /// <param name="CDFDM_Result">Result for Calc_Discrete_Frequency_Distribution_Mean</param>
        protected double CDFDM_Result = 0;

        /// <param name="Add_C_GDDO">This function will add Custom Grouped_Discrete_Frequency_Data_Object to the custom list simple_Frequency_Data_Objects</param>
        public void Add_C_GDDO()
        {
            error_GD = "";
            if (GDFR1 < GDFR2)
            {
                Grouped_Discrete_Frequency_Data_Object obj = new Grouped_Discrete_Frequency_Data_Object(GDFF, GDFR1, GDFR2);
                obj.Id = grouped_Discrete_Frequency_Data_Objects.Count;
                grouped_Discrete_Frequency_Data_Objects.Add(obj);
                GDFR1 = GDFR2;
                GDFR2 = GDFR2 + obj.Range_1;
                GDFF = 0;
                isCustom_GDFD = true;
            }
            else if (GDFR1 == GDFR2)
            {
                error_GD = "Range 1 cannot equal Range 2!";
            }
            else
            {
                error_GD = "Range 1 needs to be higher from Range 2 in previous row!";
            }

            isCalculated_GFD = false;
            fx_d_total = 0;
            f_d_total = 0;
            CDFDM_Result = 0;
        }

        /// <param name="Delete_C_GDFDO">This function will delete last Custom Grouped_Discrete_Frequency_Data_Object from custom list simple_Frequency_Data_Objects</param>
        public void Delete_C_GDFDO()
        {
            if (grouped_Discrete_Frequency_Data_Objects.Count != 0)
            {
                int lenght = grouped_Discrete_Frequency_Data_Objects.Count;
                double rangeFix = grouped_Discrete_Frequency_Data_Objects[lenght - 1].Range_2 - grouped_Discrete_Frequency_Data_Objects[lenght - 1].Range_1;
                GDFR1 = GDFR1 - rangeFix;
                GDFR2 = GDFR2 - rangeFix;
                grouped_Discrete_Frequency_Data_Objects.RemoveAt(lenght - 1);

                //Reset Id numbers
                for (int i = 0; i < grouped_Discrete_Frequency_Data_Objects.Count; i++)
                {
                    grouped_Discrete_Frequency_Data_Objects[i].Id = i;
                }


                if (grouped_Discrete_Frequency_Data_Objects.Count == 0)
                {
                    isCustom_GDFD = false;
                    GDFR1 = 0;
                    GDFR2 = 0;
                }

            }
            isCalculated_GFD = false;
            f_d_total = 0;
            fx_d_total = 0;
            CDFDM_Result = 0;
        }

        /// <param name="Delete_SFDO_ByID">This function will Grouped_Discrete_Frequency_Data_Object from custom list grouped_Discrete_Frequency_Data_Objects by its Id number</param>
        public void Delete_GDFDO_ByID(int id)
        {
            if (grouped_Discrete_Frequency_Data_Objects.Count != 0)
            {
                grouped_Discrete_Frequency_Data_Objects.RemoveAt(id);

                //Reset Id numbers
                for (int i = 0; i < grouped_Discrete_Frequency_Data_Objects.Count; i++)
                {
                    grouped_Discrete_Frequency_Data_Objects[i].Id = i;
                }

                if (grouped_Discrete_Frequency_Data_Objects.Count == 0)
                {
                    isCustom_GDFD = false;
                    GDFR1 = 0;
                    GDFR2 = 0;
                    btn_d_f_text = "See example";
                }
                isCalculated_GFD = false;
                f_d_total = 0;
                fx_d_total = 0;
                CDFDM_Result = 0;
            }
        }




        /// <param name="Generate_Discrete_Frequency_D">This function will crate mock data for the table</param>
        /// <returns>Mock Data</returns>
        public void Generate_Discrete_Frequency_D()
        {
            grouped_Discrete_Frequency_Data_Objects.Clear();
            btn_d_f_text = "Reset Data";
            int _range1 = 0;
            int _range2 = 5;

            for (int i = 0; i <= 10; i++)
            {
                Grouped_Discrete_Frequency_Data_Object obj =
                    new Grouped_Discrete_Frequency_Data_Object(RandomNumber(20, 200), _range1, _range2);
                obj.Id = i;
                grouped_Discrete_Frequency_Data_Objects.Add(obj);

                _range1 += 5;
                _range2 += 5;
            }
            isRandom_GDFD = true;
            isCalculated_GFD = false;
            f_d_total = 0;
            fx_d_total = 0;
            CDFDM_Result = 0;
        }




        public void Calc_Discrete_Frequency_Distribution_Mean()
        {
            if (grouped_Discrete_Frequency_Data_Objects != null && grouped_Discrete_Frequency_Data_Objects.Count != 0)
            {
                for (int i = 0; i < grouped_Discrete_Frequency_Data_Objects.Count; i++)
                {
                    f_d_total += grouped_Discrete_Frequency_Data_Objects[i].F;
                    fx_d_total += grouped_Discrete_Frequency_Data_Objects[i].Fx;
                }
                CDFDM_Result = fx_d_total / f_d_total;
                isCalculated_GFD = true;

            }
        }

        /// <param name="RESET_GDFD">This function resets Grouped Discrete Frequency Distribution program</param>
        public async void RESET_GDFD()
        {
            grouped_Discrete_Frequency_Data_Objects.Clear();
            isRandom_GDFD = false;
            isCustom_GDFD = false;
            isCalculated_GFD = false;
            btn_d_f_text = "See example";
            GDFR1 = 0;
            f_d_total = 0;
            GDFR2 = 0;
            fx_d_total = 0;
            CDFDM_Result = 0;
        }
    }
}
