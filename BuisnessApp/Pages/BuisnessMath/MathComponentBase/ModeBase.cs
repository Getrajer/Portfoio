using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuisnessApp.Pages.BuisnessMath.MathComponentBase
{
    public class ModeBase:ComponentBase
    {
        //Simple mode program


        /// <param name="Simple_Mode_Object">This class will store objects which are going to be used to displey data</param>
        public class Simple_Mode_Object
        {
            public double Number { get; set; }
            public int Occurence { get; set; }

            public Simple_Mode_Object(double number, int occurence)
            {
                Number = number;
                Occurence = occurence;
            }
        }

        //Declaring Variables for Razor Page

        //List which will store variables
        protected List<double> mode_data_list = new List<double>();
        //s_m_number will be connected to the adding variable function
        protected double s_m_number;

        //Declaring List for mode objects
        protected List<Simple_Mode_Object> mode_variables = new List<Simple_Mode_Object>();

        //If there is no median found, or if is found this infromtation will be shown
        protected string is_Mode = "";
        protected bool is_Mode_B = true;
        //Bool to chceck if mode is beeing calculated
        protected bool isModeCalculated = false;

        //s_m_result will show median number 
        protected double s_m_result = 0;
        //s_m_o_result will show median number occurence
        protected int s_m_o_result = 0;

        /// <param name="Add_Variable_Simple_Mode">This function will add variable to mode_data_list</param>
        public void Add_Variable_Simple_Mode()
        {
            if (s_m_number != null)
            {

                mode_data_list.Add(s_m_number);

                is_Mode_B = false;
                is_Mode = "";
                s_m_number = 0;
                s_m_result = 0;
                s_m_o_result = 0;
                mode_variables.Clear();
            }

            isModeCalculated = false;

        }

        /// <param name="Delete_Variable_Simple_Mode">This function will delete last variable from mode_data_list</param>
        public void Delete_Variable_Simple_Mode()
        {
            if (mode_data_list.Count > 0)
            {
                int lenght = mode_data_list.Count;
                mode_data_list.RemoveAt(lenght - 1);
                is_Mode = "";
                mode_variables.Clear();
                s_m_result = 0;
                s_m_o_result = 0;
                is_Mode_B = true;
                isModeCalculated = false;
            }

        }

        /// <param name="Reset_Simple_Mode">This function will reset simple mode program</param>
        public void Reset_Simple_Mode()
        {
            mode_data_list.Clear();
            s_m_result = 0;
            s_m_o_result = 0;
            mode_variables.Clear();
            is_Mode = "";
            is_Mode_B = true;
            isModeCalculated = false;
        }

        /// <param name="Calculate_Simple_Mode">This function will calculate simple mode</param>
        public void Calculate_Simple_Mode()
        {
            if (mode_data_list.Count != 0)
            {
                is_Mode = "";
                s_m_result = 0;
                s_m_o_result = 0;


                List<double> temp_data_list = new List<double>();

                for (int i = 0; i < mode_data_list.Count; i++)
                {
                    temp_data_list.Add(mode_data_list[i]);
                }

                //Count variables
                while (temp_data_list.Count != 0)
                {
                    int occurence = 0;
                    Simple_Mode_Object obj = new Simple_Mode_Object(temp_data_list[0], occurence);

                    for (int j = 0; j < temp_data_list.Count; j++)
                    {
                        if (obj.Number == temp_data_list[j])
                        {

                            occurence++;
                            temp_data_list.RemoveAt(j);
                            j--;
                        }
                    }
                    obj.Occurence = occurence;
                    mode_variables.Add(obj);
                }

                int result_occurence = 0;

                //Find Mode
                for (int i = 0; i < mode_variables.Count; i++)
                {
                    result_occurence = mode_variables[i].Occurence;
                    if (s_m_o_result < result_occurence)
                    {
                        s_m_o_result = mode_variables[i].Occurence;
                        s_m_result = mode_variables[i].Number;
                    }
                }

                //If there is more than one variable occuring same ammount of times
                int counter = 0;

                for (int i = 0; i < mode_variables.Count; i++)
                {

                    if (result_occurence == mode_variables[i].Occurence)
                    {
                        counter++;
                    }


                }

                if (counter > 2)
                {
                    is_Mode_B = false;
                    is_Mode = "There is more than one number occuting same ammount of times. Check table for more details.";
                }
                else
                {
                    is_Mode_B = true;
                }

                isModeCalculated = true;
            }
        }

        /////////////////////////////////////////////////////////////////////////
        ///

        //Mode for Simple Frequency Distibution Program

        /// <param name="SFDDC_Object">Simple Frequency Distribution Data Class declaration</param>
        public class SFDDC_Object
        {
            public int Id { get; set; }
            public double X { get; set; }
            public int F { get; set; }

            public SFDDC_Object()
            {
            }

            public SFDDC_Object(double x, int f)
            {
                X = x;
                F = f;
            }
        }

        //This function will generate random number
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        //Text for example
        protected string SFD_Text_Example = "This will find mode for data which product is most popular form given data. Product (x). Number of sales (f)";

        //Declaring List for SFDDC_Object's
        protected List<SFDDC_Object> simple_frequency_data_mode = new List<SFDDC_Object>();
        //Checking if random generated data is used
        protected bool isRandom_SFD = false;
        //Checking if custom data is used
        protected bool isCustom_SFD = false;
        //Checking if mode is calculated
        protected bool isCalculated_SFD = false;

        //Text for button which generates mock data for simple_frequency_data_median
        protected string btn_s_f_text = "See example";
        //Variables for custom input
        protected double x_SFD;
        protected int f_SFD;

        //Counters for total f and total x
        protected double total_X_SFD;
        protected double total_F_SFD;

        //If there is no median found, or if is found this infromtation will be shown
        protected string is_Mode_SFD = "";
        protected bool is_Mode_B_SFD = true;

        protected SFDDC_Object result_object = new SFDDC_Object();


        //Declaring variables for Simple Frequency Distibution to Razor

        /// <param name="Add_C_SFDO">This function will add custom variable to List simple_frequency_data_mode </param>
        public void Add_C_SFDO()
        {
            isCustom_SFD = true;

            SFDDC_Object obj = new SFDDC_Object(x_SFD, f_SFD);
            obj.Id = simple_frequency_data_mode.Count;

            //Increment Counters
            total_F_SFD += obj.F;
            total_X_SFD += obj.X;

            simple_frequency_data_mode.Add(obj);
            x_SFD = 0;
            f_SFD = 0;
            is_Mode_B_SFD = true;
            isCalculated_SFD = false;

            result_object.F = 0;
            result_object.X = 0;

        }

        /// <param name="Delete_C_SFDO">This function will delete last custom variable from List simple_frequency_data_mode </param>
        public void Delete_C_SFDO()
        {
            if (simple_frequency_data_mode.Count != 0)
            {
                int lenght = simple_frequency_data_mode.Count;

                //Decrement Couners
                total_F_SFD -= simple_frequency_data_mode[lenght - 1].F;
                total_X_SFD -= simple_frequency_data_mode[lenght - 1].X;

                simple_frequency_data_mode.RemoveAt(lenght - 1);

                //Reset Id numbers
                for (int i = 0; i < simple_frequency_data_mode.Count; i++)
                {
                    simple_frequency_data_mode[i].Id = i;
                }
            }
            if (simple_frequency_data_mode.Count == 0)
            {
                isCustom_SFD = false;
            }
            is_Mode_B_SFD = true;
            isCalculated_SFD = false;
            result_object.F = 0;
            result_object.X = 0;
        }


        /// <param name="Delete_SFDO_ByID">This function will delete last simple_frequency_data_mode_object from list simple_frequency_data_mode by its Id number</param>
        public void Delete_SFDO_ByID(int id)
        {
            if (simple_frequency_data_mode.Count != 0)
            {

                //Decrement Couners
                total_F_SFD -= simple_frequency_data_mode[id].F;
                total_X_SFD -= simple_frequency_data_mode[id].X;

                simple_frequency_data_mode.RemoveAt(id);

                //Reset Id numbers
                for (int i = 0; i < simple_frequency_data_mode.Count; i++)
                {
                    simple_frequency_data_mode[i].Id = i;
                }

                if (simple_frequency_data_mode.Count == 0)
                {
                    isRandom_SFD = false;
                    isCustom_SFD = false;

                    btn_s_f_text = "See example";
                }
                isCalculated_SFD = false;
                result_object.F = 0;
                result_object.X = 0;
            }
        }

        /// <param name="Reset_SFD">This function will reset Simple Frequency Distribution Program </param>
        public void Reset_SFD()
        {
            btn_s_f_text = "See example";
            isRandom_SFD = false;
            isCustom_SFD = false;
            is_Mode_B_SFD = true;
            isCalculated_SFD = false;
            total_F_SFD = 0;
            total_X_SFD = 0;
            result_object.F = 0;
            result_object.X = 0;
            simple_frequency_data_mode.Clear();
        }

        /// <param name="Generate_Simple_Freq_D">This function will generate mock data for  </param>
        public void Generate_Simple_Freq_D()
        {
            simple_frequency_data_mode.Clear();
            for (int i = 1; i <= 10; i++)
            {
                SFDDC_Object obj = new SFDDC_Object(i, RandomNumber(1 * 2, 1 * 100));
                obj.Id = simple_frequency_data_mode.Count;

                //Increment Counters
                total_F_SFD += obj.F;
                total_X_SFD += obj.X;

                simple_frequency_data_mode.Add(obj);
            }

            isRandom_SFD = true;
            is_Mode_B_SFD = true;
            isCalculated_SFD = false;
            btn_s_f_text = "Reset Data";
            result_object.F = 0;
            result_object.X = 0;

        }

        /// <param name="Calc_SFD_Mode">This function will Calcuate Mode for Simple Frequency Distribution  </param>
        public async void Calc_SFD_Mode()
        {
            if (simple_frequency_data_mode.Count != 0)
            {
                int maxOccurence = 0;
                for (int i = 0; i < simple_frequency_data_mode.Count; i++)
                {

                    if (simple_frequency_data_mode[i].F > maxOccurence)
                    {
                        maxOccurence = simple_frequency_data_mode[i].F;

                        result_object = simple_frequency_data_mode[i];
                    }
                }

                //If there is more than one variable occuring same ammount of times
                int counter = 0;
                for (int i = 0; i < simple_frequency_data_mode.Count; i++)
                {

                    if (maxOccurence == simple_frequency_data_mode[i].F)
                    {
                        counter++;
                    }

                    if (counter > 1)
                    {
                        is_Mode_B_SFD = false;
                        is_Mode_SFD = "There is more than one number occuting same ammount of times. Check table for more details.";
                    }
                    else
                    {
                        is_Mode_B_SFD = true;
                    }
                }

                isCalculated_SFD = true;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////
        ///

        //Mode For Grouped Distribution Program
        /// <param name="GFDDC_Object">Grouped Frequency Distribution Data Class declaration</param>
        public class GFDDC_Object
        {
            public int Id { get; set; }
            public double Range1 { get; set; }
            public double Range2 { get; set; }
            public double F { get; set; }

            public GFDDC_Object(double r1, double r2, double f)
            {
                Range1 = r1;
                Range2 = r2;
                F = f;
            }
        }

        //Declaring Mode variables for a Grouped Frequency Distribution Razor Page

        //Text for example
        protected string GFD_Text_Example = "This will show mode for ages distribution for people in some area.";

        //Declaring List for GFDDC_Object's
        protected List<GFDDC_Object> grouped_frequency_data_mode = new List<GFDDC_Object>();
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
        //Lower bound of the modal class
        protected double LM;
        //Modal Class Width
        protected double C;
        //D1 - Diffrence between the largest frequency and the frequency immediately precceding it
        protected double D1;
        //D2 - Diffrence between the largest frequency and the frequency immediately fallowing it
        protected double D2;
        //////////////////////////////////////////////////
        //Variable for Mode Result
        protected double Mode_GFD_Result;

        /// <param name="Add_C_GFDO">This function will add custom variable to List grouped_frequency_data_mode </param>
        public void Add_C_GFDO()
        {
            if (grouped_frequency_data_mode.Count == 0)
            {
                GFDDC_Object obj = new GFDDC_Object(range1, range2, f_GFD);
                obj.Id = grouped_frequency_data_mode.Count;
                grouped_frequency_data_mode.Add(obj);
                range1 = 0;
                range2 = 0;
                f_GFD = 0;
                isCustom_GFD = true;
            }
            else
            {
                int lenght = grouped_frequency_data_mode.Count;
                GFDDC_Object temp = grouped_frequency_data_mode[lenght - 1];
                double r1 = temp.Range2;
                double r2 = r1 + (temp.Range2 - temp.Range1);

                GFDDC_Object obj = new GFDDC_Object(r1, r2, f_GFD);
                obj.Id = grouped_frequency_data_mode.Count;
                grouped_frequency_data_mode.Add(obj);
                range1 = 0;
                range2 = 0;
                f_GFD = 0;
            }
        }

        /// <param name="Reset_GFD">This function will reset Grouped Frequency Distribution for Mode program</param>
        public void Reset_GFD()
        {
            grouped_frequency_data_mode.Clear();
            isCustom_GFD = false;
            isRandom_GFD = false;
            range1 = 0;
            range2 = 0;
            f_GFD = 0;
            isGDF_Calculated = false;
            btn_g_f_text = "See example";
        }

        /// <param name="Delete_C_GFDO">This function will delete last object in List grouped_frequency_data_mode</param>
        public void Delete_C_GFDO()
        {
            if (grouped_frequency_data_mode.Count != 0)
            {
                int lenght = grouped_frequency_data_mode.Count;
                grouped_frequency_data_mode.RemoveAt(lenght - 1);

                //Reset Id numbers
                for (int i = 0; i < grouped_frequency_data_mode.Count; i++)
                {
                    grouped_frequency_data_mode[i].Id = i;
                }
            }
            if (grouped_frequency_data_mode.Count == 0)
            {
                isCustom_GFD = false;
            }

            range1 = 0;
            range2 = 0;
            f_GFD = 0;
            isGDF_Calculated = false;
        }

        /// <param name="Delete_SFDO_ByID">This function will delete last grouped_frequency_data_mode_object from list grouped_frequency_data_mode by its Id number</param>
        public void Delete_GDFDO_ByID(int id)
        {
            if (grouped_frequency_data_mode.Count != 0)
            {
                grouped_frequency_data_mode.RemoveAt(id);

                //Reset Id numbers
                for (int i = 0; i < grouped_frequency_data_mode.Count; i++)
                {
                    grouped_frequency_data_mode[i].Id = i;
                }

                if (grouped_frequency_data_mode.Count == 0)
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

        /// <param name="Generate_Grouped_Freq_D">This function will generate mock data for grouped_frequency_data_mode List</param>
        public void Generate_Grouped_Freq_D()
        {
            grouped_frequency_data_mode.Clear();
            isRandom_GFD = true;
            int r1 = 5;
            int r2 = 10;

            for (int i = 0; i <= 10; i++)
            {
                if (grouped_frequency_data_mode.Count == 0)
                {
                    double f = RandomNumber(100, 1000);
                    GFDDC_Object obj = new GFDDC_Object(r1, r2, f);
                    obj.Id = grouped_frequency_data_mode.Count;
                    grouped_frequency_data_mode.Add(obj);
                    r1 += 5;
                    r2 += 5;
                }
                else
                {
                    int lenght = grouped_frequency_data_mode.Count;
                    GFDDC_Object temp = grouped_frequency_data_mode[lenght - 1];
                    double f = RandomNumber(100, 1000);
                    GFDDC_Object obj = new GFDDC_Object(r1, r2, f);
                    obj.Id = grouped_frequency_data_mode.Count;
                    r1 += 5;
                    r2 += 5;
                    grouped_frequency_data_mode.Add(obj);
                }
            }
            btn_g_f_text = "Reset Data";
        }

        /// <param name="Calculate_GFDM">This fuction will Calcualte_Grouped Frequency Distribution Mode </param>
        public void Calculate_GFDM()
        {
            if (grouped_frequency_data_mode.Count != 0)
            {
                //Finding largest frequency (f) and then finding positions for position_preceding_largest and position_fallowing_largest
                double largest_frequency = 0;
                int position_of_largest = 0;
                int position_preceding_largest = 0;
                int position_fallowing_largest = 0;
                for (int i = 0; i < grouped_frequency_data_mode.Count; i++)
                {
                    if (largest_frequency < grouped_frequency_data_mode[i].F)
                    {
                        largest_frequency = grouped_frequency_data_mode[i].F;

                        if (i == 0)
                        {
                            position_preceding_largest = i;
                            position_fallowing_largest = i + 1;
                            position_of_largest = i;
                        }
                        else if (i == grouped_frequency_data_mode.Count)
                        {
                            position_preceding_largest = i - 1;
                            position_fallowing_largest = i;
                            position_of_largest = i;
                        }
                        else
                        {
                            position_preceding_largest = i - 1;
                            position_fallowing_largest = i + 1;
                            position_of_largest = i;
                        }
                    }
                }

                D1 = largest_frequency - grouped_frequency_data_mode[position_preceding_largest].F;
                D2 = largest_frequency - grouped_frequency_data_mode[position_fallowing_largest].F;
                C = grouped_frequency_data_mode.Count - 1;
                LM = grouped_frequency_data_mode[position_of_largest].Range1;
                Mode_GFD_Result = LM + ((D1 / (D1 + D2)) * C);
                isGDF_Calculated = true;
            }
        }
    }
}
